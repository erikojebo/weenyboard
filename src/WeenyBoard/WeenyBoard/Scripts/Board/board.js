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

    boardViewModel.findSelectedItem = function () {
	    return this.swimLanes()[0].items()[0];
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

    boardViewModel.selectNextItem = function () {
	    alert("next");
    };
    boardViewModel.selectPreviousItem = function () {
	    alert("prev");
    };

    boardViewModel.swimLanes()[0].items()[0].select();
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

        return true;
    });
}

function refreshSelectionMarkers() {
    $(".boardItem").removeClass("selected");
    
    var selectedItemId = boardViewModel.findSelectedItemId();

    $("li[data-id='" + selectedItemId +"']").addClass("selected");
}
