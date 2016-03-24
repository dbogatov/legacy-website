/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
	rimraf = require("rimraf"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	uglify = require("gulp-uglify"),
	ts = require('gulp-typescript'),
	shell = require('gulp-shell'),
	sourcemaps = require('gulp-sourcemaps'),
	merge = require('merge'),
	fs = require("fs");

var paths = {
	webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

paths.npm = "./node_modules/";
paths.lib = paths.webroot + "/lib/";

paths.tsSource = "./TypeScript/app/**/*.ts";
paths.tsMaps = "./TypeScript/app/**/*.js.map";
paths.tsJS = "./TypeScript/app/**/*.js";

paths.tsOutput = paths.webroot + "/ts/";
paths.tsDef = "./TypeScript/definitions/";

eval("var project = " + fs.readFileSync("./project.json"));

gulp.task("clean:js", function (cb) {
	rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
	rimraf(paths.concatCssDest, cb);
});

gulp.task("clean:ts", function (cb) {
	rimraf(paths.tsOutput, cb);
});

gulp.task("clean", ["clean:js", "clean:css", "clean:ts"]);

var tsProject = ts.createProject({
	declarationFiles: true,
	noExternalResolve: false,
	module: 'AMD',
	removeComments: true
});

gulp.task("min:js", function () {
	return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
		.pipe(concat(paths.concatJsDest))
		.pipe(uglify())
		.pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
	return gulp.src([paths.css, "!" + paths.minCss])
		.pipe(concat(paths.concatCssDest))
		.pipe(cssmin())
		.pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("ts-debug", function () {
	return merge([
		gulp
			.src(paths.tsSource)
			.pipe(sourcemaps.init())
			.pipe(ts(tsProject))
			.pipe(sourcemaps.write("."))
			.pipe(gulp.dest(paths.tsOutput)),
		gulp
			.src(paths.tsSource)
			.pipe(gulp.dest(paths.tsOutput))
	]);
});

gulp.task('ts-gen-defs', function () {
	gulp
		.src(paths.tsSource)
		.pipe(ts(tsProject))
		.dts
		.pipe(gulp.dest(paths.tsDef));
});