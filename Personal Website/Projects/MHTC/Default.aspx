<%@ Page Title="MHTC" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.MHTC.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2><%: Title %>.</h2>
	<div class="row">
		<div class="col-md-6">

			<div class="form-horizontal">
				<h4>API Request</h4>
				<hr />

				<div class="form-group">
					<asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Query</asp:Label>
					<div class="col-md-10">
						<asp:TextBox Rows="3" TextMode="MultiLine" ID="UserName" runat="server" CssClass="form-control requestUri" Text="http://mhtc-vm1.cs.wpi.edu:8080/dev-open-api/data?apiKey={yourKey}" placeholder="http://mhtc-vm1.cs.wpi.edu:8080/dev-open-api/data?apiKey={yourKey}" />
					</div>
				</div>

				<div class="form-group">
					<div class="col-md-offset-2 col-md-10">
						<asp:Button runat="server" Text="Send request" CssClass="btn btn-default requestBtn" />
					</div>
				</div>

			</div>

		</div>

		<div class="col-md-6">

			<h4>API Response</h4>
			<hr />
			<div id="apiResponse">
			</div>

		</div>
	</div>

	<script>
		$(document).ready(function () {
			$(".requestBtn").click(function () {
				var uri = $(".requestUri").val();
				$.get(uri)
					.done(function (returned) {
						alert("second success");
					})
					.fail(function (jqXHR, textStatus, errorThrown) {
						console.log(textStatus, errorThrown);
					});
			});
		});
	</script>

</asp:Content>
