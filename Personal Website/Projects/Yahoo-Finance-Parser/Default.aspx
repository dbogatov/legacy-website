<%@ Page Title="Fiannce Parser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Yahoo_Finance_Parser.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script src="financeParser.js"></script>

	<asp:Label runat="server" ID="Strike"></asp:Label>
	<asp:Label runat="server" ID="FinanceContent"></asp:Label>

</asp:Content>
