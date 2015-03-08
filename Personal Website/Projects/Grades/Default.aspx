<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Grades.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<div class="GradeTable">
		<table id="gradeTable">
			<thead>
				<tr>
					<th>Term</th>
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

</asp:Content>
