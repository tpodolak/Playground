var React = require("react");
var Router = require('react-router');
var AuthorForm = require("./authorForm");
var AuthorApi = require("../../api/authorApi");

var ManageAuthorPage = React.createClass({
    mixins:[
        Router.Navigation
    ],

    getInitialState: function () {
        return {
            author: {
                id: '',
                firstName: '',
                lastName: ''
            }
        }
    },

    setAuthorState: function (event) {
        this.state.author[event.target.name] = event.target.value;
        this.setState({
            author: this.state.author
        });
    },

    saveAuthor: function (event) {
        event.preventDefault();
        AuthorApi.saveAuthor(this.state.author);
        this.transitionTo("authors");
    },

    render: function () {
        return (
            <AuthorForm author={this.state.author} onChange={this.setAuthorState} onSave={this.saveAuthor}/>
        );
    }
});

module.exports = ManageAuthorPage;