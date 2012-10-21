Array.prototype.mapToObservable = function (mappingFunction) {
    var items = this.map(mappingFunction);
    return ko.observableArray(items);
}

//wrapper to an observable that requires accept/cancel
ko.protectedObservable = function(initialValue) {
    //private variables
    var _actualValue = ko.observable(initialValue),
        _tempValue = initialValue;

    //computed observable that we will return
    var result = ko.computed({
        //always return the actual value
        read: function() {
           return _actualValue();
        },
        //stored in a temporary spot until commit
        write: function(newValue) {
             _tempValue = newValue;
        }
    });

    //if different, commit temp value
    result.commit = function() {
        if (_tempValue !== _actualValue()) {
             _actualValue(_tempValue);
        }
    };

    //force subscribers to take original
    result.reset = function() {
        _actualValue.valueHasMutated();
        _tempValue = _actualValue();   //reset temp value
    };

    result.uncommitted = function () {
        return _tempValue;
    }

    return result;
};

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

ko.bindingHandlers.focusWhen = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var observableValue = valueAccessor();
        var value = ko.utils.unwrapObservable(observableValue);

        if (value){
            element.focus();
        }
    }
};

ko.bindingHandlers.selectWhen = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var observableValue = valueAccessor();
        var value = ko.utils.unwrapObservable(observableValue);

        if (value){
            element.select();
        }
    }
};
