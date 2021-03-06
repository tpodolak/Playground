﻿var http = require('http'),
    express = require('express'),
    bodyParser = require('body-parser'),
    flash = require("connect-flash"),
    expressSession = require('express-session'),
    cookieParser = require('cookie-parser'),
    app = express();

//set view engine
app.set('view engine', 'vash');
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(cookieParser());
app.use(expressSession({
    resave: true,
    saveUninitialized: true,
    secret: "TheBoard"
}));
app.use(flash());
// static resources
app.use(express.static(__dirname + "/public"));

var auth = require("./auth");
auth.init(app);

var controller = require("./controllers");
// map the routes
controller.init(app);


var server = http.createServer(app);
server.listen(3000);
console.log( "Server listening on port 3000" );
var updater = require("./updater");
updater.init( server );