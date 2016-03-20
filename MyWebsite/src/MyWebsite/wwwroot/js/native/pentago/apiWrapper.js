var pentagoAPIWrapper = (function() {
	var apiUrl = "/api/projects/pentago/";
	var ajaxReturnValue;
	var generalCallback;

	/* successCallback has a signature
	 * void function successCallback(data) {}
	 * It must set AJAXReturnValue
	 * 
	 * data and successCallback are optional
	 * 
	*/
	function performAjax(async, url, data, successCallback) {
		data = data || {};

		successCallback = successCallback || function(data) {
			ajaxReturnValue = data;
		};

		ajaxReturnValue = null;

		jQuery.ajaxSetup({ async: async });

		$.post(apiUrl + url, "=" + JSON.stringify(data)).done(successCallback)
			.fail(function(xhr, textStatus, errorThrown) {
				alert(textStatus);
			});

		jQuery.ajaxSetup({ async: true });

		return ajaxReturnValue;
	}

	/*
	*	Requires init() first.
	*/
	function performAjaxAsync(url, data) {
		data = data || {};

		var successCallback = generalCallback;

		ajaxReturnValue = null;

		$.post(apiUrl + url, data)
			.done(successCallback)
			.fail(function(xhr, textStatus, errorThrown) {
				alert(textStatus);
			});
	}

	function hostGame() {
		performAjaxAsync("host");
	}

	function joinGame(gameCode) {
		performAjaxAsync("join", { gameCode: gameCode });
	}

	function checkJoin(gameCode) {
		performAjaxAsync("checkjoin", { gameCode: gameCode });
	}

	function getField() {
		performAjaxAsync("getfield");
	}

	function isMyMarkCross() {
		performAjaxAsync("ismymarkcross");
	}

	function isMyTurn() {
		performAjaxAsync("ismyturn");
	}

	function getGameResult() {
		performAjaxAsync("getgameresult");
	}

	function makeTurn(x, y, mark, field, dir) {
		performAjaxAsync("maketurn", {
			x: x,
			y: y,
			mark: mark,
			field: field,
			direction: dir
		});
	}

	function getLastTurn() {
		performAjaxAsync("getlastturn");
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

	function init(callback) {
		generalCallback = callback;
		return this;
	}

	return {
		init: init,

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
		isGameInProgress: isGameInProgress,
		getLastTurn: getLastTurn
	};

})();

// HELPERS

function async(fn, callback) {
	setTimeout(function() {
		fn();
		callback();
	}, 0);
}