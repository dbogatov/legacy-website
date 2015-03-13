<%@ Page Title="My Grades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Grades.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<h2><%: Title %>.</h2>

	<asp:PlaceHolder id="gradesTable" runat="server"/>

</asp:Content>
