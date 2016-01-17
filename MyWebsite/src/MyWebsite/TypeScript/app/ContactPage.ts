class ContactPage {

	private form = (<any>$("#contact_form")).parsley();

	private setupForm(): void {

		var errorHandler = () => {
			$('#responseView').modal();
			$("#responseText").text("Unfortunatelly, something went wrong... Please, try again later.");
		};

		$("#contactBtn").click(() => {
			this.form.validate();
		});

		this.form
			.on("form:success", () => {
				$.post(
					"/api/contact",
					{
						name: $("#userName").val(),
						email: $("#email").val(),
						comment: $("#comment").val(),
						language: $("#lang").val()
					},
					(response) => {
						if (response) {
							$('#responseView').modal();
							$("#responseText").text("Thank you! I have got your mesage!");
							$("#responseCloseBtn").html("Go home");
							$("#responseCloseBtn").click(() => {
								location.href = "/";
							});
						} else {
							errorHandler();
						}
					}
				).fail(errorHandler);
			});
	}

	run(): void {
		this.setupForm();
	}
}

export = ContactPage;