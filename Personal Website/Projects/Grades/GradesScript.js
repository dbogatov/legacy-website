$(document).ready(function () {

	
	$(".table").tablesorter({

		sortList: [[1, 1], [0, 1]],

		textExtraction:
			function (node) {

				switch ($(node).text()) {
					case "A":
						return "C";
						break;
					case "B":
						return "D";
						break;
					case "C":
						return "A";
						break;
					case "D":
						return "B";
						break;
					default:
						return $(node).text();
				}
			}
	});
});