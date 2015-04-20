<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignInRequest.ascx.cs" Inherits="Personal_Website.General_User_Controls.SignInRequest" %>

<div id="alertBanner" runat="server" class="alert alert-info" role="alert">

	<h3> Dear visitor! </h3>
	
	<h4>
		I am so glad to see you here, on my website! <br />
		I would really want to share all the information with you but I cannot leave it public... :( <br />
		Nevertheless, I just need your name and email to grant you full access. I am so interested in talking to you! <br /> 
	</h4>
	<a id="returnBtn" class="btn btn-primary" runat="server" href="~/Account/Contact.aspx">Kindly leave some contact information</a>

</div>