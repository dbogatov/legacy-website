<%@ Page Title="Banker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Banker.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


	<div class="row">
		<div class="col-md-12 row-centered" style="padding-bottom:10px; padding-top:10px">
			<h2>WPI Calendar Event Creator</h2>
			<h3>By Dmytro Bogatov</h3>
		</div>


		<div class="col-md-3">
			<div class="panel panel-default">
				<div class="panel-heading">
					<h3 class="panel-title">App Screenshots</h3>
				</div>
				<div class="panel-body">
					<div id="carousel-example-generic" class="carousel slide" data-ride="carousel">

						<!-- Wrapper for slides -->
						<div class="carousel-inner" role="listbox">
							<div class="item active">
								<img src="../../Images/CalendarEventCreator/tutorial1.png" alt="App Screenshot">
							</div>
							<div class="item">
								<img src="../../Images/CalendarEventCreator/tutorial3.png" alt="App Screenshot">
							</div>
							<div class="item">
								<img src="../../Images/CalendarEventCreator/tutorial5.png" alt="App Screenshot">
							</div>
							<div class="item">
								<img src="../../Images/CalendarEventCreator/tutorial6.png" alt="App Screenshot">
							</div>
							<div class="item">
								<img src="../../Images/CalendarEventCreator/tutorial7.png" alt="App Screenshot">
							</div>
						</div>

						<!-- Controls -->
						<a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
							<span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
							<span class="sr-only">Previous</span>
						</a>
						<a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
							<span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
							<span class="sr-only">Next</span>
						</a>
					</div>
				</div>
			</div>
		</div>


		<div class="col-md-6">
			<h3>Description</h3>

			<br />
			
			<p style="text-align:justify">WPI Calendar Event Creator is a simple iPhone application that allows WPI students to create appointment events in their standard calendars. Application has a local databse of faculty members including their names, offices, emails and photos. Whith this app it takes just a few seconds to find a professor you want to make an appointment with, everything else is done for you. Faculty member will be notified by email with the event attached, if you would like.</p>
			<br />
			Features:
			<ul>
				<li> Light and fast app </li>
				<li> Local database of faculty members' information. No Internet needed. </li>
				<li> Favorite professors view </li>
				<li> Preview of an event to be created </li>
				<li> Check for time conflicts </li>
				<li> Generates and sends an email with the event (on demand) </li>
			</ul>

		</div>
		<div class="col-md-3">
			<div class="thumbnail">
				<img src="../../Images/WPICalendarEventCreator.png" alt="App Picture" width="200" class="img-rounded">
				<div class="caption">
					<h3>Free</h3>
					<p> This app is designed for iPhone. </p>
					<table>
						<tbody>
							<tr>
								<td style="width:80px;">
									<strong>Category</strong>
								</td>
								<td>
									Utilities
								</td>
							</tr>
							<tr>
								<td>
									<strong>Updated</strong>
								</td>
								<td>
									Never
								</td>
							</tr>
							<tr>
								<td>
									<strong>Version</strong>
								</td>
								<td>
									1.0
								</td>
							</tr>
							<tr>
								<td>
									<strong>Size</strong>
								</td>
								<td>
									< 15 MB
								</td>
							</tr>
							<tr>
								<td>
									<strong>Language</strong>
								</td>
								<td>
									English
								</td>
							</tr>
							<tr>
								<td>
									<strong>Seller</strong>
								</td>
								<td>
									Dmytro Bogatov
								</td>
							</tr>
						</tbody>
					</table>
					<hr />
					<p style="text-align:center"><a href="#" target="_blank" class="btn btn-primary disabled" role="button">Not in iTunes</a> <a href='#' data-toggle='tooltip' data-placement='top' title='WPI Marketing office did not allow me to post this app. They did not expalin me the reasons.'>why?</a> </p>
					<p> © 2015 Dmytro Bogatov </p>
			  </div>
			</div>
		</div>
		
		

	</div>


</asp:Content>
