<%@ Page Title="My Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Default" %>
<%@ Register TagPrefix="My" TagName="ProjectThubnail" Src="~/Projects/ProjectThubnail.ascx" %>
<%@ Register TagPrefix="My" TagName="SpecializedSignInRequest" Src="~/General User Controls/SpecializedSignInRequest.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="Projects.js"></script>
	
	<div class="row">

		<div class="col-md-12 row-centered">
			<h3><%: Title %></h3>

			<My:SpecializedSignInRequest runat="server" Text="Please, tell me who you are to enable links to projects and their repositories."></My:SpecializedSignInRequest>

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

		<div id="projectsDiv" runat="server"></div>
		
	</div>
	

</asp:Content>
