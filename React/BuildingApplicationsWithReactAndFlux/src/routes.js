var React = require("react");
var Router = require("react-router");
var Route = Router.Route;
var DefaultRoute = Router.DefaultRoute;
var NoFoundRoute = Router.NotFoundRoute;
var Redirect = Router.Redirect;

var routes = (
    <Route name="app" path="/" handler={require('./components/app')}>
        <DefaultRoute handler={require('./components/homePage')} />
        <Route name="authors" handler={require('./components/authors/authorPage')} />
        <Route name="addAuthor" path="author" handler={require('./components/authors/manageAuthorPage')} />
        <Route name="about" handler={require('./components/about/aboutPage')} />
        <NoFoundRoute handler={require('./components/notFoundPage')} />
    </Route>
);

module.exports = routes;