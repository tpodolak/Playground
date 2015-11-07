(function (updater) {
    var socketio = require("socket.io");
    updater.init = function (server) {
        var io = socketio.listen(server);
        io.sockets.on("connection", function (socket) {
            console.log("socket was conneted");
            socket.on("newNote", function (data) {
                socket.broadcast.to(data.category).emit("broadcast note", data.note);
            });

            socket.on( "join category", function( category ) {
                socket.join( category );
            } );
        });
    }

}(module.exports))