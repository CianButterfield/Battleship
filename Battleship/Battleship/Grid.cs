using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Grid
    {
        //enumeration
        public enum PlaceShipDirection
        {
            Horizontal, Vertical
        }

        //properties
        public Point[,] Ocean { get; set; }
        public List<Ship> ListOfShips { get; set; }
        public bool AllShipsDestroyed { get{ return ListOfShips.All(x => x.IsDestroyed); } }
        public int CombatRound { get; set; }

        //constructor
        public Grid()
        {
            //initialize the ocean
            this.Ocean = new Point[10, 10];
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    this.Ocean[x, y] = new Point(x, y, Point.PointStatus.Empty);
                }
            }
            
            //make the list of ships
            this.ListOfShips = new List<Ship>() { new Ship(Ship.ShipType.Carrier), new Ship(Ship.ShipType.Battleship), new Ship(Ship.ShipType.Cruiser), new Ship(Ship.ShipType.Submarine), new Ship(Ship.ShipType.Minesweeper)};
            
            //place all the ships
            PlaceShip(this.ListOfShips[0], PlaceShipDirection.Horizontal, 0, 0);
            PlaceShip(this.ListOfShips[1], PlaceShipDirection.Horizontal, 6, 0);
            PlaceShip(this.ListOfShips[2], PlaceShipDirection.Horizontal, 1, 9);
            PlaceShip(this.ListOfShips[3], PlaceShipDirection.Horizontal, 6, 9);
            PlaceShip(this.ListOfShips[4], PlaceShipDirection.Vertical, 9, 4);
        }

        //methods and functions
        public void PlaceShip(Ship shipToPlace, PlaceShipDirection direction, int startX, int startY)
        {
            for (int i = 0; i < shipToPlace.Length; i++)
            {
                this.Ocean[startX, startY].Status = Point.PointStatus.Ship;
                shipToPlace.OccupiedPoints.Add(this.Ocean[startX, startY]);

                if (direction == PlaceShipDirection.Horizontal)
                {
                    startX++;
                }
                else
                {
                    startY++;
                }
            }
        }

        public void DisplayOcean()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   0  1  2  3  4  5  6  7  8  9   X");
            Console.WriteLine(" ╔══════════════════════════════╗");
            for (int y = 0; y < 10; y++)
            {
                Console.Write(" ║");
                for (int x = 0; x < 10; x++)
                {
                    if (this.Ocean[x, y].Status == Point.PointStatus.Empty || this.Ocean[x, y].Status == Point.PointStatus.Ship)
                    {
                        Console.Write("╔╦╗");
                    }
                    else if (this.Ocean[x, y].Status == Point.PointStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("╔╦╗");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("╔╦╗");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
                Console.Write("║\n{0}║", y);
                for (int x = 0; x < 10; x++)
                {
                    if (this.Ocean[x, y].Status == Point.PointStatus.Empty || this.Ocean[x, y].Status == Point.PointStatus.Ship)
                    {
                        Console.Write("╠╬╣");
                    }
                    else if (this.Ocean[x, y].Status == Point.PointStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("╠╬╣");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("╠╬╣");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
                Console.Write("║\n ║");
                for (int x = 0; x < 10; x++)
                {
                    if (this.Ocean[x, y].Status == Point.PointStatus.Empty || this.Ocean[x, y].Status == Point.PointStatus.Ship)
                    {
                        Console.Write("╚╩╝");
                    }
                    else if (this.Ocean[x, y].Status == Point.PointStatus.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("╚╩╝");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("╚╩╝");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
                Console.Write("║\n");
            }
            Console.WriteLine(" ╚══════════════════════════════╝");
            Console.WriteLine("Y\n");
        }

        public void Target(int x, int y)
        {
            if (this.Ocean[x, y].Status == Point.PointStatus.Ship)
            {
                this.Ocean[x, y].Status = Point.PointStatus.Hit;
            }
            else if (this.Ocean[x, y].Status == Point.PointStatus.Empty)
            {
                this.Ocean[x, y].Status = Point.PointStatus.Miss;
            }
        }

        public void PlayGame()
        {
            //change the console windo title
            Console.Title = "BattleShip";
            Console.SetWindowSize(35, 38);

            while (!this.AllShipsDestroyed)
            {
                DisplayOcean();

                //bools for if user input is valid
                bool validX = false;
                bool validY = false;

                //int for inputs
                int inputX = 10;
                int inputY = 10;

                //get x coordinate
                while (!validX)
                {
                    Console.WriteLine("Enter coordinates (X, Y)");
                    Console.Write("(");
                    char charInputX = Console.ReadKey().KeyChar;
                    if (char.IsDigit(charInputX)) 
                    {
                        inputX = int.Parse(charInputX.ToString());
                        validX = true;
                    }
                    else
                    {
                        DisplayOcean();
                        Console.WriteLine("Invalid Input");
                    }
                }
                DisplayOcean();
                //get y coordinate
                while (!validY)
                {
                    
                    Console.WriteLine("Enter coordinates ({0}, Y)", inputX);
                    Console.Write("({0}, ", inputX);
                    char charInputY = Console.ReadKey().KeyChar;
                    if (char.IsDigit(charInputY))
                    {
                        inputY = int.Parse(charInputY.ToString());
                        validY = true;
                    }
                    else
                    {
                        DisplayOcean();
                        Console.WriteLine("Invalid Input");
                    }
                }

                //call the target
                Target(inputX, inputY);
                
                CombatRound++;
            }
            DisplayOcean();
            Console.WriteLine("You Win!! It took {0} rounds", CombatRound);
            //give the user a moment to read
            System.Threading.Thread.Sleep(3000);
            //add high score
            AddHighScore(CombatRound);
            //display highscores
            DisplayHighScores();

        }

        static void AddHighScore(int playerScore)
        {
            Console.Clear();

            //get player name for high score
            Console.Write("Your name: "); string playerName = Console.ReadLine();

            //create a gateway to the database
            CianEntities db = new CianEntities();

            //create a new high score object
            // fill it with our user's data
            HighScore newHighScore = new HighScore();
            newHighScore.DateCreated = DateTime.Now;
            newHighScore.Game = "Battleship";
            newHighScore.Name = playerName;
            newHighScore.Score = playerScore;

            //add it to the database
            db.HighScores.Add(newHighScore);

            //save our changes
            db.SaveChanges();
        }

        static void DisplayHighScores()
        {
            //clear the console
            Console.Clear();
            Console.Title = "ΦBattleship High ScoresΦ";
            Console.WriteLine("Battleship High Scores");
            Console.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡");

            //create a new connection to the database
            CianEntities db = new CianEntities();
            //get the high score list
            List<HighScore> highScoreList = db.HighScores.Where(x => x.Game == "Battleship").OrderBy(x => x.Score).Take(10).ToList();

            foreach (HighScore highScore in highScoreList)
            {
                Console.WriteLine("{0}. {1} - {2}", highScoreList.IndexOf(highScore) + 1, highScore.Name, highScore.Score);
            }
            Console.ReadKey();
        }
    }
}
