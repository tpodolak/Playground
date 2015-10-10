require.config({
    paths: {
        knockout: '../scripts/knockout/dist/knockout.debug',
        jquery: '../scripts/jquery/dist/jquery'
    }
});

define([
    'knockout',
    'viewmodels/todoviewmodel',
    'extensions/handlers'
], function (ko, TodoViewModel) {

    ko.applyBindings(new TodoViewModel());
});


