function openPlace(x, y) {
	sendJSON(
        "openPlace",
        //JSON.stringify(
                {
                	x: x,
                	y: y
                },
        //    ),
        parseAnswer
        );
}

function runSolver(field) {
	sendJSON(
        "runSolver",
        JSON.stringify(
            {
            	"json": field
            }
        ),
        parseSolverResponse
        );
}

function parseSolverResponse(msg) {
	$('.fieldCell').css({ 'border': '1px solid darkgrey' });
	var move = jQuery.parseJSON(msg.d);
	console.log(move);
	if (!move.length) {
		$('#checkBox').prop('checked', false);
		clearInterval(solverHolder);
		solverHolder = 0;
		return;
	}
	var action;
	var message = "";
	var pleaseStop = false;
	for (var i = 0; i < move.length; i++) {
		switch (move[i].action) {
			case 0:
				action = "open";
				if (!solverHolder)
					$('#' + move[i].x + '-' + move[i].y).css({ 'border': '1px solid green', 'background': 'lightgreen' });
				else
					openPlace(move[i].x, move[i].y)
				pleaseStop = true;
				break;
			case 1:
				action = "flag";
				if (!solverHolder)
					$('#' + move[i].x + '-' + move[i].y).css({ 'border': '1px solid red', 'background': '#C80000' });
				else {
					$('#' + move[i].x + '-' + move[i].y).html($('#imgHolder').html());
					fieldArray[move[i].y][move[i].x] = 'f';
				}
				break;
			default:
				action = "KILL"
		}
		message += "You should " + action + " the place at x=" + move[i].x + ", y=" + move[i].y + "\n";
	}
	if (!pleaseStop) {
		$('#checkBox').prop('checked', false);
		clearInterval(solverHolder);
		solverHolder = 0;
		return;
	}


	//alert(message);
}

function getName() {
	sendJSON(
        "getNickName",
        "{}",
        alertAnswer
        );
}

function getAllMines() {
	if (cellOpened == sizeX * sizeY - mines)
		$('.result').html($('#win').html());
	else
		$('.result').html($('#loose').html());
	sendJSON(
        "getAllMines",
        "{}",
        parseMines
        );
}

function checkForResult() {
	sendJSON(
        "isWon",
        "{}",
        alertIfTrue
        );
	sendJSON(
        "isLost",
        "{}",
        alertIfTrue
        );
}

function win() {
	sendJSON(
        "win",
        "{}",
        alertAnswer
        );
}

function isGameRunning() {
	sendJSON(
        "isGameRunning",
        "{}",
        alertAnswer
        );
}

function parseMines(msg) {
	var changes = jQuery.parseJSON(msg.d);

	for (var i = 0; i < changes.length; i++) {
		var buttonid = changes[i]["x"] + "-" + changes[i]["y"];

		$("#" + buttonid).html('<img src="/images/Minesweeper/mine.jpg">');

	}
}

function parseAnswer(msg) {
	var changes = jQuery.parseJSON(msg.d);
	var preEnd = false;

	for (var i = 0; i < changes.length; i++) {
		cellOpened++;
		if (cellOpened == sizeX * sizeY - mines)
			if (!end)
				preEnd = true;
		var buttonid = changes[i]["x"] + "-" + changes[i]["y"];
		if (!end)
			$("#" + buttonid).css({ 'background-color': 'white' });
		if (changes[i]["place"]["isMine"]) {
			$("#" + buttonid).html('<img src="/images/Minesweeper/mine.jpg">');
			if (!end) {
				preEnd = true;
				$("#" + buttonid).css({ 'background-color': 'white' });
			}
		}
		else if (!end) {
			if (changes[i]["place"]["number"] != 0)
				$("#" + buttonid).html(changes[i]["place"]["number"]);
			else
				$("#" + buttonid).html(' ');
			var num = parseInt($("#" + buttonid).html());
			switch (num) {
				case 1: { $("#" + buttonid).css({ 'color': 'blue' }); break; }
				case 2: { $("#" + buttonid).css({ 'color': 'green' }); break; }
				case 3: { $("#" + buttonid).css({ 'color': 'red' }); break; }
				case 4: { $("#" + buttonid).css({ 'color': 'purple' }); break; }
				case 5: { $("#" + buttonid).css({ 'color': 'black' }); break; }
				case 6: { $("#" + buttonid).css({ 'color': 'maroon' }); break; }
				case 7: { $("#" + buttonid).css({ 'color': 'gray' }); break; }
				case 8: { $("#" + buttonid).css({ 'color': 'turquoise' }); break; }
				default:
					$("#" + buttonid).css({ 'color': 'white', 'transition-duration': '0s' });

			}
		}

		if ($("#" + buttonid).html() == ' ')
			fieldArray[changes[i]["y"]][changes[i]["x"]] = '0';
		else
			fieldArray[changes[i]["y"]][changes[i]["x"]] = $("#" + buttonid).html();

		if (preEnd) {
			end = true;
			preEnd = false;
			checkForResult();
		}
	}
}



function alertAnswer(msg) {
	alert(msg.d);
}

function alertIfTrue(msg) {
	if (msg.d) {
		getAllMines();
		clearInterval(timerContainer);
	}
}

function startGame() {
	sendJSON(
        "startGame",
        {
        	width: $('#fieldWidth').val(),
        	height: $('#fieldHeight').val(),
        	minesNumber: $('#minesNumber').val(),
        	userName: $('#userName').val(),
        	mode: $("#mode").val()
        },
        alertAnswer
	);
}

// places is an array of structures Coordinate
// Coordinate is a structure which has: int x, int y
// One of the ways of creating such array is
//
// var places = [];
// places.push(
//		{
//			x: yourX,
//			y: yourY
//		}
//	);
//	...
// 
function openPlaces(places) {
	sendJSON(
        "openPlaces",
        /*JSON.stringify(
                {
                	"places": places
                }
           )*/
		{
			places: places
		},
            parseAnswer
        );
}

function sendJSON(path, data, answerCallback) {

	/*
		$.ajax({
			url: "/api/Minesweeper/" + path,
			type: 'POST',
			contentType: 'application/x-www-form-urlencoded; charset=utf-8',
			data: r,
			success: answerCallback,
			error: errorAJAX
		});
		*/

	$.post("/api/Minesweeper/" + path, data)
			.done(function (result) {
				var data = {
					d: result
				};
				answerCallback(data);
			})
			.fail(errorAJAX);


	/*
    $.ajax({
        type: "POST",
        url: "Default.aspx/"+path,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: errorAJAX,
        success: answerCallback
    });*/
}

function errorAJAX(xhr, ajaxOptions, thrownError) {
	var message = "AJAX error" + "\n" + xhr.status + "\n" + xhr.responseText;
	alert(message);
}













