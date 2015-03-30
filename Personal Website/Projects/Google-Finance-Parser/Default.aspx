<%@ Page Title="Fiannce Parser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Yahoo_Finance_Parser.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="financeParser.js"></script>

	<asp:Label runat="server" ID="MyStrike"></asp:Label>
	<asp:Label runat="server" ID="MySymbol"></asp:Label>
	<asp:Label runat="server" ID="FinanceContent"></asp:Label>

	<h2>Welcome to Google Option Chain Parser</h2>
	<h4>Here you may instantly get a bid and ask prices of the option for a given symbol and strike price.</h4>

	<div class="form-inline">
		<div class="form-group">
			<label for="symbol">Symbol</label>
			<asp:TextBox runat="server" CssClass="form-control" ID="Symbol" placeholder="eq. JNJ"></asp:TextBox>
		</div>
		<div class="form-group">
			<label for="myStrike">Strike</label>
			<asp:TextBox runat="server" CssClass="form-control" ID="Strike" placeholder="eq. 94.5"></asp:TextBox>
		</div>
		<button type="submit" class="btn btn-primary" style="vertical-align:bottom">Get prices!</button>
	</div>

	<h2>
		<span class="label label-success" id="response"></span>
	</h2>	

</asp:Content>
