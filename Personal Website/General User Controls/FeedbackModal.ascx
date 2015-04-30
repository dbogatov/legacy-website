<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeedbackModal.ascx.cs" Inherits="Personal_Website.General_User_Controls.FeedbackModal" %>

<script src="../API/ajaxAPI.js"></script>

<script>

	function success(msg) {
		$('#responseView').modal();
	}

	function error(xhr, ajaxOptions, thrownError) {
		$('#responseView').modal();

		$("#responseText").text("Unfortunatelly, something went wrong... Please, try again later.");

	}

	function validate() {
		return $("#<%= subject.ClientID %>").val() != "" && $("#<%= messageText.ClientID %>").val() != "";
	}

	$(document).ready(function () {

		$("#sendFeedback").on("click", function () {

			if (validate()) {

				$('#feedbackView').modal('hide');

				sendJSON(
					"FeedbackManager",
					"leaveFeedback",
					JSON.stringify(
						{
							"from": $("#<%= sender.ClientID %>").val(),
							"subject": $("#<%= subject.ClientID %>").val(),
							"body": $("#<%= messageText.ClientID %>").val(),
							"url": window.location.href
						}
					),
					success,
					error);

				$(this).closest('form').find("input[type=text], textarea").val("");

			} else {
				$("#errorLabel").show();
			}
		});
	});

</script>

<div class="modal fade" id="responseView" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title" id="responseViewLabel">Thank you!</h4>
			</div>
			<div class="modal-body">
				<h3 id="responseText">I got your feedback and will be in touch shortly.</h3>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
			</div>
		</div>
	</div>
</div>

<div class="modal fade" id="feedbackView" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title" id="exampleModalLabel">Feedback Message</h4>
			</div>
			<div class="modal-body">
				<form>
					<div class="form-group">
						<label for="sender" class="control-label">Your email: (not required, but appreciated)</label>
						<input type="text" class="form-control" id="sender" runat="server" placeholder="eq. name@example.com">
					</div>
					<div class="form-group">
						<label for="subject" class="control-label">Subject*:</label>
						<input type="text" class="form-control" id="subject" runat="server" placeholder="eq. Broken link">
					</div>
					<div class="form-group">
						<label for="message-text" class="control-label">Message*:</label>
						<textarea rows="6" class="form-control" id="messageText" runat="server" placeholder="eq. The link to your project on this page is broken."></textarea>
					</div>
				</form>
			</div>
			<div class="modal-footer">
				<span class="text-danger pull-left" hidden id="errorLabel">Required fields are marked with *</span>
				<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
				<button type="button" class="btn btn-primary" id="sendFeedback">Send message</button>
			</div>
		</div>
	</div>
</div>