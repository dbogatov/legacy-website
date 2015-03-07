<%@ Page Title="Author" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Author.aspx.cs" Inherits="Personal_Website.Author" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<h2><%: Title %>.</h2>

	<table class="AuthorTable">
		<tr>
			<td colspan="2" id="nameCellAT">
				<span id="nameSpan">Dmytro Bogatov</span> <br />
				<span id="contactSpan">100 Institute Road, Box 3397 • Worcester, MA 01609 • 508-667-7440 • <a href="mailto:dbogatov@wpi.edu" target="_top">dbogatov@wpi.edu</a></span>
			</td>
		</tr>
		<tr id="imageRowAT">
			<td id="imageCellAT">
				<asp:Image runat="server" ImageUrl="~/Images/DmytroBogatov.jpg" Width="300px" />
			</td>
			<td id="resumeCellAT" rowspan="3">
				<!-- #include file ="Includes/Resume.html" -->
			</td>
		</tr>
		<tr>
			<td id="infoCellAT">
				Worcester Polytechnic Institute <br />
				Computer Science, Class of 2017
			</td>
		</tr>
		<tr>
			<td id="blankCellAT">
				
			</td>
		</tr>
	</table>

</asp:Content>