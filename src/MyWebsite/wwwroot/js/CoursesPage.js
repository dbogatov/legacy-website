define(["require", "exports", 'Utility'], function (require, exports, util) {
    "use strict";
    var CoursesPage = (function () {
        function CoursesPage() {
        }
        CoursesPage.prototype.sortGrades = function () {
            $(".table").tablesorter({
                sortList: [[1, 1], [0, 1]],
                textExtraction: function (node) {
                    switch ($(node).text()) {
                        case "A":
                            return "C";
                        case "B":
                            return "D";
                        case "C":
                            return "A";
                        case "D":
                            return "B";
                        default:
                            return $(node).text();
                    }
                }
            });
        };
        CoursesPage.prototype.computeGPA = function (courses) {
            var completed = courses.filter(function (course) { return course.status === "Completed"; });
            return (completed
                .map(function (course) { return CoursesPage.letterToNumber[course.gradeLetter]; })
                .reduce(function (accumulator, element) { return accumulator + element; }, 0) /
                (completed.length > 0 ? completed.length : 1))
                .toPrecision(3);
        };
        CoursesPage.prototype.groupParentRequirements = function () {
            var dictionary = {};
            this.courses.forEach(function (course) {
                if (dictionary[course.parentRequirement.title] === undefined) {
                    dictionary[course.parentRequirement.title] = new Array(course);
                }
                else {
                    dictionary[course.parentRequirement.title].push(course);
                }
            });
            return dictionary;
        };
        CoursesPage.prototype.groupCourses = function () {
            var dictionary = {};
            this.courses.forEach(function (course) {
                if (dictionary[course.requirement] === undefined) {
                    dictionary[course.requirement] = new Array(course);
                }
                else {
                    dictionary[course.requirement].push(course);
                }
            });
            return dictionary;
        };
        CoursesPage.prototype.drawParentRequirements = function () {
            var dictionary = this.groupParentRequirements();
            for (var requirement in dictionary) {
                var courses = dictionary[requirement];
                var column = courses[0].parentRequirement.column === 1 ? "leftBlock" : "rightBlock";
                $("#" + column).append(CoursesPage.parentTemplate({
                    gpa: this.computeGPA(courses),
                    title: requirement,
                    htmlId: requirement.replace(/\W/g, '')
                }));
            }
        };
        CoursesPage.prototype.displayCourses = function () {
            this.drawParentRequirements();
            var dictionary = this.groupCourses();
            for (var requirement in dictionary) {
                var courses = dictionary[requirement];
                var htmlId = courses[0].parentRequirement.title.replace(/\W/g, '');
                var html = CoursesPage.childTemplate({
                    gpa: this.computeGPA(courses),
                    reqId: courses[0].requirement.replace(/\W/g, ''),
                    reqText: courses[0].requirement,
                    courses: courses.map(function (course) { return course.getHtmlView(); })
                });
                $("#" + htmlId).after(html);
            }
            this.sortGrades();
        };
        CoursesPage.prototype.displayTotalGPA = function () {
            $("#totalGPA").html(this.computeGPA(this.courses));
        };
        CoursesPage.prototype.loadCourses = function () {
            var _this = this;
            $.get("/api/Courses", {}, function (courses) {
                _this.courses = courses.map(function (course) {
                    return new Course().deserialize(course);
                });
                _this.displayCourses();
                _this.displayTotalGPA();
            });
        };
        CoursesPage.prototype.run = function () {
            util.Utility.Utility.enableAJAXLoadBar();
            this.loadCourses();
        };
        CoursesPage.letterToNumber = {
            A: 4,
            B: 3,
            C: 2,
            NR: 0
        };
        CoursesPage.parentTemplate = _.template("\n\t\t<h3 class=\"majorRequirement\"><%= title %> (GPA:  <%= gpa %>)</h3>\n\t\t<hr id=\"<%= htmlId %>\" />\n\t");
        CoursesPage.childTemplate = _.template("\n\t\t<h4 class=\"text-center\"><%= reqText %> (GPA: <%= gpa %>)</h4>\n\t\t<table id=\"gradeTable_<%= reqId %>\" class=\"table table-striped table-bordered table-hover\">\n\t\t\t<thead>\n\t\t\t\t<tr>\n\t\t\t\t\t<th>Term</th>\n\t\t\t\t\t<th>Year</th>\n\t\t\t\t\t<th>Title</th>\n\t\t\t\t\t<th>Grade</th>\n\t\t\t\t\t<th>Status</th>\n\t\t\t\t</tr>\n\t\t\t</thead>\n\t\t\t<tbody>\n\t\t\t\t<% _.each(courses,function(course){ %>\n\t\t\t\t\t<%= course %>\n\t\t\t\t<% }); %>\n\t\t\t</tbody>\n\t\t</table>\n\t");
        return CoursesPage;
    }());
    var Course = (function () {
        function Course() {
        }
        Course.prototype.deserialize = function (input) {
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
        };
        Course.prototype.getHtmlView = function () {
            var color;
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
                    color = "danger";
            }
            return Course.template({
                term: this.term.toUpperCase(),
                year: this.year,
                title: this.title,
                grade: this.gradeLetter,
                status: this.status,
                color: color
            });
        };
        Course.template = _.template("\n\t\t<tr class=\"<%- color %>\">\n\t\t\t<td><%- term %>\n\t\t\t<td><%- year %>\n\t\t\t<td><%- title %>\n\t\t\t<td><%- grade %>\n\t\t\t<td><%- status %>\n\t\t</tr>\n\t");
        return Course;
    }());
    return CoursesPage;
});

//# sourceMappingURL=CoursesPage.js.map
