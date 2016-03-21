require.config({
    baseUrl: '/js',
    paths: {},
    shim: {}
});
require(['MasterPage'], function (MasterPage) {
    var app = new MasterPage();
    app.run();
});

//# sourceMappingURL=config.js.map
