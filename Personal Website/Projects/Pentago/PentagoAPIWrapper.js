var pentagoAPIWrapper = (function () {
	var apiUrl = "/api/projects/pentago/";
	var AJAXReturnValue;

	/* successCallback has a signature
	 * void function successCallback(data) {}
	 * It must set AJAXReturnValue
	 * 
	 * data and successCallback are optional
	 * 
	*/
	function performAJAX(async, url, data, successCallback) {
		data = data || {};

		successCallback = successCallback || function (data) {
			AJAXReturnValue = data;
		};

		AJAXReturnValue = null;

		jQuery.ajaxSetup({ async: async });

		$.post(apiUrl + url, "=" + JSON.stringify(data)).done(successCallback)
			.fail(function (xhr, textStatus, errorThrown) {
				alert(textStatus);
			});

		jQuery.ajaxSetup({ async: true });

		return AJAXReturnValue;
	}

	function hostGame() {
		return performAJAX(false, "host");
	}

	function joinGame(gameCode) {
		return performAJAX(false, "join", { gameCode: gameCode });
	}

	function checkJoin(gameCode) {
		return performAJAX(false, "checkjoin", { gameCode: gameCode });
	}

	return {
		hostGame: hostGame,
		joinGame: joinGame,
		checkJoin: checkJoin
	};

})();

// HELPERS

function async(fn, callback) {
	setTimeout(function () {
		fn();
		callback();
	}, 0);
}