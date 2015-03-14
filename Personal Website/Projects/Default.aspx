<%@ Page Title="My Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="Projects.js"></script>

	<div class="row">



		<div class="col-md-12 row-centered">
			<h3><%: Title %></h3>
			<ul class="nav nav-pills nav-justified" id="myTab">
				<li role="presentation" class="active"><a href="#all">All</a></li>
				<li role="presentation"><a href="#large">Large</a></li>
				<li role="presentation"><a href="#university">University</a></li>
			</ul>
		</div>

		<hr />

		<% =GetProjects() %>
		
	</div>
	

</asp:Content>
