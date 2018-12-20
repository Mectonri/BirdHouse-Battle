using BirdHouse_Battle.Model;
using SFML.Graphics;
using SFML.Window;
using System;

namespace BirdHouse_Battle.UI
{
    public class Game
    {
        #region Fields
        RenderWindow _window;
        Arena _arena;
        InputHandler _iHandler;
        private Drawer draw;
        bool _paused; // track whether the game is paused or not
        bool _return;
        double _previousP;
        Color white;
        double _msPerUpdate = 0.0000006;
        //double Accel;
        string _status;
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

            white = new Color(255, 255, 255);
        }

        #region Getter

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

        public RenderWindow Window
        {
            get { return _window; }
        }

        public bool Paused
        {
            get { return _paused; }
            set { _paused = value; }
        }

        public double MsPerUpdate
        {
            get { return _msPerUpdate; }
        }

        
        #endregion
        
        static void Load()
        {
            SFML.SystemNative.Load();
            SFML.WindowNative.Load();
            SFML.GraphicsNative.Load();
            SFML.AudioNative.Load();
        }
        
        #region Relevant to Gameloop

        static double GetCurrentTime()
        {
            return DateTime.Now.ToOADate();
        }

        static void Update(Arena arena)
        {
            arena.Update();

        }

        private static double TimeP
        {
            get { return 0.000002; }
        }

        public double PreviousP
        {
            get { return _previousP; }
        }

        public bool Return
        {
            get { return _return; }
        }

        public void Switch(string input)
        {
            switch (input)
            {
                case "P":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        Paused = !Paused;
                        _previousP = GetCurrentTime();
                        Console.WriteLine("P key is pressed");
                        _status = "pause";
                    }
                    break;

                case "ESC":
                    Window.Close();
                    _status = "close";
                    Console.WriteLine("ESC key is pressed");
                    break;

                case "Right":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        _previousP = GetCurrentTime();
                        Console.WriteLine("right arrow is pressed");
                        if (MsPerUpdate > 0.0000006/200)
                        {

                            _msPerUpdate = MsPerUpdate / 20;
                        }
                    }
                    break;

                case "Left":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        _previousP = GetCurrentTime();
                        Console.WriteLine("Left key is pressed");
                        if (MsPerUpdate < 0.0000006)
                        {
                            _msPerUpdate = MsPerUpdate * 20;
                        }
                    }
                    break;

                case "RETURN":
                    _return = false;
                    break;
            }
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
                }

                if (!_window.IsOpen || !Return)
                {
                    Status = "main";
                    arena.Teams.Clear();
                    arena.Projectiles.Clear();
                    arena.DeadTeams.Clear();
                    arena.DeadProjectiles.Clear();
                }
                Render(arena);
            }

            Status = "main";
            arena.Teams.Clear();
            arena.Projectiles.Clear();
            arena.DeadTeams.Clear();
            arena.DeadProjectiles.Clear();

            return true;
        }

        public void Prep(Arena arena)
        {
            // Part One

            Team blue = arena.CreateTeam("blue");
            Team red = arena.CreateTeam("red");
            Team green = arena.CreateTeam("green");
            Team yellow = arena.CreateTeam("yellow");
            red.AddArcher(10); red.AddGobelin(40); red.AddPaladin(35);
            red.AddDrake(5); red.AddBalista(5); red.AddCatapult(5);
            blue.AddArcher(10); blue.AddGobelin(45); blue.AddPaladin(40);
            blue.AddDrake(5); blue.AddBalista(5); blue.AddCatapult(5);
            green.AddArcher(10); green.AddGobelin(45); green.AddPaladin(40);
            green.AddDrake(5); green.AddBalista(5); green.AddCatapult(5);
            yellow.AddArcher(10); yellow.AddGobelin(45); yellow.AddPaladin(40);
            yellow.AddDrake(5); yellow.AddBalista(5); yellow.AddCatapult(5);

            // Part Two

            //Team blue = arena.CreateTeam("blue");
            //Team red = arena.CreateTeam("red");
            //red.AddCatapult(10);
            //blue.AddPaladin(5);
            //blue.AddDrake(5);

            // Part Three

            //Team blue = arena.CreateTeam("blue");
            //Team red = arena.CreateTeam("red");
            //red.AddBalista(10);
            //blue.AddDrake(5);
            //blue.AddPaladin(5);

            // Part Four

            //Team blue = arena.CreateTeam("blue");
            //Team red = arena.CreateTeam("red");
            //red.AddBalista(3);
            //red.AddGobelin(5);
            //blue.AddCatapult(2);
            //blue.AddDrake(5);

            arena.SpawnUnit();
        }

        //private void WindowEscaping(object sender, KeyEventArgs e)
        //{
        //    if (e.Code == Keyboard.Key.Escape) Window.Close();
        //}

        //private void WindowClosed(object sender, EventArgs e)
        //{
        //    _window.Close();
        //}

        public void Render(Arena arena)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            draw.BackGroundGame();
            draw.UnitDisplay(arena);
            Window.Display();
        }

        /// <summary>
        /// Init the main menu
        /// </summary>
        public RectangleShape[] InitGUI()
        {
            
            Window.Clear( white);
            
           //Drawer draw = new Drawer(Window);
            RectangleShape[] buttons = draw.MenuDisplay();
            Window.Display();
            
            return buttons;
        }

        public RectangleShape[] InitPause()
        {
            //throw new NotImplementedException();
            
            Window.Clear(white);
            
            RectangleShape[] buttons = draw.PauseDisplay();
            Window.Display();

            return buttons;
        }

        public void Run()
        {
            //Prep(Arena);
            GameLoop(Arena);
        }

        /// <summary>
        ///Asks for a YES/NO confirmation  Game
        /// </summary>
        public void ExitMenu()
        {

        }


        /// <summary>
        /// Trigered xwhen the game is on pause
        /// </summary>
        public void PauseMenu()
        {
            while (Window.IsOpen && Status == "pause")
            {
                RectangleShape[] buttons = InitPause();
                _iHandler.HandlerPause(buttons);
            }
        }
        
        /// <summary>
        /// trigered when game start
        /// </summary>
        public void MainMenu()
        {
            while (Window.IsOpen && Status=="main")
            {
                RectangleShape[] buttons = InitGUI();
                
                _iHandler.HandlerMain(buttons);
            }
        }

        public RectangleShape[] InitPreGame(string[] status, int[,] teamComposition)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            RectangleShape[] buttons = draw.PreGameDisplay(status, teamComposition);
            Window.Display();
            return buttons;
        }

        public bool IsValidToAddUnit(int [,]TeamCompo, int i)
        {
            int count = 0;
            for (int j = 0; j< 6; j++)
            {
                count += TeamCompo[i, j];
            }
            if (count>123)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


        public void PreGame()
        {
            Team blue = Arena.CreateTeam("blue"); 
            Team red = Arena.CreateTeam("red");
            Team green = null;
            Team yellow = null;
            string [] status =new string [7];
            status[0] = "selected";
            status[1] = "active";
            status[2]= "inactive";
            status[3] = "inactive";

            status[4] = "none";
            status[5] = "add";
            status[6] = "1";



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
                    RectangleShape[] buttons = InitPreGame(status, teamComposition);
                    Window.DispatchEvents();
                    status = _iHandler.HandlerPreGame(buttons, status);


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
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i)==true && status[5]=="add")
                                {
                                    teamComposition[i, 0] += Int32.Parse(status[6]);
                                }
                                else 
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
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i) == true && status[5] == "add")
                                {
                                    teamComposition[i, 1] += Int32.Parse(status[6]);
                                }
                                else
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 0] < 0)
                                    {
                                        teamComposition[i, 0] = 0;
                                    }
                                }
                            }
                            break;
                        case "gobelin":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i) == true && status[5] == "add")
                                {
                                    teamComposition[i, 2] += Int32.Parse(status[6]);
                                }
                                else 
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 0] < 0)
                                    {
                                        teamComposition[i, 0] = 0;
                                    }
                                }
                            }
                            break;
                        case "paladin":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i) == true && status[5] == "add")
                                {
                                    teamComposition[i, 3] += Int32.Parse(status[6]);
                                }
                                else
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 0] < 0)
                                    {
                                        teamComposition[i, 0] = 0;
                                    }
                                }
                            }
                            break;
                        case "balista":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i) == true && status[5] == "add")
                                {
                                    teamComposition[i, 4] += Int32.Parse(status[6]);
                                }
                                else 
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 0] < 0)
                                    {
                                        teamComposition[i, 0] = 0;
                                    }
                                }
                            }
                            break;
                        case "catapult":
                            for (int i = 0; i < 4; i++)
                            {
                                if (status[i] == "selected" && IsValidToAddUnit(teamComposition, i) == true && status[5] == "add")
                                {
                                    teamComposition[i, 5] += Int32.Parse(status[6]);
                                }
                                else
                                {
                                    teamComposition[i, 0] -= Int32.Parse(status[6]);
                                    if (teamComposition[i, 0] < 0)
                                    {
                                        teamComposition[i, 0] = 0;
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