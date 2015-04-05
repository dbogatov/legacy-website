$(document).ready(function () {

	var symbol = $("#MainContent_MySymbol").text();
	var strikes = $("#MainContent_MyStrike").text().split("#");  //parseFloat($("#MainContent_MyStrike").text());
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

	s = s.split("#");

	for (var i = 0; i < s.length; i++) {

		try {
			var $data = $.parseJSON(s[i]);

			for (var i = 0; i < $data.length; i++) {
				if ($data[i].strike == parseFloat(strikes[i])) {
					bid = $data[i].b;
					ask = $data[i].a;

					alert("bid: " + bid + ", ask: " + ask);
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

	}
});

function replaceAll(str, find, replace) {
	var re = new RegExp(find, 'g');

	return str.replace(re, replace);
}

var rowsNum = 1;

function addRow(id) {
	
	//alert(id);

	$("#myRow").remove();

	var toAdd = '<br><div class="form-inline row" id="row' + (rowsNum + 1) + '"><div class="form-group col-md-2"><input type="text" class="form-control symbol" placeholder="eq. JNJ" onchange="decideOnButton(false);" onkeypress="decideOnButton(false);" onpaste="decideOnButton(false);"></input></div><div class="form-group col-md-2"><input type="text" class="form-control strike" placeholder="eq. 94.5" onchange="decideOnButton(false);" onkeypress="decideOnButton(true);" onpaste="decideOnButton(true);"></input></div><button onclick="addRow(); return false;" class="btn btn-default addRow" id="myRow" style="vertical-align:bottom">Add symbol</button></div>';

	$("#row"+rowsNum).after(toAdd);
	
	rowsNum++;

}

function decideOnButton(force) {

	if (force) {
		$("#myRow").removeAttr('disabled');
	} else {
		console.log($(".symbol").val() + " " + $(".strike").val());

		if ($(".symbol").val() != "" && $(".strike").val() != "") {
			$("#myRow").removeAttr('disabled');
		} else {
			$("#myRow").attr('disabled', 'disabled');
		}
	}

	
}

function generateSubmit() {
	
	var symbols = "";

	$(".symbol").each(function() {
		symbols += $(this).val() + "+";
	});

	symbols = symbols.substring(0, symbols.length - 1);

	var strikes = "";

	$(".strike").each(function () {
		strikes += $(this).val() + "+";
	});

	strikes = strikes.substring(0, strikes.length - 1);

	$("#MainContent_Symbol").val(symbols);
	$("#MainContent_Strike").val(strikes);

	//alert($("#MainContent_Symbol").val());

	return false;
}