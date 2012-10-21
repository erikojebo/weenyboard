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
    return ko.mapping.fromJS(item);
}

function initializeBoard() {
    ko.applyBindings(boardViewModel);
}
