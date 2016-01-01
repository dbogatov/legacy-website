require.config({
    baseUrl: 'js',
    paths: {},
    shim: {}
});
require(['main'], function (main) {
    var app = new main();
    app.run();
});
