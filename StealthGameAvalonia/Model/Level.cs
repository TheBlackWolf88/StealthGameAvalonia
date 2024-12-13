using StealthGameAvalonia.Model.Entities;
using StealthGameAvalonia.Model.Floors;
using StealthGameAvalonia.Model.Utils;
using StealthGameAvalonia.Persistence;
using System;
using System.Collections.Generic;
using System.Timers;

namespace StealthGameAvalonia.Model
{
    public class Level : IDisposable
    {
        private readonly Timer timer;
        public Floor[,] Board { get; private set; }
        public string Name { get; init; }

        public readonly Player Player;
        public readonly List<Guard> Guards;
        private readonly int n;
        private readonly int m;

        //true if win, false if lose
        //also why cant I name it
        //whyyyy
        /// <summary>
        /// Occurs when the game ends.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A boolean value indicating the game's result: <c>true</c> if the player won, <c>false</c> otherwise.</param>
        public event EventHandler<bool>? GameOver;
        public event EventHandler<((int r, int c) curr, (int r, int c) last)>? CellChanged;

        public Level(ITextFileManager textFileManager)
        {
            timer = new()
            {
                Interval = 2000
            };
            timer.Elapsed += HandleTimerTick;
            Guards = [];
            string[] file = textFileManager.Load();
            Name = file[0];
            if (!int.TryParse(file[1].Split(",")[0], out n))
            {
                throw new Exception("Error with level file! (Size is not a number)");
            }
            if (!int.TryParse(file[1].Split(",")[1], out m))
            {
                throw new Exception("Error with level file! (Size is not a number)");
            }

            Board = new Floor[n, m];

            //I hate this so much
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Board[i, j] = new Floor(i, j);
                }
            }

            for (int i = 2; i < file.Length; i++)
            {
                string[] line = file[i].Split(",");

                if (line[0] == "e")
                {
                    if (int.TryParse(line[1], out int r) && int.TryParse(line[2], out int c))
                    {
                        Board[r, c] = new Exit(r, c);
                    }
                }
                else if (line[0] == "w")
                {
                    if (int.TryParse(line[1], out int r) && int.TryParse(line[2], out int c))
                    {
                        Board[r, c] = new Wall(r, c);
                    }
                }
                else if (line[0] == "p")
                {
                    if (int.TryParse(line[1], out int r) && int.TryParse(line[2], out int c))
                    {
                        Player = new Player(r, c);
                        Board[r, c].AddEntity(Player);
                    }
                }
                else if (line[0] == "g")
                {
                    if (int.TryParse(line[1], out int r) && int.TryParse(line[2], out int c))
                    {
                        Guard g = new(r, c);
                        Guards.Add(g);
                        Board[r, c].AddEntity(g);

                    }
                }
            }

            if (Player == null)
            {
                throw new Exception("No player was created!");
            }


        }

        public bool ValidateCoordForGuard(int r, int c)
        {
            //oszinten tul ritkan van ket guard egy cellben es tobb ido lenne implementalni hogy jo legyen
            return r >= 0 && c >= 0 && r < n && c < m && Board[r, c] is not Wall and not Exit && Board[r, c].Guard is null;
        }
        public bool NoWallInSight(int r, int c)
        {
            return r >= 0 && c >= 0 && r < n && c < m && Board[r, c] is not Wall;
        }

        public void Pause()
        {
            if (timer.Enabled)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }

        private void HandleTimerTick(object? sender, ElapsedEventArgs e)
        {
            foreach (Guard g in Guards)
            {
                (int r, int c) last = (g.Row, g.Col);
                (int r, int c) = g.SimulateMove();
                if (ValidateCoordForGuard(r, c))
                {
                    Board[last.r, last.c].RemoveEntity();
                    g.Move(r, c);
                    Board[g.Row, g.Col].AddEntity(g);
                    CellChanged?.Invoke(g, ((g.Row, g.Col), last));

                    if (IsPlayerInRange(g))
                    {
                        GameOver?.Invoke(g, false);
                    }
                }
                else
                {
                    g.ChangeDirection();
                }
            }
        }

        public bool IsPlayerInRange(Guard g)
        {
            //i dont want irrational numbers ruining my stuff
            int dist_2 = (int)(Math.Pow(Math.Abs(g.Row - Player.Row), 2) + Math.Pow(Math.Abs(g.Col - Player.Col), 2));
            if (dist_2 > 8) { return false; }
            //im so goshdarnly proud
            else return NoWallInSight(Player.Row + Math.Sign(g.Row - Player.Row), Player.Col + Math.Sign(g.Col - Player.Col));
            
        }

        public void MovePlayer(Direction dir)
        {
            (int r, int c) last = (Player.Row, Player.Col);
            (int r, int c) = Player.SimulateMove(dir);
            if (NoWallInSight(r, c))
            {

                Board[last.r, last.c].RemoveEntity();
                Player.Move(r, c);
                Board[Player.Row, Player.Col].AddEntity(Player);
                CellChanged?.Invoke(Player, ((Player.Row, Player.Col), last));
                if (Board[r, c] is Exit)
                {
                    GameOver?.Invoke(Player, true);
                    return;
                }
                foreach (Guard g in Guards)
                {
                    if (IsPlayerInRange(g))
                    {
                        GameOver?.Invoke(g, false);
                        return;
                    }
                }
            }
        }

        public void Dispose()
        {
            ((IDisposable)timer).Dispose();
        }
    }

}
