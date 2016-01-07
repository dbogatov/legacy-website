declare class ProjectsPage {
    private projects;
    private tags;
    private resizeHandler;
    private succinct(element, options);
    private filterUsingKey(key);
    private updateProjects(text);
    private setListeners();
    private displayProjects();
    private displayTags();
    private loadProjects();
    private loadTags();
    run(): void;
}
export = ProjectsPage;
