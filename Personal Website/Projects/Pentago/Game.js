var d3ViewController = (function () {

	var width = 900;

	function setDimensions() {
		width = $("#mainPanel").width();
	}

	function getDimensions() {
		//width = 900;
		var height = width;
		var crossBorderFraction = 0.012;
		var cellOffsetCorrection = 0.0;


		return {
			width: width,
			height: height,
			fieldSize: width * (0.5 - 2 * crossBorderFraction),
			fieldOffset: width * crossBorderFraction,
			cellSize: width * (0.5 - 2 * crossBorderFraction) * (0.333333 - 2 * crossBorderFraction),
			cellOffset: width * (0.5 - 2 * crossBorderFraction) * crossBorderFraction,
			cellOffsetCorrection: width * cellOffsetCorrection,
			fieldColor: "blue",
			cellColor: "red",
			cellOnHoverColor: "white"
		};
	}

	// left: 37, up: 38, right: 39, down: 40,
	// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
	var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };
	var allowTurns;
	var svg;
	var handler = function () {

		d3.select(this).style("fill", getDimensions().cellColor);

		var x = $(this).attr('id').split('_')[2];
		var y = $(this).attr('id').split('_')[1];
		var field;

		if (x < 3) {
			if (y < 3) {
				field = 0;
			} else {
				field = 3;
			}
		} else {
			if (y < 3) {
				field = 1;
			} else {
				field = 2;
			}
		}


		gameController.putMark(x, y, field, 0);
	};

	function logToUser(text) {
		$("#userLog").text(text);
	}

	function preventDefault(e) {
		e = e || window.event;
		if (e.preventDefault)
			e.preventDefault();
		e.returnValue = false;
	}

	function preventDefaultForScrollKeys(e) {
		if (keys[e.keyCode]) {
			preventDefault(e);
			return false;
		}
	}

	function disableScroll() {
		if (window.addEventListener) // older FF
			window.addEventListener('DOMMouseScroll', preventDefault, false);
		window.onwheel = preventDefault; // modern standard
		window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
		window.ontouchmove = preventDefault; // mobile
		document.onkeydown = preventDefaultForScrollKeys;
	}

	function enableScroll() {
		if (window.removeEventListener)
			window.removeEventListener('DOMMouseScroll', preventDefault, false);
		window.onmousewheel = document.onmousewheel = null;
		window.onwheel = null;
		window.ontouchmove = null;
		document.onkeydown = null;
	}

	function resizeTable() {
		setDimensions();
	}

	function setHandlers() {

		var data = getDimensions();

		d3.selectAll("[data-status=empty]")
			.on("mouseover", function () {
				d3.select(this).style("fill", data.cellOnHoverColor);
			})
			.on("mouseout", function () {
				d3.select(this).style("fill", data.cellColor);
			})
			.on("click", handler);
	}

	function constructInnerField(isXOffset, isYOffset, name) {
		var data = getDimensions();

		var idOffsetI = isYOffset ? 0 : 3;
		var idOffsetJ = isXOffset ? 0 : 3;

		var xOffset = isXOffset ? data.fieldSize + 3 * data.fieldOffset : data.fieldOffset;
		var yOffset = isYOffset ? data.fieldSize + 3 * data.fieldOffset : data.fieldOffset;

		console.log("Offsets: " + [xOffset, yOffset]);
		console.log("Cell size and offset: " + [data.cellSize, data.cellOffset]);

		var field = svg.append("g")
			.attr("id", name);

		field.append("rect")
			.attr("x", xOffset)
			.attr("y", yOffset)
			.attr("width", data.fieldSize)
			.attr("height", data.fieldSize)
			.attr("fill", data.fieldColor);

		for (var i = 0; i < 3; i++) {
			for (var j = 0; j < 3; j++) {
				field.append("rect")
					.attr("x", xOffset + data.cellOffset + j * data.cellSize + j * 2 * data.cellOffset)
					.attr("y", yOffset + data.cellOffset + i * data.cellSize + i * 2 * data.cellOffset)
					.attr("width", data.cellSize)
					.attr("height", data.cellSize)
					.attr("fill", data.cellColor)
					.attr("class", "pentagoCell")
					.attr("id", "cell_" + (i + idOffsetI) + "_" + (j + idOffsetJ))
					.attr("data-status", "empty");;
			}
		}

		return field;
	}

	function drawCross(cellObj) {
		var data = getDimensions();
		var x = parseFloat(cellObj.attr("x")) + data.cellSize * 0.1;
		var y = parseFloat(cellObj.attr("y")) + data.cellSize * 0.1;
		var size = data.cellSize * 0.8;

		cellObj.attr("data-status", "cross");

		d3.select(cellObj.node().parentNode).append("line")
			.attr({
				x1: x,
				x2: x + size,
				y1: y,
				y2: y + size
			})
			.attr("stroke-width", 5)
			.attr("stroke", "green")
			.attr("class", "mark");

		d3.select(cellObj.node().parentNode).append("line")
			.attr({
				x1: x,
				x2: x + size,
				y1: y + size,
				y2: y
			})
			.attr("stroke-width", 5)
			.attr("stroke", "green")
			.attr("class", "mark");

	}

	function drawCircle(cellObj) {
		var data = getDimensions();
		var size = data.cellSize * 0.4;
		var x = parseFloat(cellObj[0][0].getAttribute("x")) + data.cellSize * 0.5;
		var y = parseFloat(cellObj[0][0].getAttribute("y")) + data.cellSize * 0.5;

		cellObj.attr("data-status", "donut");

		d3.select(cellObj.node().parentNode).append("circle")
			.attr("r", size)
			.attr("cx", x)
			.attr("cy", y)
			.attr("stroke-width", 5)
			.attr("stroke", "green")
			.attr("class", "mark");
	}

	function renderField(field) {

		try {
			field = jQuery.parseJSON(field);
		} catch (err) {
			alert("There was an error.");
			return false;
		}

		d3.selectAll(".mark").remove();

		for (var i = 0; i < 6; i++) {
			for (var j = 0; j < 6; j++) {
				switch (field[i][j]) {
					case 0:
						drawCross(d3.select("#cell_" + i + "_" + j));
						break;
					case 1:
						drawCircle(d3.select("#cell_" + i + "_" + j));
						break;
					default:
				}
			}
		}

		return true;
	}

	function constructTable() {

		setDimensions();
		disableScroll();

		var data = getDimensions();

		svg = d3.select("#d3Canvas")
			.append("svg")
			.attr("height", data.height)
			.attr("width", data.width);

		var rects = [];
		rects.push(constructInnerField(false, false, "ULeft"));
		rects.push(constructInnerField(true, false, "URight"));
		rects.push(constructInnerField(false, true, "BLeft"));
		rects.push(constructInnerField(true, true, "BRight"));
	}

	function renderFieldAnimated(newField, rotatedField, direction) {
		return renderField(newField);
	}

	function allowMakingTurns() {
		allowTurns = true;
		console.log("Allow turns.");
		setHandlers();
		logToUser("Your turn. Please, select a cell.");
	}

	function disableMakingTurns() {
		console.log("Restrict turns.");
		allowTurns = false;
		d3.selectAll(".pentagoCell")
			.on("mouseover", null)
			.on("mouseout", null)
			.on("click", null);
		logToUser("Yoour opponents turn. Please, wait.");
	}

	return {
		initTable: constructTable,
		renderField: renderField,
		animateField: renderFieldAnimated,
		allowTurn: allowMakingTurns,
		restrictTurn: disableMakingTurns,
		resizeTable: resizeTable
	};

})();


var gameController = (function () {

	var mark;
	var isMyTurn;
	var waitIntervalHandler;
	var waitInterval = 2000;
	var viewController;

	function winHandler() {
		alert("You won!");
	}

	function loseHandler() {
		alert("You lost.");
	}

	function tieHandler() {
		alert("Tie!");
	}

	function endGame(result) {
		if (pentagoAPIWrapper.didIWin(result)) {
			winHandler();
			return;
		}
		if (pentagoAPIWrapper.didILose(result)) {
			loseHandler();
		}
		tieHandler();
	}

	function checkGameResult() {

		pentagoAPIWrapper.init(function (result) {
			console.log("Checking game result: " + result);

			if (!pentagoAPIWrapper.isGameInProgress(result)) {
				endGame(result);
			}
		}).gameResult();
	}

	function stopWaitingForTurn() {
		console.log("It's my turn, stop waitng.");
		isMyTurn = true;
		window.clearInterval(waitIntervalHandler);
		pentagoAPIWrapper.init(function (result) {
			viewController.animateField(result);
			checkGameResult();
			viewController.allowTurn();
		}).getField();

	}

	function waitForTurn() {
		waitIntervalHandler = window.setInterval(function () {

			pentagoAPIWrapper.init(function (result) {
				var myTurn = result;
				console.log("Waiting for turn: myTurn? " + myTurn);
				if (myTurn) {
					stopWaitingForTurn();
				}
			}).isMyTurn();


		}, waitInterval);
	}

	function drawInitialField() {
		pentagoAPIWrapper.init(function (result) {
			viewController.renderField(result);
		}).getField();
	}

	function initControler(suppliedViewController) {
		viewController = suppliedViewController;

		pentagoAPIWrapper.init(function (result) {
			mark = result ? 0 : 1;
		}).amICross();

		pentagoAPIWrapper.init(function (result) {
			isMyTurn = result;

			if (isMyTurn) {
				stopWaitingForTurn();
			} else {
				waitForTurn();
			}
		}).isMyTurn();

		drawInitialField();

	}

	function putMark(x, y, field, dir) {
		if (isMyTurn) {

			pentagoAPIWrapper.init(function (result) {
				if (viewController.animateField(result, field, dir)) {
					console.log("just made a turn: " + x + " " + y);
					viewController.restrictTurn();
					checkGameResult();
					waitForTurn();
				}
			}).makeTurn(x, y, mark, field, dir);

		}
	}

	return {
		putMark: putMark,
		drawInitialField: drawInitialField,
		init: initControler
	};

})();

var globalViewController = d3ViewController;

$(document).ready(function () {

	// D3 Init
	//d3ViewController.initTable();

	// Initialization
	globalViewController.initTable();
	gameController.init(globalViewController);

});

$(window).resize(function () {
	console.log("resize");
	globalViewController.resizeTable();
});



var tableViewController = (function () {

	// left: 37, up: 38, right: 39, down: 40,
	// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
	var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };
	var allowTurns;
	var handler = function () {
		var x = $(this).attr('id').split('_')[2];
		var y = $(this).attr('id').split('_')[1];
		var field;

		if (x < 3) {
			if (y < 3) {
				field = 0;
			} else {
				field = 3;
			}
		} else {
			if (y < 3) {
				field = 1;
			} else {
				field = 2;
			}
		}


		gameController.putMark(x, y, field, 0);
	};

	function logToUser(text) {
		$("#userLog").text(text);
	}

	function preventDefault(e) {
		e = e || window.event;
		if (e.preventDefault)
			e.preventDefault();
		e.returnValue = false;
	}

	function preventDefaultForScrollKeys(e) {
		if (keys[e.keyCode]) {
			preventDefault(e);
			return false;
		}
	}

	function disableScroll() {
		if (window.addEventListener) // older FF
			window.addEventListener('DOMMouseScroll', preventDefault, false);
		window.onwheel = preventDefault; // modern standard
		window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
		window.ontouchmove = preventDefault; // mobile
		document.onkeydown = preventDefaultForScrollKeys;
	}

	function enableScroll() {
		if (window.removeEventListener)
			window.removeEventListener('DOMMouseScroll', preventDefault, false);
		window.onmousewheel = document.onmousewheel = null;
		window.onwheel = null;
		window.ontouchmove = null;
		document.onkeydown = null;
	}

	function resizeTable() {
		var width = screen.availWidth;
		var height = screen.availHeight;

		if (width > height) {
			//$("#mainPanel").height(height - 2 * 50);
		} else {
			//$("#mainPanel").width(width);
		}

		var cellHeight = $(".innerField td").width();
		$(".pentagoCell").css({ 'height': cellHeight + 'px' });
		$(".pentagoCell").css({ 'font-size': cellHeight * 45 / 124 + 'pt' });
	}

	function setHandlers() {
		for (var i = 0; i < 6; i++) {
			for (var j = 0; j < 6; j++) {
				if ($("#cell_" + i + "_" + j).text() === '') {
					$("#cell_" + i + "_" + j).click(handler);
					$("#cell_" + i + "_" + j).addClass("clickableCell");
				}
			}
		}
	}

	function constructInnerField(iStart, iEnd, jStart, jEnd, name) {
		var html = "";

		for (var i = iStart; i < iEnd; i++) {
			html += "<tr>";
			for (var j = jStart; j < jEnd; j++) {
				html += "<td class='pentagoCell' style='width: 33%;' id='cell_" + i + "_" + j + "'>" + i + " " + j + "</td>";
			}
			html += "</tr>";
		}

		$("#" + name + "TBody").append(html);
	}

	function constructTable() {
		constructInnerField(0, 3, 0, 3, "ULeft");
		constructInnerField(0, 3, 3, 6, "URight");
		constructInnerField(3, 6, 0, 3, "BLeft");
		constructInnerField(3, 6, 3, 6, "BRight");

		resizeTable();
		disableScroll();
	}

	function renderField(field) {

		try {
			field = jQuery.parseJSON(field);
		} catch (err) {
			alert("There was an error.");
			return false;
		}

		for (var i = 0; i < 6; i++) {
			var mark = '';
			for (var j = 0; j < 6; j++) {
				switch (field[i][j]) {
					case 0:
						mark = '&#10060;';
						break;
					case 1:
						mark = '&#9711;';
						break;
					case 2:
						mark = '';
						break;
					default:
				}

				$("#cell_" + i + "_" + j).html(mark);
			}
		}

		return true;
	}

	function renderFieldAnimated(newField, rotatedField, direction) {
		return renderField(newField);
	}

	function allowMakingTurns() {
		allowTurns = true;
		console.log("Allow turns.");
		setHandlers();
		logToUser("Your turn. Please, select a cell.");
	}

	function disableMakingTurns() {
		console.log("Restrict turns.");
		allowTurns = false;
		$(".pentagoCell").off("click", handler);
		$(".pentagoCell").removeClass("clickableCell");
		logToUser("Yoour opponents turn. Please, wait.");
	}

	return {
		initTable: constructTable,
		renderField: renderField,
		animateField: renderFieldAnimated,
		allowTurn: allowMakingTurns,
		restrictTurn: disableMakingTurns,
		resizeTable: resizeTable
	};

})();
