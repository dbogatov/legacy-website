<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="Personal_Website.Projects.Pentago.Play" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Play Pentago</title>

	<webopt:BundleReference runat="server" Path="~/Content/css" />
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />

	<!-- My Styles -->
	<link rel="stylesheet" href="Style.css" />
</head>
<body id="myBody">
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
		<script src="Game.js"></script>
		<script src="PentagoAPIWrapper.js"></script>

		<div class="row">
			<div class="col-md-6 col-md-offset-3">
				<div class="panel panel-primary" id="mainPanel">
					<div class="panel-heading">Pentago</div>
					<div class="panel-body" style="text-align: center;">
						<h3 id="userLog">Make your choice</h3>
					</div>

					<table class="table" id="gameTable">
						<tbody id="gameTBody" class="gameCell">
							<tr>
								<td style="width: 50%;">
									<table class="table table-bordered innerField upperField" id="ULeft">
										<tbody id="ULeftTBody"></tbody>
									</table>
								</td>
								<td style="width: 50%;">
									<table class="table table-bordered innerField upperField" id="URight">
										<tbody id="URightTBody"></tbody>
									</table>
								</td>
							</tr>
							<tr>
								<td style="width: 50%; border-top: none;">
									<table class="table table-bordered innerField bottomField" id="BLeft">
										<tbody id="BLeftTBody"></tbody>
									</table>
								</td>
								<td style="width: 50%; border-top: none;">
									<table class="table table-bordered innerField bottomField" id="BRight">
										<tbody id="BRightTBody"></tbody>
									</table>
								</td>
							</tr>
						</tbody>
					</table>

				</div>
			</div>
		</div>
	</form>
</body>
</html>
