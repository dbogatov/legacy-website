var d3ViewController = (function () {

	var width;

	function setDimensions() {
		var svgWidth = $("#mainPanel").width();
		var svgHeight = Math.max($(document).height(), $(window).height()) - $("#mainPanel").height() - 20 - 20;

		width = Math.min(svgHeight, svgWidth);
	}

	function getDimensions() {
		var height = width;
		var crossBorderFraction = 0.012;
		var cellOffsetCorrection = 0.0;
		var markStrokeFractionOfCellSize = 0.05;

		return {
			width: width,
			height: height,
			fieldSize: width * (0.5 - 2 * crossBorderFraction),
			fieldOffset: width * crossBorderFraction,
			cellSize: width * (0.5 - 2 * crossBorderFraction) * ((1 / 3) - 2 * crossBorderFraction),
			cellOffset: width * (0.5 - 2 * crossBorderFraction) * crossBorderFraction,
			cellOffsetCorrection: width * cellOffsetCorrection,
			fieldColor: "#0B0B61",
			cellColor: "#380B61",
			cellOnHoverColor: "white",
			fieldPlaceholderColor: "white",
			fieldPlaceholderOpacity: 0.5,
			fieldPlaceholderSelectedOpacity: 0.1,
			arrowColor: "#819FF7",
			arrowOnHoverColor: "black",
			crossColor: "#FA5858",
			circleColor: "#F3E2A9",
			markStroke: width * (0.5 - 2 * crossBorderFraction) * ((1 / 3) - 2 * crossBorderFraction) * markStrokeFractionOfCellSize
		};
	}

	function unsetHandlers() {
		d3.selectAll(".pentagoCell")
			.on("mouseover", null)
			.on("mouseout", null)
			.on("click", null);
	}

	// left: 37, up: 38, right: 39, down: 40,
	// spacebar: 32, pageup: 33, pagedown: 34, end: 35, home: 36
	var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };
	var allowTurns;
	var svg;
	var command = {
		x: null,
		y: null,
		field: null,
		dir: null
	};

	function logToUser(text) {
		$("#userLog").text(text);
	}

	function performTurn() {
		gameController.putMark(command.x, command.y, command.field, command.dir);
		command = null;
	}

	function cleanUp() {
		d3.selectAll(".rightArrow").remove();
		d3.selectAll(".leftArrow").remove();
		d3.selectAll(".selectedFieldPlaceholder").remove();
		d3.selectAll(".fieldPlaceholder").remove();
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
			.attr("stroke-width", data.markStroke)
			.attr("stroke", data.crossColor)
			.attr("class", "mark");

		d3.select(cellObj.node().parentNode).append("line")
			.attr({
				x1: x,
				x2: x + size,
				y1: y + size,
				y2: y
			})
			.attr("stroke-width", data.markStroke)
			.attr("stroke", data.crossColor)
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
			.attr("stroke-width", data.markStroke)
			.attr("stroke", data.circleColor)
			.attr("fill", data.cellColor)
			.attr("class", "mark");
	}

	function arraowHandler() {
		command.dir = $(this).attr('class') === "rightArrow" ? 0 : 1;

		cleanUp();
		performTurn();

	}

	function arrowOnMouseIn() {
		var data = getDimensions();

		var arrow = d3.select(this);
		var arrowClass = $(this).attr('class');
		d3.selectAll("." + arrowClass).style("fill", data.arrowOnHoverColor);
	}

	function arrowOnMouseOut() {
		var data = getDimensions();

		var arrow = d3.select(this);
		var arrowClass = $(this).attr('class');
		d3.selectAll("." + arrowClass).style("fill", data.arrowColor);
	}

	function putArrow(field, startAngle, endAngle, lineData, className) {
		var placeholderX = parseFloat(field.getAttribute("x"));
		var placeholderY = parseFloat(field.getAttribute("y"));
		var placeholderWidth = parseFloat(field.getAttribute("width"));
		var placeholderHeight = parseFloat(field.getAttribute("height"));

		var data = getDimensions();

		var arc = d3.svg.arc()
			.innerRadius(placeholderWidth * 0.5 * ((1 / 3) + (2 / 3) * (1 / 3)))
			.outerRadius(placeholderWidth * 0.5 * ((1 / 3) + (2 / 3) * (2 / 3)))
			.startAngle(startAngle)
			.endAngle(endAngle);

		d3.select(field.parentNode).append("path")
			.attr("d", arc)
			.attr("transform", "translate(" + (placeholderX + placeholderWidth / 2) + ", " + (placeholderY + placeholderHeight / 2) + ")")
			.attr("fill", data.arrowColor)
			.attr("class", className)
			.on("mouseover", arrowOnMouseIn)
			.on("mouseout", arrowOnMouseOut)
			.on("click", arraowHandler);

		var lineFunction = d3.svg.line()
			.x(function (d) { return d.x; })
			.y(function (d) { return d.y; })
			.interpolate("linear");

		d3.select(field.parentNode).append("path")
			.attr("d", lineFunction(lineData))
			.attr("fill", data.arrowColor)
			.attr("class", className)
			.on("mouseover", arrowOnMouseIn)
			.on("mouseout", arrowOnMouseOut)
			.on("click", arraowHandler);
	}

	function showDirectionSelection(field) {

		var placeholderX = parseFloat(field.getAttribute("x"));
		var placeholderY = parseFloat(field.getAttribute("y"));
		var placeholderWidth = parseFloat(field.getAttribute("width"));
		var placeholderHeight = parseFloat(field.getAttribute("height"));

		var lineData = [
			{ "x": placeholderX + placeholderWidth * (2 / 3), "y": placeholderY + placeholderHeight * (1 / 2) },
			{ "x": placeholderX + placeholderWidth, "y": placeholderY + placeholderHeight * (1 / 2) },
			{ "x": placeholderX + placeholderWidth * (5 / 6), "y": placeholderY + placeholderHeight * (2 / 3) }
		];

		putArrow(field, 0, Math.PI / 2, lineData, "rightArrow");

		lineData = [
			{ "x": placeholderX + placeholderWidth * (1 / 3), "y": placeholderY + placeholderHeight * (1 / 2) },
			{ "x": placeholderX, "y": placeholderY + placeholderHeight * (1 / 2) },
			{ "x": placeholderX + placeholderWidth * (1 / 6), "y": placeholderY + placeholderHeight * (2 / 3) }
		];

		putArrow(field, -Math.PI / 2, 0, lineData, "leftArrow");

		logToUser("Finally, pick a rotational direction.");
	}

	var fieldSelectionHandler = function () {
		var field = d3.select(this);
		var data = getDimensions();

		command.field = parseInt($(this).attr('id').split('_')[1], 10);

		field
			.attr("class", "selectedFieldPlaceholder")
			.on("mouseover", null)
			.on("mouseout", null)
			.on("click", null);

		d3.selectAll(".fieldPlaceholder").remove();
		field.style("fill-opacity", data.fieldPlaceholderOpacity);

		showDirectionSelection(field[0][0]);
	}

	function showSelectField() {
		var fields = d3.selectAll(".field")[0];
		var data = getDimensions();

		unsetHandlers();

		for (var i = 0; i < fields.length; i++) {
			var field = fields[i];

			var placeholderX = field.getAttribute("x");
			var placeholderY = field.getAttribute("y");
			var placeholderWidth = field.getAttribute("width");
			var placeholderHeight = field.getAttribute("height");

			d3.select(field.parentNode).append("rect")
				.attr({
					x: placeholderX,
					y: placeholderY,
					width: placeholderWidth,
					height: placeholderHeight
				})
				.attr("fill", data.fieldPlaceholderColor)
				.attr("fill-opacity", data.fieldPlaceholderOpacity)
				.attr("class", "fieldPlaceholder")
				.attr("id", "fieldPlaceholder_" + i)
			.on("mouseover", function () {
				d3.select(this).style("fill-opacity", data.fieldPlaceholderSelectedOpacity);
			})
			.on("mouseout", function () {
				d3.select(this).style("fill-opacity", data.fieldPlaceholderOpacity);
			})
			.on("click", fieldSelectionHandler);
		}

		logToUser("Now, select a field you want to rotate.");
	}

	var handler = function () {

		d3.select(this).style("fill", getDimensions().cellColor);

		var x = $(this).attr('id').split('_')[2];
		var y = $(this).attr('id').split('_')[1];

		if (gameController.myMark() === 0) {
			drawCross(d3.select(this));
		} else {
			drawCircle(d3.select(this));
		}

		command = new Object();
		command.x = x;
		command.y = y;

		showSelectField();
	};

	function putMark(x, y, mark) {
		if (mark === 0) {
			drawCross(d3.select("#cell_" + y + "_" + x));
		} else {
			drawCircle(d3.select("#cell_" + y + "_" + x));
		}
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

		var idOffsetI = isYOffset ? 3 : 0;
		var idOffsetJ = isXOffset ? 3 : 0;

		var xOffset = isXOffset ? data.fieldSize + 3 * data.fieldOffset : data.fieldOffset;
		var yOffset = isYOffset ? data.fieldSize + 3 * data.fieldOffset : data.fieldOffset;

		var field = svg.append("g")
			.attr("id", name)
			.attr("data-cx", xOffset + data.fieldSize / 2)
			.attr("data-cy", yOffset + data.fieldSize / 2);

		field.append("rect")
			.attr("x", xOffset)
			.attr("y", yOffset)
			.attr("rx", data.fieldSize * 0.02)
			.attr("ry", data.fieldSize * 0.02)
			.attr("width", data.fieldSize)
			.attr("height", data.fieldSize)
			.attr("fill", data.fieldColor)
			.attr("class", "field");

		for (var i = 0; i < 3; i++) {
			for (var j = 0; j < 3; j++) {
				field.append("rect")
					.attr("x", xOffset + data.cellOffset + j * data.cellSize + j * 2 * data.cellOffset)
					.attr("y", yOffset + data.cellOffset + i * data.cellSize + i * 2 * data.cellOffset)
					.attr("rx", data.cellSize * 0.1)
					.attr("ry", data.cellSize * 0.1)
					.attr("width", data.cellSize)
					.attr("height", data.cellSize)
					.attr("fill", data.cellColor)
					.attr("class", "pentagoCell")
					.attr("id", "cell_" + (i + idOffsetI) + "_" + (j + idOffsetJ))
					.attr("data-status", "empty");
			}
		}

		return field;
	}

	function renderField(field) {

		try {
			field = jQuery.parseJSON(field);
		} catch (err) {
			alert("There was an error.");
			return false;
		}

		d3.selectAll(".mark").remove();
		d3.selectAll(".pentagoCell").attr("data-status", "empty");

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
			.attr("width", data.width)
			.attr("id", "d3Svg");

		var rects = [];
		rects.push(constructInnerField(false, false, "ULeft"));
		rects.push(constructInnerField(true, false, "URight"));
		rects.push(constructInnerField(false, true, "BLeft"));
		rects.push(constructInnerField(true, true, "BRight"));
	}

	function resizeTable(field) {
		d3.select("svg").remove();
		setDimensions();
		constructTable();
		gameController.drawInitialField(field);
	}

	function fieldEnumToClassName(field) {
		switch (field) {
			case 0:
				return "ULeft";
			case 1:
				return "URight";
			case 2:
				return "BLeft";
			case 3:
				return "BRight";
			default:
				return "";
		}
	}

	function renderFieldAnimated(newField, rotatedField, direction) {

		var field = fieldEnumToClassName(rotatedField);

		if (field.length > 0) {
			var fieldObj = d3.select("#" + fieldEnumToClassName(rotatedField))[0][0];

			var cx = fieldObj.getAttribute("data-cx");
			var cy = fieldObj.getAttribute("data-cy");
			//var huy = "rotate(90 " + cx + " " + cy + ")";

			d3.select("#" + fieldEnumToClassName(rotatedField))
				.transition()
				.attr("transform", "rotate(" + (direction === 0 ? "90" : "-90") + " " + (cx) + " " + (cy) + ")")
				.duration(1500)
				.each("end", function () {
					resizeTable(newField);
				});
		} else {
			resizeTable(newField);
		}

		return true;
	}

	function allowMakingTurns() {
		allowTurns = true;
		console.log("Allow turns.");
		setHandlers();
		logToUser("Your turn. Please, select a cell. Your mark is " + (gameController.myMark() === 0 ? "CROSS" : "CIRCLE" + "."));
	}

	function disableMakingTurns() {
		console.log("Restrict turns.");
		allowTurns = false;
		unsetHandlers();
		logToUser("Your opponent\'s turn. Please, wait.");
	}

	return {
		initTable: constructTable,
		renderField: renderField,
		animateField: renderFieldAnimated,
		allowTurn: allowMakingTurns,
		restrictTurn: disableMakingTurns,
		resizeTable: resizeTable,
		putMark: putMark
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
			return;
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

		pentagoAPIWrapper.init(function (turnResult) {

			if (turnResult != null) {
				turnResult = JSON.parse(turnResult);
				viewController.putMark(turnResult.x, turnResult.y, turnResult.mark);

				pentagoAPIWrapper.init(function (fieldResult) {
					viewController.animateField(fieldResult, turnResult.field, turnResult.direction);
					checkGameResult();
					setTimeout(function () {
						viewController.allowTurn();
					}, 2000);

				}).getField();
			} else {
				pentagoAPIWrapper.init(function (fieldResult) {
					viewController.renderField(fieldResult);
					checkGameResult();
					viewController.allowTurn();
				}).getField();
			}
		}).getLastTurn();
	}

	function waitForTurn() {
		viewController.restrictTurn();

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

	function drawInitialField(field) {
		if (field !== undefined) {
			viewController.renderField(field);
		} else {
			pentagoAPIWrapper.init(function (result) {
				viewController.renderField(result);
			}).getField();
		}
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

	function getMyMark() {
		return mark;
	}

	function putMark(x, y, field, dir) {
		if (isMyTurn) {

			pentagoAPIWrapper.init(function (result) {
				if (viewController.animateField(result, field, dir)) {
					console.log("just made a turn: " + x + " " + y);
					checkGameResult();
					waitForTurn();
				}
			}).makeTurn(x, y, mark, field, dir);

		}
	}

	return {
		putMark: putMark,
		drawInitialField: drawInitialField,
		init: initControler,
		myMark: getMyMark
	};

})();

var globalViewController = d3ViewController;

$(document).ready(function () {
	// Initialization
	globalViewController.initTable();
	gameController.init(globalViewController);
});

$(window).resize(function () {
	console.log("resize");
	globalViewController.resizeTable();
});