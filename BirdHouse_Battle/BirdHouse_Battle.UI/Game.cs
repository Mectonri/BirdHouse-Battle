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

            InputHandler inputHandler = new InputHandler(this);

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

        void Render(Arena arena)
        {
            Window.Clear();
            Drawer d = new Drawer(Window);
            d.UnitDisplay(arena);
            Window.Display();
        }

        void InitGUI()
        {

        }

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

        void Update(Arena arena)
        {
            Window.DispatchEvents();

            //GameLoop;
            double previous = getCurrentTime();
            double lag = 0.0;
            while (arena.TeamCount > 1)
            {
                double current = getCurrentTime();
                double elapsed = current - previous;
                previous = current;
                lag += elapsed;

                while (lag >= MS_PER_UPDATE)
                {
                    update(arena);
                    lag -= MS_PER_UPDATE;
                }

                Render(arena);
            }
        }



        public void Prep(Arena arena)
        {
            //Preparation of the game.



            Team blue = arena.CreateTeam("blue"); // Part One
            Team red = arena.CreateTeam("red");

            //Team green = arena.CreateTeam("green"); // Part Two
            //Team yellow = arena.CreateTeam("yellow");

            //Each unit is represented by a shape
            //Archers are triagles, goblins by circles and paladin by rectangular shapes

            red.AddArcher(15); // Part One
            red.AddGobelin(55);
            red.AddPaladin(55);
            blue.AddArcher(15);
            blue.AddGobelin(55);
            blue.AddPaladin(55);

            //green.AddArcher(15); // Part Two
            //green.AddGobelin(55);
            //green.AddPaladin(55);
            //yellow.AddArcher(15);
            //yellow.AddGobelin(55);
            //yellow.AddPaladin(55);

            arena.SpawnUnit();
            //End of Preparation            
        }

        public void Run()
        {
            Window.DispatchEvents();

            while (Window.IsOpen)
            {

                //inputHandler.Handler();
                Prep(Arena);
                Update(Arena); // ceci contien la gameloop
                Render(Arena);
            }
        }
    }
}
