<%@ Page Title="IQP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.IQP.Default" %>
<%@ Register TagPrefix="My" TagName="Pagination" Src="~/General User Controls/Pagination.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<My:Pagination ID="Pagination" runat="server" URL="~/Projects/IQP/Default.aspx" pageNum="8" defaultActive="1" displayNum="10" ></My:Pagination>

</asp:Content>
