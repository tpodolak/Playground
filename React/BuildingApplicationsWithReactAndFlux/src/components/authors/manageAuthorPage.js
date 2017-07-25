var React = require("react");
var AuthorForm = require("./authorForm");

var ManageAuthorPage = React.createClass({

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

    render: function () {
        return (
            <AuthorForm author={this.state.author} onChange={this.setAuthorState}/>
        );
    }
});

module.exports = ManageAuthorPage;