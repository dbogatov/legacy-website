<%@ Page Title="My Grades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Grades.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<h2><%: Title %>.</h2>

	<div class="GradeTable">
		<table id="gradeTable">
			<thead>
				<tr id="gradesHeader">
					<th class="gradesTerm">Term</th>
					<th>Year</th>
					<th>Title</th>
					<th>CourseID</th>
					<th>Grade %</th>
					<th>Grade</th>
					<th>Status</th>
				</tr>
			</thead>
			<tbody>
				<%= GetGrades() %>
			</tbody>

		</table>
	</div>

	<script src="../../Scripts/jquery.tablesorter.min.js"></script>
	<script src="GradesScript.js"></script>

</asp:Content>
