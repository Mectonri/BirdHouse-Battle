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
            get { return 0.0000004; }
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
            draw.BackGroundGame();
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
            Prep(Arena);
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

    }
}
