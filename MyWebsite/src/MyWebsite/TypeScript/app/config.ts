/// <reference path="../extdefinitions/tsd.d.ts" />
require.config({
    baseUrl: '/js',
    paths: {},
    shim: {}
});

declare var globalModule: string; 

switch (globalModule) {
	case "ProjectPage":
		// load AMD module ProjectPage.ts (compiled to ProjectPage.js)
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

	default:
		break;
}

