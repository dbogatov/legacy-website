define(["require", "exports"], function (require, exports) {
    "use strict";
    var ContactPage = (function () {
        function ContactPage() {
            this.form = $("#contact_form");
        }
        ContactPage.prototype.setupForm = function () {
            var errorHandler = function () {
                $('#responseView').modal();
                $("#responseText").text("Unfortunatelly, something went wrong... Please, try again later.");
            };
            $("#contactBtn").click(function () {
                $.post("/api/contact", {
                    name: $("#userName").val(),
                    email: $("#email").val(),
                    comment: $("#comment").val(),
                    language: $("#lang").val()
                }, function (response) {
                    if (response) {
                        $('#responseView').modal();
                        $("#responseText").text("Thank you! I have got your mesage!");
                        $("#responseCloseBtn").html("Go home");
                        $("#responseCloseBtn").click(function () {
                            location.href = "/";
                        });
                    }
                    else {
                        errorHandler();
                    }
                }).fail(errorHandler);
            });
        };
        ContactPage.prototype.run = function () {
            this.setupForm();
        };
        return ContactPage;
    }());
    return ContactPage;
});

//# sourceMappingURL=ContactPage.js.map
