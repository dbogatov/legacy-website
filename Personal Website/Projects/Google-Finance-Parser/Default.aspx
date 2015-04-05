<%@ Page Title="Fiannce Parser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Yahoo_Finance_Parser.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<!--<script src="financeParser.js"></script> -->

	<asp:Label runat="server" ID="MyStrike"></asp:Label>
	<asp:Label runat="server" ID="MySymbol"></asp:Label>
	<asp:Label runat="server" ID="FinanceContent"></asp:Label>

	<h2>Welcome to Google Option Chain Parser</h2>
	<h4>Here you may instantly get a bid and ask prices of the option for a given symbol and strike price.</h4>

	<div class="form-inline symbolRow row" id="row1">
		<div class="form-group col-md-2">
			<label for="symbol">Symbol</label>
			<asp:TextBox runat="server" CssClass="form-control symbol" ID="Symbol" placeholder="eq. JNJ"></asp:TextBox>
		</div>
		<div class="form-group col-md-2">
			<label for="myStrike">Strike</label>
			<asp:TextBox runat="server" CssClass="form-control strike" ID="Strike" placeholder="eq. 97.5"></asp:TextBox>
		</div>
	</div>
	<br />

	<button class="btn btn-primary col-md-4" style="vertical-align:bottom">Get prices!</button>
	
	<br />
	<br />

	<table class="table table-bordered table-hover">
		<thead>
			<tr>
				<th>Symbol</th>
				<th>Strike</th>
				<th>Bid</th>
				<th>Ask</th>
			</tr>
		</thead>
		<tbody id="resultingTable" runat="server">

		</tbody>
	</table>	

</asp:Content>
