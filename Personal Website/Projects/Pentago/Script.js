// Globals

var mode;
var hostCode = "";
var checkJoinIntervalID;

$(document).ready(fixPanel);

$(window).resize(fixPanel);

function fixPanel() {
	$(".centering").css({
		"margin-top": (0.9 * ($(window).height() - $(".panel").height()) / 2) + "px"
	});
}

function configlayBtn(mode) {
	var text = "";
	var btnClass = "";

	switch (mode) {
		case "easy":
			text = "easy";
			btnClass = "success";
			break;
		case "medium":
			text = "medium";
			btnClass = "warning";
			break;
		case "hard":
			text = "hard";
			btnClass = "danger";
			break;
		default:
	}

	$("#playBtn").text("Play " + text + " mode");

	$("#playBtn").removeClass("btn-success");
	$("#playBtn").removeClass("btn-warning");
	$("#playBtn").removeClass("btn-danger");

	$("#playBtn").addClass("btn-" + btnClass);
	mode = text;
}

$(document).ready(function () {

	// Single player

	configlayBtn("easy");

	$("#playEasyBtn").click(function () {
		configlayBtn("easy");
	});
	$("#playMediumBtn").click(function () {
		configlayBtn("medium");
	});
	$("#playHardBtn").click(function () {
		configlayBtn("hard");
	});

	// Multiplayer

	$('#multiplayerTabs a').click(function (e) {
		e.preventDefault();
		$(this).tab('show');
	});

	$("#hostBtn").click(getHostCode);

	$("#enterCodeBtn").click(function () {
		if ($("#multiplayerCode").val().length > 0) {
			joinGame($("#multiplayerCode").val());
		}
	});
});

function getHostCode() {
	if (hostCode.length === 0) {
		async(function () {
			hostCode = pentagoAPIWrapper.hostGame();
			$("#hostCode").text(hostCode);
		}, lookForJinedPlayer);

		//lookForJinedPlayer();
	}
}

function joinGame(gameCode) {
	if (pentagoAPIWrapper.joinGame(gameCode)) {
		$("#playMultiBtn").removeClass("disabled");
		$("#host").hide();
		$("#join").hide();
		$("#multiplayerTabs").hide();
		$("#readyLabel").show();
	}
}

function lookForJinedPlayer() {

	checkJoinIntervalID = window.setInterval(function () {

		if (pentagoAPIWrapper.checkJoin(hostCode)) {
			window.clearInterval(checkJoinIntervalID);

			$("#playMultiBtn").removeClass("disabled");
			$("#host").hide();
			$("#join").hide();
			$("#multiplayerTabs").hide();
			$("#readyLabel").show();
		} else {
			console.log("Not joined yet...");
		};

	}, 3000);
}
