<%@ Page Title="My Grades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Grades.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%@ Register TagPrefix="My" TagName="GradesTable" Src="~/Projects/Grades/GradesTable.ascx" %>
<%@ Register TagPrefix="My" TagName="SpecializedSignInRequest" Src="~/General User Controls/SpecializedSignInRequest.ascx" %>

	<h2><%: Title %><span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>> (GPA <%= CSIQP.Round( (decimal)(CSIQP.sumPoints + Math.sumPoints + Science.sumPoints + SocScinces.sumPoints + Breadth.sumPoints + Depth.sumPoints + Seminar.sumPoints + PE.sumPoints + IQP.sumPoints + FreeElect.sumPoints) /(CSIQP.completedNumber + Math.completedNumber + Science.completedNumber + SocScinces.completedNumber + Breadth.completedNumber + Depth.completedNumber + Seminar.completedNumber + PE.completedNumber + IQP.completedNumber + FreeElect.completedNumber), 2) %>)</span>.</h2>

	<My:SpecializedSignInRequest runat="server" Text="Please, tell me who you are to see all classes and grades."></My:SpecializedSignInRequest>

	<div class="row">
		<div class="col-md-6">

			<h3>Major Requirements <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% = CSIQP.Round( (decimal)(CSIQP.sumPoints + Math.sumPoints + Science.sumPoints) /(CSIQP.completedNumber + Math.completedNumber + Science.completedNumber), 2) %>)</span></h3>
			<hr />

			<h4 class="text-center">CS Classes + MQP <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =CSIQP.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="CS Classes + MQP" ID="CSIQP"></My:GradesTable>

			<h4 class="text-center">Math <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =Math.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Math"  ID="Math"></My:GradesTable>

			<h4 class="text-center">Science <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =Science.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Science" ID="Science"></My:GradesTable>
			
			<h3>Social Requirements <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =SocScinces.GPA %>)</span></h3>
			<hr />

			<h4 class="text-center">Social Sciences <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =SocScinces.GPA %>)</h4>
			<My:GradesTable runat="server" Requirement="Social Sciences" ID="SocScinces"></My:GradesTable>

		</div>
		<div class="col-md-6">

			<h3>Humanities Requirements <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% = Breadth.Round( (decimal)(Breadth.sumPoints + Depth.sumPoints + Seminar.sumPoints) /(Breadth.completedNumber + Depth.completedNumber + Seminar.completedNumber), 2)  %>)</span></h3>
			<hr />

			<h4 class="text-center">Breadth <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =Breadth.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Breadth" ID="Breadth"></My:GradesTable>

			<h4 class="text-center">Depth <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =Depth.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Depth" ID="Depth"></My:GradesTable>

			<h4 class="text-center">Seminar <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =Seminar.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Seminar" ID="Seminar"></My:GradesTable>

			<h3>PE Requirements <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =PE.GPA %>)</span></h3>
			<hr />

			<h4 class="text-center">Physical Education <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =PE.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Physical Education" ID="PE"></My:GradesTable>

			<h3>IQP Requirements <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =IQP.GPA %>)</span></h3>
			<hr />

			<h4 class="text-center">IQP <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =IQP.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="IQP" ID="IQP"></My:GradesTable>

			<h3>Other Requirements <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =FreeElect.GPA %>)</span></h3>
			<hr />

			<h4 class="text-center">Free Elective <span <%# Personal_Website.Models.Authentication.grantAccess() ? "" : "hidden" %>>(GPA <% =FreeElect.GPA %>)</span></h4>
			<My:GradesTable runat="server" Requirement="Free Elective" ID="FreeElect"></My:GradesTable>

		</div>
	</div>

	<script src="../../Scripts/jquery.tablesorter.min.js"></script>
	<script src="GradesScript.js"></script>

</asp:Content>
