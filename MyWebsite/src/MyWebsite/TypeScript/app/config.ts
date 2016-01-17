/// <reference path="../extdefinitions/tsd.d.ts" />
require.config({
    baseUrl: '/js',
    paths: {},
    shim: {}
});

// load AMD module MasterPage.ts (compiled to MasterPage.js)
require(['MasterPage'], (MasterPage) => {
	var app = new MasterPage();
	app.run();
});

declare var globalModule: string;

switch (globalModule) {
	case "ProjectPage":
		require(['ProjectPage'], (ProjectPage) => {
			var app = new ProjectPage();
			app.run();
		});
		break;

	case "CoursesPage":
		require(['CoursesPage'], (CoursesPage) => {
			var app = new CoursesPage();
			app.run();
		});
		break;

	case "ContactPage":
		require(['ContactPage'], (ContactPage) => {
			var app = new ContactPage();
			app.run();
		});
		break;

	default:
		break;
}

