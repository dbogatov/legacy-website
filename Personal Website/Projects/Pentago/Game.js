var viewController = (function () {

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

	function resizeTable() {
		var height = $("#gameTable td").width();
		$(".gameCell").css({ 'height': height + 'px' });
	}

	function setHandlers() {
		for (var i = 0; i < 6; i++) {
			for (var j = 0; j < 6; j++) {
				if ($(this).text() === '') {
					$("#cell_" + i + "_" + j).click(handler);
					$("#cell_" + i + "_" + j).addClass("clickableCell");
				}
			}
		}
	}

	function constructTable() {
		var html = "";

		for (var i = 0; i < 6; i++) {
			html += "<tr>";
			for (var j = 0; j < 6; j++) {
				html += "<td class='pentagoCell' id='cell_" + i + "_" + j + "'>" + i + " " + j + "</td>";
			}
			html += "</tr>";
		}

		$("#gameTBody").append(html);

		resizeTable();

		//setHandlers();
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

		return true;
	}

	function renderFieldAnimated(newField, rotatedField, direction) {
		return renderField(newField);
	}

	function allowMakingTurns() {
		allowTurns = true;
		console.log("Allow turns.");
		setHandlers();
	}

	function disableMakingTurns() {
		console.log("Restrict turns.");
		allowTurns = false;
		$(".pentagoCell").off("click", handler);
		$(".pentagoCell").removeClass("clickableCell");
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