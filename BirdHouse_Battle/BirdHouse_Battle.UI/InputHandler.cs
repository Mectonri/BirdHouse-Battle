using SFML.Window;
using System;
using System.Threading;

namespace BirdHouse_Battle.UI
{
    class InputHandler
    {
        private Game game;

        public InputHandler(Game game)
        {
            this.game = game;
        }

        public void Handler()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Console.WriteLine("ESC key is pressed");
                game.Window.Close();
                //Thread.Sleep(200);
            }
            //Conditioon to start the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                Console.WriteLine("RETURN key is pressed");
                game.Paused = !game.Paused;
                Thread.Sleep(200); // using sleep will take in count only one press of the key 
              // the right way to do it is using a key up event   
            }
            //condition to pause the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                Console.WriteLine("P key is pressed");
                game.Paused = !game.Paused;
                Thread.Sleep(200);
            }
        }
    }
}