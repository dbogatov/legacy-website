<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GradesTable.ascx.cs" Inherits="Personal_Website.Projects.Grades.GradesTable" %>

<table id="gradeTable" class="table table-striped table-bordered table-hover">
	<thead>
		<tr>
			<th>Term</th>
			<th>Year</th>
			<th>Title</th>
			<th id="gradeHeader" runat="server">Grade</th>
			<th>Status</th>
		</tr>
	</thead>
	<tbody id="body" runat="server">

	</tbody>
</table>