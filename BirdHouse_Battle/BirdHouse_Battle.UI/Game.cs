﻿using BirdHouse_Battle.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Linq;

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
        string _lastStatus;
        public int _winner = 0;
        public int _tour = 0;
        public int[,] _teamComposition =
             {
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0}
            };
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

      public int[,] TeamCompo
        {
            get { return _teamComposition; }
            set {_teamComposition=value; }
        }

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

        public string LastStatus
        {
            get { return _lastStatus; }
            set { _lastStatus = value; }
        }

        static double GetCurrentTime() => DateTime.Now.ToOADate();

        internal static void Update(Arena arena)
        {
            arena.Update();
        }

        #endregion


        public void StatusSwitch(string last, string present)
        {
            Status = present;
            LastStatus = last;
        }
        public bool GameLoop(Arena arena, string mode, string path)
        {
            if (mode == "play")
            {
                path = SaveState();
                Arena.Field.SavePicture(path);
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
                    _iHandler.HandlerPause(draw.PauseDisplay());
                }

                if (!_window.IsOpen || !Return)
                {
                    if (mode == "history") Status = "historyEnded";
                    else { Status = "ended"; }
                    Clearer();
                }

                if (!Paused) Render(arena, mode, path);
                else PauseMenu();
            }
            //ListTeam(arena);
            _winner = FindWinner();
            if (mode == "play")
            {
                JToken endGame = new JObject(
                    new JProperty("winner", _winner),
                    new JProperty("tour", _tour));

                using (FileStream fs = File.OpenWrite(path + "/endGame.json"))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    endGame.WriteTo(jw);
                }
            }
            if (mode == "history") Status = "historyEnded";
            else { Status = "ended"; }
            Clearer();

            return true;
        }

        /// <summary>
        /// Clears the Arena after a game.
        /// </summary>
        public void Clearer()
        {
            _arena.Teams.Clear();
            _arena.Projectiles.Clear();
            _arena.DeadTeams.Clear();
            _arena.DeadProjectiles.Clear();
        }

        public string SaveState()
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd HH mm");
            string pathString = $"../../../../saveStates/{date}";
            Directory.CreateDirectory(pathString);

            JToken save = Arena.Serialize();
            using (FileStream fs = File.OpenWrite(pathString + "/saveState.json"))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonTextWriter jw = new JsonTextWriter(sw))
            {
                save.WriteTo(jw);
            }

            return pathString;
        }

        /// <summary>
        /// Find the winning team
        /// </summary>
        /// <returns></returns>
        public int FindWinner()
        {
            foreach (KeyValuePair<string, Team> kv in Arena.Teams)
            {
                return _winner = kv.Value.TeamNumber;
            }
            return _winner;
        }

        /// <summary>
        /// Writte the different levels in a JSON
        /// </summary>
        internal void LevelWritte()
        {

            Dictionary<int, Arena> arenas = new Dictionary<int, Arena>();

            string path = "../../../../res/HistoryMode.JSON";

            Arena a = new Arena();
            Team t = a.CreateTeam("red");
            t.AddArcher(10);
            t.Acount = 10;
            arenas.Add(0, a);

            Arena a1 = new Arena();
            Team t1 = a1.CreateTeam("red");
            t1.AddGoblin(10);
            t.Gcount = 10;
            arenas.Add(1, a1);

            Arena a2 = new Arena();
            Team t2 = a2.CreateTeam("red");
            t2.AddDrake(10);
            t2.Dcount = 10;
            arenas.Add(2, a2);

            Arena a3 = new Arena();
            Team t3 = a3.CreateTeam("red");
            t3.AddPaladin(10);
            t3.Pcount = 10;
            arenas.Add(3, a3);

            Arena a4 = new Arena();
            Team t4 = a4.CreateTeam("red");
            t4.AddCatapult(10);
            t4.Ccount = 10;
            arenas.Add(4, a4);

            {
                JToken jToken = Serialize(arenas);
                using (FileStream fs = File.OpenWrite(path))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    jToken.WriteTo(jw);
                }
            }
            Console.WriteLine("serilization ended");
        }

        public JToken Serialize(Dictionary<int, Arena> arenas)
        {
            return new JObject(
                new JProperty("Arenas", arenas.Select(kv => kv.Value.Serialize())));
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
                JArray jArena = (JArray)jToken["Arenas"];

                _arena = new Arena(jArena[Level], true);
            }
        }

        public void Run(string mode, string path, bool useless)
        {
            JToken save = "";
            using (FileStream fs = File.OpenRead("../../../../saveStates/" + path + "/saveState.json"))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {
                save = JToken.ReadFrom(jr);
            }
            _arena = new Arena(save);
            GameLoop(Arena, mode, path);
        }

        public void Run(string mode, string path)
        {
            GameLoop(Arena, mode, path);
        }

        public void RunHistory(string path)
        {
            GameLoop(Arena, "history", path);
        }

        public void Render(Arena arena, string mode, string path)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            draw.BackGroundGame(mode, path);
            draw.UnitDisplay(arena);
            Window.Display();
        }

        internal double[] StartingHealth(Arena arena)
        {
            double[] healths = new double[4];
            int i = 0;
            foreach (KeyValuePair<string, Team> kv in arena.Teams)
            {
                healths[i] = kv.Value.HealthCalculation();
                i++;
            }

            return healths;
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

            Run("play", "");
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
            int max = 0;

            for (int f = 0; f < 6; f++)
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

        public Shape[] InitGUIElder(string[] dNames, string[] winner, string[] tours)
        {
            Window.Clear();
            Shape[] buttons = draw.ElderGameDisplay(dNames, winner, tours);
            Window.Display();

            return buttons;
        }

        #region Credit
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
        #endregion

        #region pause
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

        #endregion

        #region HistoryMode

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Shape[] InitLevelSelection()
        {
            Window.Clear();
            Shape[] buttons = draw.HistoryLvSeletionDisplay();
            Window.Display();

            return buttons;
        }

        internal void LevelSelectionMenu()
        {
            Shape[] buttons = InitLevelSelection();

            while (Window.IsOpen && Status == "history")
            {
                _iHandler.HandlerLVSelection(buttons);
            }
        }

        public Shape[] InitHistoryPreGame(int[,] teamComposition)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            Shape[] buttons = draw.HistoryPreGameDisplay(teamComposition);
            Window.Display();
            return buttons;
        }

        internal void HistoryPreGame(Arena arena)
        {
            Team team1 = arena.GetTeam("red"); //computer team 
            Team team2 = arena.CreateTeam("blue"); //player team
            team2.GoldAmount = 500.0;

            int[,] teamComposition =
            {
                {team2.Acount, team2.Bcount, team2.Ccount, team2.Dcount, team2.Gcount, team2.Pcount, (int)team2.GoldAmount},// Player Team
                {team1.Acount, team1.Bcount, team1.Ccount, team1.Dcount, team1.Gcount, team1.Pcount, 0},// Computer team
            };
            double previous = GetCurrentTime();

            while (Window.IsOpen && Status == "historyPreGame")
            {
                double current = GetCurrentTime();
                if (current - previous >= 0.0000019)
                {
                    previous = current;
                    Shape[] buttons = InitHistoryPreGame(teamComposition);
                    Window.DispatchEvents();
                    teamComposition = _iHandler.HandlerHistoryPreGame(teamComposition,team2, buttons);
                }
                Arena.SpawnUnit();
            }
        }
        #endregion

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

        public string ElderGame()
        {
            string path = "";
            DirectoryInfo dir = new DirectoryInfo("../../../../saveStates");

            DirectoryInfo[] dossiers = dir.GetDirectories();
            string[] dNames = new string[10];

            int[]    winner = new int   [10];
            string[] winningTeam = new string[10];
            string[] tours  = new string[10];
            
            string[] paths = new  string[10];
            
            int j = 0;

            for (int i = dossiers.Length - 1; i >= 0 && i > dossiers.Length - 11 ; i--)
            {
                dNames[j] = dossiers[i].Name;
                j++;
            }

            for (int i = 0; i < dNames.Length; i++)
            {
                paths[i] = "../../../../saveStates/" + dNames[i] + "/endGame.JSON";

                if (File.Exists(paths[i]) == true)
                {

                    if (dNames[i] != null)
                    {
                        using (FileStream fs = File.OpenRead(paths[i]))
                        using (StreamReader sr = new StreamReader(fs))
                        using (JsonTextReader jr = new JsonTextReader(sr))
                        {
                            JToken token = JToken.ReadFrom(jr);

                            winner[i] = token["winner"].Value<int>();
                            tours[i] = token["tour"].Value<int>().ToString();
                        }
                        if (winner[i] == 0) winningTeam[i] = "Blue";
                        else if (winner[i] == 1) winningTeam[i] = "Red";
                        else if (winner[i] == 2) winningTeam[i] = "Green";
                        else winningTeam[i] = "Yellow";
                    }
                }
                else
                {
                    winningTeam[i] = "Unfinished";
                    tours[i] = "";
                }
            }

            Shape[] buttons = InitGUIElder(dNames, winningTeam, tours);

            while (Window.IsOpen && Status == "elderGame")
            {
               path = _iHandler.HandlerHistoric( buttons, dNames);
            }

            return path;
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

        

        #region Result Pages
        internal Shape[] InitResult()
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
        public void ResultWindow()
        {
            Shape[] buttons = InitResult();

            while (Window.IsOpen && Status == "ended")
            {
                _iHandler.HandlerResult(buttons);
            }
        }

        internal void HistoryResultWindow()
        {
            Shape[] buttons = InitHistoryResult();

            while (Window.IsOpen && Status == "historyEnded")
            {
                _iHandler.HandlerHistoryResult(buttons);
            }
        }

        private Shape[] InitHistoryResult()
        {

            Window.Clear();

            Shape[] buttons = draw.HistoryResultDisplay(Winner);
            Window.Display();

            return buttons;
        }

        #endregion


        public void ReturnMenu()
        {
            Shape[] buttons = InitReturn();
            while (Window.IsOpen && Status == "return")
            {
                _iHandler.HandlerReturn(buttons);
            }
        }

        #region Pregamge
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
                if (current - previous >= 0.0000025)
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
                            red.AddArcher(teamComposition[0, i]);
                            break;
                        case 1:
                            red.AddDrake(teamComposition[0, i]);
                            break;
                        case 2:
                            red.AddGoblin(teamComposition[0, i]);
                            break;
                        case 3:
                            red.AddPaladin(teamComposition[0, i]);
                            break;
                        case 4:
                            red.AddBalista(teamComposition[0, i]);
                            break;
                        case 5:
                            red.AddCatapult(teamComposition[0, i]);
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
                            blue.AddArcher(teamComposition[1, i]);
                            break;
                        case 1:
                            blue.AddDrake(teamComposition[1, i]);
                            break;
                        case 2:
                            blue.AddGoblin(teamComposition[1, i]);
                            break;
                        case 3:
                            blue.AddPaladin(teamComposition[1, i]);
                            break;
                        case 4:
                            blue.AddBalista(teamComposition[1, i]);
                            break;
                        case 5:
                            blue.AddCatapult(teamComposition[1, i]);
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
                TeamCompo = teamComposition;
                //Arena.SpawnUnit();
            }
        }

        public Shape[] InitPlacement(string[] status, int[,]compoLeft, Unit[] unitToDraw)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            Shape [] buttons = draw.PlacementDisplay(Arena, status, _teamComposition, compoLeft, unitToDraw);
            Window.Display();
            return buttons;
        }

        public void Placement()
        {
            string[] status = new string[9];
            status[0] = "red ";
            status[1] = "10";
            status[2] = "archer";
            status[3] = "NA";
            status[4] = "NA";
            status[5] = "NA";
            status[6] = "NA";
            status[7] = "?";
            status[8] = "false";
            double previous = GetCurrentTime();
            int[,] compoLeft = TeamCompo;
            int[,] actualCompo = (int[,])TeamCompo.Clone();
            Unit[] unitToDraw = new Unit[800];

            for (int i = 0; i < 800; i++)
            {
                unitToDraw[i] = null;
            }

            while (Window.IsOpen && Status=="placement")
            {
                
                double current = GetCurrentTime();
                if (current - previous >= 0.000002)
                {
                    status[7] = "?";
                    for (int i = 0; i < compoLeft.GetLength(0); i++)
                    {
                        for (int j = 0; j < compoLeft.GetLength(1); j++)
                        {
                            if (compoLeft[i, j] != 0)
                            {
                                status[7] = "false";
                            }
                        }
                    }
                    if (status[7]!="false")
                    {
                        status[7] = "true";
                    }
                    previous = current;
                    Shape[] buttons  = InitPlacement(status, compoLeft, unitToDraw);
                    Window.DispatchEvents();
                    status = _iHandler.HandlerPlacement(status, buttons, Arena);
                    if (status[6]!="NA")
                    {
                        double x = Int32.Parse(status[3]);
                        double y = Int32.Parse(status[4]);
                        double x2 = Int32.Parse(status[5]);
                        double y2 = Int32.Parse(status[6]);
                        x = ((x-256)*250)/256;
                        x2 =((x2-256)*250)/256;
                        y =((y-256)*250)/256;
                        y2 = ((y2-256)*250)/256;

                        double xLength = x - x2;
                        double yLength = y - y2;

                        int unitNumberToSpawn = 0;
                        int unitIndice=0;
                        int activeTeam=0;
                        Team team = null;
                        switch (status[0])
                        {
                            case "red":
                                activeTeam = 0;
                                team = Arena.GetTeam("red");
                                break;
                            case "blue":
                                team = Arena.GetTeam("blue");
                                activeTeam = 1;
                                break;
                            case "green":
                                activeTeam = 2;
                                team = Arena.GetTeam("green");
                                break;
                            case "yellow":
                                team = Arena.GetTeam("yellow");
                                activeTeam = 3;
                                break;
                        }

                        switch (status[2])
                        {
                            case "archer":
                                unitIndice = actualCompo[activeTeam, 0] - compoLeft[activeTeam, 0];
                                if (compoLeft[activeTeam,0]>=Int32.Parse(status[1]))
                                {
                                    unitNumberToSpawn = Int32.Parse(status[1]);
                                }
                                else
                                {
                                    unitNumberToSpawn = compoLeft[activeTeam, 0];
                                }
                                compoLeft[activeTeam, 0] -= unitNumberToSpawn;
                                break;
                            case "drake":
                                unitIndice =actualCompo[activeTeam,0] +actualCompo[activeTeam, 1] - compoLeft[activeTeam, 1];
                                if (compoLeft[activeTeam, 1] >= Int32.Parse(status[1]))
                                {
                                    unitNumberToSpawn = Int32.Parse(status[1]);
                                }
                                else
                                {
                                    unitNumberToSpawn = compoLeft[activeTeam, 1];
                                }
                                compoLeft[activeTeam, 1] -= unitNumberToSpawn;
                                break;
                            case "goblin":
                                unitIndice =actualCompo[activeTeam,0]+ actualCompo[activeTeam, 1] + actualCompo[activeTeam, 2] - compoLeft[activeTeam, 2];
                                if (compoLeft[activeTeam, 2] >= Int32.Parse(status[1]))
                                {
                                    unitNumberToSpawn = Int32.Parse(status[1]);
                                }
                                else
                                {
                                    unitNumberToSpawn = compoLeft[activeTeam, 2];
                                }
                                compoLeft[activeTeam, 2] -= unitNumberToSpawn;
                                break;
                            case "paladin":
                                unitIndice =actualCompo[activeTeam,0]+ actualCompo[activeTeam, 1] + actualCompo[activeTeam, 2] + actualCompo[activeTeam, 3] - compoLeft[activeTeam, 3];
                                if (compoLeft[activeTeam, 3] >= Int32.Parse(status[1]))
                                {
                                    unitNumberToSpawn = Int32.Parse(status[1]);
                                }
                                else
                                {
                                    unitNumberToSpawn = compoLeft[activeTeam, 3];
                                }
                                compoLeft[activeTeam, 3] -= unitNumberToSpawn;
                                break;
                            case "balista":
                                unitIndice =actualCompo[activeTeam,0]+ actualCompo[activeTeam, 1] + actualCompo[activeTeam, 2] + actualCompo[activeTeam, 3] + actualCompo[activeTeam, 4] - compoLeft[activeTeam, 4];
                                if (compoLeft[activeTeam, 4] >= Int32.Parse(status[1]))
                                {
                                    unitNumberToSpawn = Int32.Parse(status[1]);
                                }
                                else
                                {
                                    unitNumberToSpawn = compoLeft[activeTeam, 4];
                                }
                                compoLeft[activeTeam, 4] -= unitNumberToSpawn;
                                break;
                            case "catapult":
                                unitIndice =actualCompo[activeTeam,0]+ actualCompo[activeTeam, 1] + actualCompo[activeTeam, 2] + actualCompo[activeTeam, 3] + actualCompo[activeTeam, 4] + actualCompo[activeTeam, 5] - compoLeft[activeTeam, 5];
                                if (compoLeft[activeTeam, 5] >= Int32.Parse(status[1]))
                                {
                                    unitNumberToSpawn = Int32.Parse(status[1]);
                                }
                                else
                                {
                                    unitNumberToSpawn = compoLeft[activeTeam, 5];
                                }
                                compoLeft[activeTeam, 5] -= unitNumberToSpawn;
                                break;
                        }

                        double xSpacing = xLength / unitNumberToSpawn; 
                        double ySpacing = yLength / unitNumberToSpawn;

                        for (int i = unitIndice; i < unitIndice+unitNumberToSpawn; i++)
                        {

                            Unit unit = team.FindUnitByName(i);
                            Arena.SpawnUnit(unit, x, y);
                            for (int j = 0; j < 800; j++)
                            {
                                if (unitToDraw[j]==null)
                                {
                                    unitToDraw[j] = unit;
                                    break;
                                }
                            }
                            x -= xSpacing;
                            y -= ySpacing;
                        }
                       

                        for (int i = 3; i < status.Length; i++)
                        {
                            status[i] = "NA";
                        }
                    }
                }
            }

            if (status[8]=="true")
            {
                Arena.SpawnUnit();
            }
  

        }



        #endregion

        #endregion

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
    }
}