using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Personal_Website.Projects.Pentago {

	public enum Cell {
		Cross, Donut, Nothing
	}

	public enum Quadrant {
		Uleft, Uright, Bleft, Bright
	}

	public enum RotDirection {
		Clockwise, Counterclockwise
	}

	public class PentagoField {

		private Cell[,] _field = new Cell[6, 6];

		public PentagoField() {
			for (int i = 0; i < 6; i++) {
				for (int j = 0; j < 6; j++) {
					_field[i, j] = Cell.Nothing;
				}
			}
		}

		public PentagoField(String json) {
			DeserializeField(json);
		}

		public bool MakeTurn(int x, int y, Cell mark, Quadrant field, RotDirection direction) {
			if (!_field[y, x].Equals(Cell.Nothing)) {
				return false;
			}

			_field[y, x] = mark;
			RotateField(field, direction);

			return true;
		}

		private void RotateField(Quadrant field, RotDirection direction) {
			switch (field) {
				case Quadrant.Uleft:
					{
						if (direction.Equals(RotDirection.Clockwise)) {
							RotateOnneQuadrantClockwise(0, 0);
						} else {
							RotateOnneQuadrantCounterClockwise(0, 0);
						}
					}
					break;
				case Quadrant.Uright:
					{
						if (direction.Equals(RotDirection.Clockwise)) {
							RotateOnneQuadrantClockwise(0, 3);
						} else {
							RotateOnneQuadrantCounterClockwise(0, 3);
						}
					}
					break;
				case Quadrant.Bright:
					{
						if (direction.Equals(RotDirection.Clockwise)) {
							RotateOnneQuadrantClockwise(3, 3);
						} else {
							RotateOnneQuadrantCounterClockwise(3, 3);
						}
					}
					break;
				case Quadrant.Bleft:
					{
						if (direction.Equals(RotDirection.Clockwise)) {
							RotateOnneQuadrantClockwise(3, 0);
						} else {
							RotateOnneQuadrantCounterClockwise(3, 0);
						}
					}
					break;
			}
		}

		private void RotateOnneQuadrantCounterClockwise(int iOffset, int jOffset) {
			for (int i = 0; i < 3; i++) {
				RotateOnneQuadrantClockwise(iOffset, jOffset);
			}
		}

		private void RotateOnneQuadrantClockwise(int iOffset, int jOffset) {

			// Corners
			var newElement = _field[2 + iOffset, 0 + jOffset];

			newElement = SwitchElements(0, 0, newElement, iOffset, jOffset);
			newElement = SwitchElements(0, 2, newElement, iOffset, jOffset);
			newElement = SwitchElements(2, 2, newElement, iOffset, jOffset);

			_field[2 + iOffset, 0 + jOffset] = newElement;

			// Sides
			newElement = _field[1 + iOffset, 0 + jOffset];

			newElement = SwitchElements(0, 1, newElement, iOffset, jOffset);
			newElement = SwitchElements(1, 2, newElement, iOffset, jOffset);
			newElement = SwitchElements(2, 1, newElement, iOffset, jOffset);

			_field[1 + iOffset, 0 + jOffset] = newElement;
		}

		private Cell SwitchElements(int i, int j, Cell newElement, int iOffset = 0, int jOffset = 0) {
			i += iOffset;
			j += jOffset;

			var oldElement = _field[i, j];
			_field[i, j] = newElement;

			return oldElement;
		}

		public GameResult GetGameResult(Cell playerMark) {

			if (!AnySpaceAvailable()) {
				return GameResult.Tie;
			}

			var crossWon = false;
			var donutWon = false;

			// Collect horizontal
			for (int i = 0; i < 6; i++) {
				var list = new LinkedList<Cell>();
				for (int j = 0; j < 6; j++) {
					list.AddFirst(_field[i, j]);
				}
				FiveMarksInARow(list, ref crossWon, ref donutWon);
			}

			// Collect horizontal
			for (int i = 0; i < 6; i++) {
				var list = new LinkedList<Cell>();
				for (int j = 0; j < 6; j++) {
					list.AddFirst(_field[j, i]);
				}
				FiveMarksInARow(list, ref crossWon, ref donutWon);
			}

			// TODO develop diagonal check

			if (crossWon && donutWon) {
				return GameResult.Tie;
			}
			if (!crossWon && !donutWon) {
				return GameResult.Progress;
			}
			if (crossWon && playerMark.Equals(Cell.Cross) || donutWon && playerMark.Equals(Cell.Donut)) {
				return GameResult.Win;
			}

			return GameResult.Lose;
		}

		private void FiveMarksInARow(IEnumerable<Cell> enumerable, ref bool crossWon, ref bool donutWon) {
			var list = enumerable.ToList();
			if (list.Count > 4) {
				var currentCell = list.First();
				var currentCount = 1;

				foreach (var mark in list) {
					if (mark.Equals(currentCell)) {
						currentCount++;
						if (currentCount == 5) {
							switch (mark) {
								case Cell.Cross:
									crossWon = true;
									break;
								case Cell.Donut:
									donutWon = true;
									break;
							}
							break;
						}
					} else {
						currentCount = 1;
						currentCell = mark;
					}
				}
			}
		}

		public bool AnySpaceAvailable() {
			for (int i = 0; i < 6; i++) {
				for (int j = 0; j < 6; j++) {
					if (_field[i, j].Equals(Cell.Nothing)) {
						return true;
					}
				}
			}
			return false;
		}

		public String GetField() {
			return SerializeField();
		}

		private void DeserializeField(String serialized) {
			_field = (Cell[,])JsonConvert.DeserializeObject<Cell[,]>(serialized);
		}

		private String SerializeField() {
			return JsonConvert.SerializeObject(_field);
		}

	}
}