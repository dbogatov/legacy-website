$(document).ready(function () {
	constructTable();
});

function constructTable() {
	var html = "";

	for (var i = 0; i < 6; i++) {
		html += "<tr>";
		for (var j = 0; j < 6; j++) {
			html += "<td id='cell_" + i + "_" + j + "'>";
		}
		html += "</tr>";
	}

	$("#gameTBody").append(html);

	var height = $("#gameTable td").width();
	$(".gameCell").css({ 'height': height + 'px' });
}

var gameController = (function () {

	var markIsCross = true;

	function putMark() {

	}

	return {
		putMark: putMark
	};

})();