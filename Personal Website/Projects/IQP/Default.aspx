<%@ Page Title="IQP - Trading Systems, Investment and Risk Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Personal_Website.Projects.IQP.Default" %>
<%@ Register TagPrefix="My" TagName="Pagination" Src="~/General User Controls/Pagination.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="IQPStyle.css" rel="stylesheet" type="text/css" />
	<link href="Prism/prism.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


	<script>
		$(document).ready(function () {
			$("#IQP-page-" + $(".pageLabel").text()).show();
			$("#pageTitle").text($("#Title-page-" + $(".pageLabel").text()).text());
		});
	</script>

	<label id="pageLabel" class="pageLabel" runat="server" hidden></label>

	<h3><%: Title %>.</h3>
	<h3 id="pageTitle"></h3>


	<!-- Page 1 -->

	<div class="row row-centered" id="IQP-page-1" hidden>

		<label id="Title-page-1" hidden>Title page</label>

		<div class="col-md-8 col-md-offset-2 IQPPage firstPage">
			<span class="bold-data">Investment Trading And Risk Management:</span><br />
			<span class="bold-data padding-bottom-twenty">Scientifically Developing and Analyzing Trading Systems</span><br />

			<span class="italic-data">An Interactive Qualifying Project</span><br />
			<span class="italic-data">Submitted to the Faculty of</span><br />
			<span class="all-cap-data">WORCESTER POLYTECHNIC INSTITUTE</span><br />
			<span class="italic-data">in partial fulfilment of the requirements for the</span><br />
			<span class="italic-data padding-bottom-twenty">degree of Bachelor of Science.</span><br />

			<img class="padding-bottom-twenty" src="Resources/WPI.png" width="500" /><br />

			<span class="">Date</span><br />
			<span style="padding-bottom:50px;">August 8<sup>th</sup>, 2014 - May 5<sup>th</sup>, 2015</span><br />

			<span class="">Report Submitted to:</span><br />
			<span style="padding-bottom:50px;">Professors Hossein Hakim and Michael Radzicki of Worcester Polytechnic Institute</span><br />

			<p class="italic-data">
				This report represents work of WPI undergraduate students submitted to the faculty as evidence of a degree requirement. WPI routinely publishes these reports on its web site without editorial or peer review. For more information about the projects program at WPI, see <br />
				<a target="_blank" href="http://www.wpi.edu/Academics/Projects">http://www.wpi.edu/Academics/Projects</a>
			</p>

		</div>
	</div>

	<!-- Page 2 -->

	<div class="row row-centered" id="IQP-page-2" hidden>

		<label id="Title-page-2" hidden>Abstract and acknowledgements</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">ABSTRACT</span><br />
			
			<p class="usual-data" style="padding-bottom:50px; display:inline-block;">
				The purpose of this IQP project is to scientifically develop profitable systems and indicators for trading the markets. The project consists of nine individually developed strategies, which were quantitatively analyzed for profitability and then combined into a system of systems. Each individual system or indicator was given defined rules and then allocated simulated money to trade. Two types of systems were mainly developed, predictive and confirmative, leading to a system of systems that incorporated a predictive layer and a confirmative layer in the decision to take positions.
			</p>

			<span class="bold-data all-cap-data section">ACKNOWLEDGEMENTS</span><br />
			
			<p class="usual-data">
				We would like to thank Professors Hossein Hakim and Michael Radzicki for their mentorship, support, and insights throughout the duration of the Interim Qualifying Project.<br /><br />
				We would also like to thank TradeStation for allowing us to use their software for free. The ability to use a truly profession market analyzing tool with life market feeds was utterly invaluable.<br /><br />
				Finally, we would like to thank Worcester Polytechnic Institute for the opportunity to gain insights and practice theory through a real world application.<br />
			</p>

		</div>
	</div>


	<!-- Page 3 -->

	<div class="row row-centered" id="IQP-page-3" hidden>

		<label id="Title-page-3" hidden>Bollinger Bands Strategy. Description.</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">Bollinger Bands Strategy</span><br />
			
			<span class="subsection">Description</span>

			<p class="usual-data">
				This is a simple strategy that uses Bollinger Bands as its primary indicator. The Bollinger BandTM is an indicator consisting of an N-period moving average, and two bands, K multiplied by N-period standard deviation below and above the moving average. Thus there are three bands total.
			</p>

			<img src="Resources/BollingerBands.png" />

			<p class="usual-data">
				It is commonly accepted in statistical theory that, for a normal model, 95% of the data lies within two standard deviations of the mean. Here, an average price is used as the mean, and so statistically the price should stay within two standard deviation of the average 95% of the time (if K=2 is used). If it is within two standard deviations it is between the bands. Therefore, if price goes beyond the band, there is a high chance that it will come back.<br /><br />
				Another idea used in the strategy is a simple, two moving average cross strategy. If the faster moving average is above the slower one, there is a high chance of an uptrend, and vice versa.<br /><br />
				This strategy exploits these two ideas in a following way. When price “just jumps back” into the bandwidth (crosses the upper or lower band of the Bollinger Bands) and if trend signals that price will go in the direction toward the middle of bandwidth (by judging which moving average is above the other), the strategy goes long or short depending on the trend. The strategy exits when either trend changes or stop loss gets triggered.
			</p>

		</div>
	</div>


	<!-- Page 4 -->

	<div class="row row-centered" id="IQP-page-4" hidden>

		<label id="Title-page-4" hidden>Bollinger Bands Strategy. Flowchart, Rules, Applications.</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">Bollinger Bands Strategy</span><br />
			
			<span class="subsection">Flowchart</span><br />

			<img src="Resources/BollFlow.png" /><br />


			<span class="subsection">Setup, Entry and Exit</span>

			<div class="usual-data">
				Going long<br />
				<ul>
					<li>Setup is an uptrend indicated by the faster moving average being above the slower one.</li>
					<li>Entry is the price “jumping in bandwidth” from the below.</li>
				</ul>
				Going short
				<ul>
					<li>Setup is a downtrend indicated by the faster moving average being below the slower one.</li>
					<li>Entry is the price “jumping in bandwidth” from the above.</li>
				</ul>
				Exit
				<ul>
					<li>Strategy signals “sell” or “buy to cover” due to a change in trend indicated by a shift in which moving average is above the other.</li>
					<li>Stop loss signals an exit due to a loss, the size of which was predetermined to indicate the strategy was incorrect in taking the position.</li>
				</ul>
			</div>
			

			<span class="subsection">Applications</span>

			<p class="usual-data">
				This strategy works well on volatile markets where the strong trend is absent. It has been profitable for the currency pair <i>EURUSD</i> and equities <i>AAPL</i>, <i>GOOGL</i>, <i>IBM</i> and <i>YHOO</i>.
			</p>

		</div>
	</div>


	<!-- Page 5 -->

	<div class="row row-centered" id="IQP-page-5" hidden>

		<label id="Title-page-5" hidden>Bollinger Bands Strategy. Optimization.</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">Bollinger Bands Strategy</span><br />
			
			<span class="subsection">Optimization</span>

			<p class="usual-data">
				A general optimization was performed maximizing the gross profit and changing the stop loss amount from $75 to $225 with a step of $15. As well a change was made to the <i>MALength</i> parameter from 10 to 30 bars with a step of 2 bars. The optimal combination is different for each symbol tested, though they are all around the same parameters, indicating that the optimization is not over fitting to any one data set.
			</p>

			<img src="Resources/BollOpt.png" /><br />


			<span class="subsection">Walk Forward</span>

			<p class="usual-data">
				A Walk Forward optimization confirmed that the system is likely to be consistent. It indicated that no run constituted an overly large percent of the overall profit, and the system is likely to net a profit in almost all cases.
			</p>

			<img src="Resources/BollWalkForw.png" /><br />

		</div>
	</div>


	<!-- Page 6 -->

	<div class="row row-centered" id="IQP-page-6" hidden>

		<label id="Title-page-6" hidden>Bollinger Bands Strategy. Performance and Monte Carlo Analysis.</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">Bollinger Bands Strategy</span><br />
			
			<span class="subsection">Performance and Monte Carlo Analysis</span>

			<p class="usual-data">
				The following figure includes an analysis of the strategy’s performance on the <i>EURUSD</i>.
			</p>

			<img src="Resources/BollPerf.png" /><br />

			<p class="usual-data">
				Monte Carlo analysis was performed on trades generated by this strategy on <i>EURUSD</i> symbol (60 minutes chart, 5 years ago from April 1<sup>st</sup>). Analysis shows that the rate of return on that particular sample is 53% with 100% confidence. Max drawdown is 22% with 99% confidence.
			</p>

			<img src="Resources/BollMonte1.png" /><br />


			<p class="usual-data">
				The equity curve was also potted. Notice it has a strong average uptrend indicated by the red line.
			</p>

			<img src="Resources/BollMonte2.png" /><br />

			<p class="usual-data">
				Next, an oval was generated around the average equity curve, indicating where the systems equity should always reside statistically. Plot points sometimes go outside of the normal area, but they go higher, which is positive that system generates some returns, but indicates minor inconsistencies.
			</p>

			<img src="Resources/BollMonte3.png" /><br />

			<p class="usual-data">
				According to the analysis, if additional trades were generated, the rate of return would be at least 44% with 99% confidence and maximum dropdown would be less than 10% with the same confidence level. These are very strong indicators in support of the systems quality.
			</p>

			<img src="Resources/BollMonte4.png" /><br />

			<p class="usual-data">
				As a final test for quality, a cone was projected out after N number of trades using the average equity curve line slope. If the equity curve resided entirely within the cone for remainder of the trades within the system, then it indicated consistency and predictability of future earnings. As one can see in the figure below, though the equity curve resided below the average slope, it remained entirely within the cone again confirming quality of the system.
			</p>

			<img src="Resources/BollMonte5.png" /><br />

		</div>
	</div>


	<!-- Page 7 -->

	<div class="row row-centered" id="IQP-page-7" hidden>

		<label id="Title-page-7" hidden>Bollinger Bands Strategy. System Quality and Dead Ends</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">Bollinger Bands Strategy</span><br />
			
			<span class="subsection">System Quality</span><br />

			<p class="usual-data">
				Observe Expectancy and Expectunity in the previously displayed figure below. Below are the meanings of systems quality calculations:
			</p>

			<div class="usual-data">
				<ul>
					<li>
						<b>Expectancy</b>
						<ul>
							<li><u>R Mult 1</u>: profit or loss per dollar risked per <i>trade</i> computed via the <i>average</i> loosing trade (<i>$353.27</i> profit in this case);</li>
							<li><u>R Mult 2</u>: profit or loss per dollar risked per <i>trade</i> computed via the <i>largest</i> loosing trade (<i>$23.11</i> profit in this case);</li>
						</ul>
					</li>
					<li>
						<b>Opportunities</b>: average number of trades per year (<i>281.47</i> trades in this case)
					</li>
					<li>
						<b>Expectunity</b> (annualized expectancy)
						<ul>
							<li><u>R Mult 1</u>: profit or loss per dollar risked per <i>year</i> computed via the <i>average</i> loosing trade (<i>$99436.36</i> profit in this case);</li>
							<li><u>R Mult 2</u>: profit or loss per dollar risked per <i>year</i> computed via the <i>largest</i> loosing trade (<i>$6505.87</i> profit in this case);</li>
						</ul>
					</li>
					<li>
						<b>System Quality</b>: total profit or loss per dollar risked relative to the total variability of the profit or loss per dollar risked (<i>$8931.90</i> profit in this case, dimensionless, the higher the better)
					</li>
				</ul>
			</div>
			
			<p class="usual-data">
				As one can see both the R multiples are very high for this system meaning there is a lot of profit per dollar risked. As well, the system provides ample opportunity to be in the market, trading on average 281 trades per year. Finally the overall system quality rating is high, and the higher the better.
			</p>

			<img src="Resources/BollPerf.png" /><br />


			<span class="subsection">Dead Ends</span>

			<p class="usual-data">
				This strategy works extremely poorly on trending assets. Since the core of this strategy is a volatility indicator it garners large losses for strong trends as it looks to play support and resistance line. I was even unable to optimize for trending stocks at all.
			</p>

		</div>
	</div>


	<!-- Page 8 -->

	<script src="Prism/prism.js"></script>

	<div class="row row-centered" id="IQP-page-8" hidden>

		<label id="Title-page-8" hidden>Bollinger Bands Strategy. Code.</label>

		<div class="col-md-8 col-md-offset-2 IQPPage">
			<span class="bold-data all-cap-data section">Bollinger Bands Strategy</span><br />
			
			<span class="subsection">Code</span>

			<pre><code class="language-EasyLanguage" style="text-align:left">
{Bollinger Bands Strategy}

Input:
	BPrice(Close),	// price for band
	MALength(20),	// moving average length
	StdDevNum(2);	// number of standard deviations

Variable:
	LBand(0),	// lower band
	UBand(0),	// upper band
	FastAvg(0),	// fast moving average
	SlowAvg(0);	// slow moving average

{Computing values}
LBand = BollingerBand(BPrice, MALength, -StdDevNum);
UBand = BollingerBand(BPrice, MALength, StdDevNum);
FastAvg = AverageFC(BPrice, Round(0.5*MALength, 0) );
SlowAvg = AverageFC(BPrice, Round(1.5*MALength, 0) );

{conditions for going long}
if CurrentBar > 1 AND BPrice crosses over LBand AND SlowAvg < FastAvg then
	Buy("BBandBuy") next bar at LBand stop;

{conditions for going long}	
if CurrentBar > 1 AND BPrice crosses below UBand AND FastAvg < SlowAvg then
	SellShort("BBandSell") next bar at UBand stop;
			</code></pre>

		</div>
	</div>

	<!-- End Pages -->

	<My:Pagination ID="Pagination" runat="server" URL="~/Projects/IQP/Default.aspx" pageNum="8" defaultActive="1" displayNum="5" ></My:Pagination>

</asp:Content>

