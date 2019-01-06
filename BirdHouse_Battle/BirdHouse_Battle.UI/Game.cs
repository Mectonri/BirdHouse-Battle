using BirdHouse_Battle.Model;
using Newtonsoft.Json;
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

        public int Winner =>_winner;

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

        public bool GameLoop(Arena arena)
        {
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

        //private void ListTeam(Arena arena)
        //{
        //    foreach (KeyValuePair<string, Team> team in arena.Teams)
        //    {
        //        Console.WriteLine(team.Key);
        //        Console.WriteLine(team.Value.Name);

        //    }
        //}

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
        /// Load the selected level to be played
        /// </summary>
        internal void LoadLevel( int Level)
        {
            string path = Level.ToString(); //stranstaltes the level to a string to obtain it path

            using (FileStream fs = File.OpenRead(path))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {
                while (jr.Read())
                {
                    if (jr.Value != null)
                    {
                        Console.WriteLine("Token: {0}, Value: {1}", jr.TokenType, jr.Value);
                        CreateTeam( jr.Value, jr.Value, jr.Value, jr.Value, jr.Value, jr.Value );
                    }
                    else
                    {
                        Console.WriteLine("Token: {0}", jr.TokenType);
                    }
                }
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
        private void CreateTeam(int AToAdd, int BToAdd, int CToAdd, int DToAdd, int GToAdd, int PToAdd)
        {
            Team team = new Team( Arena, "red", 125);

            team.AddArcher(AToAdd);
            team.AddArcher(BToAdd);
            team.AddCatapult(CToAdd);
            team.AddDrake(DToAdd);
            team.AddGobelin(GToAdd);
            team.AddPaladin(PToAdd);
        }


        public void Run()
        {
            GameLoop(Arena);
        }

        public void Render(Arena arena )
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

                i.Value.AddGobelin(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddPaladin(rn.Next(125 - i.Value.UnitCount));
            }

            arena.SpawnUnit();

            Run();
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
            Window.Clear( );
            Shape[] buttons = draw.MenuDisplay();
            Window.Display();
            
            return buttons;
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

        internal void HistoryMenu()
        {
            Shape[] buttons = InitHistory();

            while (Window.IsOpen && Status == "history")
            {
                _iHandler.HandlerHistoryPage(buttons);
            }
        }

        private Shape[] InitHistory()
        {
            Window.Clear();
            Shape[] buttons = draw.HistoryPageDisplay();
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

        public bool IsValidToAddUnit(int [,]TeamCompo, int i, string[]status)
        {
            int count = 0;
            for (int j = 0; j< 6; j++)
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
            string [] status =new string [8];
            status[0] = "selected";
            status[1] = "active";
            status[2]= "inactive";
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
                if (current - previous >= 0.000002)
                {
                    previous = current;
                    Shape[] buttons = InitPreGame(status, teamComposition);
                    Window.DispatchEvents();
                    status = _iHandler.HandlerPreGame(buttons, status);

                    if (status[7] == "no")
                    {
                        for (int i= 0; i < status.Length; i++)
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
                                else if (status[i] == "selected" && status[5]!= "add")
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i,0] <0)
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
                                blue.AddGobelin(teamComposition[0, i]);
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
                                red.AddGobelin(teamComposition[1, i]);
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
                                green.AddGobelin(teamComposition[2, i]);
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
                                yellow.AddGobelin(teamComposition[3, i]);
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
    }
}
#endregion