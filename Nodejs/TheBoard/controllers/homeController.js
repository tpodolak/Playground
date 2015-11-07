(function (homeController) {
    
    var data = require('../data');
    var auth = require('../auth');
    homeController.init = function (app) {
        
        app.get('/', function (req, res) {
            data.getNoteCategories(function (error, results) {
                res.render('index', {
                    title: 'Express + Vash', 
                    error: error, 
                    categories: results,
                    newCatError: req.flash("newCatName"),
                    user: req.user
                });
            });
        });
        
        
        app.get("/notes/:categoryName", auth.ensureAuthenticated, function (req, res) {
            var categoryName = req.params.categoryName;
            res.render("notes", { title: categoryName, user: req.user });
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