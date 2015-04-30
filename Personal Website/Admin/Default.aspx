<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%: Title %>.</h2>

	<h3>As an admin you can see and manage:</h3>

	<div class="row">
		<div class="col-md-4">
			<div class="jumbotron">
				<h1>Feedback</h1>
				<p>Look up all feedback messages</p>
				<p><a class="btn btn-primary btn-lg" href="Feedback.aspx" role="button">Manage</a></p>
			</div>
		</div>

		<div class="col-md-4">
			<div class="jumbotron">
				<h1>Users</h1>
				<p>Look up all users who contacted you</p>
				<p><a class="btn btn-primary btn-lg" href="Users.aspx" role="button">Manage</a></p>
			</div>
		</div>
	</div>

</asp:Content>
