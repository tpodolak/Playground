import React from 'react';
import {Link} from 'react-router';

class HomePage extends React.Component{
    constructor(props){
        super(props);
    }

    render(){
        return (
            <div className="jumbotron">
                <h1>Pluralsight</h1>
                <p>React, Redux and React Router in ES6 for ultra-responsive apps elo elo</p>
                <Link to="about" className="btn btn-primary btn-lg">Learn more</Link>
            </div>
        );
    }
}

export default HomePage;