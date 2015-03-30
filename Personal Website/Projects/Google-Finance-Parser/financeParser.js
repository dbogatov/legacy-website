$(document).ready(function () {

	var symbol = $("#MainContent_MySymbol").text();
	var strike = parseFloat($("#MainContent_MyStrike").text());
	var s = $("#MainContent_FinanceContent").text();

	s = s.substr(s.indexOf("calls:") + 6, s.indexOf(",underlying_id") - 6 - s.indexOf("calls:"));

	s = replaceAll(s, 'expiry', '\"expiry\"');
	s = replaceAll(s, 'cid', '\"cid\"');
	s = replaceAll(s, 'name', '\"name\"');
	s = replaceAll(s, ',s:', ',\"s\":');
	s = replaceAll(s, ',e:', ',\"e\":');
	s = replaceAll(s, ',p:', ',\"p\":');
	s = replaceAll(s, ',c:', ',\"c\":');
	s = replaceAll(s, ',b:', ',\"b\":');
	s = replaceAll(s, ',a:', ',\"a\":');
	s = replaceAll(s, ',oi:', ',\"oi\":');
	s = replaceAll(s, ',cs:', ',\"cs\":');
	s = replaceAll(s, ',cp:', ',\"cp\":');
	s = replaceAll(s, 'vol', '\"vol\"');
	s = replaceAll(s, 'strike', '\"strike\"');

	var bid = 0;
	var ask = 0;

	try {
		var $data = $.parseJSON(s);

		for (var i = 0; i < $data.length; i++) {
			if ($data[i].strike == strike) {
				bid = $data[i].b;
				ask = $data[i].a;
			}
		}
	} catch (e) {
		 
	}
	
	if (bid > 0 && ask > 0) {
		$("#response").text("Results as of now. Stock: " + symbol + ", strike: " + strike + ", bid: " + bid + ", ask: " + ask + ".");
	} else {
		$("#response").text("Something went wrong. Possible reason is incorrect symbol or strike price. Please, try again.");
		$("#response").addClass("label-danger").removeClass("label-success");
	}

});

function replaceAll(str, find, replace) {
	var re = new RegExp(find, 'g');

	return str.replace(re, replace);
}