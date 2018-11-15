using SFML.Window;
using System;

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
                game.Window.Close();
            }
            //Conditioon to start the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                game.Paused = !game.Paused;
            }
            //condition to pause the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                game.Paused = !game.Paused;
            }
        }
    }
}