import util = require('Utility');

class CoursesPage {
	private courses: Course[];
	
	private static letterToNumber = {
		A: 4,
		B: 3,
		C: 2,
		NR: 0
	};

	private static parentTemplate = _.template(`
		<h3 class="majorRequirement"><%= title %> (GPA:  <%= gpa %>)</h3>
		<hr id="<%= htmlId %>" />
	`);
	
	private static childTemplate = _.template(`
		<h4 class="text-center"><%= reqText %> (GPA: <%= gpa %>)</h4>
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

	private computeGPA(courses: Array<Course>): string {
		var completed = courses.filter((course) => course.status === "Completed");
		
		return (completed
			.map((course) => CoursesPage.letterToNumber[course.gradeLetter])
			.reduce((accumulator, element) => accumulator + element, 0) /
			(completed.length > 0 ? completed.length : 1))
				.toPrecision(3);
	}
	
	private groupParentRequirements() {
		var dictionary = {};

		this.courses.forEach((course) => {
			if (dictionary[course.parentRequirement.title] === undefined) {
				dictionary[course.parentRequirement.title] = new Array<Course>(course);
			} else {
				dictionary[course.parentRequirement.title].push(course);
			}
		});

		return dictionary;
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

	private drawParentRequirements() {
		var dictionary = this.groupParentRequirements();
		
		for (var requirement in dictionary) {
			var courses = <Array<Course>>dictionary[requirement];
			var column = courses[0].parentRequirement.column === 1 ? "leftBlock" : "rightBlock";
			
			$(`#${column}`).append(CoursesPage.parentTemplate({
				gpa: this.computeGPA(courses),
				title: requirement,
				htmlId: requirement.replace(/\W/g, '')
			}));
		}
	}
	
	private displayCourses(): void {
		this.drawParentRequirements();
		
		var dictionary = this.groupCourses();

		for (var requirement in dictionary) {
			var courses = <Array<Course>>dictionary[requirement];
			var htmlId = courses[0].parentRequirement.title.replace(/\W/g, ''); //this.getParentRequirement(requirement);

			
			
			var html = CoursesPage.childTemplate({
				gpa: this.computeGPA(courses),
				reqId: courses[0].requirement.replace(/\W/g, ''),
				reqText: courses[0].requirement,
				courses: courses.map((course) => course.getHtmlView())
			});

			$(`#${htmlId}`).after(html);
		}

		this.sortGrades();
	}

	private displayTotalGPA(): void {
		$("#totalGPA").html(this.computeGPA(this.courses));
	}
	
	private loadCourses(): void {
		$.get("/api/Courses", {}, courses => {
			this.courses = courses.map(course =>
				new Course().deserialize(course)
			);

			this.displayCourses();
			this.displayTotalGPA();
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
	parentRequirement: {
		title: string,
		column: number;
	}


	deserialize(input): Course {
		this.title = input.Title;
		this.term = input.Term;
		this.year = input.Year;
		this.courseId = input.CourseId;
		this.gradePercent = input.GradePercent;
		this.gradeLetter = input.GradeLetter;
		this.status = input.Status;
		this.requirement = input.Requirement.ReqName;
		this.parentRequirement = {
			title: input.Requirement.ParentRequirement.Title,
			column: input.Requirement.ParentRequirement.DisplayColumn
		};

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