define(["require", "exports"], function (require, exports) {
    var Main = (function () {
        function Main() {
        }
        Main.prototype.displayProjects = function () {
            $("#projectsDiv").html(this.projects.map(function (project) { return project.getHtmlView(); }).join(""));
        };
        Main.prototype.displayTags = function () {
            $("#myTab").append(this.tags.map(function (tag) { return tag.getHtmlView(); }).join(""));
        };
        Main.prototype.loadProjects = function () {
            var _this = this;
            $.get("api/Projects", {}, function (projects) {
                _this.projects = projects.map(function (project) {
                    return new Project().deserialize(project);
                });
                _this.displayProjects();
            });
        };
        Main.prototype.loadTags = function () {
            var _this = this;
            $.get("api/Tags", {}, function (tags) {
                _this.tags = tags.map(function (tag) {
                    return new Tag().deserialize(tag);
                });
                _this.displayTags();
            });
        };
        Main.prototype.run = function () {
            this.loadProjects();
            this.loadTags();
        };
        return Main;
    })();
    var Tag = (function () {
        function Tag() {
        }
        Tag.prototype.getHtmlView = function () {
            return Tag.template({
                tagLink: this.tagName.toLowerCase(),
                tagName: this.tagName
            });
        };
        Tag.prototype.deserialize = function (input) {
            this.tagName = input.TagName;
            return this;
        };
        Tag.template = _.template("\n\t\t<li role='presentation'><a href='#\" + <%= tagLink %> + \"'>\" + <%= tagName %> + \"</a></li>\"\n\t");
        return Tag;
    })();
    var Project = (function () {
        function Project() {
        }
        Project.prototype.getHtmlView = function () {
            return Project.template({
                projectType: this.tags.map(function (tag) { return ("project-" + tag); }).join(" "),
                imageSrc: this.imgeFilePath,
                title: this.title,
                date: this.dateCompleted,
                description: this.descriptionText,
                tryRef: this.weblink,
                srcRef: this.sourceLink
            });
        };
        Project.prototype.deserialize = function (input) {
            this.projectId = input.ProjectId;
            this.title = input.Title;
            this.descriptionText = input.DescriptionText;
            this.dateCompleted = input.DateCompleted;
            this.weblink = input.Weblink;
            this.sourceLink = input.SourceLink;
            this.imgeFilePath = input.ImgeFilePath;
            this.tags = input.Tags.map(function (tag) { return tag.TagName; });
            return this;
        };
        Project.template = _.template("\n\t\t<div class='col-sm-6 col-md-4 project-thumbnail <%= projectType %>' style='padding-top:10px' >\n\t\t\t<div class='thumbnail' style='height: 450px'>\n\t\t\t\t<img src='<%= imageSrc %>' alt='Here should have been an image' style='max-height:255px' class='img-rounded'>\n\t\t\t\t<div class='fixHeight'></div>\n\t\t\t\t<div class='caption'>\n\t\t\t\t\t<h3 class=\"projectTitle\"><%= title %></h3>\n\t\t\t\t\t<h5><%= date %></h5>\n\t\t\t\t\t<p class='description' style='text-align: justify;'><%= description %></p>\n\t\t\t\t\t<p><a href='<%= tryRef %>' target=_blank class='btn btn-primary' role='button'>Try it!</a> <a href='<%= srcRef %>' target=_blank class='btn btn-default' role='button'>View source</a></p>\n\t\t\t\t</div>\n\t\t\t</div>\n\t\t</div>\n\t");
        return Project;
    })();
    return Main;
});
//# sourceMappingURL=main.js.map