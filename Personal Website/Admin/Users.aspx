<%@ Page Title="Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Personal_Website.Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2><%: Title %>.</h2>

	<table id="usersTable" class="table table-striped table-bordered table-hover">
		<thead>
			<tr>
				<th style="width:15%">Name</th>
				<th style="width:15%">Email</th>
				<th style="width:60%">Comment</th>
				<th style="width:10%">Language</th>
				<th style="width:10%">Reg time</th>
			</tr>
		</thead>
		<tbody id="body" runat="server">

		</tbody>
	</table>

</asp:Content>
