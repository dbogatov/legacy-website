class ProjectsPage {

	private projects: Project[];
	private tags: Tag[];

	displayProjects(): void {
		$("#projectsDiv").html(this.projects.map(project => project.getHtmlView()).join(""));
	}

	displayTags(): void {
		$("#myTab").append(this.tags.map(tag => tag.getHtmlView()).join(""));
	}

	loadProjects(): void {
		$.get("api/Projects", {}, projects => {
			this.projects = projects.map(project =>
				new Project().deserialize(project)
			);

			this.displayProjects();
		});
	}

	loadTags(): void {
		$.get("api/Tags", {}, tags => {
			this.tags = tags.map(tag =>
				new Tag().deserialize(tag)
			);

			this.displayTags();
		});
	}

    run() {
		this.loadProjects();
		this.loadTags();
    }
}

interface ISerializable<T> {
    deserialize(input: Object): T;
}

class Tag implements ISerializable<Tag> {
	private static template = _.template(`
		<li role='presentation'><a href='#<%= tagLink %>'><%= tagName %></a></li>
	`);

	tagName: string;

	getHtmlView(): string {
		return Tag.template({
			tagLink: this.tagName.toLowerCase(),
			tagName: this.tagName
		});
	}

	deserialize(input): Tag {
		this.tagName = input.TagName;

		return this;
	}
}

class Project implements ISerializable<Project> {
	private static template = _.template(`
		<div class='col-sm-6 col-md-4 project-thumbnail <%= projectType %>' style='padding-top:10px' >
			<div class='thumbnail' style='height: 450px'>
				<img src='<%= imageSrc %>' alt='Here should have been an image' style='max-height:255px' class='img-rounded'>
				<div class='fixHeight'></div>
				<div class='caption'>
					<h3 class="projectTitle"><%= title %></h3>
					<h5><%= date %></h5>
					<p class='description' style='text-align: justify;'><%= description %></p>
					<p><a href='<%= tryRef %>' target=_blank class='btn btn-primary' role='button'>Try it!</a> <a href='<%= srcRef %>' target=_blank class='btn btn-default' role='button'>View source</a></p>
				</div>
			</div>
		</div>
	`);

	projectId: number;
	title: string;
	descriptionText: string;
	dateCompleted: Date;
	weblink: string;
	sourceLink: string;
	imgeFilePath: string;
	tags: string[];

	getHtmlView(): string {
		return Project.template({
			projectType: this.tags.map(tag => `project-${tag}`).join(" "),
			imageSrc: this.imgeFilePath,
			title: this.title,
			date: this.dateCompleted,
			description: this.descriptionText,
			tryRef: this.weblink,
			srcRef: this.sourceLink
		});
	}

	deserialize(input): Project {
        this.projectId = input.ProjectId;
		this.title = input.Title;
		this.descriptionText = input.DescriptionText;
		this.dateCompleted = input.DateCompleted;
		this.weblink = input.Weblink;
		this.sourceLink = input.SourceLink;
		this.imgeFilePath = input.ImgeFilePath;
		this.tags = input.Tags.map(tag => tag.TagName);

        return this;
    }
}

export = ProjectsPage;