(function (data) {
    
    var seedData = require('./seedData');
    var database = require('./database');
    
    data.getNoteCategories = function (next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err, null);
            } else {
                db.notes.find().sort({ name: 1 }).toArray(function (err, results) {
                    if (err) {
                        next(err, results);
                    } else {
                        next(null, results);
                    }
                });
            }
        });
    }
    
    data.createNewCategory = function (categoryName, next) {
        database.getDb(function (err, db) {
            if (err) {
                next(err);
            } else {
                
                db.notes.find({ name: categoryName }).count(function (error, count) {
                    
                    if (error) {
                        next( error, null );
                    }
                    if ( count !== 0 ) {
                        next( "Category already exists" );
                    } else {
                        var cat = {
                            name: categoryName,
                            notes: []
                        }
                        db.notes.insert(cat, function (err) {
                            if (err) {
                                next(err, null);
                            } else {
                                next(null);
                            }
                        });
                    }
                });
            }
        });
    }
    
    function seedDatabase() {
        database.getDb(function (error, db) {
            if (error) {
                console.log("Failed to seed database: " + error);
            } else {
                
                db.notes.count(function (error, count) {
                    if (error) {
                        console.log("Failed to retrieve database count");
                    } else {
                        if (count === 0) {
                            console.log("Seeding the Database...");
                            seedData.initialNotes.forEach(function (item) {
                                db.notes.insert(item, function (error) {
                                    if (error) {
                                        log.error("Failed to insert note into database");
                                    }
                                });
                            });
                        } else {
                            console.log("Database already seeded");
                        }
                    }
                });
            }
        });
    }
    
    seedDatabase();
})(module.exports);