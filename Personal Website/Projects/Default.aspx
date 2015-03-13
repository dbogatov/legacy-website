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

		<div class="col-sm-6 col-md-4 project-thumbnail project-large project-university" style="padding-top:10px">
			<div class="thumbnail">
				<img src="../Images/DmytroBogatov.jpg" alt="Dmytro">
				<div class="caption">
					<h3>My Project</h3>
					<p>Here is the long description! Here is the long description! Here is the long description! Here is the long description!</p>
					<p><a href="#" class="btn btn-primary" role="button">Try it!</a> <a href="#" class="btn btn-default" role="button">View source</a>(large, university)</p>
				</div>
			</div>
		</div>

		<div class="col-sm-6 col-md-4 project-thumbnail project-university" style="padding-top:10px">
			<div class="thumbnail">
				<img src="../Images/DmytroBogatov.jpg" alt="Dmytro">
				<div class="caption">
					<h3>My Project</h3>
					<p>Here is the long description! Here is the long description! Here is the long description! Here is the long description!</p>
					<p><a href="#" class="btn btn-primary" role="button">Try it!</a> <a href="#" class="btn btn-default" role="button">View source</a>(university)</p>
				</div>
			</div>
		</div>

		<div class="col-sm-6 col-md-4 project-thumbnail project-large" style="padding-top:10px">
			<div class="thumbnail">
				<img src="../Images/DmytroBogatov.jpg" alt="Dmytro">
				<div class="caption">
					<h3>My Project</h3>
					<p>Here is the long description! Here is the long description! Here is the long description! Here is the long description!</p>
					<p><a href="#" class="btn btn-primary" role="button">Try it!</a> <a href="#" class="btn btn-default" role="button">View source</a>(large)</p>
				</div>
			</div>
		</div>

		<div class="col-sm-6 col-md-4 project-thumbnail" style="padding-top:10px">
			<div class="thumbnail">
				<img src="../Images/DmytroBogatov.jpg" alt="Dmytro">
				<div class="caption">
					<h3>My Project</h3>
					<p>Here is the long description! Here is the long description! Here is the long description! Here is the long description!</p>
					<p><a href="#" class="btn btn-primary" role="button">Try it!</a> <a href="#" class="btn btn-default" role="button">View source</a>()</p>
				</div>
			</div>
		</div>
		
	</div>
	

</asp:Content>
