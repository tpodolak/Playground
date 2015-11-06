(function (database) {

    var mongodb = require("mongodb"),
        mongoUrl = "mongodb://localhost:27017/theBoard",
        theDb = null;
    database.getDb = function (next) {
        if (!theDb) {
            mongodb.MongoClient.connect(mongoUrl, function (error, db) {
                if (error) {
                    next(error, null);
                } else {
                    theDb = {
                        db: db,
                        notes:db.collection('notes')
                    };
                    next(null, theDb);
                }
            })
        } else {
            next(null, theDb);
        }
    }

})(module.exports);