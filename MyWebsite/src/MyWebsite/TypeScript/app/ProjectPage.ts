import util = require('Utility');

class ProjectsPage {

	private projects: Project[];
	private tags: Tag[];

	private projectsLoaded = false;
	private tagsLoaded = false;

	private resizeHandler = () => {
		$(".fixHeight").each((index, element) => {
			$(element).height(260 - $(element).parent().children().eq(0).height());
		});

		$("[href='#']").addClass("disabled");
	}

	private maySetListeners(): void {
		if (this.projectsLoaded && this, this.tagsLoaded) {
			this.setListeners();
		}
	}

	private succinct(element, options) {

		var settings = $.extend({
			size: 240,
			omission: "...",
			ignore: true
		}, options);

		return element.each((index, eachElement) => {

			var textDefault: string,
				textTruncated: string,
				elements = $(eachElement),
				regex = /[!-\/:-@\[-`{-~]$/,
				init = () => {
					elements.each(() => {
						textDefault = $(eachElement).html();

						if (textDefault.length > settings.size) {
							textTruncated = $.trim(textDefault)
								.substring(0, settings.size)
								.split(" ")
								.slice(0, -1)
								.join(" ");

							if (settings.ignore) {
								textTruncated = textTruncated.replace(regex, "");
							}

							$(eachElement).html(textTruncated + settings.omission);
						}
					});
				};
			init();
		});
	}

	private filterUsingKey(key: string) {

		if (key === "all") {
			$(".project-thumbnail").fadeIn("slow", null);
		} else {
			$(`.project-thumbnail:not(.project-${key})`).fadeOut("slow", null);
			$(`.project-${key}`).fadeIn("slow", null);
		}
	}

	private updateProjects(text: string): void {
		var atLeastOneShow = false;

		$(".project-thumbnail").each((index, element) => {
			var flag = false;

			if ($("#titleChbx").is(":checked") && $(element).find(".projectTitle").text().toLowerCase().indexOf(text.toLowerCase()) >= 0) {
				flag = true;
			}

			if (!flag && $("#descriptionChbx").is(":checked") && $(element).find(".description").text().toLowerCase().indexOf(text.toLowerCase()) >= 0) {
				flag = true;
			}

			if (!flag) {
				$(element).hide();
			} else {
				$(element).show();
				atLeastOneShow = true;
			}
		});

		if (atLeastOneShow) {
			$("#emptySearchPlaceholder").hide();
			$("#searchDiv").removeClass("has-error").addClass("has-success");
		} else {
			$("#emptySearchPlaceholder").show();
			$("#searchDiv").removeClass("has-success").addClass("has-error");
		}
	}

	private setListeners(): void {
		// Search events
		var thisInstance = this;

		$("#searchBar").keyup((event) => {
			thisInstance.updateProjects($(event.target).val());
		});

		$("#searchBar").on("paste", () => {
			setTimeout(() => {
				thisInstance.updateProjects($("#searchBar").val());
			}, 200);
		});

		$("#searchBar").on("cut", () => {
			setTimeout(() => {
				thisInstance.updateProjects($("#searchBar").val());
			}, 200);
		});

		$("input:checkbox").change(() => {
			thisInstance.updateProjects($("#searchBar").val());
		});

		$("#clearSearch").click(() => {
			$("input:checkbox").prop("checked", true);
			$("#searchBar").val("");
			thisInstance.updateProjects("");
		});

		// Filter code

		$("#myTab a").click((event) => {
			event.preventDefault();
			$(event.target).tab("show");

			thisInstance.filterUsingKey($(event.target).attr("href").substr(1));

			$("#searchBar").val("");
		});

		$(".description").each((index, element) => {
			if ($(element).text().length > 70) {

				var title = $(element).text();

				thisInstance.succinct($(element), {
					size: 70
				}).append(` <a href='#' data-toggle='tooltip' data-placement='top' title='${title}'>view all</a>`);
			}
		});

		$(() => {
			$('[data-toggle="tooltip"]').tooltip();
		});

		$(window).resize(this.resizeHandler);
		$(".fixHeight").ready(() => { $(window).trigger("resize"); });

		setInterval(() => { $(window).trigger("resize"); }, 300);
	}

	private displayProjects(): void {
		$("#projectsDiv").html(this.projects.map(project => project.getHtmlView()).join(""));
	}

	private displayTags(): void {
		$("#myTab").append(this.tags.map(tag => tag.getHtmlView()).join(""));
	}

	private loadProjects(): void {
		$.get("api/Projects", {}, projects => {
			this.projects = projects.map(project =>
				new Project().deserialize(project)
			);

			this.displayProjects();
			this.projectsLoaded = true;
			this.maySetListeners();
		});
	}

	private loadTags(): void {
		$.get("api/Tags", {}, tags => {
			this.tags = tags.map(tag =>
				new Tag().deserialize(tag)
			);

			this.displayTags();
			this.tagsLoaded = true;
			this.maySetListeners();
		});
	}

    run() {
		util.Utility.Utility.enableAJAXLoadBar();
		
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
			<div class='thumbnail' style='height: 460px'>
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
			projectType: this.tags.map(tag => `project-${tag.toLowerCase()}`).join(" "),
			imageSrc: this.imgeFilePath,
			title: this.title,
			date: `${util.Utility.Utility.getMonthName(this.dateCompleted.getMonth())}, ${this.dateCompleted.getFullYear()}`,
			description: this.descriptionText,
			tryRef: this.weblink,
			srcRef: this.sourceLink
		});
	}

	deserialize(input): Project {
        this.projectId = input.ProjectId;
		this.title = input.Title;
		this.descriptionText = input.DescriptionText;
		this.dateCompleted = new Date(input.DateCompleted);
		this.weblink = input.Weblink;
		this.sourceLink = input.SourceLink;
		this.imgeFilePath = input.ImgeFilePath;
		this.tags = input.Tags.map(tag => tag.TagName);

        return this;
    }
}

export = ProjectsPage;