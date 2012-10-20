var boardViewModel = {}

$(document).ready(function () {
    $.getJSON("api/board", function (board) {
        createBoardViewModel(board);
        initializeBoard();
    });
});


function createBoardViewModel(board) {
    var laneViewModels = board.swimLanes.map(createLaneViewModel);

    boardViewModel = {
        name: ko.observable(board.name),
        lanes: ko.observableArray(laneViewModels)
    };
}

function createLaneViewModel(lane) {
    var itemViewModels = lane.items.map(createItemViewModel);

    return {
        name: ko.observable(lane.name),
        items: ko.observableArray(itemViewModels)
    };
}

function createItemViewModel(item) {
    return {
        description: ko.observable(item.description)
    };
}

function initializeBoard() {
    ko.applyBindings(boardViewModel);
}
