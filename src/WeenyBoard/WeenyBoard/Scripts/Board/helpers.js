Array.prototype.mapToObservable = function (mappingFunction) {
    var items = this.map(mappingFunction);
    return ko.observableArray(items);
}
