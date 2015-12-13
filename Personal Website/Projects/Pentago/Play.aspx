﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Play.aspx.cs" Inherits="Personal_Website.Projects.Pentago.Play" %>

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
<body id="myBody" style="background-color: #4376ff;">
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

		<!-- D3 Engine -->
		<script src="//d3js.org/d3.v3.min.js" charset="utf-8"></script>

		<!-- My Script -->
		<script src="Game.js"></script>
		<script src="PentagoAPIWrapper.js"></script>

		<div class="row">
			<div class="col-md-6 col-md-offset-3">
				<div class="panel panel-primary" id="mainPanel">
					<div class="panel-heading">Pentago</div>
					<div class="panel-body gameCanvas" style="text-align: center;">
						<h3 id="userLog">Welcome to the game</h3>
					</div>

					<div class="gameCanvas" id="d3Canvas"></div>

				</div>
			</div>
		</div>

		<!-- About Player -->
		<div class="modal fade" id="endGameModal" tabindex="-1" role="dialog" aria-labelledby="endGameModalLabel">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="modal-title" id="endGameModalLabel">End of the game</h4>
					</div>
					<div class="modal-body">
						<span id="gameResultSpan"></span>
					</div>
					<div class="modal-footer">
						<a id="endGameBtn" class="btn btn-default" href="Default.aspx">Close</a>
					</div>
				</div>
			</div>
		</div>
	</form>
</body>
</html>
