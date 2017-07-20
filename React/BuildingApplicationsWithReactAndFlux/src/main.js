$ = JQuery = require("jquery");
var React = require("react");
var routes = require("./routes");
var Router = require("react-router");



Router.run(routes, function(Handler){
    React.render(<Handler />, document.getElementById("app"));
});