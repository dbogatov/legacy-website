var fieldArray = [];

//function that fits sizes and fonts on page due to screen size
function screenSize() {
	$('body').css({ 'height': $(window).height(), 'font-size': $(window).height() / 20 });
	$('.page, .footer').css({ 'margin-top': $(window).height() * 0.05 });
	$('input, span').css({ 'height': $(window).height() * 0.05, 'margin-top': $(window).height() * 0.035 });
	$('#loading, #done').css({ 'margin-top': '-20%', 'font-size': $(window).height() / 35 });
	$('#lblMinesweeper').css({ 'height': 'auto' });
	$('#lblMinesweeper, .newGameButton, .leaderBoardButton').css({ 'margin-top': '0.5%' });
	$('#ukr_button, #eng_button').css({ 'margin-top': '4.5%' });
	$('.sizeTip, .mineAmountTip').css({ 'border-radius': '20%' });
	cellSize = Math.round(Math.min((0.51 * ($(window).height()) - (sizeY * 2) - 4) / sizeY, (0.7 * $(window).width() - (sizeX * 2) - 4) / sizeX));
	if (cellSize < $(window).height() / 40) {
		cellSize = $(window).height() / 40;
		$('.field').css({ 'overflow-y': 'scroll' });
	}
	else
		$('.field').css({ 'overflow-y': 'none' });
	$('.fieldCell').css({ 'height': cellSize, 'width': cellSize, 'font-size': cellSize * 0.9 });
	$('.header input').css({ 'font-size': $(window).height() / 35 });
	$('.leaderBoard table tbody').height($('.sheet').height() * 0.7);
	$('.leaderBoard table tr').width('100%');
	$('.footer').css({ 'font-size': $(window).height() / 35 });
	$('.copyright').css({ 'font-size': $(window).height() / 70 });
	$('.rightBar span').css({ 'font-size': '60%' });
	$('#lblSolverRun').css({ 'font-size': '40%' });
	$('.leaderBoard span').css({ 'height': 'auto', 'margin-top': '0px' });
	$('.startGame, .restartGame').css({ 'height': 'auto', 'margin-top': '0px' });
}

//constants of minimum or maximum sizes of field
var minimumFieldSize = 5, maximumFieldSize = 30;
//indicator of was menu options chosen or not
var flag = 0;
//parameters of game																											   
var sizeX, sizeY, mines, flags = 0;
//size of field cell
var cellSize;

var username;

var mode;

var cellOpened = 0;

var end = false;

var stop = false;

/*
	Functions in this section work with new game page
*/

function usernamePreload(msg) {
	$('.usernameField:input').val(msg.d);
}

function solver() {
	if (end) {
		$('#checkBox').prop('checked', false);
		clearInterval(solverHolder);
		solverHolder = 0;
	}
	var places = sizeX + ' ' + sizeY + ' ' + mines + ' ';
	for (var i = 0; i < sizeY; i++) {
		for (var j = 0; j < sizeX; j++) {
			if (fieldArray[i][j] == 'f')
				places += '0 1 ';
			else
				if (fieldArray[i][j] == '')
					places += '0 0 ';
				else
					places += fieldArray[i][j] + ' 3 '
		}
	}
	runSolver(places);
}

var solverHolder = 0;

//adding event listeners to new game page
$(document).ready(function () {
	window.history.pushState({}, '', '');

	$('#lblGetMove').click(function () {
		if (timerContainer) {
			if ($('#checkBox').prop('checked'))
				if (!solverHolder)
					solverHolder = setInterval(solver, 1000);
				else {
					clearInterval(solverHolder);
					solverHolder = 0;
				}
			else
				solver();
		}
	})

	$('#lblSolver').click(function () {
		$('.rightBar>*').slideUp('slow');
		$('.solverButton').slideDown('slow');
		$('#solverMenu').slideDown('slow');
	});

	$('#lblSolverCancel').click(function () {
		$('.solverButton').slideUp('slow');
		$('.rightBar>*').slideDown('slow');
		$('#solverMenu').slideUp('slow');
		$('#checkBox').prop('checked', false);
		clearInterval(solverHolder);
		solverHolder = 0;
	});

	if (!Modernizr.borderradius || !Modernizr.fontface || !Modernizr.backgroundsize || !Modernizr.opacity || !Modernizr.csstransitions || !Modernizr.sessionstorage) {
		$('body').html($('#errorLabel').html());
		$('body').css({ 'background': '#F8F8F8', 'text-align': 'center', 'font-size': '40px' });
		return;
	}
	//field size tip
	$('.sizeTip').hover(function () {
		$('.tipText.size').fadeIn('slow');
	}, function () {
		$('.tipText.size').fadeOut('slow');
	});

	sendJSON(
        "getNickName",
        "{}",
        usernamePreload,
        errorAJAX
        );

	//mine amount tip
	$('.mineAmountTip').hover(function () {
		var text = $('#lblMineTip').html().split('.');
		$('.tipText.mineAmount').html(text[0] + Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.1) + text[1] + Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.7) + '<br>' + text[2]);
		$('.tipText.mineAmount').fadeIn('slow');
	}, function () {
		$('.tipText.mineAmount').fadeOut('slow');
	});

	//checks numbers that are user inputs into option fields
	$('.fieldSize, .mineAmount').on('change', function (e) {
		//basic styles if everything is correct
		$('.fieldSize, .mineAmount').css({ 'color': 'black' });
		$('.sizeAlert, .mineAmountAlert').css({ 'opacity': 0 });

		//maximum value check
		if ($('.fieldSize.X').val() > maximumFieldSize)
			$('.fieldSize.X').val(maximumFieldSize);
		if ($('.fieldSize.Y').val() > parseInt($('.fieldSize.X').val()))
			$('.fieldSize.Y').val($('.fieldSize.X').val());
		if ($('.mineAmount:input').val() > Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.7))
			$('.mineAmount:input').val(Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.7));

		//minimum value check
		if ($('.fieldSize.X').val() < minimumFieldSize)
			$('.fieldSize.X').val(minimumFieldSize);
		if ($('.fieldSize.Y').val() < minimumFieldSize)
			$('.fieldSize.Y').val(minimumFieldSize);
		if ($('.mineAmount:input').val() < Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.1))
			$('.mineAmount:input').val(Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.1));
	});

	//check if user does not try to enter non number
	$('.fieldSize, .mineAmount').on('keydown', function (e) {
		var code = e.keyCode || e.which;
		if (((code < 48) || (code > 57)) && (code != 127) && (code != 8) && (code != 9) && (code != 37) && (code != 39) && !((code > 95) && (code < 106)))
			return false;
	});

	$('.fieldSize, .mineAmount').on('keyup', function () {
		//change chosen option into custom game
		mode = 4;
		$('.customGame').height('15%');
		$('.menuOption').slideUp('slow');
		$('.customGame').slideDown('fast');
		$('.customGame, .menuOption').css({ 'border-bottom': '1px solid ' });
		$('.customGame').css({ 'border-bottom': '3px solid' });

		//width of field check
		if (($('.fieldSize.X').val() > maximumFieldSize) || ($('.fieldSize.X').val() < minimumFieldSize)) {
			$('.sizeAlert').css({ 'opacity': 1 });
			$(('.fieldSize.X')).css({ 'color': 'red' });
		}
		else {
			$('.sizeAlert').css({ 'opacity': 0 });
			$('.fieldSize.X').css({ 'color': 'black' });
		}

		//height of field check
		if ((parseInt($('.fieldSize.Y').val()) > parseInt($('.fieldSize.X').val())) || ($('.fieldSize.Y').val() < minimumFieldSize)) {
			$('.sizeAlert').css({ 'opacity': 1 });
			$('.fieldSize.Y').css({ 'color': 'red' });
		}
		else {
			$('.sizeAlert').css({ 'opacity': 0 });
			$('.fieldSize.Y').css({ 'color': 'black' });
		}

		//mine amount check
		if (($('.mineAmount:input').val() > Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.7)) || ($('.mineAmount:input').val() < Math.round($('.fieldSize.X').val() * $('.fieldSize.Y').val() * 0.1))) {
			$('.mineAmountAlert').css({ 'opacity': 1 });
			$('.mineAmount').css({ 'color': 'red' });
		}
		else {
			$('.mineAmountAlert').css({ 'opacity': 0 });
			$('.mineAmount').css({ 'color': 'black' });
		}
	});

	//functions for clicking menu option
	$('.customGame, .menuOption').on('click', function () {
		//block annoying clicking
		if (!$(':animated').html()) {
			//if something is already chosen
			if (flag) {
				//return everything to normal
				$(this).css({ 'border-bottom': '1px solid black' });
				$(this).removeClass('menuChosen');
				$('.customGame, .menuOption').height('25%');
				//show all options again
				$('.menuOption, .customGame').slideDown('slow');
				flag = 0;
			}
			else {
				//highlight chosen option
				$(this).height('15%');
				$(this).css({ 'border-bottom': '3px solid black' });
				$(this).addClass('menuChosen');
				//hide another options
				$('.customGame, .menuOption').slideUp('slow');
				flag = 1;

				$(this).slideDown('slow');
			}
		}
	});

	//--------------------filling option fields with basic values--------------------
	//for small field
	$('.menuOption.small').on('click', function () {
		if (flag) {
			$('.fieldSize').val(9);
			$('.mineAmount').val(10);
			mode = 1;
		}
	});

	//for medium field
	$('.menuOption.medium').on('click', function () {
		if (flag) {
			$('.fieldSize').val(16);
			$('.mineAmount').val(40);
			mode = 2;
		}
	});

	//for professional field
	$('.menuOption.professional').on('click', function () {
		if (flag) {
			$('.fieldSize.X').val(30);
			$('.fieldSize.Y').val(16);
			$('.mineAmount').val(100);
			mode = 3;
		}
	});
	//-------------------------------------------------------------------------------

	//resizing page on load || on window resize
	$(window).resize(screenSize);
	screenSize();
});

//event listener for start game button
$(document).ready(function () {
	$('.startGame').on('click', function () {
		username = $('.usernameField').val();
		// username validation
		if (($('.usernameField').val() == '') || (username.indexOf('<') + 1) || (username.indexOf('>') + 1))
			return false;
		if (($('.fieldSize.X').val() > maximumFieldSize) || ($('.fieldSize.X').val() < minimumFieldSize) || ($('.fieldSize.Y').val() > maximumFieldSize) || ($('.fieldSize.Y').val() < minimumFieldSize) || ($('.username input').val().length > 16))
			alert('Wrong size');
		else {
			//remembering field properties
			sizeX = $('.fieldSize.X:input').val();
			sizeY = $('.fieldSize.Y:input').val();
			mines = $('.mineAmount:input').val();

			//changing page
			$('.main').slideUp('slow');
			$('.gamePage').slideDown('slow');
			fieldConstructor();

			$('.customGame, .menuOption').slideDown('fast');
			$('.customGame, .menuOption').css({ 'border-bottom': '1px solid black' });
			$('.customGame, .menuOption').height('25%');
			flag = 0;
		}
	});
});

/*
	Functions in this section work with game page
*/

//choosing page to show
$(document).ready(function () {
	flag = 0;
	if ($('.newGame').css('display') == 'none') {
		$('.main').slideUp('fast');
		$('.newGame').slideDown('slow');
		$('.customGame, .menuOption').css({ 'border-bottom': '1px solid black' });
		$('.customGame, .menuOption').height('25%');
		$('.customGame, .menuOption').slideDown('fast');
	}
	else {
		$('.customGame, .menuOption').css({ 'border-bottom': '1px solid black' });
		$('.customGame, .menuOption').height('25%');
		$('.customGame, .menuOption').slideDown('fast');
	}
	$('.newGameButton').on('click', newGameCustomization);
	$('.restartGame').on('click', function () {
		clearInterval(timerContainer);
		fieldConstructor();
	});
});

//Selecting new game page
function newGameCustomization() {
	if (!$(':animated').html()) {
		$('.customGame, .menuOption').removeClass('menuChosen');
		flag = 0;
		if ($('.newGame').css('display') == 'none') {
			$('.main').fadeOut('fast');
			$('.newGame').fadeOut('slow');
			$('.customGame, .menuOption').css({ 'border-bottom': '1px solid black' });
			$('.customGame, .menuOption').height('25%');
			$('.customGame, .menuOption').slideDown('fast');
		}
		else {
			$('.customGame, .menuOption').css({ 'border-bottom': '1px solid black' });
			$('.customGame, .menuOption').height('25%');
			$('.customGame, .menuOption').slideDown('fast');
		}
	}
}

//Construction of field
function fieldConstructor() {
	$('.result').html('');

	for (var i = 0; i < sizeY; i++) {
		fieldArray[i] = new Array(sizeX);
		for (var j = 0; j < sizeX; j++)
			fieldArray[i][j] = '';
	}

	stop = true;
	clearInterval(timerContainer);
	$('.timer').html('00:00:00');
	cellOpened = 0;
	end = false;
	flags = mines;
	$('.flagLeft').html($('#imgHolder').html() + ':' + flags);
	$('.field').css({ 'display': 'none' })
	sendJSON(
                "startGame",
                //JSON.stringify(
                {
                	width: sizeX,
                	height: sizeY,
                	minesNumber: mines,
                	userName: username,
                	mode: mode
                },
            //),
		function () {
			$('.field').css({ 'display': 'block' });
		},
            errorAJAX
        );

	//formula to count field cell size
	cellSize = Math.round(Math.min((0.51 * ($(window).height()) - (sizeY * 2) - 4) / sizeY, (0.7 * $(window).width() - (sizeX * 2) - 4) / sizeX));
	if (cellSize < $(window).height() / 40) {
		cellSize = $(window).height() / 40;
		$('.field').css({ 'overflow-y': 'scroll' });
	}
	else
		$('.field').css({ 'overflow-y': 'none' });
	//creating table
	var htmlInsert = '<table>';
	for (var i = 0; i < sizeY; i++) {
		htmlInsert += '<tr>';
		for (var j = 0; j < sizeX; j++)
			htmlInsert += '<td class="fieldCell" id="' + j + '-' + i + '"></td>';
		htmlInsert += '</tr>';
	}

	htmlInsert += '</table>';
	$('.field').html(htmlInsert);
	//styling cells
	$('.field').css({ 'width': Math.round(cellSize * (parseInt(sizeX) + 1) + sizeX * 2 + 4) });
	$('.fieldCell').css({ 'height': cellSize, 'width': cellSize, 'font-size': cellSize * 0.9 });

	//click on cell
	$('.fieldCell').on('click', function (e) {
		if ($(this).html() == '') {
			if (!cellOpened) {
				stop = false;
				timerStart();
			}
			var coord = $(this).attr('id');
			coord = coord.split('-');
			openPlace(coord[0], coord[1]);
		}
		//else
		//    if ($(this).html() > '0' && $(this).html() <= '9') {
		//        var coord = $(this).attr('id');
		//        coord = coord.split('-');
		//        var nearFlags = 0;
		//        var places = [];
		//        var circle = [];
		//        for (var i = 0; i < 8; i++) {
		//            circle[i] = new Array(8);
		//        }
		//        circle[0][0] = circle[1][1] = circle[2][0] = circle[3][0] = circle[4][1] = circle[5][1] = circle[6][0] = circle[7][0] = 0;
		//        circle[0][1] = circle[1][0] = circle[6][1] = circle[7][1] = 1;
		//        circle[2][1] = circle[3][1] = circle[4][0] = circle[5][0] = -1;

		//        for (var i = 0; i < 8; i++) {
		//            coord[0] = parseInt(coord[0]) + parseInt(circle[i][0]);
		//            coord[1] = parseInt(coord[1]) + parseInt(circle[i][1]);
		//            if ($("#" + coord[0] + '-' + coord[1]).html() == '<img src="Resources/Pictures/flag.jpg">')
		//                nearFlags++;
		//            else
		//                if ((coord[0] > -1) && (coord[1] > -1) && (coord[0] < sizeX) && (coord[1] < sizeY))
		//                    places.push(
		//                    {
		//                        x: coord[0],
		//                        y: coord[1]
		//                    }
		//            );
		//        }

		//        if ($(this).html() == nearFlags)
		//            openPlaces($(this).html(), places);
		//    }
	});

	$('.fieldCell').on('dblclick', function (e) {
		if ($(this).html() > '0' && $(this).html() <= '9') {
			var coord = $(this).attr('id');
			coord = coord.split('-');
			var nearFlags = 0;
			var places = [];
			var circle = [];
			for (var i = 0; i < 8; i++) {
				circle[i] = new Array(8);
			}
			circle[0][0] = circle[1][1] = circle[2][0] = circle[3][0] = circle[4][1] = circle[5][1] = circle[6][0] = circle[7][0] = 0;
			circle[0][1] = circle[1][0] = circle[6][1] = circle[7][1] = 1;
			circle[2][1] = circle[3][1] = circle[4][0] = circle[5][0] = -1;

			for (var i = 0; i < 8; i++) {
				coord[0] = parseInt(coord[0]) + parseInt(circle[i][0]);
				coord[1] = parseInt(coord[1]) + parseInt(circle[i][1]);
				if ($("#" + coord[0] + '-' + coord[1]).html() == $('#imgHolder').html())
					nearFlags++;
				else
					if ((coord[0] > -1) && (coord[1] > -1) && (coord[0] < sizeX) && (coord[1] < sizeY) && ($("#" + coord[0] + '-' + coord[1]).html() == '')) {
						places.push(
                        {
                        	x: coord[0],
                        	y: coord[1]
                        });
						//cellOpened++;
					}
			}

			if ($(this).html() == nearFlags) {
				openPlaces(places);
			}
		}
	});

	//right click on cell
	$('.fieldCell').on('contextmenu', function (ev) {
		ev.preventDefault();
		if ($(this).html() == '') {
			$(this).html($('#imgHolder').html());
			flags--;
			$('.flagLeft').html($('#imgHolder').html() + ':' + flags);

			var coord = $(this).attr('id');
			coord = coord.split('-');
			fieldArray[coord[1]][coord[0]] = 'f';
		}
		else
			if ($(this).html() == $('#imgHolder').html()) {
				$(this).html('');
				flags++;
				$('.flagLeft').html($('#imgHolder').html() + ':' + flags);

				var coord = $(this).attr('id');
				coord = coord.split('-');
				fieldArray[coord[1]][coord[0]] = '';
			}
		return false;
	});

	$('.fieldCell').on('mousedown', function () {
		return false;
	});

	$('.fieldCell').hover(function () {
		$(this).css({ 'border-color': 'black' });
	}, function () {
		$(this).css({ 'border-color': 'darkgrey' });
	});
}

var timer = 0;
var hour = 0;
var minute = 0;
var second = 0;



var timerContainer;

function timerStart() {
	timer = 0;
	hour = 0;
	minute = 0;
	second = 0;

	clearInterval(timerContainer);
	timerContainer = setInterval(function () {
		++timer;
		hour = Math.floor(timer / 3600);
		minute = Math.floor((timer - hour * 3600) / 60);
		second = timer - hour * 3600 - minute * 60;
		if (hour < 10) hour = '0' + hour;
		if (minute < 10) minute = '0' + minute;
		if (second < 10) second = '0' + second;
		$('.timer').html(hour + ':' + minute + ':' + second);
	}, 1000);
}

function revealField() {
	for (var i = 0; i < sizeY; i++)
		for (var j = 0; j < sizeX; j++) {
			if (stop)
				return;
			openPlace(j, i);
		}
}

/*
	Functions in this section work with leader board
*/

//leader board open
$(document).ready(function () {
	$('.leaderBoardButton').on('click', function () {
		$('.main').fadeOut('slow');
		$('.leaderBoard').fadeIn('slow');
		leaderChoose('easy');
		$('.leaderBoard table tbody').height($('.sheet').height() * 0.7);
	});
	$('.newGameButton').on('click', function () {
		$('.leaderBoard, .main').fadeOut('slow');
		$('.main.newGame').fadeIn('slow');
	});
	$('.authors').on('click', function () {
		$('.main').fadeOut('slow');
		$('.leaderBoard').fadeOut('slow');
		$('.authorsPage').fadeIn('slow');
	});
	$('.pdfInfo').on('click', function () {
		$('.main').fadeOut('slow');
		$('.leaderBoard').fadeOut('slow');
		$('.pdfPage').fadeIn('slow');
	});
	$('#lblHelp').on('click', function () {
		$('.main').fadeOut('slow');
		$('.leaderBoard').fadeOut('slow');
		$('.help').fadeIn('slow');
		$('#helpFrame').attr('src', $('#lblHelpSrc').html());
	});
	$('#lblRules').on('click', function () {
		$('.main').fadeOut('slow');
		$('.leaderBoard').fadeOut('slow');
		$('.rules').fadeIn('slow');
		$('#rulesFrame').attr('src', $('#lblRulesSrc').html());
	});
});

//leader board choose table to show
var difficulty

function leaderChoose(e) {
	difficulty = e;
	var mode;
	if (difficulty == 'easy')
		mode = 1;
	else
		if (difficulty == 'medium')
			mode = 2;
		else
			mode = 3;
	getLeaderBoard(mode);
}

function getLeaderBoard(mode) {
	sendJSON(
        "getLeaderBoard",
        //JSON.stringify(
                {
                	"mode": mode
                },
        //    ),
        parseLeaders,
        errorAJAX
        );
}

function parseLeaders(msg) {
	var leaders = jQuery.parseJSON(msg.d);
	var message = '';
	for (var i = 0; i < leaders.length; i++) {
		message += '<tr><td> ' + parseInt(i + 1) + "</td><td>" + leaders[i]['UserNickName'] + "</td><td>" + Math.round(leaders[i]['Duration'] / 10) / 100 + "</td></tr>";
	}

	$('.sheet tbody').html(message);
	$('.sheet').css({ 'border-bottom': '1px solid black;' });
	$('.leaderBoard table tr *').width('42%');
	$('.leaderBoard table tr td:first-child').width('15%');
	$('.leaderHead').css({ 'background-color': '#3333FF', 'color': '#CCFF00', 'width': '30%' });
	$('.leaderHead.' + difficulty).css({ 'background-color': '#CCFF00', 'color': '#3333FF', 'width': '40%' });
	$('.leaderHead').on('click', function () {
		leaderChoose($(this).attr('id'));
	});
	$('.leaderBoard table tbody').height($('.sheet').height() * 0.7);

}



//images preload

function preCacheImages() {
	var imgSrcArray = [
        '/images/Minesweeper/flag.jpg',
        '/images/Minesweeper/refresh.png',
        '/images/Minesweeper/start_button.gif',
        '/images/Minesweeper/mine.jpg',
	];
	$.each(imgSrcArray, function () {
		var img = new Image();
		img.src = this;
	});
};

$(document).ready(function () {
	preCacheImages();
});
















; (function ($) {
	$.fn.fixMe = function () {
		return this.each(function () {
			var $this = $(this),
               $t_fixed;
			function init() {
				$this.wrap('<div class="container" />');
				$t_fixed = $this.clone();
				$t_fixed.find("tbody").remove().end().addClass("fixed").insertBefore($this);
				resizeFixed();
			}
			function resizeFixed() {
				$t_fixed.find("th").each(function (index) {
					$(this).css("width", $this.find("th").eq(index).outerWidth() + "px");
				});
			}
			function scrollFixed() {
				var offset = $(this).scrollTop(),
                tableOffsetTop = $this.offset().top,
                tableOffsetBottom = tableOffsetTop + $this.height() - $this.find("thead").height();
				if (offset < tableOffsetTop || offset > tableOffsetBottom)
					$t_fixed.hide();
				else if (offset >= tableOffsetTop && offset <= tableOffsetBottom && $t_fixed.is(":hidden"))
					$t_fixed.show();
			}
			$(window).resize(resizeFixed);
			$(window).scroll(scrollFixed);
			init();
		});
	};
})(jQuery);

$(document).ready(function () {
	$("table").fixMe();
	$(".up").click(function () {
		$('html, body').animate({
			scrollTop: 0
		}, 2000);
	});
});

var loadingHandler;

$(document).ajaxStart(function () {
	$('#loading').fadeIn('fast');
	$('#loading').css({ 'display': 'block' });
	$('#done').css({ 'display': 'none' });
	loadingHandler = setInterval(function () {
		if ($('.headerLoadingBar').width() < $('.headerLoading').width()) {
			$('.headerLoadingBar').width(($('.headerLoadingBar').width() / $('.headerLoading').width() + 0.01) * 100 + 1 + '%');
		}
		else
			$('.headerLoadingBar').width('0px');
	}, 10)
	$('.loading').fadeIn('fast');
});

$(document).ajaxStop(function () {
	$('.headerLoadingText').fadeOut('slow');
	clearInterval(loadingHandler);
	$('.headerLoadingBar').width('0px');
	$('#done').fadeIn('fast');
	$('#done').css({ 'display': 'block' });
	$('#loading').css({ 'display': 'none' });
	$('.loading').fadeOut('fast');
});