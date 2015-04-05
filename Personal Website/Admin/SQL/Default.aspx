<%@ Page Title="SQL Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Admin.SQL.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%: Title %></h2>

	<div class="form-horizontal">

		<div class="form-group">
			<asp:Label runat="server" AssociatedControlID="ConnString" CssClass="col-md-2 control-label">Connection String</asp:Label>
			<div class="col-md-10">
				<asp:TextBox runat="server" ID="ConnString" CssClass="form-control" placeholder="" />
			</div>
		</div>

		<div class="form-group">
			<label for="symbol">SQL Query</label>
			<asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="SQLQuery" placeholder="SELECT * FROM Projects;"></asp:TextBox>
		</div>
		<button type="submit" class="btn btn-primary" style="vertical-align: bottom">Submit query</button>
	</div>

	<h2>
		<asp:Label runat="server" CssClass="label label-success" ID="SQLResponse"></asp:Label>
	</h2>
	<asp:Label runat="server" ID="Message"></asp:Label>

</asp:Content>
