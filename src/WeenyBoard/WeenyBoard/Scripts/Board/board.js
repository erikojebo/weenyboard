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
}
