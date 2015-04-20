<%@ Page Title="Author" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compact.aspx.cs" Inherits="Personal_Website.Compact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<h3><%: Title %>.</h3>



	<div class="row">
		<div class="col-md-12 row-centered" style="padding-bottom:10px">
			<span id="nameSpan">Dmytro Bogatov</span> <br />
			<span id="contactSpan">100 Institute Road, Box 3397 • Worcester, MA 01609 • 508-667-7440 • <a href="mailto:dbogatov@wpi.edu" target="_top">dbogatov@wpi.edu</a></span>
		</div>
		<div class="col-md-12">
			<div class="col-md-3 row-centered">
				<asp:Image runat="server" ImageUrl="~/Images/DmytroBogatov.jpg" Width="210px" CssClass="img-rounded" style="padding-bottom:5px" /><br />
				Worcester Polytechnic Institute <br />
				Computer Science, Class of 2017
			</div>
			<div class="resume col-md-9">
				<!-- #include file ="../Includes/Resume.html" -->
			</div>
		</div>
	</div>

</asp:Content>