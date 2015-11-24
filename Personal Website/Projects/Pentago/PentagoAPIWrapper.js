var pentagoAPIWrapper = (function () {
	var apiUrl = "/api/projects/pentago/";
	var ajaxReturnValue;

	/* successCallback has a signature
	 * void function successCallback(data) {}
	 * It must set AJAXReturnValue
	 * 
	 * data and successCallback are optional
	 * 
	*/
	function performAjax(async, url, data, successCallback) {
		data = data || {};

		successCallback = successCallback || function (data) {
			ajaxReturnValue = data;
		};

		ajaxReturnValue = null;

		jQuery.ajaxSetup({ async: async });

		$.post(apiUrl + url, "=" + JSON.stringify(data)).done(successCallback)
			.fail(function (xhr, textStatus, errorThrown) {
				alert(textStatus);
			});

		jQuery.ajaxSetup({ async: true });

		return ajaxReturnValue;
	}

	function hostGame() {
		return performAjax(false, "host");
	}

	function joinGame(gameCode) {
		return performAjax(false, "join", { gameCode: gameCode });
	}

	function checkJoin(gameCode) {
		return performAjax(false, "checkjoin", { gameCode: gameCode });
	}

	function getField() {
		return performAjax(false, "getfield");
	}

	function isMyMarkCross() {
		return performAjax(false, "ismymarkcross");
	}

	function isMyTurn() {
		return performAjax(false, "ismyturn");
	}

	function getGameResult() {
		return performAjax(false, "getgameresult");
	}

	function makeTurn(x, y, mark, field, dir) {
		return performAjax(false, "maketurn", {
			x: x,
			y: y,
			mark: mark,
			field: field,
			direction: dir
		});
	}

	function didIWin(result) {
		return result === 0;
	}

	function didILose(result) {
		return result === 1;
	}

	function isGameInProgress(result) {
		return result === 3;
	}

	return {
		hostGame: hostGame,
		joinGame: joinGame,
		checkJoin: checkJoin,
		getField: getField,
		amICross: isMyMarkCross,
		makeTurn: makeTurn,
		isMyTurn: isMyTurn,
		gameResult: getGameResult,
		didIWin: didIWin,
		didILose: didILose,
		isGameInProgress: isGameInProgress
	};

})();

// HELPERS

function async(fn, callback) {
	setTimeout(function () {
		fn();
		callback();
	}, 0);
}