using BirdHouse_Battle.Model;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace BirdHouse_Battle.UI
{
    public class Game
    {
        RenderWindow _window;
        Arena _arena;
        InputHandler iHandler;
        string _status;

        public Game()
        {
            SFML.SystemNative.Load();
            SFML.WindowNative.Load();
            SFML.GraphicsNative.Load();
            SFML.AudioNative.Load();
            iHandler = new InputHandler(this);
            _window = new RenderWindow(new VideoMode(512, 512), "BirdHouseBattle", Styles.Default);
            _arena = new Arena();
            _status = "main";

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

        public RenderWindow Window
        {
            get { return _window; }
        }


        /// <summary>
        /// Process events
        /// </summary>
        void ProcessEvents()
        {

        }

        /// <summary>
        ///Asks for a YES/NO confirmation  Game
        /// </summary>
        void ExitConf()
        {

        }

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

        public void Prep(Arena arena)
        {
            Team blue = arena.CreateTeam("blue"); // Part One
            Team red = arena.CreateTeam("red");

            Team green = arena.CreateTeam("green"); // Part Two
            Team yellow = arena.CreateTeam("yellow");

            //Each unit is represented by a shape
            //Archers are triagles, goblins by circles and paladin by rectangular shapes

            red.AddArcher(15); // Part One
            red.AddGobelin(55);
            red.AddPaladin(55);
            //red.AddDrake(10);
            blue.AddArcher(15);
            blue.AddGobelin(55);
            blue.AddPaladin(55);
            //blue.AddDrake(10);

            green.AddArcher(15); // Part Two
            green.AddGobelin(55);
            green.AddPaladin(55);
            //green.AddDrake(10);
            yellow.AddArcher(15);
            yellow.AddGobelin(55);
            yellow.AddPaladin(55);
            //yellow.AddDrake(10);

            arena.SpawnUnit();
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

        public void GameLoop(Arena arena)
        {    
            
            double previous = getCurrentTime();
            double lag = 0.0;
            double current;
            double elapsed;

            //! 
           
            Window.DispatchEvents();
            this.iHandler.Handler();
            //while (_window.IsOpen)
           
            while (arena.TeamCount > 1)
            {
                TimeLaps(lag, previous, out current, out elapsed, out previous, out lag);

                update(arena);

                while (lag <= MS_PER_UPDATE)
                {
                    TimeLaps(lag, previous, out current, out elapsed, out previous, out lag);
                }
                lag -= MS_PER_UPDATE;

                this.iHandler.Handler();
                if (!_window.IsOpen) break;
                Render(arena);
                
            }
            Status = "main";
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
            //WindowClosed(); // INCOMPLET
        }



        public void MainMenu()
        {
            while (Window.IsOpen && Status=="main")
            {
                RectangleShape[] buttons = InitGUI();
                Window.DispatchEvents();
                this.iHandler.HandlerMain(buttons);
            }
        }

    }
}
