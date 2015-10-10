define([
    'knockout'
], function (ko) {


    function TodoModel(title, completed) {
        var me = this;
        me.title = ko.observable(title);
        me.completed = ko.observable(completed);
        me.editing = ko.observable(false);
    }

    return TodoModel;
});