import util = require('Utility');

class CoursesPage {
	private courses: Course[];
	
	// Ideally, pulled from DB
	private requirementsTree = {
		major: ["CS Classes + MQP", "Math", "Science"],
		social: ["Social Sciences"],
		humanitites: ["Breadth", "Depth", "Seminar"],
		pe: ["Physical Education"],
		iqp: ["IQP"],
		other: ["Free Elective"]
	};

	private static template = _.template(`
		<h4 class="text-center"><%= reqText %></h4>
		<table id="gradeTable_<%= reqId %>" class="table table-striped table-bordered table-hover">
			<thead>
				<tr>
					<th>Term</th>
					<th>Year</th>
					<th>Title</th>
					<th>Grade</th>
					<th>Status</th>
				</tr>
			</thead>
			<tbody>
				<% _.each(courses,function(course){ %>
					<%= course %>
				<% }); %>
			</tbody>
		</table>
	`);

	private sortGrades(): void {

		// little hack		
		(<any>$(".table")).tablesorter({

			sortList: [[1, 1], [0, 1]],

			textExtraction: function(node) {
				switch ($(node).text()) {
					case "A":
						return "C";
						break;
					case "B":
						return "D";
						break;
					case "C":
						return "A";
						break;
					case "D":
						return "B";
						break;
					default:
						return $(node).text();
				}
			}
		});
	}

	private getParentRequirement(requirement: string): string {
		var result: string;
		for (var parent in this.requirementsTree) {
			if (this.requirementsTree[parent].indexOf(requirement) > -1) {
				result = parent;
				break;
			}
		}

		return result;
	}

	private groupCourses() {
		var dictionary = {};

		this.courses.forEach((course) => {
			if (dictionary[course.requirement] === undefined) {
				dictionary[course.requirement] = new Array<Course>(course);
			} else {
				dictionary[course.requirement].push(course);
			}
		});

		return dictionary;
	}

	private displayCourses(): void {
		var dictionary = this.groupCourses();

		for (var requirement in dictionary) {
			var courses = <Array<Course>>dictionary[requirement];
			var htmlId = this.getParentRequirement(requirement);

			var html = CoursesPage.template({
				reqId: courses[0].requirement.replace(/\W/g, ''),
				reqText: courses[0].requirement,
				courses: courses.map((course) => course.getHtmlView())
			});

			$(`#${htmlId}`).after(html);
		}
		
		this.sortGrades();
	}

	private loadCourses(): void {
		$.get("/api/Courses", {}, courses => {
			this.courses = courses.map(course =>
				new Course().deserialize(course)
			);

			this.displayCourses();
		});
	}

	run() {
		this.loadCourses();
	}
}

class Course implements util.Utility.ISerializable<Course>, util.Utility.ITemplatable {
	private static template = _.template(`
		<tr class="<%- color %>">
			<td><%- term %>
			<td><%- year %>
			<td><%- title %>
			<td><%- grade %>
			<td><%- status %>
		</tr>
	`);

	title: string;
	term: string;
	year: number;
	courseId: string;
	gradePercent: string;
	gradeLetter: string;
	status: string;
	requirement: string;


	deserialize(input): Course {
		this.title = input.Title;
		this.term = input.Term;
		this.year = input.Year;
		this.courseId = input.CourseId;
		this.gradePercent = input.GradePercent;
		this.gradeLetter = input.GradeLetter;
		this.status = input.Status;
		this.requirement = input.Requirement.ReqName;

		return this;
	}

	getHtmlView(): string {
		var color: string;

		switch (this.status) {
			case "Completed":
				color = "success";
				break;
			case "in progress...":
				color = "active";
				break;
			case "Registered":
				color = "warning";
				break;
			default:
				color: "danger";
		}

		return Course.template({
			term: this.term.toUpperCase(),
			year: this.year,
			title: this.title,
			grade: this.gradeLetter,
			status: this.status,
			color: color
		});
	}
}

export = CoursesPage;