<%@ Page Title="Contact" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Personal_Website.Account.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
	<div class="row">
        <div class="col-md-8">

				<div class="form-horizontal">
					<h4>Contact developer.</h4>
					<hr />
					<asp:ValidationSummary runat="server" CssClass="text-danger" />

					<div class="form-group">
						<asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">Your name</asp:Label>
						<div class="col-md-10">
							<asp:TextBox runat="server" ID="UserName" CssClass="form-control" placeholder="eq. John Smith" />
							<asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
								CssClass="text-danger" ErrorMessage="The name field is required." />
						</div>
					</div>

					<div class="form-group">
						<asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Your email</asp:Label>
						<div class="col-md-10">
							<asp:TextBox runat="server" ID="Email" CssClass="form-control" placeholder="eq. admin@dbogatov.org" />
							<asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
								CssClass="text-danger" ErrorMessage="The email field is required." />
							<br />
							<asp:RegularExpressionValidator ID="regexEmailValid" runat="server"
								ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
								ControlToValidate="Email" ErrorMessage="Invalid Email Format" CssClass="text-danger">
							</asp:RegularExpressionValidator>
						</div>
					</div>
        
					<div class="form-group">
						<asp:Label runat="server" AssociatedControlID="Comment" CssClass="col-md-2 control-label">Your message</asp:Label>
						<div class="col-md-5">
							<asp:TextBox runat="server" ID="Comment" TextMode="MultiLine" CssClass="form-control" Rows="5" placeholder="Who are you and why do you contact me. Your answer is appreciated." />
						</div>
					</div>
        
					<div class="form-group">
						<asp:Label runat="server" AssociatedControlID="Language" CssClass="col-md-2 control-label">Language</asp:Label>
						<div class="col-md-5">
							<asp:DropDownList runat="server" ID="Language" CssClass="form-control">
								<asp:ListItem>English</asp:ListItem>
								<asp:ListItem>Ukrainian</asp:ListItem>
								<asp:ListItem>Russian</asp:ListItem>
							</asp:DropDownList>
						</div>
					</div>

					<div class="form-group">
						<div class="col-md-offset-2 col-md-10">
							<asp:Button runat="server" OnClick="SendInfo_Click" Text="Contact developer" CssClass="btn btn-default" />
						</div>
					</div>

				</div>

		</div>

		<div class="col-md-4">
            
			<h4>What's next?
			</h4>
			<hr />
			<div>
                <p style="text-align:justify">Immediatelly after you send your contact information you will be given access to all information on the website. You will also receive an email with a link allowing you to access the website without sending information again. </p>
            </div>
			
        </div>
    </div>

</asp:Content>
