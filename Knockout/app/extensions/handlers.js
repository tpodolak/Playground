define([
    'knockout'
], function (ko) {

    ko.bindingHandlers.executeOnEnter = ko.bindingHandlers.executeOnEnter || {
            init: function (element, valueAccessor, allBindingsAccessor, data, bindingContext) {

                var wrappedHandler;
                var newValueAccessor;

                wrappedHandler = function (data, event) {
                    if (event.keyCode === 13) {
                        valueAccessor().call(this, data, event);
                    }
                };

                newValueAccessor = function () {
                    return {
                        keyup: wrappedHandler
                    };
                };

                // call the real event binding's init function
                ko.bindingHandlers.event.init(element, newValueAccessor, allBindingsAccessor, data, bindingContext);
            }
        };
});