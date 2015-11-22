$(document).ready(function () {
	constructTable();
	gameController.drawInitialField();
	gameController.setMark();
	gameController.testMove();
});

function AJAXTest() {
	var data = JSON.stringify({ data: "foo" });

	$.post("/api/projects/pentago/host", "=" + data)
		.done(function (returned) {
			alert(returned);
		})
	.fail(function (xhr, textStatus, errorThrown) {
		alert(textStatus);
	});
}

function constructTable() {
	var html = "";

	for (var i = 0; i < 6; i++) {
		html += "<tr>";
		for (var j = 0; j < 6; j++) {
			html += "<td id='cell_" + i + "_" + j + "'>" + i + " " + j + "</td>";
		}
		html += "</tr>";
	}

	$("#gameTBody").append(html);

	var height = $("#gameTable td").width();
	$(".gameCell").css({ 'height': height + 'px' });
}

var gameController = (function () {

	var markIsCross;

	function renderField(field) {
		field = jQuery.parseJSON(field);

		for (var i = 0; i < 6; i++) {
			var mark = '';
			for (var j = 0; j < 6; j++) {
				switch (field[i][j]) {
					case 0:
						mark = 'x';
						break;
					case 1:
						mark = 'o';
						break;
					case 2:
						mark = '-';
						break;
					default:
				}

				$("#cell_" + i + "_" + j).text(mark);
			}
		}

	}

	function drawInitialField() {
		renderField(pentagoAPIWrapper.getField());
	}

	function setMark() {
		markIsCross = pentagoAPIWrapper.amICross();
	}

	function testMove() {
		renderField(pentagoAPIWrapper.makeTurn(0, 0, (markIsCross ? 0 : 1), 0, 1));
	}

	function putMark() {

	}

	return {
		putMark: putMark,
		drawInitialField: drawInitialField,
		setMark: setMark,
		testMove: testMove
	};

})();