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
        private double MsPerUpdate = 0.0000006;
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
            string s = Keyboard.Key.Right.ToString();
            Console.WriteLine(s);

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
                        if (MsPerUpdate >=0.000006/100)
                        {

                            MsPerUpdate = MsPerUpdate / 5;
                        }
                    }
                    break;

                case "Left":
                    if (GetCurrentTime() - PreviousP >= TimeP)
                    {
                        _previousP = GetCurrentTime();
                        Console.WriteLine("Left key is pressed");
                        if (MsPerUpdate <= 0.000006*100)
                        {
                            MsPerUpdate = MsPerUpdate * 5;
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
            blue.AddGobelin(5);
            blue.AddPaladin(5);
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
            Prep(Arena);
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

    }
}
