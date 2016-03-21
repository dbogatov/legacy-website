class GoogleFinanceParser {

	private static template = _.template(`
		<tr class='success'>
			<th><%= symbol %></th>
			<th><%= strike %></th>
			<th><%= bid %></th>
			<th><%= ask %></th>
		<tr>
	`);

	private compute(symbol, strike) {

		var data = {
			symbol: symbol,
			strike: strike
		};

		$.get("/api/Projects/GoogleFinanceParser", data, results => {
			$("#resultingTable").html(GoogleFinanceParser.template({
				symbol: (<String>results.symbol).toUpperCase(),
				strike: results.strike,
				bid: results.bid,
				ask: results.ask
			}));
		});

	}

	private setListeners(): void {
		$("#getPricesBtn").click(() => {
			var symbol = $("#symbol").val();
			var strike = $("#strike").val();
			if (symbol.length > 0 && strike.length > 0) {

				this.compute(symbol, strike);

			} else {
				alert("Fields cannot be empty");
			}
		});

		$("#exampleBtn").click(() => {
			var symbol = "AAPL";
			var strike = 100;

			this.compute(symbol, strike);
		});
	}

	run(): void {
		this.setListeners();
	}
}

export = GoogleFinanceParser;