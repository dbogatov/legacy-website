/// <reference path="../extdefinitions/tsd.d.ts" />
require.config({
	baseUrl: '/ts',
	paths: {},
	shim: {}
});

// load AMD module MasterPage.ts (compiled to MasterPage.js)
require(['MasterPage'], (MasterPage) => {
	var app = new MasterPage();
	app.run();
});
