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
        InputHandler iHandler;
        public bool _paused = true; // track whether the game is paused or not
        #endregion

        string _status;

        public Game()
        {
            Load();

            iHandler = new InputHandler(this);

            _arena = new Arena();

            _window = new RenderWindow(new VideoMode(512, 512), "BirdHouseBattle", Styles.Default);

            _paused = Paused; // the game start paused so this bool = true
            _arena = new Arena();
            _status = "main";

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

        static double getCurrentTime()
        {
            return DateTime.Now.ToOADate();
        }

        static void update(Arena arena)
        {
            arena.Update();
        }

        static double MS_PER_UPDATE
        {
            get { return 0.0000006; }
        }
        
        public void TimeLaps(double Lag, double Previous, out double current, out double elapsed, out double previous, out double lag)
        {
            lag = Lag;
            previous = Previous;
            current = getCurrentTime();
            elapsed = current - previous;
            previous = current;
            lag += elapsed;
        }

        #endregion




        public void GameLoop(Arena arena)
        {

            double previous = getCurrentTime();
            double lag = 0.0;
            double current;
            double elapsed;

            Window.DispatchEvents();
            iHandler.Handler();

            while (arena.TeamCount > 1)
            {
                while(Paused)
                {
                    iHandler.Handler();
                    previous = getCurrentTime();
                }

                TimeLaps(lag, previous, out current, out elapsed, out previous, out lag);

                update(arena);

                while (lag <= MS_PER_UPDATE)
                {
                    TimeLaps(lag, previous, out current, out elapsed, out previous, out lag);
                }
                lag -= MS_PER_UPDATE;

                iHandler.Handler();
                if (!_window.IsOpen) break;
                Render(arena);

            }
            Status = "main";
            arena.Teams.Clear();
            arena.Projectiles.Clear();
            arena.DeadTeams.Clear();
            arena.DeadProjectiles.Clear();
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
            Prep(Arena);
            GameLoop(Arena);
        }



        public void MainMenu()
        {
            while (Window.IsOpen && Status=="main")
            {
                RectangleShape[] buttons = InitGUI();
                Window.DispatchEvents();
                iHandler.HandlerMain(buttons);
            }
        }

    }
}
