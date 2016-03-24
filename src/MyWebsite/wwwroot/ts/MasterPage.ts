class MasterPage {

	private setFeedback(): void {

		var errorHandler = () => {
			$('#responseView').modal();
			$("#responseText").text("Unfortunatelly, something went wrong... Please, try again later.");
		};

		var successHandler = () => {
			$('#responseView').modal();
			$("#responseText").text("I got your feedback and will be in touch shortly.");

			$("#sender").val("");
			$("#subject").val("");
			$("#messageText").val("");
		};

		$("#sendFeedback").on("click", () => {
			if ($("#subject").val().length > 0 && $("#messageText").val().length > 0) {
				$("#feedbackView").modal("hide");

				$.post(
					"/api/feedback",
					new Feedback(
						$("#sender").val(),
						$("#subject").val(),
						$("#messageText").val(),
						window.location.href
					),
					(response) => {
						if (response) {
							successHandler();
						} else {
							errorHandler();
						}
					}
				).fail(errorHandler);
			}
		});
	}

	run() {
		this.setFeedback();
	}
}

class Feedback {
	email: string;
	subject: string;
	body: string;
	url: string;

	constructor(email: string, subject: string, body: string, url: string) {
		this.email = email;
		this.subject = subject;
		this.body = body;
		this.url = url;
	}
}

export = MasterPage;