/*
  The solver works as follows:
  1. Get the array of numbers (with coordinates) which border with undiscovered places;
  2. For each such number remember how many flags and undiscovered places border it;
  3. Use info from (2) conclude whether
	a) there are no more flags than possibly may be, so we can alert that flags around this number should be reconsidered
	b) there are just as many flags as number says, so we can alert that undiscovered places are safe to be opened
	c) there are as many undiscovered places as flags lacked, so we can alert that these undiscovered places are mines and should be flagged
  4. If (3) gives no result, for each such number suppose (one by one) that each undiscovered place is mine. Having done that, try to continue game until deadlock or contradiction. If contradiction is met, we can alert that initial suppose was wrong and the number is safe to be opened;
  5. If (4) gives no result, we can alert that all player can do is to guess and wish him good luck;
*/

var checkForBoundsResult = {
	flags: 0,
	undiscovered: 0
}

function getNumbers() {
	var numbers = new Array();

	for (var i = 0; i < fieldArray.length; i++) {
		for (var j = 0; j < fieldArray[0].length; j++) {
			if (fieldArray[i][j] != '' && fieldArray[i][j] != '0' && fieldArray[i][j] != 'f') {
				checkForUndiscovered(i, j);
				if (checkForBoundsResult.undiscovered > 0) {
					numbers.push(
						{
							x: j,
							y: i,
							number: fieldArray[i][j],
							flagsAround: checkForBoundsResult.flags,
							undiscoveredAround: checkForBoundsResult.undiscovered
						}
					);
				}
			}
		}
	}

	basicAnalisys(numbers);
}

function basicAnalisys(numbers) {
	for (var i = 0; i < numbers.length; i++) {
		var place = numbers[i];

		if (place.flagsAround > place.number) {
			console.log("There are too many flags around number " + place.number + " which is located at (" + place.x + ", " + place.y + ")");
			break;
		} else if (place.flagsAround == place.number) {
			console.log("All undiscovered places are safe to be opened around number " + place.number + " which is located at (" + place.x + ", " + place.y + ")");
			break;
		} else {
			var flagsLacked = place.number - place.flagsAround;

			if (place.undiscoveredAround == flagsLacked) {
				console.log("All undiscovered places are mines around number " + place.number + " which is located at (" + place.x + ", " + place.y + ")");
				break;
			}	
		}
	}
}

function checkForUndiscovered(y, x) {
	checkForBoundsResult.flags = 0;
	checkForBoundsResult.undiscovered = 0;

	checkForBounds(y + 1, x);
	checkForBounds(y - 1, x);
	checkForBounds(y + 1, x + 1);
	checkForBounds(y - 1, x + 1);
	checkForBounds(y + 1, x - 1);
	checkForBounds(y - 1, x - 1);
	checkForBounds(y, x + 1);
	checkForBounds(y, x - 1);
}

function checkForBounds(y, x) {
	if (y >= 0 && y < fieldArray.length && x >= 0 && x < fieldArray[0].length) {
		if (fieldArray[y][x] == '') {
			checkForBoundsResult.undiscovered++;
		} else if (fieldArray[y][x] == 'f') {
			checkForBoundsResult.flags++;
		}
	}
}