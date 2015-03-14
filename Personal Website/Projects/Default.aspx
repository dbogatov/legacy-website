<%@ Page Title="My Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="Projects.js"></script>

	<div class="row">

		<div class="col-md-12 row-centered">
			<h3><%: Title %></h3>
			<ul class="nav nav-pills nav-justified" id="myTab">
				<li role="presentation" class="active"><a href="#all">All</a></li>
				<%					
					foreach (var tag in new Personal_Website.Projects.ProjectsDataContext().Tags ) {
						Response.Write("<li role='presentation'><a href='#" + tag.tagName.ToString().ToLower() + "'>" + tag.tagName + "</a></li>");
					}
					 %>
			</ul>
		</div>

		<hr />

		<% =GetProjects() %>
		
	</div>
	

</asp:Content>
