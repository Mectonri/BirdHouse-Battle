using BirdHouse_Battle.Model;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Reflection.Metadata.Ecma335;

namespace BirdHouse_Battle.UI
{
    public class Game
    {
        #region Fields
        RenderWindow _window;
        Arena _arena;
        InputHandler _iHandler;
        bool _paused; // track whether the game is paused or not
        bool _return;
        double _previousP;

        #endregion

        string _status;

        public Game()
        {
            Load();
            _iHandler = new InputHandler(this);
            _arena = new Arena();
            _window = new RenderWindow(new VideoMode(512, 512), "BirdHouseBattle", Styles.Default);
            _status = "main";
            _previousP = GetCurrentTime();
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

        
        #endregion

        static void Load()
        {
            SFML.SystemNative.Load();
            SFML.WindowNative.Load();
            SFML.GraphicsNative.Load();
            SFML.AudioNative.Load();
        }

        /// <summary>
        ///Asks for a YES/NO confirmation  Game
        /// </summary>
        void ExitConfirm()
        {
            
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

        static double MsPerUpdate
        {
            get { return 0.0000006; }
        }

        private static double TimeP
        {
            get { return 0.00002; }
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
                    }
                    break;

                case "ESC":
                    Window.Close();
                    _status = "close";
                    Console.WriteLine("ESC key is pressed");
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
            Team blue = arena.CreateTeam("blue"); // Part One
            Team red = arena.CreateTeam("red");

            Team green = arena.CreateTeam("green"); // Part Two
            Team yellow = arena.CreateTeam("yellow");

            //Each unit is represented by a shape
            //Archers are triagles, goblins by circles and paladin by rectangular shapes

            red.AddArcher(10); // Part One
            red.AddGobelin(55);
            red.AddPaladin(55);
            red.AddDrake(5);
            blue.AddArcher(10);
            blue.AddGobelin(55);
            blue.AddPaladin(55);
            blue.AddDrake(5);

            green.AddArcher(10); // Part Two
            green.AddGobelin(55);
            green.AddPaladin(55);
            green.AddDrake(5);
            yellow.AddArcher(10);
            yellow.AddGobelin(55);
            yellow.AddPaladin(55);
            yellow.AddDrake(5);

            arena.SpawnUnit();
        }

        private void WindowEscaping(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) Window.Close();
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            _window.Close();
        }

        public void Render(Arena arena)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            draw.UnitDisplay(arena);
            Window.Display();
        }


        /// <summary>
        /// Init the main menu
        /// </summary>
        public RectangleShape[] InitGUI()
        {

            Window.Clear();
            Drawer draw = new Drawer(Window);
            RectangleShape[] buttons = draw.MenuDisplay();
            Window.Display();


            return buttons;
        }


        public void Run()
        {
            //Prep(Arena);
            GameLoop(Arena);
        }

       

        public void MainMenu()
        {
            while (Window.IsOpen && Status=="main")
            {
                RectangleShape[] buttons = InitGUI();
                Window.DispatchEvents();
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
                {0,0,0,0 },
                {0,0,0,0 },
                {0,0,0,0 },
                {0,0,0,0 }
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
                        default:
                            break;
                    }
                    status[4] = "none";
                }
            }

            if (Status != "preGame")
            {

                    for (int i = 0; i < teamComposition.GetLength(0); i++)
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

                            default:
                                break;
                        }
                    }




                    for (int i = 0; i < teamComposition.GetLength(0); i++)
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

                            default:
                                break;
                        }
                    }
             

                if (Arena.FindTeam("green") == true)
                {
                    for (int i = 0; i < teamComposition.GetLength(0); i++)
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

                            default:
                                break;
                        }
                    }
                }

                if (Arena.FindTeam("yellow") == true)
                {
                    for (int i = 0; i < teamComposition.GetLength(0); i++)
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