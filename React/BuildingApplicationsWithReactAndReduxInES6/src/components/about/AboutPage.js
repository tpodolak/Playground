import React from 'react';

class AboutPage extends React.Component {
    constructor(props){
        super(props);
    }

    render(){
        return (
            <div>
                <h1>About</h1>
                <p>
                    This application uses React, Redux and React Router and a variety of handful other libraries
                </p>
            </div>
        );
    }
}

export default AboutPage;