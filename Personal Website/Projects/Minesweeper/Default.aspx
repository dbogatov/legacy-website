﻿<%@ Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.Minesweeper.Default" UICulture="uk-ua" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Minesweeper</title>
    <link rel="icon" type="image/png" href="Resources/Pictures/mine.jpg" />
	<link href="CSS Styles/Style.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js"></script>
	<script type="text/javascript" src="scripts/jsAPI.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>
    <script type="text/javascript" src="scripts/minesweeper.js"></script>
	<script type="text/javascript" src="scripts/solver.js"></script>
	<script type="text/javascript" src="scripts/modernizr.custom.06116.js"></script>
	<link href='http://fonts.googleapis.com/css?family=Roboto:300&subset=latin,cyrillic-ext,greek-ext,greek,vietnamese,latin-ext,cyrillic' rel='stylesheet' type='text/css' />
	<link href='http://fonts.googleapis.com/css?family=Oswald' rel='stylesheet' type='text/css' />
    <noscript>
        Turn on javascript in your browser, or if it does not support it, download modern browser.
    </noscript>
</head>
<body>
	<form id="languageForm" runat="server">
	<div class="rightBar" >
		<div class="solverButton">
			<asp:Label runat="server" ID="lblSolver" meta:resourcekey="lblSolver"/>
			<div style="width:90%; margin: 10% 0px 0px 5%; border-top:1px solid black;"></div>
		</div>
		<asp:Label runat="server" ID="lblRules" meta:resourcekey="lblRules"/>
		<div style="width:90%; margin: 10% 0px 0px 5%; border-top:1px solid black;"></div>
		<asp:Label runat="server" ID="lblHelp" meta:resourcekey="lblHelp"/>
		<div id="solverMenu">
			<asp:Label runat="server" ID="lblGetMove" meta:resourcekey="lblGetMove"/>
			<input type="checkbox" id="checkBox" /><asp:Label runat="server" ID="lblSolverRun" meta:resourcekey="lblSolverRun"/>
			<asp:Label runat="server" ID="lblSolverCancel" meta:resourcekey="lblSolverCancel"/>	
		</div>
	</div>

	<div id="imgHolder" style="display: none;">
		<asp:Image runat="server" ID="aspImage"  meta:resourcekey="flagImage" ImageUrl="http://www.hgsitebuilder.com/files/writeable/uploads/hostgator507407/image/american20flag.jpg"/>
	</div>
	<asp:Label runat="server" ID="lblHelpSrc" meta:resourcekey="lblHelpSrc" style="display: none;"/>
	<asp:Label runat="server" ID="lblRulesSrc" meta:resourcekey="lblRulesSrc" style="display: none;"/>
    <asp:Label runat="server" ID="win" meta:resourcekey="lblWin" style="display: none;"/>
    <asp:Label runat="server" ID="loose" meta:resourcekey="lblLoose" style="display: none;"/>
	<asp:Label runat="server" ID="lblMineTip" meta:resourcekey="lblMineTip" style="display: none;"/>
    <div class="tipText size"><asp:Label runat="server" ID="lblTipSize" Text="" meta:resourcekey="lblTipSize" /></div>
	<div class="tipText mineAmount"></div>
    <div class="loading"><div class="loadingText"><asp:Label runat="server" ID="lblLoading" Text="" meta:resourcekey="lblLoading" /></div></div>
	<div class="header">
		<asp:Label runat="server" ID="errorLabel" meta:resourcekey="lblError" style="display: none;"/>
		<div class="leftHeaderContainer">
			<asp:Button value="uk-ua" ID="ukr_button" runat="server" meta:resourcekey="iptUkrainian" />
			<div style="width:90%; margin: 3% 0px 0px 5%; border-top:1px solid black;"></div>
			<asp:Button value="en-US" ID="eng_button" runat="server" meta:resourcekey="iptEnglish" />
		</div>
		<div style="float: left; height:80%; margin-top: 1%; border-left:1px solid black;"></div>
		<div class="headerMiddle">
			<asp:Label runat="server" ID="lblMinesweeper" meta:resourcekey="lblMinesweeper" style="display: block;"/>
			<input type="button" value="" class="newGameButton" runat="server" meta:resourcekey="iptNewGame" />
			<div style="float: left; height:28%; margin-top: 1%; border-left:1px solid black;"></div>
			<input type="button" value="" class="leaderBoardButton" runat="server" meta:resourcekey="iptLeaders" />
		</div>
		<div style="float: left; height:80%; margin-top: 1%; border-left:1px solid black;"></div>
		<div class="headerLoading">
			<div class="headerLoadingBar"></div>
			<asp:Label runat="server" ID="loading" meta:resourcekey="lblLoadingRun" style="display: block;"/>
			<asp:Label runat="server" ID="done" meta:resourcekey="lblLoadingStop" style="display: block;"/>
		</div>
	</div>
	<div class="page">
		<div class="main newGame">
			<div class="menu">
				<div class="menuOption small">
					<asp:Label runat="server" ID="Label1" Text="" meta:resourcekey="lblBeginner" />
				</div>
				<div class="menuOption medium">
					<asp:Label runat="server" ID="Label2" Text="" meta:resourcekey="lblMedium" />
				</div>
				<div class="menuOption professional">
					<asp:Label runat="server" ID="Label3" Text="" meta:resourcekey="lblProfessional" />
				</div>
				<div class="customGame">
					<asp:Label runat="server" ID="Label4" Text="" meta:resourcekey="lblSpecial" />
				</div>
					<div class="username">
						<asp:TextBox class='usernameField' runat="server" meta:resourcekey="tfUserName" />
					</div>
					<div class="customGameOptions">
						<asp:Label runat="server" ID="Label6" Text="" meta:resourcekey="lblSizeOfField" />: <br><asp:TextBox class='fieldSize X' runat="server" meta:resourcekey="tfSizeX" /> <asp:Label runat="server" Text="" meta:resourcekey="lblBy" /> <input type='text' class='fieldSize Y' /><span class='sizeAlert'>!</span><span class='sizeTip'>&nbsp;&nbsp;?&nbsp;&nbsp;</span><br>
						<asp:Label runat="server" ID="Label7" Text="" meta:resourcekey="lblNumberOFMines" />: <input type='text' class='mineAmount' /><span class='mineAmountAlert'>!</span><span class='mineAmountTip'>&nbsp;&nbsp;?&nbsp;&nbsp;</span><br>
						<asp:Label class='startGame' Text="" runat="server" meta:resourcekey="lblStartButton"></asp:Label>
					</div>
			</div>
		</div>
		
		<div class="main gamePage">
			<div class="gameMenu">
				<div class="flagLeft">
				    
				</div>
				<div class="smile">
					<asp:Label class='restartGame' Text="" runat="server" meta:resourcekey="lblRestartButton"></asp:Label>
				</div>
				<div class="timer">
				    00:00:00
				</div>
			</div>
			<div class="field"></div>
            <div class="result"></div>
		</div>
		
		<div class="leaderBoard">
			<div class="sheet">
				<table>
				    <thead>
                        <tr>
                            <td class="leaderHead easy" id="easy">
                                <asp:Label runat="server" meta:resourcekey="lblLeadEasy"/>
                            </td>
                            <td class="leaderHead medium" id="medium">
                                <asp:Label runat="server" meta:resourcekey="lblLeadMedium"/>
                            </td>
                            <td class="leaderHead professional" id="professional">
                                <asp:Label runat="server" meta:resourcekey="lblLeadProfessional"/>
                            </td>
                        </tr>
                        <tr class="columnName">
                            <td> # </td>
                            <td>
                                <asp:Label runat="server" meta:resourcekey="lblLeadUsername"/>
                            </td>
                            <td>
                                <asp:Label runat="server" meta:resourcekey="lblLeadTime"/>
                            </td>
                        </tr>
				    </thead>
                    <tbody>

                    </tbody>
				</table>
			</div>
		</div>
        <div class="main authorsPage">
            <div class="authorsInfo">
				<div class="dimaInfo">
					<a href="http://facebook.com/dima4ka007" target="_blank"><img src="Resources/Pictures/Authors/Dmytro.jpg" /></a>
					<div class="authorName">
						<asp:Label runat="server" Text="" meta:resourcekey="lblDimaName" />
						<br /><asp:Label runat="server" Text="" meta:resourcekey="lblDimaSurname" />
						<br /><a href="mailto:dbogatov@wpi.edu">dbogatov@wpi.edu</a>
					</div>
					<div class="authorDescription">
						<br /><i><asp:Label runat="server" ID="Label8" Text="" meta:resourcekey="lblBackEnd" /></i>
						<br /><b><asp:Label runat="server" ID="Label9" Text="" meta:resourcekey="lblProjectLeader" /></b>
						<br /><asp:Label runat="server" ID="Label23" Text="" meta:resourcekey="lblServerSide" />
						<ul>
							<li>
								<asp:Label runat="server" ID="Label10" Text="" meta:resourcekey="lblGameEngine" /></li>
							<li>
								<asp:Label runat="server" ID="Label11" Text="" meta:resourcekey="lblDatabaseManagment" /></li>
							<li>
								<asp:Label runat="server" ID="Label5" Text="" meta:resourcekey="lblLocalization" /></li>
							<li>
								<asp:Label runat="server" ID="Label20" Text="" meta:resourcekey="lblPoster" /></li>
							<li>
								<asp:Label runat="server" ID="Label24" Text="" meta:resourcekey="lblSolver" /> (<asp:HyperLink meta:resourcekey="aSolver" Target="_blank" NavigateUrl="http://robertmassaioli.wordpress.com/2013/01/12/solving-minesweeper-with-matricies/" runat="server">Robert Massaioli</asp:HyperLink>)</li>
						</ul>
					</div>
				</div>
                <div class="bogdanInfo">
					<a href="http://vk.com/omarox" target="_blank"><img src="Resources/Pictures/Authors/Bogdan.jpg" /></a>
					<div class="authorName">
						<asp:Label runat="server" ID="Label12" Text="" meta:resourcekey="lblBogdanName" />
						<br /><asp:Label runat="server" ID="Label13" Text="" meta:resourcekey="lblBogdanSurname" />
						<br /><a href="mailto:omaroxtv@gmail.com">omaroxtv@gmail.com</a>
					</div>
					<div class="authorDescription">
						<br />
						<i><asp:Label runat="server" ID="Label14" Text="" meta:resourcekey="lblFrontEnd" /></i>
						<br />
						<br /><asp:Label runat="server" ID="Label25" Text="" meta:resourcekey="lblCleintSide" />
						<ul>
							<li>
								<asp:Label runat="server" ID="Label26" Text="" meta:resourcekey="lblStyles" /></li>
							<li>
								<asp:Label runat="server" ID="Label27" Text="" meta:resourcekey="lblScripts" /></li>
							<li>
								<asp:Label runat="server" ID="Label28" Text="" meta:resourcekey="lblHTMLMarkup" /></li>
							<li>
								<asp:Label runat="server" ID="Label29" Text="" meta:resourcekey="lblInterfaceRendering" /></li>
						</ul>
					</div>
                </div>
            </div>
			
		</div>
        <div class="main pdfPage">
			<div class="poster">
				<img src="Resources/Pictures/Poster.png" />
			</div>
			<asp:HyperLink id="hyperlink1" NavigateUrl="Resources/PDFs/Poster.pdf" runat="server" Target="_blank" meta:resourcekey="aPDF" />
			<asp:HyperLink id="hyperlink2" NavigateUrl="Resources/Pictures/Poster.png" runat="server" Target="_blank" meta:resourcekey="aPNG" />
		</div>
		<div class="main help">
			<iframe id ="helpFrame"></iframe>
		</div>
		<div class="main rules">
			<iframe id ="rulesFrame"></iframe>
		</div>
	</div>
	<div class="footer">
		<div class="info">
            <div class="authors">
                <asp:Label runat="server" ID="Label15" meta:resourcekey="lblAuthors" />
            </div>
            <div style="float: left; height:80%; margin-top: 0px; border-left:1px solid black;"></div>
            <div class="pdfInfo">
                <asp:Label runat="server" ID="Label19" meta:resourcekey="lblStructure" />
            </div>
		</div>
        <div class="copyright">
            <asp:Label runat="server" ID="Label18" meta:resourcekey="lblCopyright" />
        </div>
	</div>
	</form>
</body>
</html>