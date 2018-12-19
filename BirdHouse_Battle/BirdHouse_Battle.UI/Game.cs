using BirdHouse_Battle.Model;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

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
            _arena = new Arena();
            _window = new RenderWindow(new VideoMode(512, 712), "BirdHouseBattle", Styles.Default);
            _status = "main";
            _previousP = GetCurrentTime();
            draw = new Drawer(_window);
            _winner = FindWinner();

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
            set {_arena = value; }
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

            FindWinner();

            Status = "ended";
            arena.Teams.Clear();
            arena.Projectiles.Clear();
            arena.DeadTeams.Clear();
            arena.DeadProjectiles.Clear();

            return true;
        }
        
        /// <summary>
        /// Find the winning team
        /// </summary>
        /// <returns></returns>
        public int FindWinner()
        {
            if (_arena.FindTeam("blue") == true)
            {
                Console.WriteLine("Blue team won");
                return _winner = 1;
            }
            else if (_arena.FindTeam("red") == true)
            {
                Console.WriteLine("Red team won");
                return _winner = 2;
            }
            else if (_arena.FindTeam("green") == true)
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

        //public void Prep(Arena arena)
        //{
        //    // Part One

        //    Team blue = arena.CreateTeam("blue");
        //    Team red = arena.CreateTeam("red");
        //    Team green = arena.CreateTeam("green");
        //    Team yellow = arena.CreateTeam("yellow");
        //    red.AddArcher(10); red.AddGobelin(40); red.AddPaladin(35);
        //    red.AddDrake(5); red.AddBalista(5); red.AddCatapult(5);
        //    blue.AddArcher(10); blue.AddGobelin(45); blue.AddPaladin(40);
        //    blue.AddDrake(5); blue.AddBalista(5); blue.AddCatapult(5);
        //    green.AddArcher(10); green.AddGobelin(45); green.AddPaladin(40);
        //    green.AddDrake(5); green.AddBalista(5); green.AddCatapult(5);
        //    yellow.AddArcher(10); yellow.AddGobelin(45); yellow.AddPaladin(40);
        //    yellow.AddDrake(5); yellow.AddBalista(5); yellow.AddCatapult(5);

        //    // Part Two

        //    //Team blue = arena.CreateTeam("blue");
        //    //Team red = arena.CreateTeam("red");
        //    //red.AddCatapult(10);
        //    //blue.AddPaladin(5);
        //    //blue.AddDrake(5);

        //    // Part Three

        //    //Team blue = arena.CreateTeam("blue");
        //    //Team red = arena.CreateTeam("red");
        //    //red.AddBalista(10);
        //    //blue.AddDrake(5);
        //    //blue.AddPaladin(5);

        //    // Part Four

        //    //Team blue = arena.CreateTeam("blue");
        //    //Team red = arena.CreateTeam("red");
        //    //red.AddBalista(3);
        //    //red.AddGobelin(5);
        //    //blue.AddCatapult(2);
        //    //blue.AddDrake(5);

        //    arena.SpawnUnit();
        //}

        public void RandomGame( Arena arena)
        {
            Random rn = new Random();
            int t = rn.Next(2, 5);

            for (int f = 1; f <= t; f++)
            {
                arena.CreateTeam( "Team"+ f.ToString() );
            }
            
            foreach (KeyValuePair<string, Team> i in arena.Teams)
            {
                i.Value.AddArcher(rn.Next(125));
                
                i.Value.AddBalista(rn.Next(125-i.Value.UnitCount));

                i.Value.AddCatapult(rn.Next(125 - i.Value.UnitCount));
               
                i.Value.AddDrake(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddGobelin(rn.Next(125 - i.Value.UnitCount));

                i.Value.AddPaladin(rn.Next(125 - i.Value.UnitCount));
            }

            arena.SpawnUnit();
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
                    Status = "close";
                    Console.WriteLine("switch : ESC key is pressed");
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
            while(Window.IsOpen && Status == "credit")
            {
                Shape[] buttons = InitCredit();
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

        /// <summary>
        /// Display Main Menu
        /// </summary>
        public void MainMenu()
        {
            while (Window.IsOpen && Status == "main")
            {
                Shape[] buttons = InitGUI();
                _iHandler.HandlerMain(buttons);
            }
        }
        
        public Shape[] InitExit()
        {
            Window.Clear();           
            Shape[] buttons = draw.ExitDisplay();
           
            Window.Display();
            return buttons;
        }

        /// <summary>
        ///Asks for a YES/NO confirmation  Game
        /// </summary>
        public void ExitMenu()
        {
            while (Window.IsOpen && Status == "close")
            {
                Shape[] buttons = InitExit();
                _iHandler.HandlerExit(buttons);
            }
        }
      
        public Shape[] InitPreGame(string[] status, int[,] teamComposition)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            Shape[] buttons = draw.PreGameDisplay(status, teamComposition);
            Window.Display();
            return buttons;
        }

        public void ResultWindow()
        {
            while (Window.IsOpen && Status == "ended")
            {
                Shape[] buttons = InitEnd();
                _iHandler.HandlerEnd(buttons);
            }
        }

        private Shape[] InitEnd()
        {
            Window.Clear();
            Font font = new Font("../../../../res/Overlock-Bold.ttf");
            Text ToursFinal = new Text(_tour.ToString() + "TURNS", font, 50)
            {
                Position = new Vector2f(150, 550)
            };
            Shape[] buttons = draw.EndDisplay(Winner);
            Window.Draw(ToursFinal);
            Window.Display();

            return buttons;
        }
        
        public void PreGame()
        {
            
            Team blue = Arena.CreateTeam("blue"); 
            Team red = Arena.CreateTeam("red");
            Team green = null;
            Team yellow = null;
            string [] status =new string [5];
            status[0] = "selected";
            status[1] = "active";
            status[2]= "inactive";
            status[3] = "inactive";

            status[4] = "none";

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
                            for (int i = 0; i < status.Length - 1; i++)
                            {
                                if (status[i] == "selected")
                                {
                                    teamComposition[i, 0] +=1;
                                }
                            }
                            break;
                        case "drake":
                            for (int i = 0; i < status.Length - 1; i++)
                            {
                                if (status[i] == "selected")
                                {
                                    teamComposition[i, 1] +=1;
                                }
                            }
                            break;
                        case "gobelin":
                            for (int i = 0; i < status.Length - 1; i++)
                            {
                                if (status[i] == "selected")
                                {
                                    teamComposition[i, 2] +=1;
                                }
                            }
                            break;
                        case "paladin":
                            for (int i = 0; i < status.Length - 1; i++)
                            {
                                if (status[i] == "selected")
                                {
                                    teamComposition[i, 3] +=1;
                                }
                            }
                            break;
                        case "balista":
                            for (int i = 0; i < status.Length - 1; i++)
                            {
                                if (status[i] == "selected")
                                {
                                    teamComposition[i, 4] += 1;
                                }
                            }
                            break;
                        case "catapult":
                            for (int i = 0; i < status.Length - 1; i++)
                            {
                                if (status[i] == "selected")
                                {
                                    teamComposition[i, 5] += 1;
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