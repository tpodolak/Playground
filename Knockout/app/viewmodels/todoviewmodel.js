define([
    'knockout',
    'models/todomodel'
], function (ko, TodoModel) {


    function TodoViewModel() {
        var me = this;
        me.todos = ko.observableArray([]);
        me.current = ko.observable();

        me.add = function () {
            var current = me.current();
            if (current) {
                me.todos.push(new TodoModel(current, false));
                me.current("");
            }
        };

        me.remove = function (item) {
            me.todos.remove(item);
        };


        me.remainingCount = ko.computed(function () {
            return me.todos().length - ko.utils.arrayFilter(me.todos(), function (item) {
                    return item.completed();
                }).length;
        });

        me.humanizeItems = function (count) {
            return ko.utils.unwrapObservable(count) === 1 ? 'item' : 'items';
        };

        me.startEditing = function (item) {
            item.editing(true);
        };

        me.finishEditing = function (item) {
            item.editing(false);
            var title = item.title().trim();
            if (!title) {
                me.todos.remove(item);
            }
        };

        me.toggleAll = function () {
            var todos = me.todos(),
                selected = ko.utils.arrayFilter(todos, function (item) {
                return item.completed();
            }).length === todos.length;

            ko.utils.arrayForEach(todos, function (item) {
                item.completed(!selected);
            });
        };
    }

    return TodoViewModel;

});