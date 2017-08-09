"use strict";
var React = require("react");
var Router = require('react-router');
var Link = Router.Link;

var AuthorList = React.createClass({
    render: function () {
        var createAuthorRow = function (author) {
            return (
                <tr key={author.id}>
                    <td><a href="#" onClick={this.props.onDelete.bind(this, author.id)}>Delete</a></td>
                    <td><Link to="manageAuthor" params={{id: author.id}}>{author.id}</Link></td>
                    <td>{author.firstName} {author.lastName}</td>
                </tr>
            );
        };

        return (
            <table className="table">
                <thead>
                <th>Id</th>
                <th>Name</th>
                </thead>
                <tbody>
                {this.props.authors.map(createAuthorRow, this)}
                </tbody>
            </table>
        );
    }
});


module.exports = AuthorList;