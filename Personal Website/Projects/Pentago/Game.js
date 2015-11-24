var viewController = (function () {

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



var gameController = (function () {

	var mark;
	var isMyTurn;
	var waitIntervalHandler;
	var waitInterval = 2000;

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

		/*
		var result = pentagoAPIWrapper.gameResult();
		console.log("Checking game result: " + result);
		if (!pentagoAPIWrapper.isGameInProgress(result)) {
			endGame(result);
		}*/
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

	function initControler() {

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


	}
	/*
	function testMove() {
		viewController.renderField(pentagoAPIWrapper.makeTurn(0, 0, mark, 0, 1));
	}*/

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
		//testMove: testMove
	};

})();

$(document).ready(function () {

	// Initialization
	viewController.initTable();
	gameController.drawInitialField();
	gameController.init();


	//gameController.testMove();
});

$(window).resize(function () {
	console.log("resize");
	viewController.resizeTable();
});