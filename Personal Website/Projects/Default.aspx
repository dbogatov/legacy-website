<%@ Page Title="My Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Default" %>
<%@ Register TagPrefix="My" TagName="ProjectThubnail" Src="~/Projects/ProjectThubnail.ascx" %>
<%@ Register TagPrefix="My" TagName="SpecializedSignInRequest" Src="~/General User Controls/SpecializedSignInRequest.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="Projects.js"></script>
	
	<div class="row">

		<div class="col-md-12 row-centered">
			<h3><%: Title %></h3>

			<My:SpecializedSignInRequest runat="server" Text="Please, tell me who you are to enable links to projects and their repositories."></My:SpecializedSignInRequest>

			<div style="padding-bottom:20px">
				<ul class="col-md-12 nav nav-pills nav-justified" id="myTab">
					<li role="presentation" class="active"><a href="#all">All</a></li>
					<%					
						foreach (var tag in new Personal_Website.Projects.ProjectsDataContext().Tags ) {
							Response.Write("<li role='presentation'><a href='#" + tag.tagName.ToString().ToLower() + "'>" + tag.tagName + "</a></li>");
						}
						 %>
				</ul>
			</div>
			
			<div class="form-inline">
				<div class="form-group has-success" id="searchDiv">
					<div class="input-group" style="width: 300px;">
						<div class="input-group-addon"><span class="glyphicon glyphicon-search"></span></div>
						<input type="text" class="form-control" placeholder="eq. Minesweeper..." id="searchBar" />
					</div>
				</div>
				<div class="form-group">
					<input type="button" class="form-control btn btn-primary" value="Clear search" id="clearSearch" style="width: 100px;"/>
				</div>
				<div class="checkbox" style="padding-left:50px">
					<label>
						<strong>Title:</strong>
						<input type="checkbox" checked id="titleChbx" />
					</label>
				</div>
				<div class="checkbox" style="padding-left:50px">
					<label>
						<strong>Description:</strong>
						<input type="checkbox" checked id="descriptionChbx" />
					</label>
				</div>
			</div>

		</div>
		<hr />

		<div class="row-centered" id="emptySearchPlaceholder" hidden>
			<h3>
				No results match your search string...
			</h3>
		</div>

		<div id="projectsDiv" runat="server"></div>
		
	</div>
	

</asp:Content>

