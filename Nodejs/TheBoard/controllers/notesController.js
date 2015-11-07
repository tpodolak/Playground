(function (notesController) {
    
    var data = require("../data");
    var auth = require('../auth');
    
    notesController.init = function (app) {
        app.get("/api/notes/:categoryName", function (req, res) {
            data.getNotes(req.params.categoryName, function (err, data) {
                if (err) {
                    res.send(400, err);
                } else {
                    res.set('Content-Type', 'application/json');
                    res.send(data.notes);
                }
            });

        });
        
        app.post('/api/notes/:categoryName', auth.ensureApiAuthenticated, function (req, res) {
            var categoryName = req.params.categoryName,
                note = {
                    note: req.body.note,
                    color: req.body.color,
                    author: "me"
                };
            data.addNote(categoryName, note, function (err) {
                if (err) {
                    res.send(400, "Failed to add note to data store");
                } else {
                    res.set('Content-Type', 'application/json');
                    res.send(201, note);
                }
            });
        });
    }
     
})(module.exports);