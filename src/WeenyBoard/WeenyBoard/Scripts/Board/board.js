var boardViewModel = {}

$(document).ready(function () {
    $.getJSON("api/board", function (board) {
        createBoardViewModel(board);
        initializeBoard();
    });
});


function createBoardViewModel(board) {
    boardViewModel = ko.mapping.fromJS(board);
    boardViewModel.swimLanes = board.swimLanes.mapToObservable(createLaneViewModel);

    boardViewModel.findSelectedItemPosition = function () {
	    for (var i = 0; i < this.swimLanes().length; i++) {
            for (var j = 0; j < this.swimLanes()[i].items().length; j++) {
                if (this.swimLanes()[i].items()[j].isSelected())
                    return { swimLaneIndex: i, itemIndex: j };
            }
        }
        return null;
    }

    boardViewModel.hasSelectedItem = function () {
        var position = this.findSelectedItemPosition();
        return position !== null;
    }

    boardViewModel.getItemByPosition = function (position) {
	    return this.swimLanes()[position.swimLaneIndex].items()[position.itemIndex];
    }

    boardViewModel.findSelectedItem = function () {
        var position = this.findSelectedItemPosition();
        return this.getItemByPosition(position);
    };

    boardViewModel.findSelectedItemId = function () {
        return this.findSelectedItem().id;
    };

    boardViewModel.isEditing = function () {
	    for (var i = 0; i < this.swimLanes().length; i++) {
            for (var j = 0; j < this.swimLanes()[i].items().length; j++) {
                if (this.swimLanes()[i].items()[j].isEditing())
                    return true;
            }
        }
        return false;
    };

    boardViewModel.editSelected = function () {
	    var item = this.findSelectedItem();
        item.isEditing(true);
    }

    boardViewModel.getNextItemPosition = function (position) {
	    var nextItemSwimLaneIndex = position.swimLaneIndex;
        var nextItemIndex = position.itemIndex + 1;

        if (nextItemIndex >= boardViewModel.swimLanes()[position.swimLaneIndex].items().length) {
            nextItemIndex = 0;
            nextItemSwimLaneIndex++;
        }

        if (nextItemSwimLaneIndex >= boardViewModel.swimLanes().length) {
            return position;
        }

        return { swimLaneIndex: nextItemSwimLaneIndex, itemIndex: nextItemIndex };
    }

    boardViewModel.getPreviousItemPosition = function (position) {
	    var previousItemSwimLaneIndex = position.swimLaneIndex;
        var previousItemIndex = position.itemIndex - 1;

        if (previousItemIndex < 0) {
            previousItemSwimLaneIndex--;
            previousItemIndex = this.swimLanes()[previousItemSwimLaneIndex].items().length - 1;
        }

        if (previousItemSwimLaneIndex < 0) {
            return position;
        }

        return { swimLaneIndex: previousItemSwimLaneIndex, itemIndex: previousItemIndex };
    }


    boardViewModel.selectNextItem = function () {
        var position = this.findSelectedItemPosition();
        var nextPosition = this.getNextItemPosition(position);
        this.selectItem(nextPosition);
    };

    boardViewModel.selectPreviousItem = function () {
        var position = this.findSelectedItemPosition();
        var previousPosition = this.getPreviousItemPosition(position);
        this.selectItem(previousPosition);
    };

    boardViewModel.selectItem = function (position) {
        this.deselectAllItems();
	    var item = this.getItemByPosition(position);
        item.select();
    }
    
    boardViewModel.deselectAllItems = function () {
	    this.swimLanes().foreach(function (lane) {
	        lane.items().foreach(function (item) {
	            item.deselect();
            })
        });
    }
    
    boardViewModel.selectItem({ swimLaneIndex: 0, itemIndex: 0});
}

function createLaneViewModel(lane) {
    var viewModel = ko.mapping.fromJS(lane);
    viewModel.items = lane.items.mapToObservable(createItemViewModel);
    return viewModel;
}

function createItemViewModel(item) {
    var viewModel = {};

    viewModel.description = ko.protectedObservable(item.description);
    viewModel.id = item.id;
    viewModel.isEditing = ko.observable(false);
    viewModel.isHovered = ko.observable(false);
    viewModel.isSelected = ko.observable(false);
    viewModel.select = function () {
        this.isSelected(true);
        refreshSelectionMarkers();
    }
    viewModel.deselect = function () {
        this.isSelected(false);
        refreshSelectionMarkers();
    }
    viewModel.beginEdit = function () {
        this.isEditing(true);
    };
    viewModel.confirmEdit = function () {
        $.post(
            'api/board/updateitemdescription', 
            { id: this.id, newDescription: this.description.uncommitted() },
            function(data) {
                viewModel.description.commit();
        });

        this.isEditing(false);
    };
    viewModel.cancelEdit = function () {
        this.description.reset();
        this.isEditing(false);
    };
    viewModel.onMouseOver = function () {
        this.isHovered(true);
    }
    viewModel.onMouseOut = function () {
        this.isHovered(false);
    }
    return viewModel;
}

function initializeBoard() {
    ko.applyBindings(boardViewModel);

    refreshSelectionMarkers();

    $(document).keydown(function (e) {
        var keycode = e.which;
        var j = 74;
        var k = 75;
        var f2 = 113;

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
