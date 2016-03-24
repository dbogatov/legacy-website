require.config({
    baseUrl: '/ts',
    paths: {},
    shim: {}
});
require(['MasterPage'], function (MasterPage) {
    var app = new MasterPage();
    app.run();
});

//# sourceMappingURL=config.js.map
