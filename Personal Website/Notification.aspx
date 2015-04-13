﻿<%@ Page Title="Notification" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="Personal_Website.Notification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<script>
		$(document).ready(function () {
			// Handler for .ready() called.
			window.setTimeout(function () {
				location.href = "Default.aspx";
			}, 5000);

			CreateTimer("counter", 5);
		});

		var Timer;
		var TotalSeconds;


		function CreateTimer(TimerID, Time) {
			Timer = document.getElementById(TimerID);
			TotalSeconds = Time;

			UpdateTimer()
			window.setTimeout("Tick()", 1000);
		}

		function Tick() {
			TotalSeconds -= 1;
			UpdateTimer()
			window.setTimeout("Tick()", 1000);
		}

		function UpdateTimer() {
			Timer.innerHTML = TotalSeconds;
		}

	</script>

	<h2>
		Dear <%
			Response.Write(Request.QueryString["name"]);	
		 %>,
	</h2>
	<h3 class="success">
		<%
			Response.Write(Request.QueryString["message"]);	
		 %>
	</h3>
	<a href="Default.aspx">Go home...</a> or you will be redirected there in <span id="counter">5</span> seconds...
</asp:Content>