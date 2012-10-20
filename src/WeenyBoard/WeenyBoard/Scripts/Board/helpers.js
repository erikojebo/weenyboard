Array.prototype.mapToObservable = function (mappingFunction) {
    var items = this.map(mappingFunction);
    return ko.observableArray(items);
}

ko.bindingHandlers.executeOnEnter = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        $(element).keypress(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 13) {
                allBindings.executeOnEnter.call(viewModel);
                return false;
            }
            return true;
        });
    }
};

ko.bindingHandlers.executeOnEscape = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        $(element).keydown(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 27) {
                allBindings.executeOnEscape.call(viewModel);
                return false;
            }
            return true;
        });
    }
};
