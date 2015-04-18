<%@ Page Title="My Grades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Grades.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%@ Register TagPrefix="My" TagName="GradesTable" Src="~/Projects/Grades/GradesTable.ascx" %>

	<h2><%: Title %>.</h2>

	<div class="row">
		<div class="col-md-6">

			<h3>Major Requirements</h3>
			<hr />

			<h4 class="text-center">CS Classes + MQP</h4>
			<My:GradesTable runat="server" Requirement="CS Classes + MQP"></My:GradesTable>

			<h4 class="text-center">Math</h4>
			<My:GradesTable runat="server" Requirement="Math"></My:GradesTable>

			<h4 class="text-center">Science</h4>
			<My:GradesTable runat="server" Requirement="Science"></My:GradesTable>
			
			<h3>Social Requirements</h3>
			<hr />

			<h4 class="text-center">Social Sciences</h4>
			<My:GradesTable runat="server" Requirement="Social Sciences"></My:GradesTable>

		</div>
		<div class="col-md-6">

			<h3>Humanities Requirements</h3>
			<hr />

			<h4 class="text-center">Breadth</h4>
			<My:GradesTable runat="server" Requirement="Breadth"></My:GradesTable>

			<h4 class="text-center">Depth</h4>
			<My:GradesTable runat="server" Requirement="Depth"></My:GradesTable>

			<h4 class="text-center">Seminar</h4>
			<My:GradesTable runat="server" Requirement="Seminar"></My:GradesTable>

			<h3>PE Requirements</h3>
			<hr />

			<h4 class="text-center">Physical Education</h4>
			<My:GradesTable runat="server" Requirement="Physical Education"></My:GradesTable>

			<h3>IQP Requirements</h3>
			<hr />

			<h4 class="text-center">IQP</h4>
			<My:GradesTable runat="server" Requirement="IQP"></My:GradesTable>

			<h3>Other Requirements</h3>
			<hr />

			<h4 class="text-center">Free Elective</h4>
			<My:GradesTable runat="server" Requirement="Free Elective"></My:GradesTable>

		</div>
	</div>

	<!-- <asp:PlaceHolder id="gradesTable" runat="server"/> -->

	<script src="../../Scripts/jquery.tablesorter.min.js"></script>
	<script src="GradesScript.js"></script>

</asp:Content>
