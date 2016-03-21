define(["require", "exports"], function (require, exports) {
    "use strict";
    var GoogleFinanceParser = (function () {
        function GoogleFinanceParser() {
        }
        GoogleFinanceParser.prototype.compute = function (symbol, strike) {
            var data = {
                symbol: symbol,
                strike: strike
            };
            $.get("/api/Projects/GoogleFinanceParser", data, function (results) {
                $("#resultingTable").html(GoogleFinanceParser.template({
                    symbol: results.symbol.toUpperCase(),
                    strike: results.strike,
                    bid: results.bid,
                    ask: results.ask
                }));
            });
        };
        GoogleFinanceParser.prototype.setListeners = function () {
            var _this = this;
            $("#getPricesBtn").click(function () {
                var symbol = $("#symbol").val();
                var strike = $("#strike").val();
                if (symbol.length > 0 && strike.length > 0) {
                    _this.compute(symbol, strike);
                }
                else {
                    alert("Fields cannot be empty");
                }
            });
            $("#exampleBtn").click(function () {
                var symbol = "AAPL";
                var strike = 100;
                _this.compute(symbol, strike);
            });
        };
        GoogleFinanceParser.prototype.run = function () {
            this.setListeners();
        };
        GoogleFinanceParser.template = _.template("\n\t\t<tr class='success'>\n\t\t\t<th><%= symbol %></th>\n\t\t\t<th><%= strike %></th>\n\t\t\t<th><%= bid %></th>\n\t\t\t<th><%= ask %></th>\n\t\t<tr>\n\t");
        return GoogleFinanceParser;
    }());
    return GoogleFinanceParser;
});

//# sourceMappingURL=GoogleFinanceParser.js.map
