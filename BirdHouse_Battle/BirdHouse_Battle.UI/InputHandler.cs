using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;
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
                game.Switch("ESC");
            }
            //Conditioon to start the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                game.Switch("RETURN");
            }
            //condition to pause the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                game.Switch("P");
            }
        }

        public void HandlerMain(RectangleShape [] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Window.Close();
            }
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))     
            {
                game.Status = "preGame";
            }
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }
        }

        public void HandlerPreGame(RectangleShape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Window.Close();
            }
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }

        }
    }
}