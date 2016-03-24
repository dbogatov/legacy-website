define(["require", "exports"], function (require, exports) {
    "use strict";
    var MasterPage = (function () {
        function MasterPage() {
        }
        MasterPage.prototype.setFeedback = function () {
            var errorHandler = function () {
                $('#responseView').modal();
                $("#responseText").text("Unfortunatelly, something went wrong... Please, try again later.");
            };
            var successHandler = function () {
                $('#responseView').modal();
                $("#responseText").text("I got your feedback and will be in touch shortly.");
                $("#sender").val("");
                $("#subject").val("");
                $("#messageText").val("");
            };
            $("#sendFeedback").on("click", function () {
                if ($("#subject").val().length > 0 && $("#messageText").val().length > 0) {
                    $("#feedbackView").modal("hide");
                    $.post("/api/feedback", new Feedback($("#sender").val(), $("#subject").val(), $("#messageText").val(), window.location.href), function (response) {
                        if (response) {
                            successHandler();
                        }
                        else {
                            errorHandler();
                        }
                    }).fail(errorHandler);
                }
            });
        };
        MasterPage.prototype.run = function () {
            this.setFeedback();
        };
        return MasterPage;
    }());
    var Feedback = (function () {
        function Feedback(email, subject, body, url) {
            this.email = email;
            this.subject = subject;
            this.body = body;
            this.url = url;
        }
        return Feedback;
    }());
    return MasterPage;
});

//# sourceMappingURL=MasterPage.js.map
