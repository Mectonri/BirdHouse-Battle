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

        public Game()
        {
            SFML.SystemNative.Load();
            SFML.WindowNative.Load();
            SFML.GraphicsNative.Load();
            SFML.AudioNative.Load();

            _window = new RenderWindow(new VideoMode(512, 512), "BirdHouseBattle", Styles.Default);
            //InputHandler inputHandler = new InputHandler(this); 
            // IL NE FAUT JAMAIS FAIRE D'INITIALISATION DANS UN CONSTRUCTUER.
            _arena = new Arena();
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
        /// C'EST QUOI CE TRUC ?
        /// </summary>
        void InitGUI()
        {

        }

        /// <summary>
        /// C'EST QUOI CE TRUC ?
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

        private void WindowClosed(object sender, EventArgs e)
        {
            _window.Close();
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
            red.AddDrake(10);
            blue.AddArcher(15);
            blue.AddGobelin(55);
            blue.AddPaladin(55);
            blue.AddDrake(10);

            green.AddArcher(15); // Part Two
            green.AddGobelin(55);
            green.AddPaladin(55);
            green.AddDrake(10);
            yellow.AddArcher(15);
            yellow.AddGobelin(55);
            yellow.AddPaladin(55);
            yellow.AddDrake(10);

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
            //Window.DispatchEvents();
            
            double previous = getCurrentTime();
            double lag = 0.0;
            double current;
            double elapsed;

            while (arena.TeamCount > 1)
            {
                TimeLaps(lag, previous, out current, out elapsed, out previous, out lag);

                update(arena);

                while (lag <= MS_PER_UPDATE)
                {
                    TimeLaps(lag, previous, out current, out elapsed, out previous, out lag);
                }
                lag -= MS_PER_UPDATE;

                Render(arena);
            }
        }

        public void Render(Arena arena)
        {
            Window.Clear();
            Drawer draw = new Drawer(Window);
            draw.UnitDisplay(arena);
            Window.Display();
        }

        public void Run()
        {
            Window.DispatchEvents();

            Prep(Arena);
            GameLoop(Arena);

            //WindowClosed(); // INCOMPLET
        }
    }
}
