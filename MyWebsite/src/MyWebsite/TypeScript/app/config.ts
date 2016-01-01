﻿/// <reference path="../extdefinitions/tsd.d.ts" />
require.config({
    baseUrl: 'js',
    paths: {},
    shim: {}
});
 
// load AMD module main.ts (compiled to main.js)
require(['main'], (main) => {
    var app = new main();
    app.run();
});