<%@ Page Title="Fiannce Parser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Yahoo_Finance_Parser.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<h2>Welcome to Google Option Chain Parser</h2>
	<h4>Here you may instantly get a bid and ask prices of the option for a given symbol and strike price.</h4>

	<div class="row">
	
		<div class="col-md-5">
			<div class="form-inline">
				<div class="form-group">
					<label for="symbol">Symbol</label>
					<asp:TextBox runat="server" CssClass="form-control symbol" ID="Symbol" placeholder="eq. JNJ"></asp:TextBox>
				</div>
				<div class="form-group">
					<label for="myStrike">Strike</label>
					<asp:TextBox runat="server" CssClass="form-control strike" ID="Strike" placeholder="eq. 97.5"></asp:TextBox>
				</div>
			</div>
	
			<div class="" style="margin-top:30px">
				<button class="btn btn-primary col-md-4 btn-block">Get prices!</button>
				<a class="btn btn-info col-md-2 btn-block" role="button" href="Default.aspx?symbol={UBS+JBLU+JNJ+JPM+JPM+KO+KO+HP+HP}&strike={20+20+103+62+62.5+41.5+42+70+75}">Example</a>
				<a class="btn btn-warning col-md-2 btn-block" id="lastInput" <%= (Response.Cookies["parserCookie"]==null) ? "hidden" : "" %> href="<%= (Response.Cookies["parserCookie"]!=null) ? Response.Cookies["parserCookie"].Value : "#" %>">Last input</a>
			</div>
		</div>

		<div class="col-md-7">
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
		</div>

	</div>	

</asp:Content>
