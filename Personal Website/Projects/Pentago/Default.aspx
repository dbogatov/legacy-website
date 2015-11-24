<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Pentago.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Pentago</title>

	<webopt:BundleReference runat="server" Path="~/Content/css" />
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />

	<!-- My Styles -->
	<link rel="stylesheet" href="Style.css" />
</head>
<body>
	<form id="form1" runat="server">

		<asp:ScriptManager runat="server">
			<Scripts>
				<%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
				<%--Framework Scripts--%>
				<asp:ScriptReference Name="MsAjaxBundle" />
				<asp:ScriptReference Name="jquery" />
				<asp:ScriptReference Name="bootstrap" />
				<asp:ScriptReference Name="respond" />
				<asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
				<asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
				<asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
				<asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
				<asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
				<asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
				<asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
				<asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
				<asp:ScriptReference Name="WebFormsBundle" />
				<%--Site Scripts--%>
			</Scripts>
		</asp:ScriptManager>

		<!-- My Script -->
		<script src="Script.js"></script>
		<script src="PentagoAPIWrapper.js"></script>

		<div class="row centering">
			<div class="panel panel-primary" id="setupPanel">
				<div class="panel-heading">Pentago</div>
				<div class="panel-body">
					Make your choice
				</div>
				<ul class="list-group">
					<li class="list-group-item">
						<button type="button" class="btn btn-block btn-primary" data-toggle="modal" data-target="#singlePlayerModal">Single Player</button>
					</li>
					<li class="list-group-item">
						<button type="button" class="btn btn-block btn-success" data-toggle="modal" data-target="#multiplayerModal">Multplayer</button>
					</li>
					<li class="list-group-item">
						<a href="https://en.wikipedia.org/wiki/Pentago" target="_blank" class="btn btn-block btn-warning">Rules</a>
					</li>
					<li class="list-group-item">
						<button type="button" class="btn btn-block btn-info" data-toggle="modal" data-target="#aboutModal">About</button>
					</li>
				</ul>
			</div>
		</div>

	</form>


	<!-- Modal Views -->

	<!-- Single Player -->
	<div class="modal fade" id="singlePlayerModal" tabindex="-1" role="dialog" aria-labelledby="singlePlayerModalLabel">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
					<h4 class="modal-title" id="singlePlayerModalLabel">Single Player</h4>
				</div>
				<div class="modal-body">
					<div class="btn-group btn-group-justified" role="group" aria-label="...">
						<button type="button" id="playEasyBtn" class="btn btn-success">Easy</button>
						<button type="button" id="playMediumBtn" class="btn btn-warning">Medium</button>
						<button type="button" id="playHardBtn" class="btn btn-danger">Hard</button>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
					<a href="Play.aspx" id="playBtn" class="btn btn-primary disabled">Play</a>
				</div>
			</div>
		</div>
	</div>

	<!-- Multiplayer -->
	<div class="modal fade" id="multiplayerModal" tabindex="-1" role="dialog" aria-labelledby="multiplayerModalLabel">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
					<h4 class="modal-title" id="multiplayerModalLabel">Multiplayer</h4>
				</div>
				<div class="modal-body">

					<div>

						<!-- Nav tabs -->
						<ul class="nav nav-pills nav-justified" role="tablist" id="multiplayerTabs">
							<li role="presentation"><a href="#host" aria-controls="host" role="tab" data-toggle="tab" id="hostBtn">Host</a></li>
							<li role="presentation" class="active"><a href="#join" aria-controls="join" role="tab" data-toggle="tab">Join</a></li>
						</ul>

						<!-- Tab panes -->
						<div class="tab-content" id="hostContent">

							<h3 style="margin-top: 15px;" hidden id="readyLabel">You are ready! Hit "Play" button!</h3>

							<div role="tabpanel" class="tab-pane fade" id="host">
								<div class="panel panel-default" style="margin-top: 15px;">
									<div class="panel-heading">Waiting for the other player to join</div>

									<div class="panel-body">
										<p id="codeGenerating">Generating code for you... Please wait a second.</p>
										<p id="codeGenerated" hidden>Here is your code: <strong id="hostCode">ERROR</strong>. Give it to your friend and ask to join.</p>

										<div class="progress" style="margin-top: 15px;">
											<div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
												<span class="sr-only">Waiting</span>
											</div>
										</div>
									</div>
								</div>
							</div>

							<div role="tabpanel" class="tab-pane fade in active" id="join">
								<div class="panel panel-default" style="margin-top: 15px;">
									<div class="panel-heading">Enter your code (obtain from the host)</div>

									<div class="panel-body">
										<form>
											<div class="form-group">
												<label for="multiplayerCode">Code</label>
												<input type="text" class="form-control" id="multiplayerCode" placeholder="eq. SLDDFJS" />
											</div>
											<button type="button" class="btn btn-default" id="enterCodeBtn">Enter</button>
										</form>
									</div>
								</div>
							</div>
						</div>

					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
					<a href="Play.aspx" id="playMultiBtn" class="btn btn-primary disabled">Play</a>
				</div>
			</div>
		</div>
	</div>

	<!-- Single Player -->
	<div class="modal fade" id="aboutModal" tabindex="-1" role="dialog" aria-labelledby="aboutModalLabel">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
					<h4 class="modal-title" id="aboutModalLabel">About</h4>
				</div>
				<div class="modal-body">
					This is a CS4445 B15 Webware project.
					<br />
					Dedicated to <a href="https://www.facebook.com/svetlana.barmina.79" target="_blank">Sveta</a>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
				</div>
			</div>
		</div>
	</div>

</body>
</html>
