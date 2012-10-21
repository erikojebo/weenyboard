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
    var viewModel = ko.mapping.fromJS(item);

    viewModel.isEditing = ko.observable(false);
    viewModel.beginEdit = function () {
        this.isEditing(true);
    };
    viewModel.endEdit = function () {
        this.isEditing(false);
    };
    return viewModel;
}

function initializeBoard() {
    ko.applyBindings(boardViewModel);
}
