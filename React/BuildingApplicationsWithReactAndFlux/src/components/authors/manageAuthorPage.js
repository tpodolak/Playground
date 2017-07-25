var React = require("react");
var Router = require('react-router');
var Toastr = require('toastr');
var AuthorForm = require("./authorForm");
var AuthorApi = require("../../api/authorApi");

var ManageAuthorPage = React.createClass({
    mixins: [
        Router.Navigation
    ],

    getInitialState: function () {
        return {
            author: {id: '', firstName: '', lastName: ''},
            errors: {}
        }
    },

    authorFormIsValid: function() {
        var formIsValid = true;
        this.state.errors = {}; //clear any previous errors.

        if (this.state.author.firstName.length < 3) {
            this.state.errors.firstName = 'First name must be at least 3 characters.';
            formIsValid = false;
        }

        if (this.state.author.lastName.length < 3) {
            this.state.errors.lastName = 'Last name must be at least 3 characters.';
            formIsValid = false;
        }

        this.setState({errors: this.state.errors});
        return formIsValid;
    },

    setAuthorState: function (event) {
        this.state.author[event.target.name] = event.target.value;
        this.setState({
            author: this.state.author
        });
    },

    saveAuthor: function (event) {
        event.preventDefault();
        if(!this.authorFormIsValid()){
            return;
        }

        AuthorApi.saveAuthor(this.state.author);
        Toastr.success('Author saved.');
        this.transitionTo("authors");
    },

    render: function () {
        return (
            <AuthorForm author={this.state.author}
                        onChange={this.setAuthorState}
                        onSave={this.saveAuthor}
                        errors={this.state.errors} />
        );
    }
});

module.exports = ManageAuthorPage;