<%@ Page Title="Feedback" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="Personal_Website.Admin.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%: Title %>.</h2>

	<table id="feedbackTable" class="table table-striped table-bordered table-hover">
		<thead>
			<tr>
				<th style="width: 20%">Email</th>
				<th style="width: 25%">Subject</th>
				<th style="width: 55%">Body</th>
			</tr>
		</thead>
		<tbody id="body" runat="server">
		</tbody>
	</table>

</asp:Content>
