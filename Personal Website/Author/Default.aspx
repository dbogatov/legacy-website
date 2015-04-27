<%@ Page Title="Author" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Author.Default" %>
<%@ Register TagPrefix="My" TagName="SpecializedSignInRequest" Src="~/General User Controls/SpecializedSignInRequest.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
	<link href="Author.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



	<h3><%: Title %>.</h3>
	<h4 id="downloads" runat="server">
		<a href="Compact.aspx">See compact version</a><br />
		<a href="Resume.pdf">Download PDF</a>
	</h4>

	<My:SpecializedSignInRequest runat="server" Text="Please, tell me who you are to view full resume - my working experience and completed projects."></My:SpecializedSignInRequest>

	<div id="mainContent">

		<!-- Contact -->

		<div class="row">

			<div class="col-md-7 col-md-offset-1">
				<div id="name" class="col-md-12 row-centered">Dmytro Bogatov</div>
			
				<div class="col-md-12 row-centered" id="contact">
					<span class="glyphicon glyphicon-envelope icon" aria-hidden="true"></span><a href="mailto:dbogatov@wpi.edu" class="data">dbogatov@wpi.edu</a>
					<span class="glyphicon glyphicon-earphone icon" aria-hidden="true"></span><span class="data">508-667-7440</span>
					<span class="glyphicon glyphicon-map-marker icon" aria-hidden="true"></span><span class="data">Worcester, MA</span>
					<br />
					<span class="glyphicon glyphicon-globe icon" aria-hidden="true"></span><a href="http://dbogatov.org" class="data">dbogatov.org</a>
				</div>

			</div>
			<div class="col-md-3 col-md-offset-0 row-centered">
				<asp:Image runat="server" ImageUrl="~/Images/DmytroBogatov.jpg" Width="210px" CssClass="img-rounded" style="padding-bottom:5px" />
			</div>

		</div>
	
		<!-- Statement -->

		<div class="row">

			<div class="col-md-10 col-md-offset-1 row-centered" id="statement">
				<hr style="opacity: 1;">
				<h2>Summer Internship in Computer Science</h2>
				<hr style="opacity: 1;">
			</div>

		</div>

		<!-- Education -->

		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="education">
			
				<h2 class="row-centered section">Education</h2>
				<span id="degree">Bachelor of Science in Computer Science</span>
				<span class="large-description">Worcester Polytechnic Institute • Worcester, MA • 2017 • <strong>3.95 GPA</strong></span>
				<div class="col-md-10 col-md-offset-1 pull-left">
					<span class="mid-data">Related Coursework</span><br />
					<div class="description">
						Software Engineering • Database Systems • Systems Programming • Object-Oriented Design Concepts • Algorithms* <br />
						(* To be completed by May 2015)
					</div>

				</div>

			</div>
		</div>

		<!-- Independent Projects -->

		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="independentProjects" runat="server">
			
				<hr style="opacity: 1;">
				<h2 class="row-centered section">Independent Projects</h2>
			
				<div class="col-md-10 col-md-offset-1 pull-left job">
					<span class="bold-data">Banker Game Assistant</span><br />
					<span class="mid-data">Worcester Polytechnic Institute • Worcester, MA • January 2015</span><br />
					<div class="description">
						Developed an iOS application, Banker Game Assistant, which serves as an useful tool when playing board games involving cash like Monopoly®. Led application from the idea to the AppStore. 
						<p>Link: <a target="_blank" href="../Projects/Banker/Default.aspx">http://dbogatov.org/Projects/Banker/</a></p>
					</div>
				</div>

				<div class="col-md-10 col-md-offset-1 pull-left job next-job">
					<span class="bold-data">Minesweeper Web Application</span><br />
					<span class="mid-data">Kiev • Ukraine • June - August 2014</span><br />
					<div class="description">
						Worked with a partner to develop a simple web-based game, Minesweeper. Programmed the server side of the application.
						<p>Link: <a target="_blank" href="../Projects/Minesweeper/Default.aspx">http://dbogatov.org/Projects/Minesweeper/</a></p>
					</div>
				</div>

				<div class="col-md-10 col-md-offset-1 pull-left job next-job">
					<span class="bold-data">WPI Calendar Event Creator</span><br />
					<span class="mid-data">Worcester Polytechnic Institute • Worcester, MA • September - December 2013</span><br />
					<div class="description">
						Developed an iOS application, WPI Calendar Event Creator, which provides an easy and fast way to create university-related events in a calendar. 
						<p>Link: <a target="_blank" href="../Projects/Calendar-Event-Creator/Default.aspx">http://dbogatov.org/Projects/Calendar-Event-Creator/</a></p>
					</div>
				</div>

			</div>
		</div>

		<!-- Academic Projects -->
		
		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="academicProjects" runat="server">
			
				<hr style="opacity: 1;">
				<h2 class="row-centered section">Academic Projects</h2>
			
				<div class="col-md-10 col-md-offset-1 pull-left job">
					<span class="bold-data">Interactive Qualifying Project (IQP) • Trading Systems and Investment</span><br />
					<span class="mid-data">Worcester Polytechnic Institute • Worcester, MA • September 2014 - Present</span><br />
					<div class="description">
						Led an interdisciplinary four member team designing a trading system working with equities, currencies, commodities and options.
					</div>
				</div>

				<div class="col-md-10 col-md-offset-1 pull-left job next-job">
					<span class="bold-data">Software Engineering Coursework Project • WPI-Suite</span><br />
					<span class="mid-data">Worcester Polytechnic Institute • Worcester, MA • November - December 2014</span><br />
					<div class="description">
						Programmed a large piece of software, WPI-Suite. Participated in a team of 15 which developed a Java module for a software, WPI-Suite. Programmed a GUI of the module. Managed the Q/A process in the team.
					</div>
				</div>

			</div>
		</div>

		<!-- Skills -->

		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="skills">
			
				<hr style="opacity: 1;">
				<h2 class="row-centered section">Skills</h2>
			
				<div class="col-md-10 col-md-offset-1 row-centered">
					<div class="col-md-4 col-md-offset-0">
						<span class="mid-data">Operating Systems</span><br />
						<span class="description">Mac OS X • Windows • Linux</span>
					</div>
					<div class="col-md-4 col-md-offset-0">
						<span class="mid-data">Programming Languages</span><br />
						<span class="description">C# • Java • Objective-C/Swift • C++ • HTML • JavaScript • CSS • PHP</span>
					</div>
					<div class="col-md-4 col-md-offset-0">
						<span class="mid-data">Applications</span><br />
						<span class="description">Eclipse • Visual Studio • xCode • Jira</span>
					</div>
				</div>

			</div>
		</div>

		<!-- Experience -->

		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="experience" runat="server">
			
				<hr style="opacity: 1;">
				<h2 class="row-centered section">Experience</h2>
			
				<div class="col-md-10 col-md-offset-1 pull-left job">
					<span class="bold-data">Worcester Polytechnic Institute • Worcester, MA • September 2014 - Present</span><br />
					<span class="mid-data">Senior Assistant at Computer Science Department</span><br />
					<div class="description">
						<ul>
							<li>Assisted in teaching WPI students in graduate and undergraduate level Computer Science courses</li>
							<li>Held office hours and led laboratory sections</li>
							<li>Proctored and graded homework, quizzes and exams</li>
						</ul>
					</div>
				</div>

				<div class="col-md-10 col-md-offset-1 pull-left job next-job">
					<span class="bold-data">TradeStation Inc. • Plantation, FL • June 2015 - August 2015</span><br />
					<span class="mid-data">Quantitative Analyst/Development Summer Internship</span><br />
					<div class="description">
						<ul>
							<li>Designed and developed statistical models and pattern recognition algorithms to be used in TradeStation’s next generation analytics product</li>
							<li>Designed and tested new analysis concepts using time series data, fundamental data, macro-economic data, and social media sentiment data</li>
							<li>Evaluated historical chart patterns and event data</li>
							<li>Worked closely with the product management and development teams to specify and scope features and understand and optimize functionality</li>
						</ul>
					</div>

				</div>

			</div>
		</div>

		<!-- Activities -->

		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="activities">
			
				<hr style="opacity: 1;">
				<h2 class="row-centered section">Activities</h2>
			
				<div class="col-md-10 col-md-offset-1 row-centered">
					<div class="col-md-6 col-md-offset-0">
						<span class="mid-data">IT-Manager</span><br />
						<span class="description">September 2013 - Present</span><br />
						<span class="description">Russian-Speaking Students and Scholars Association (RSSA), WPI</span>
					</div>
					<div class="col-md-6 col-md-offset-0">
						<span class="mid-data">Career Fair Volunteer</span><br />
						<span class="description">February 2014</span><br />
						<span class="description">WPI</span>						
					</div>
				</div>

			</div>
		</div>

		<!-- Awards -->

		<div class="row">
			<div class="col-md-10 col-md-offset-1" id="awards">
			
				<hr style="opacity: 1;">
				<h2 class="row-centered section">Awards</h2>
			
				<div class="col-md-10 col-md-offset-1 row-centered">
					<div class="col-md-4 col-md-offset-0">
						<span class="mid-data">Charles O. Thompson Scholar</span><br />
						<span class="description">Spring 2014</span><br />
						<span class="description">WPI</span>
					</div>
					<div class="col-md-4 col-md-offset-0">
						<span class="mid-data">WPI's Dean's List</span><br />
						<span class="description">Fall 2013 • Spring 2014 • Fall 2014</span><br />
						<span class="description">WPI</span>						
					</div>
					<div class="col-md-4 col-md-offset-0">
						<span class="mid-data">Prize</span><br />
						<span class="description">December 2012</span><br />
						<span class="description">"The Future of Ukraine" National Project Competition • Kiev • Ukraine</span>						
					</div>
				</div>

			</div>
		</div>
		
	</div>

</asp:Content>
