"use strict";
var React = require("react");
var Home = React.createClass({
   render: function() {
       return (
           <div className="jumbotron">
                <h1>Pluralsight</h1>
               <p>React, React router, flux</p>
           </div>
       );
   }
});

module.exports = Home;