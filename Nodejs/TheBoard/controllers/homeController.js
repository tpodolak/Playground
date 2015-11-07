(function (homeController) {
    
    var data = require('../data');
    homeController.init = function (app) {
        
        app.get('/', function (req, res) {
            data.getNoteCategories(function (error, results) {
                res.render('index', {
                    title: 'Express + Vash', 
                    error: error, 
                    categories: results,
                    newCatError: req.flash("newCatName")
                });
            });
        });
        
        
        app.get("/notes/:categoryName", function (req, res) {
            var categoryName = req.params.categoryName;
            res.render( "notes", { title: categoryName } );
        });
        
        app.post('/newCategory', function (req, res) {
            var categoryName = req.body.categoryName;
            data.createNewCategory(categoryName, function (error) {
                if (error) {
                    req.flash('newCatName', error);
                    res.redirect("/");
                } else {
                    res.redirect("/notes/" + categoryName);
                }
            });
        });

    }
})(module.exports);