define(["require", "exports", 'Utility'], function (require, exports, util) {
    "use strict";
    var ProjectsPage = (function () {
        function ProjectsPage() {
            this.projectsLoaded = false;
            this.tagsLoaded = false;
            this.resizeHandler = function () {
                $(".fixHeight").each(function (index, element) {
                    $(element).height(260 - $(element).parent().children().eq(0).height());
                });
                $("[href='#']").addClass("disabled");
            };
        }
        ProjectsPage.prototype.maySetListeners = function () {
            if (this.projectsLoaded && this, this.tagsLoaded) {
                this.setListeners();
            }
        };
        ProjectsPage.prototype.succinct = function (element, options) {
            var settings = $.extend({
                size: 240,
                omission: "...",
                ignore: true
            }, options);
            return element.each(function (index, eachElement) {
                var textDefault, textTruncated, elements = $(eachElement), regex = /[!-\/:-@\[-`{-~]$/, init = function () {
                    elements.each(function () {
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
        };
        ProjectsPage.prototype.filterUsingKey = function (key) {
            if (key === "all") {
                $(".project-thumbnail").fadeIn("slow", null);
            }
            else {
                $(".project-thumbnail:not(.project-" + key + ")").fadeOut("slow", null);
                $(".project-" + key).fadeIn("slow", null);
            }
        };
        ProjectsPage.prototype.updateProjects = function (text) {
            var atLeastOneShow = false;
            $(".project-thumbnail").each(function (index, element) {
                var flag = false;
                if ($("#titleChbx").is(":checked") && $(element).find(".projectTitle").text().toLowerCase().indexOf(text.toLowerCase()) >= 0) {
                    flag = true;
                }
                if (!flag && $("#descriptionChbx").is(":checked") && $(element).find(".description").text().toLowerCase().indexOf(text.toLowerCase()) >= 0) {
                    flag = true;
                }
                if (!flag) {
                    $(element).hide();
                }
                else {
                    $(element).show();
                    atLeastOneShow = true;
                }
            });
            if (atLeastOneShow) {
                $("#emptySearchPlaceholder").hide();
                $("#searchDiv").removeClass("has-error").addClass("has-success");
            }
            else {
                $("#emptySearchPlaceholder").show();
                $("#searchDiv").removeClass("has-success").addClass("has-error");
            }
        };
        ProjectsPage.prototype.setListeners = function () {
            var thisInstance = this;
            $("#searchBar").keyup(function (event) {
                thisInstance.updateProjects($(event.target).val());
            });
            $("#searchBar").on("paste", function () {
                setTimeout(function () {
                    thisInstance.updateProjects($("#searchBar").val());
                }, 200);
            });
            $("#searchBar").on("cut", function () {
                setTimeout(function () {
                    thisInstance.updateProjects($("#searchBar").val());
                }, 200);
            });
            $("input:checkbox").change(function () {
                thisInstance.updateProjects($("#searchBar").val());
            });
            $("#clearSearch").click(function () {
                $("input:checkbox").prop("checked", true);
                $("#searchBar").val("");
                thisInstance.updateProjects("");
            });
            $("#myTab a").click(function (event) {
                event.preventDefault();
                $(event.target).tab("show");
                thisInstance.filterUsingKey($(event.target).attr("href").substr(1));
                $("#searchBar").val("");
            });
            $(".description").each(function (index, element) {
                if ($(element).text().length > 70) {
                    var title = $(element).text();
                    thisInstance.succinct($(element), {
                        size: 70
                    }).append(" <a href='#' data-toggle='tooltip' data-placement='top' title='" + title + "'>view all</a>");
                }
            });
            $(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
            $(window).resize(this.resizeHandler);
            $(".fixHeight").ready(function () { $(window).trigger("resize"); });
            setInterval(function () { $(window).trigger("resize"); }, 300);
        };
        ProjectsPage.prototype.displayProjects = function () {
            $("#projectsDiv").html(this.projects.map(function (project) { return project.getHtmlView(); }).join(""));
        };
        ProjectsPage.prototype.displayTags = function () {
            $("#myTab").append(this.tags.map(function (tag) { return tag.getHtmlView(); }).join(""));
        };
        ProjectsPage.prototype.loadProjects = function () {
            var _this = this;
            $.get("api/Projects", {}, function (projects) {
                _this.projects = projects.map(function (project) {
                    return new Project().deserialize(project);
                });
                _this.displayProjects();
                _this.projectsLoaded = true;
                _this.maySetListeners();
            });
        };
        ProjectsPage.prototype.loadTags = function () {
            var _this = this;
            $.get("api/Tags", {}, function (tags) {
                _this.tags = tags.map(function (tag) {
                    return new Tag().deserialize(tag);
                });
                _this.displayTags();
                _this.tagsLoaded = true;
                _this.maySetListeners();
            });
        };
        ProjectsPage.prototype.run = function () {
            util.Utility.Utility.enableAJAXLoadBar();
            this.loadProjects();
            this.loadTags();
        };
        return ProjectsPage;
    }());
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
        Tag.template = _.template("\n\t\t<li role='presentation'><a href='#<%= tagLink %>'><%= tagName %></a></li>\n\t");
        return Tag;
    }());
    var Project = (function () {
        function Project() {
        }
        Project.prototype.getHtmlView = function () {
            return Project.template({
                projectType: this.tags.map(function (tag) { return ("project-" + tag.toLowerCase()); }).join(" "),
                imageSrc: this.imgeFilePath,
                title: this.title,
                date: util.Utility.Utility.getMonthName(this.dateCompleted.getMonth()) + ", " + this.dateCompleted.getFullYear(),
                description: this.descriptionText,
                tryRef: this.weblink,
                srcRef: this.sourceLink
            });
        };
        Project.prototype.deserialize = function (input) {
            this.projectId = input.ProjectId;
            this.title = input.Title;
            this.descriptionText = input.DescriptionText;
            this.dateCompleted = new Date(input.DateCompleted);
            this.weblink = input.Weblink;
            this.sourceLink = input.SourceLink;
            this.imgeFilePath = input.ImgeFilePath;
            this.tags = input.Tags.map(function (tag) { return tag.TagName; });
            return this;
        };
        Project.template = _.template("\n\t\t<div class='col-sm-6 col-md-4 project-thumbnail <%= projectType %>' style='padding-top:10px' >\n\t\t\t<div class='thumbnail' style='height: 460px'>\n\t\t\t\t<img src='<%= imageSrc %>' alt='Here should have been an image' style='max-height:255px' class='img-rounded'>\n\t\t\t\t<div class='fixHeight'></div>\n\t\t\t\t<div class='caption'>\n\t\t\t\t\t<h3 class=\"projectTitle\"><%= title %></h3>\n\t\t\t\t\t<h5><%= date %></h5>\n\t\t\t\t\t<p class='description' style='text-align: justify;'><%= description %></p>\n\t\t\t\t\t<p><a href='<%= tryRef %>' target=_blank class='btn btn-primary' role='button'>Try it!</a> <a href='<%= srcRef %>' target=_blank class='btn btn-default' role='button'>View source</a></p>\n\t\t\t\t</div>\n\t\t\t</div>\n\t\t</div>\n\t");
        return Project;
    }());
    return ProjectsPage;
});

//# sourceMappingURL=ProjectPage.js.map
