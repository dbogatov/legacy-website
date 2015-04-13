using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;

namespace Personal_Website.Projects.Minesweeper
{
    static class SolverLauncher
    {
        public static List<Move> runSolver(string field)
        {
			int width = ((Parameters)HttpContext.Current.Session["Parameters"]).width;
			int height = ((Parameters)HttpContext.Current.Session["Parameters"]).height;

			String[] splitField = field.Split(' ');

			Debug.WriteLine(field);
			Debug.WriteLine(width);

			int column = 0;
			int row = 0;
			String line = "";
			string[] lines = new string[height];
			for (int i = 3; i < splitField.Length; i+=2) {
				if (i + 1 < splitField.Length) {
					if (splitField[i + 1] == "0" || splitField[i + 1] == "1") {
						line += " ";
					} else {
						line += splitField[i];
					}

					column++;

					if (column == width) {
						Debug.WriteLine(line);
						column = 0;
						lines[row] = line;
						line = "";
						row++;
					}
				}
			}

            Solver solver = new Solver(lines);



			List<Move> moves = new List<Move>();
			foreach (QuestCell cell in solver.resolve()) {
				Console.WriteLine("X=" + (cell.x + 1) + " Y=" + (cell.y + 1) + " M=" + (cell.isMine.Value ? "flag" : "open"));
				moves.Add(new Move(cell.x, cell.y, (cell.isMine.Value ? 1 : 0)));
			}
            
            Console.WriteLine(FieldCell.steps);

			return moves;
        }
    }

    class Solver
    {
        static int[] dx = new int[8] { -1, 0, 1, 1, 1, 0, -1, -1 };
        static int[] dy = new int[8] { -1, -1, -1, 0, 1, 1, 1, 0 };

        List<KnownCell> conditions;
        List<QuestCell> variables;

        public Solver(string[] rows) // playfield rows array
        {
            int h = rows.Length; if (h == 0) throw new Exception("No rows found!");
            int w = rows[0].Length; if (w == 0) throw new Exception("No columns found!");
            for (int i = rows.Length - 1; i > 0; i--) if (rows[i].Length != w)
                    throw new Exception("The number of columns must be the same for each row!");

            conditions = new List<KnownCell>();
            variables = new List<QuestCell>();

            QuestCell[,] quests = new QuestCell[h, w];
            List<KnownCell>[,] nearbyKnowns = new List<KnownCell>[h, w];
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    if (isKnown(rows[y][x]))
                    {
                        List<QuestCell> nearbyQuests = new List<QuestCell>();
                        for (int i = 0; i < 8; i++)
                        {
                            int xi = x + dx[i], yi = y + dy[i];
                            if (xi >= 0 && xi < w && yi >= 0 && yi < h && isQuest(rows[yi][xi]))
                            {
                                if (quests[yi, xi] == null)
                                {
                                    quests[yi, xi] = new QuestCell(xi, yi);
                                    nearbyKnowns[yi, xi] = new List<KnownCell>();
                                    variables.Add(quests[yi, xi]);
                                }
                                nearbyQuests.Add(quests[yi, xi]);
                            }
                        }
                        if (nearbyQuests.Count > 0)
                        {
                            KnownCell known = new KnownCell(x, y, (byte)(rows[y][x] - '0'), nearbyQuests.ToArray());
                            foreach (QuestCell quest in known.neighbors)
                                nearbyKnowns[quest.y, quest.x].Add(known);
                            conditions.Add(known);
                        }
                    }
            if (conditions.Count == 0 || variables.Count == 0) throw new Exception("Nothing was found to resolve");
            foreach (QuestCell quest in variables) quest.neighbors = nearbyKnowns[quest.y, quest.x].ToArray();
        }

        public List<QuestCell> resolve()
        {
            int changes;
            do
            {
                changes = 0;
                foreach (KnownCell cell in conditions)
                    if (cell.hasQuests) changes += cell.update();
            } while (changes > 0);
			
            foreach (QuestCell cell in variables)
                if (!cell.isMine.HasValue)
                {
                    if (tryCell(cell, false)) cell.isMine = true;
                    else if (tryCell(cell, true)) cell.isMine = false;
                }

            List<QuestCell> solved = new List<QuestCell>();
            foreach (QuestCell cell in variables)
                if (cell.isMine.HasValue) solved.Add(cell);

            return solved;
        }

        private bool isKnown(char cell)
        {
            return cell >= '0' && cell <= '8';
        }

        private bool isQuest(char cell)
        {
            return cell < '0' || cell > '8';
        }

        private bool tryCell(QuestCell tryCell, bool tryValue)
        {
            foreach (QuestCell cell in variables)
                cell.ifIsMine = cell.isMine;
            foreach (KnownCell cell in conditions)
                cell.ifHasQuests = cell.hasQuests;
            
            tryCell.ifIsMine = tryValue;

            int changes;
            do
            {
                changes = 0;
                foreach (KnownCell cell in conditions)
                    if (cell.ifHasQuests)
                    {
                        int? changed = cell.check();
                        if (changed.HasValue)
                            changes += changed.Value;
                        else return true;
                    }
            } while (changes > 0);
            return false;
        }
    }

    abstract class FieldCell
    {
        public int x, y;
        public FieldCell[] neighbors;

        public static int steps = 0;
    }

    class QuestCell : FieldCell
    {
        public bool? isMine;
        public bool? ifIsMine;

        public QuestCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class KnownCell : FieldCell
    {
        public byte mines;
        public bool hasQuests;
        public bool ifHasQuests;

        public KnownCell(int x, int y, byte mines, QuestCell[] quests)
        {
            this.x = x;
            this.y = y;
            this.mines = mines;
            this.neighbors = quests;
            hasQuests = true;
        }

        public int update()
        {
            steps++;
            byte questsLeft = 0;
            byte minesFound = 0;
            foreach (QuestCell cell in neighbors)
                if (!cell.isMine.HasValue) questsLeft++;
                else if (cell.isMine.Value) minesFound++;

            if (minesFound > mines) throw new Exception("Impossible field conditions: not enough mines for cell X=" + (x + 1) + " Y=" + (y + 1));
            byte minesLeft = (byte)(mines - minesFound);

            if (minesLeft > questsLeft) throw new Exception("Impossible field conditions: too many mines for cell X=" + (x + 1) + " Y=" + (y + 1));
            if (questsLeft == 0)
            {
                hasQuests = false;
                return 0;
            }

            if (minesLeft == questsLeft || minesLeft == 0)
            {
                foreach (QuestCell cell in neighbors)
                    if (!cell.isMine.HasValue)
                        cell.isMine = minesLeft > 0;
                hasQuests = false;
                return questsLeft;
            }
            else
            {
                hasQuests = true;
                return 0;
            }
        }

        public int? check()
        {
            steps++;
            byte questsLeft = 0;
            byte minesFound = 0;
            foreach (QuestCell cell in neighbors)
                if (!cell.ifIsMine.HasValue) questsLeft++;
                else if (cell.ifIsMine.Value) minesFound++;

            if (minesFound > mines) return null;
            byte minesLeft = (byte)(mines - minesFound);

            if (minesLeft > questsLeft) return null;
            if (questsLeft == 0)
            {
                ifHasQuests = false;
                return 0;
            }

            if (minesLeft == questsLeft || minesLeft == 0)
            {
                foreach (QuestCell cell in neighbors)
                    if (!cell.ifIsMine.HasValue)
                        cell.ifIsMine = minesLeft > 0;
                ifHasQuests = false;
                return questsLeft;
            }
            else
            {
                ifHasQuests = true;
                return 0;
            }
        }

    }
}
