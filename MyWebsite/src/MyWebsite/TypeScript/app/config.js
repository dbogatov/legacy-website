/// <reference path="../extdefinitions/tsd.d.ts" />
require.config({
    baseUrl: 'js',
    paths: {},
    shim: {}
});
// load AMD module main.ts (compiled to main.js)
require(['ProjectPage'], function (ProjectPage) {
    var app = new ProjectPage();
    app.run();
});
//# sourceMappingURL=config.js.map