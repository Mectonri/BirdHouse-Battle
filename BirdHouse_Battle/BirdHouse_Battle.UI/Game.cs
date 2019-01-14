using BirdHouse_Battle.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.IO;

namespace BirdHouse_Battle.UI
{
    public class Game
    {
        #region Fields

        RenderWindow _window;
        Arena _arena;
        InputHandler _iHandler;
        Drawer draw;
        bool _return;
        double _previousP;
        double _msPerUpdate = 0.0000006;
        string _status;
        public int _winner = 0;
        public int _tour = 0;

        #endregion

        public Game()
        {
            Load();

            _iHandler = new InputHandler(this);
            _window = new RenderWindow(new VideoMode(512, 712), "BirdHouseBattle", Styles.Default);
            _status = "main";
            _previousP = GetCurrentTime();
            draw = new Drawer(_window);
        }

        #region Getter

        public int Winner => _winner;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public Arena Arena
        {
            get { return _arena; }
        }
        
        public RenderWindow Window => _window;

        public bool Paused { get; set; }

        public double MsPerUpdate => _msPerUpdate;

        #endregion

        static void Load()
        {
            SFML.SystemNative.Load();
            SFML.WindowNative.Load();
            SFML.GraphicsNative.Load();
            SFML.AudioNative.Load();
        }

        public void NewArena()
        {
            _arena = new Arena();
        }

        #region Relevant to Gameloop

        public double PreviousP => _previousP;

        public bool Return => _return;

        private static double TimeP => 0.000002;

        static double GetCurrentTime() => DateTime.Now.ToOADate();

        internal static void Update(Arena arena)
        {
            arena.Update();
        }

        #endregion

        public bool GameLoop(Arena arena, bool maySave)
        {
            if (maySave)
            {
                SaveState();
            }
            _return = true;
            double previous = GetCurrentTime();

            _iHandler.Handler();

            while (arena.TeamCount > 1)
            {
                _iHandler.Handler();

                if (GetCurrentTime() - previous >= MsPerUpdate && !Paused)
                {
                    Update(arena);
                    previous = GetCurrentTime();
                    _tour++;
                }
                else if (GetCurrentTime() - previous >= MsPerUpdate && Paused)
                {
                    //PauseMenu();
                    _iHandler.HandlerPause(draw.PauseDisplay());
                }

                if (!_window.IsOpen || !Return)
                {
                    Status = "ended";
                    arena.Teams.Clear();
                    arena.Projectiles.Clear();
                    arena.DeadTeams.Clear();
                    arena.DeadProjectiles.Clear();
                }

                if (!Paused) Render(arena);
                else PauseMenu();
            }
            //ListTeam(arena);
            _winner = FindWinner();

            Status = "ended";
            arena.Teams.Clear();
            arena.Projectiles.Clear();
            arena.DeadTeams.Clear();
            arena.DeadProjectiles.Clear();

            return true;
        }

        public void SaveState()
        {
            string date = DateTime.Now.ToString("MM_dd_yyyy H:mm");
            string pathString = $"../../../../saveStates/{date}";
            Directory.CreateDirectory(pathString);
        }

        private void ListTeam(Arena arena)
        {
            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                Console.WriteLine(team.Key);
                Console.WriteLine(team.Value.Name);

            }
        }

        /// <summary>
        /// Find the winning team
        /// </summary>
        /// <returns></returns>
        public int FindWinner()
        {
            if (_arena.FindTeam("blue") == true || _arena.FindTeam("Team1"))
            {
                Console.WriteLine("Blue team won");
                return _winner = 1;
            }
            else if (_arena.FindTeam("red") == true || _arena.FindTeam("Team2"))
            {
                Console.WriteLine("Red team won");
                return _winner = 2;
            }
            else if (_arena.FindTeam("green") == true || _arena.FindTeam("Team3"))
            {
                Console.WriteLine("Green team won");
                return _winner = 3;
            }
            else
            {
                Console.WriteLine("Yellow team won");
                return _winner = 4;
            }
        }

        /// <summary>
        /// Writte the different levels in a JSON
        /// </summary>
        internal void LevelWritte()
        {
            List<Arena> arenas = new List<Arena>();

            string path = "../../../../res/HistoryMode.JSON";

            Arena a = new Arena();
            Team t = a.CreateTeam("red");
            t.AddGoblin(10);
            t.Acount = 10;
            arenas.Add(a);

            Arena a1 = new Arena();
            Team t1 = a1.CreateTeam("red");
            t1.AddArcher(10);
            t1.Acount = 10;
            arenas.Add(a1);

            Arena a2 = new Arena();
            Team t2 = a2.CreateTeam("red");
            t2.AddGoblin(10);
            t2.Acount = 10;
            arenas.Add(a2);

            Arena a3 = new Arena();
            Team t3 = a3.CreateTeam("red");
            t3.AddGoblin(10);
            t3.Acount = 10;
            arenas.Add(a3);

            JArray jArray = new JArray();
            for (int i = 0; i < 4; i++)
            {
                jArray[i] = arenas[i].Serialize();
                using (FileStream fs = File.OpenWrite(path))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    jArray.WriteTo(jw);
                }
            }

            ////foreach (Arena arena in arenas)
            ///// Writting in the JToken
            ////{
            ////    JToken jToken = a.Serialize();
            ////    using (FileStream fs = File.OpenWrite(path))
            ////    using (StreamWriter sw = new StreamWriter(fs))
            ////    using (JsonTextWriter jw = new JsonTextWriter(sw))
            ////    {
            ////        jToken.WriteTo(jw);
            ////    }
            ////}

            //{
            //    using (FileStream fs = File.OpenRead(fileName))
            //    using (StreamReader sr = new StreamReader(fs))
            //    using (JsonTextReader jr = new JsonTextReader(sr))
            //    {
            //        JToken token = JToken.ReadFrom(jr);
            //        Team team = new Team(token);
            //        IEnumerable<Unit> units = team.GetUnits();
            //        Assert.That(units.Count(), Is.EqualTo(2));
            //        Assert.That(units.Any(u => u.Health == 25.0 && u.Speed == 30.0));
            //        Assert.That(units.Any(u => u.Health == 15.0 && u.Speed == 10.0));
            //        Assert.That(units.All(u => u.Team == team));
            //    }
            //}
            // Status = "game";
        }
        
        /// <summary>
        /// Load the selected level to be played
        /// </summary>
        internal void LevelLoad(int Level)
        {
            string path = "../../../../res/HistoryMode.JSON";

            using (FileStream fs = File.OpenRead(path))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {

                JToken jToken = JToken.ReadFrom(jr);
                JArray jArena = (JArray)jToken["Teams"];


                Arena arena = new Arena(jArena[Level]);

                HistoryPreGame(arena);
            }
            
        }

        private void CreateTeam(object value1, object value2, object value3, object value4, object value5, object value6)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fills a team read from the JSON
        /// </summary>
        /// <param name="AToAdd"></param>
        /// <param name="BToAdd"></param>
        /// <param name="CToAdd"></param>
        /// <param name="DToAdd"></param>
        /// <param name="GToAdd"></param>
        /// <param name="PToAdd"></param>
        private void FillTeam(Team team)
        {
            
        }


        public void Run(bool maySave)
        {
            GameLoop(Arena, maySave);
        }

        public void Render(Arena arena)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            draw.BackGroundGame();
            draw.UnitDisplay(arena);
            Window.Display();
        }

        public void Switch(string input)
        {
            switch (input)
            {
                case "P":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        Console.WriteLine("switch : P key is pressed");
                        Console.WriteLine("pause is :{0}", Paused);
                        Paused = !Paused;
                        _previousP = GetCurrentTime();
                    }
                    break;

                case "ESC":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        Status = "return";
                        Console.WriteLine("switch : ESC key is pressed");
                        _previousP = GetCurrentTime();
                    }
                    break;

                case "Right":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        _previousP = GetCurrentTime();
                        Console.WriteLine(" switch : Right arrow is pressed");
                        if (MsPerUpdate > 0.0000006 / 200)
                        {
                            _msPerUpdate = MsPerUpdate / 20;
                        }
                    }
                    break;

                case "Left":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        _previousP = GetCurrentTime();
                        Console.WriteLine("swithc : Left key is pressed");
                        if (MsPerUpdate < 0.0000006)
                        {
                            _msPerUpdate = MsPerUpdate * 20;
                        }
                    }
                    break;

                case "RETURN":
                    _return = false;
                    break;

                default:
                    break;
            }
        }

        #region Fill random and overloads

        /// <summary>
        /// Create a random game
        /// </summary>
        /// <param name="arena"></param>
        public void FillRandom(Arena arena)
        {

            Random rn = new Random();
            int t = rn.Next(2, 5);

            for (int f = 1; f <= t; f++)
            {
                arena.CreateTeam("Team" + f.ToString());
            }

            foreach (KeyValuePair<string, Team> i in arena.Teams)
            {
                i.Value.AddArcher(rn.Next(125));

                i.Value.AddBalista(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddCatapult(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddDrake(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddGoblin(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddPaladin(rn.Next(125 - i.Value.UnitCount));
            }

            arena.SpawnUnit();

            Run(true);
        }

        /// <summary>
        /// Fill a team at random
        /// </summary>
        /// <param name="i"></param>
        /// <param name="TeamComp"></param>
        /// <returns></returns>
        public int[,] FillRandom(int i, int[,] TeamComp)
        {
            Random rdm = new Random();
            int f = 0;
            int max = 0;

            for (f = 1; f < 6; f++)
            {
                TeamComp[i, f] = rdm.Next(125 - max);
                max = max + TeamComp[i, f];
            }

            return TeamComp;
        }
        #endregion

        #region InitMenus

        /// <summary>
        /// Init the main menu
        /// </summary>
        public Shape[] InitGUI()
        {
            Window.Clear();
            Shape[] buttons = draw.MenuDisplay();
            Window.Display();

            return buttons;
        }

        internal void HistoryPreGame( Arena arena)
        {

           

            string[] status = new string[8];
            status[0] = "selected";
            status[1] = "active";
            status[2] = "inactive";
            status[3] = "inactive";

            status[4] = "none";
            status[5] = "add";
            status[6] = "1";
            status[7] = "yes";

            int[,] teamComposition =
            {
                {0, 0, 0, 0, 0, 0},// Player Team
                {0, 0, 0, 0, 0, 0},// Computer team
            };
            double previous = GetCurrentTime();

            foreach (KeyValuePair<string, Team> kv in arena.Teams)
            {
                Team team = kv.Value;
                teamComposition[1, 0] = team.Acount;
                teamComposition[1, 1] = team.Bcount;
                teamComposition[1, 2] = team.Ccount;
                teamComposition[1, 3] = team.Dcount;
                teamComposition[1, 4] = team.Gcount;
                teamComposition[1, 5] = team.Pcount;
            }




            while (Window.IsOpen && Status == "historyPreGame")
            {
                double current = GetCurrentTime();
                if (current - previous >= 0.0000019)
                {
                    previous = current;
                    Shape[] buttons = InitHistoryPreGame(status, teamComposition);
                    Window.DispatchEvents();
                    status = _iHandler.HandlerHistoryPreGame(buttons, status);

                }
                Arena.SpawnUnit();
                
            }
        }

        public void CreditPage()
        {
            Shape[] buttons = InitCredit();

            while (Window.IsOpen && Status == "credit")
            {
                _iHandler.HandlerCredit(buttons);
            }
        }

        private Shape[] InitCredit()
        {
            Window.Clear();
            Shape[] buttons = draw.CreditDisplay();
            Window.Display();
            return buttons;
        }

        public Shape[] InitPause()
        {
            Window.Clear();
            Shape[] buttons = draw.PauseDisplay();
            Window.Display();
            return buttons;
        }

        /// <summary>
        /// Display pause Menu
        /// </summary>
        public void PauseMenu()
        {
            while (Window.IsOpen && Paused == true)
            {
                Shape[] buttons = InitPause();
                _iHandler.HandlerPause(buttons);
            }
        }

        internal void HistoryLvSelectionMenu()
        {
            Shape[] buttons = InitHistoryLvSelection();

            while (Window.IsOpen && Status == "history")
            {
                _iHandler.HandlerHistoryLvSelection(buttons);
            }
        }

        private Shape[] InitHistoryLvSelection()
        {
            Window.Clear();
            Shape[] buttons = draw.HistoryLvSeletionDisplay();
            Window.Display();

            return buttons;
        }

        /// <summary>
        /// Display Main Menu
        /// </summary>
        public void MainMenu()
        {
            Shape[] buttons = InitGUI();
            while (Window.IsOpen && Status == "main")
            {
                _iHandler.HandlerMain(buttons);
            }
        }

        public Shape[] InitReturn()
        {
            Window.Clear();
            Shape[] buttons = draw.ReturnDisplay();

            Window.Display();
            return buttons;
        }

        public void QuitPage()
        {
            Shape[] buttons = InitQuitting();

            while (Window.IsOpen && Status == "quit")
            {
                _iHandler.HandlerQuit(buttons);
            }
        }

        private Shape[] InitQuitting()
        {
            Window.Clear();
            Shape[] buttons = draw.QuitDisplay();
            Window.Display();
            return buttons;
        }

        /// <summary>
        ///Asks for a YES/NO confirmation  before quitting the game
        /// </summary>
        public void ReturnMenu()
        {
            Shape[] buttons = InitReturn();
            while (Window.IsOpen && Status == "return")
            {

                _iHandler.HandlerReturn(buttons);
            }
        }

        public bool IsValidToAddUnit(int[,] TeamCompo, int i, string[] status)
        {
            int count = 0;
            for (int j = 0; j < 6; j++)
            {
                count += TeamCompo[i, j];
            }

            count += Int32.Parse(status[6]);

            if (count > 125) return false;

            else { return true; }
        }

        public void ResultWindow()
        {
            Shape[] buttons = InitResult();

            while (Window.IsOpen && Status == "ended")
            {
                _iHandler.HandlerResulsult(buttons);
            }
        }

        private Shape[] InitResult()
        {
            Window.Clear();
            Font font = new Font("../../../../res/Overlock-Bold.ttf");
            Text ToursFinal = new Text(_tour.ToString() + "TURNS", font, 50)
            {
                Position = new Vector2f(150, 550)
            };
            Shape[] buttons = draw.ResultDisplay(Winner);
            Window.Draw(ToursFinal);
            Window.Display();

            return buttons;
        }
        
        public Shape[] InitHistoryPreGame(string[] status, int[,] teamComposition)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            Shape[] buttons = draw.HistoryPreGameDisplay(status, teamComposition);
            Window.Display();
            return buttons;
        }

        public Shape[] InitPreGame(string[] status, int[,] teamComposition)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            Shape[] buttons = draw.PreGameDisplay(status, teamComposition);
            Window.Display();
            return buttons;
        }

        public void PreGame()
        {

            Team blue = Arena.CreateTeam("blue");
            Team red = Arena.CreateTeam("red");
            Team green = null;
            Team yellow = null;
            string[] status = new string[8];
            status[0] = "selected";
            status[1] = "active";
            status[2] = "inactive";
            status[3] = "inactive";

            status[4] = "none";
            status[5] = "add";
            status[6] = "1";
            status[7] = "yes";

            int[,] teamComposition =
            {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };
            double previous = GetCurrentTime();

            while (Window.IsOpen && Status == "preGame")
            {
                double current = GetCurrentTime();
                if (current - previous >= 0.0000019)
                {
                    previous = current;
                    Shape[] buttons = InitPreGame(status, teamComposition);
                    Window.DispatchEvents();
                    status = _iHandler.HandlerPreGame(buttons, status);

                    if (status[7] == "no")
                    {
                        for (int i = 0; i < status.Length; i++)
                        {
                            if (status[i] == "selected")
                            {
                                teamComposition = FillRandom(i, teamComposition);
                                status[7] = "yes";
                            }
                        }
                    }

                    if (status[2] == "inactiveTemp")
                    {
                        status[3] = "inactive";
                        status[2] = "active";

                        for (int i = 0; i < 6; i++)
                        {
                            teamComposition[2, i] = teamComposition[3, i];
                        }
                    }

                    if (status[2] == "inactive")
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            teamComposition[2, i] = 0;
                        }
                    }
                    if (status[3] == "inactive")
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            teamComposition[3, i] = 0;
                        }
                    }

                    if (status[2] == "active" && Arena.FindTeam("green") == false)
                    {
                        green = Arena.CreateTeam("green");
                    }
                    else if (status[3] == "active" && Arena.FindTeam("yellow") == false)
                    {
                        yellow = Arena.CreateTeam("yellow");
                    }

                    switch (status[4])
                    {
                        case "archer":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i, status) == true && status[5] == "add")
                                {
                                    teamComposition[i, 0] += Int32.Parse(status[6]);
                                }
                                else if (status[i] == "selected" && status[5] != "add")
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 0] < 0)
                                    {
                                        teamComposition[i, 0] = 0;
                                    }
                                }
                            }
                            break;
                        case "drake":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i, status) == true && status[5] == "add")
                                {
                                    teamComposition[i, 1] += Int32.Parse(status[6]);
                                }
                                else if (status[i] == "selected" && status[5] != "add")
                                {
                                    teamComposition[i, 1] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 1] < 0)
                                    {
                                        teamComposition[i, 1] = 0;
                                    }
                                }
                            }
                            break;
                        case "gobelin":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i, status) == true && status[5] == "add")
                                {
                                    teamComposition[i, 2] += Int32.Parse(status[6]);
                                }
                                else if (status[i] == "selected" && status[5] != "add")
                                {
                                    teamComposition[i, 2] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 2] < 0)
                                    {
                                        teamComposition[i, 2] = 0;
                                    }
                                }
                            }
                            break;
                        case "paladin":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i, status) == true && status[5] == "add")
                                {
                                    teamComposition[i, 3] += Int32.Parse(status[6]);
                                }
                                else if (status[i] == "selected" && status[5] != "add")
                                {
                                    teamComposition[i, 3] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 3] < 0)
                                    {
                                        teamComposition[i, 3] = 0;
                                    }
                                }
                            }
                            break;
                        case "balista":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i, status) == true && status[5] == "add")
                                {
                                    teamComposition[i, 4] += Int32.Parse(status[6]);
                                }
                                else if (status[i] == "selected" && status[5] != "add")
                                {
                                    teamComposition[i, 4] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 4] < 0)
                                    {
                                        teamComposition[i, 4] = 0;
                                    }
                                }
                            }
                            break;
                        case "catapult":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i, status) == true && status[5] == "add")
                                {
                                    teamComposition[i, 5] += Int32.Parse(status[6]);
                                }
                                else if (status[i] == "selected" && status[5] != "add")
                                {
                                    teamComposition[i, 5] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 5] < 0)
                                    {
                                        teamComposition[i, 5] = 0;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    status[4] = "none";
                }
            }

            if (Status != "preGame")
            {

                for (int i = 0; i < teamComposition.GetLength(1); i++)
                {
                    switch (i)
                    {
                        case 0:
                            blue.AddArcher(teamComposition[0, i]);
                            break;
                        case 1:
                            blue.AddDrake(teamComposition[0, i]);
                            break;
                        case 2:
                            blue.AddGoblin(teamComposition[0, i]);
                            break;
                        case 3:
                            blue.AddPaladin(teamComposition[0, i]);
                            break;
                        case 4:
                            blue.AddBalista(teamComposition[0, i]);
                            break;
                        case 5:
                            blue.AddCatapult(teamComposition[0, i]);
                            break;

                        default:
                            break;
                    }
                }

                for (int i = 0; i < teamComposition.GetLength(1); i++)
                {
                    switch (i)
                    {
                        case 0:
                            red.AddArcher(teamComposition[1, i]);
                            break;
                        case 1:
                            red.AddDrake(teamComposition[1, i]);
                            break;
                        case 2:
                            red.AddGoblin(teamComposition[1, i]);
                            break;
                        case 3:
                            red.AddPaladin(teamComposition[1, i]);
                            break;
                        case 4:
                            red.AddBalista(teamComposition[1, i]);
                            break;
                        case 5:
                            red.AddCatapult(teamComposition[1, i]);
                            break;

                        default:
                            break;
                    }
                }

                if (Arena.FindTeam("green") == true)
                {
                    for (int i = 0; i < teamComposition.GetLength(1); i++)
                    {
                        switch (i)
                        {
                            case 0:
                                green.AddArcher(teamComposition[2, i]);
                                break;
                            case 1:
                                green.AddDrake(teamComposition[2, i]);
                                break;
                            case 2:
                                green.AddGoblin(teamComposition[2, i]);
                                break;
                            case 3:
                                green.AddPaladin(teamComposition[2, i]);
                                break;
                            case 4:
                                green.AddBalista(teamComposition[2, i]);
                                break;
                            case 5:
                                green.AddCatapult(teamComposition[2, i]);
                                break;

                            default:
                                break;
                        }
                    }
                }

                if (Arena.FindTeam("yellow") == true)
                {
                    for (int i = 0; i < teamComposition.GetLength(1); i++)
                    {
                        switch (i)
                        {
                            case 0:
                                yellow.AddArcher(teamComposition[3, i]);
                                break;
                            case 1:
                                yellow.AddDrake(teamComposition[3, i]);
                                break;
                            case 2:
                                yellow.AddGoblin(teamComposition[3, i]);
                                break;
                            case 3:
                                yellow.AddPaladin(teamComposition[3, i]);
                                break;
                            case 4:
                                yellow.AddBalista(teamComposition[3, i]);
                                break;
                            case 5:
                                yellow.AddCatapult(teamComposition[3, i]);
                                break;

                            default:
                                break;
                        }
                    }
                }
                Arena.SpawnUnit();
            }
        }
        #endregion
    }
}
