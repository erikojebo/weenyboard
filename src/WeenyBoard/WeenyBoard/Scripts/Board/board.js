var boardViewModel = {}

$(document).ready(function () {
    $.getJSON("api/board", function (board) {
        createBoardViewModel(board);
        initializeBoard();
    });
});


function createBoardViewModel(board) {
    var laneViewModels = board.SwimLanes.map(createLaneViewModel);

    boardViewModel = {
        name: ko.observable(board.Name),
        lanes: ko.observableArray(laneViewModels)
    };
}

function createLaneViewModel(lane) {
    var itemViewModels = lane.Items.map(createItemViewModel);

    return {
        name: ko.observable(lane.Name),
        items: ko.observableArray(itemViewModels)
    };
}

function createItemViewModel(item) {
    return {
        description: ko.observable(item.Description)
    };
}

function initializeBoard() {
    ko.applyBindings(boardViewModel);
}
