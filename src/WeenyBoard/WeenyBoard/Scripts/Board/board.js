var boardViewModel = {}

$(document).ready(function () {
    $.getJSON("api/board", function (board) {
        createBoardViewModel(board);
        initializeBoard();
    });
});


function createBoardViewModel(board) {
    boardViewModel = {
        id: board.id,
        name: ko.observable(board.name),
        swimLanes: board.swimLanes.mapToObservable(createLaneViewModel),

        findSelectedItemPosition: function () {
	        for (var i = 0; i < this.swimLanes().length; i++) {
                for (var j = 0; j < this.swimLanes()[i].items().length; j++) {
                    if (this.swimLanes()[i].items()[j].isSelected())
                        return { swimLaneIndex: i, itemIndex: j };
                }
            }
            return null;
        },

        hasSelectedItem: function () {
            var position = this.findSelectedItemPosition();
            return position !== null;
        },

        getItemByPosition: function (position) {
	        return this.swimLanes()[position.swimLaneIndex].items()[position.itemIndex];
        },

        findSelectedItem: function () {
            var position = this.findSelectedItemPosition();
            return this.getItemByPosition(position);
        },

        findSelectedItemId: function () {
            return this.findSelectedItem().id;
        },

        isEditing: function () {
	        for (var i = 0; i < this.swimLanes().length; i++) {
                for (var j = 0; j < this.swimLanes()[i].items().length; j++) {
                    if (this.swimLanes()[i].items()[j].isEditing())
                        return true;
                }
            }
            return false;
        },

        editSelected: function () {
	        var item = this.findSelectedItem();
            item.isEditing(true);
        },

        createPosition: function (swimLaneIndex, itemIndex) {
	         return { swimLaneIndex: swimLaneIndex, itemIndex: itemIndex };
        },

        getAllPositions: function () {
            var positions = [];

	        for (var i = 0; i < this.swimLanes().length; i++) {
                for (var j = 0; j < this.swimLanes()[i].items().length; j++) {
                    positions.push(this.createPosition(i,j));
                }
            }

            return positions;
        },

        getAllPositionsAfter: function (position) {
	        var allPositions = this.getAllPositions();
            var positionsAfter = [];

            allPositions.foreach(function (p) {
	            if (p.swimLaneIndex == position.swimLaneIndex && p.itemIndex > position.itemIndex || 
                    p.swimLaneIndex > position.swimLaneIndex)
                    positionsAfter.push(p);
            });


            return positionsAfter;
        },

        getAllPositionsBefore: function (position) {
	        var allPositions = this.getAllPositions();
            var positionsBefore = [];

            allPositions.foreach(function (p) {
	            if (p.swimLaneIndex == position.swimLaneIndex && p.itemIndex < position.itemIndex || 
	                p.swimLaneIndex < position.swimLaneIndex)
                    positionsBefore.push(p);
            });

            return positionsBefore.reverse();
        },

        getNextItemPosition: function (position) {
            var positionsAfter = this.getAllPositionsAfter(position);
            
            if (positionsAfter.length == 0) {
                return position;
            }

            return positionsAfter[0];
        },
        
        getPreviousItemPosition: function (position) {
            var positionsBefore = this.getAllPositionsBefore(position);
            
            if (positionsBefore.length == 0) {
                return position;
            }

            return positionsBefore[0];
        },

        selectNextItem: function () {
            var position = this.findSelectedItemPosition();
            var nextPosition = this.getNextItemPosition(position);
            this.selectItem(nextPosition);
        },

        selectPreviousItem: function () {
            var position = this.findSelectedItemPosition();
            var previousPosition = this.getPreviousItemPosition(position);
            this.selectItem(previousPosition);
        },

        selectItem: function (position) {
            this.deselectAllItems();
	        var item = this.getItemByPosition(position);
            item.select();
        },
        
        deselectAllItems: function () {
	        this.swimLanes().foreach(function (lane) {
	            lane.items().foreach(function (item) {
	                item.deselect();
                })
            });
        },

        moveSelectedItemToSwimLaneIndex: function (oldItemPosition, newSwimLaneIndex) {
            var selectedItem = this.findSelectedItem();
            var newSwimLaneId = this.swimLanes()[newSwimLaneIndex].id;

	        $.post(
                'api/board/changeswimlane', 
                { id: selectedItem.id, newSwimLaneId: newSwimLaneId },
                function(data) {
                    boardViewModel.swimLanes()[oldItemPosition.swimLaneIndex].items
                        .splice(oldItemPosition.itemIndex, 1);

                    boardViewModel.swimLanes()[newSwimLaneIndex].items
                        .push(selectedItem)

                    refreshSelectionMarkers();
                });

        },

        moveSelectedItemRight: function () {
            var selectedItemPosition = this.findSelectedItemPosition();
            var newSwimLaneIndex = selectedItemPosition.swimLaneIndex + 1;

            if (selectedItemPosition.swimLaneIndex == this.swimLanes().length) {
                return;
            }

            this.moveSelectedItemToSwimLaneIndex(selectedItemPosition, newSwimLaneIndex);
        },

        moveSelectedItemLeft: function () {
            var selectedItemPosition = this.findSelectedItemPosition();
            var newSwimLaneIndex = selectedItemPosition.swimLaneIndex - 1;

            if (selectedItemPosition.swimLaneIndex < 0) {
                return;
            }
            
            this.moveSelectedItemToSwimLaneIndex(selectedItemPosition, newSwimLaneIndex);
        }
    }
    
    boardViewModel.selectItem({ swimLaneIndex: 0, itemIndex: 0});
}

function createLaneViewModel(lane) {
    return {
        id: lane.id,
        name: ko.observable(lane.name),
        items: lane.items.mapToObservable(createItemViewModel)
    };
}

function createItemViewModel(item) {
    return {
        description: ko.protectedObservable(item.description),
        id: item.id,
        isEditing: ko.observable(false),
        isHovered: ko.observable(false),
        isSelected: ko.observable(false),
        select: function () {
            boardViewModel.deselectAllItems();
            this.isSelected(true);
            refreshSelectionMarkers();
        },
        deselect: function () {
            this.isSelected(false);
            refreshSelectionMarkers();
        },
        beginEdit: function () {
            this.isEditing(true);
        },
        confirmEdit: function () {

            // Store this in a variable so that it can be accessed and bound in the
            // success callback of the POST. 'this' is not the viewmodel instance
            // when inside the callback.
            var viewModel = this;

            $.post(
                'api/board/updateitemdescription', 
                { id: this.id, newDescription: this.description.uncommitted() },
                function(data) {
                    viewModel.description.commit();
                });

            this.isEditing(false);
        },
        cancelEdit: function () {
            this.description.reset();
            this.isEditing(false);
        },
        onMouseOver: function () {
            this.isHovered(true);
        },
        onMouseOut: function () {
            this.isHovered(false);
        },
        confirmCreate: function() {
            $.post(
                'api/board/post',
                {id: this.id, Description: this.description.uncommitted() },
                function(data) {
                    viewModel.description.commit();
                });
        }
    }
}

function initializeBoard() {
    ko.applyBindings(boardViewModel);

    refreshSelectionMarkers();

    $(document).keydown(function (e) {
        var keycode = e.which;
        var j = 74;
        var k = 75;
        var f2 = 113;
        var leftArrow = 37;
        var rightArrow = 39;

        if (boardViewModel.isEditing()) {
            return true;
        }

        if (keycode == j) {
            boardViewModel.selectNextItem();
            return false;
        }
        else if (keycode == k) {
            boardViewModel.selectPreviousItem();
            return false;
        }
        else if (keycode == f2 && boardViewModel.hasSelectedItem()) {
            boardViewModel.editSelected();
            return false;
        }
        else if (e.shiftKey && keycode == leftArrow) {
            boardViewModel.moveSelectedItemLeft();
            return false;
        }
        else if (e.shiftKey && keycode == rightArrow) {
            boardViewModel.moveSelectedItemRight();
            return false;
        }

        return true;
    });
}

function refreshSelectionMarkers() {
    $(".boardItem").removeClass("selected");
    
    if (!boardViewModel.hasSelectedItem()) {
        return;
    }

    var selectedItemId = boardViewModel.findSelectedItemId();

    $("li[data-id='" + selectedItemId +"']").addClass("selected");
}
