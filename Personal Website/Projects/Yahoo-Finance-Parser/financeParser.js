$(document).ready(function () {


	var strike = parseFloat($("#MainContent_Strike").text());
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

	var $data = $.parseJSON(s);

	var tmp = $data[4];
	
	for (var i = 0; i < $data.length; i++) {
		if ($data[i].strike == strike) {
			console.log($data[i].b);
			console.log($data[i].a);
		}
		
	}

});

function replaceAll(str, find, replace) {
	var re = new RegExp(find, 'g');

	return str.replace(re, replace);
}