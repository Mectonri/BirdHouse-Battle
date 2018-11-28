using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Diagnostics;
using System.Threading;
using BirdHouse_Battle.Model;

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
                game.Status = "game";
            }
        }

        public string[] HandlerPreGame(RectangleShape[] buttons, string[] status)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Window.Close();
                return status;
            }
            else if ((buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[0] == "1"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "2")
                    {
                        status[i] = "1";
                    }
                }
                status[0] = "2";
                return status;
            }
            else if ((buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[1] == "1"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i]=="2")
                    {
                        status[i] = "1";
                    }
                }
                status[1] = "2";
                return status;
            }
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[2] == "0")
            {
                status[2] = "1";
                return status;
            }
            else if ((buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[2]=="1"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "2")
                    {
                        status[i] = "1";
                    }
                }
                status[2] = "2";
                return status;
            }
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[3] == "0")
            {
                status[3] = "1";
                return status;
            }
            else if ((buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[3] == "1"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "2")
                    {
                        status[i] = "1";
                    }
                }
                status[3] = "2";
                return status;
            }
            else
            {
                return status;
            }
        }
    }
}