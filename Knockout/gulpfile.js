var gulp = require('gulp'),
    jshint = require('gulp-jshint');
var paths = {
    scripts: ['app/**/*.js'],
};

 
gulp.task('default', ['build']);
gulp.task('build', function (callback) {
	return gulp.src(paths.scripts)
        	   .pipe(jshint('.jshintrc'))
        	   .pipe(jshint.reporter('jshint-stylish'))
    		   .pipe(jshint.reporter('fail'));
});
