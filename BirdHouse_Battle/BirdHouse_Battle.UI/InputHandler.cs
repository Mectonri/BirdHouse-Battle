using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

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
        }

        public void HandlerMain(RectangleShape [] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Window.Close();
            }
            //else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition().X, Mouse.GetPosition().Y)==false && Mouse.IsButtonPressed(Mouse.Button.Left))     
            else if(Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                game.Status = "game";
            }
        }
    }
}
