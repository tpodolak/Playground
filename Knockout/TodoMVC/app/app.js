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


    function Actor(name) {
        this.name = name;
    }

    Actor.prototype.speak = function () {
        return "Actor speak";
    };

    Actor.prototype.walk = function () {
        return "baseWalkd";
    }

    Actor.prototype.someVariable = "xxx";

    function SilentActor(name) {
        Actor.apply(this, arguments);
    }

    SilentActor.prototype = new Actor();
    SilentActor.prototype.speak = function(){
        return "Silent actor speak";
    };

    SilentActor.prototype.someVariable = "zzz";
    var actor = new Actor("actor");
    console.log(actor.name);
    console.log(actor.speak());
    console.log(actor.walk());
    console.log(actor.someVariable);
    var silentActor = new SilentActor("silentActor")
    silentActor.someVariable="asigned";
    console.log(silentActor.name);
    console.log(silentActor.speak());
    console.log(silentActor.walk())
    console.log(silentActor.someVariable)

    var secondActor = new Actor("actor2");
    console.log(secondActor.someVariable)

    var silentActor2 = new SilentActor("silentActor2");
    console.log(silentActor2.someVariable)

    ko.applyBindings(new TodoViewModel());
});


