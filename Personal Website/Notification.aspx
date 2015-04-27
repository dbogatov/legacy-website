<%@ Page Title="Notification" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="Personal_Website.Notification" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
	<link href="Content/MainStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
	<script>

		function getParameterByName(name) {
			name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
			var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
				results = regex.exec(location.search);
			return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
		}

		$(document).ready(function () {
			// Handler for .ready() called.
			window.setTimeout(function () {
				var ref = getParameterByName('returnUrl');
				location.href = ref == "" ? location.href = location.origin : location.href = ref;
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

	<script src="Scripts/bootstrap-progressbar.js"></script>
	
	<script>
		$(document).ready(function() {
			$('.progress-bar').progressbar();
		});
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
	<a href="Default.aspx">Go home...</a> or you will be redirected where you were in <span id="counter">5</span> seconds...

	<div class="progress progress-striped">
		<div class="progress-bar five-sec-ease-in-out" role="progressbar" data-transitiongoal="100"></div>
	</div>
</asp:Content>
