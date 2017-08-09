"use strict";
var React = require("react");
var AuthorStore = require("../../stores/authorStore");
var AuthorList = require("./authorList");
var Router = require("react-router");
var Link = Router.Link;
var Toastr = require('toastr');
var AuthorActions = require("../../actions/authorActions")

var Authors = React.createClass({
    getInitialState: function () {
        return {
            authors: AuthorStore.getAllAuthors()
        };
    },

    componentWillMount: function(){
        AuthorStore.addChangeListener(this._onChange);
    },

    componentWillUnmount: function () {
        AuthorStore.removeChangeListener(this._onChange);
    },

    _onChange: function () {
        this.setState({ authors: AuthorStore.getAllAuthors() })
    },

    deleteAuthor: function (id, event) {
        event.preventDefault();
        AuthorActions.deleteAuthor(id);
        Toastr.success('Author deleted.');
    },

    render: function () {
        return (
            <div>
                <h1>Authors</h1>
                <Link to="addAuthor" className="btn btn-default">Add Author</Link>
                <AuthorList authors={this.state.authors} onDelete={this.deleteAuthor}/>
            </div>
        );
    }
});


module.exports = Authors;