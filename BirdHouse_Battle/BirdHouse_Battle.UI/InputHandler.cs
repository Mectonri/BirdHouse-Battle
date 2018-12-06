﻿using System;
using SFML.Graphics;
using SFML.Window;

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
            //Condition to start the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                game.Switch("RETURN");
            }
            //condition to pause the game
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                game.Switch("P");
            }
            if(Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                game.Switch("Right");
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            { game.Switch("Left"); }
        }

        public void HandlerPause(RectangleShape[] buttons)
        {
            Console.WriteLine("handlerPause");
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                Console.WriteLine("pause : ESC Key");
                game.Status = "game";
            }
            if ((Keyboard.IsKeyPressed(Keyboard.Key.P)))
            {
                game.Switch("P");
            }
            //CONTINUE button
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine("pause: continue button");
                game.Status = "game";
            }//RESART
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine("pause: restart button");
                game.Status = "PreGame";
            }//SETTINGS
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine("pause: setting button");
                game.Status = "game";
            }//QUIT
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine("pause: quit button");
                game.InitExit();
            }
        }


        public void HandlerExit(RectangleShape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "game";
            }//YES
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Window.Close();
            }//NO
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }
        }

        public void HandlerMain(RectangleShape [] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "close";
            }//play
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "preGame";
            }//history
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }//settings
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }//credit
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }//Quit
            else if (buttons[4].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "close";
            }
                
        }

        public string[] HandlerPreGame(RectangleShape[] buttons, string[] status)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "close";
                return status;
            }
            else if ((buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[0] == "active"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "selected")
                    {
                        status[i] = "active";
                    }
                }
                status[0] = "selected";
                return status;
            }
            else if ((buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[1] == "active"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i]== "selected")
                    {
                        status[i] = "active";
                    }
                }
                status[1] = "selected";
                return status;
            }
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[2] == "inactive")
            {
                status[2] = "active";
                return status;
            }
            else if ((buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[2]== "active"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "selected")
                    {
                        status[i] = "active";
                    }
                }
                status[2] = "selected";
                return status;
            }
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[3] == "inactive")
            {
                status[3] = "active";
                return status;
            }
            else if ((buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[3] == "active"))
            {
                for (int i = 0; i < status.Length; i++)
                {
                    if (status[i] == "selected")
                    {
                        status[i] = "active";
                    }
                }
                status[3] = "selected";
                return status;
            }
            else if ((buttons[5].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[4] = "archer";
                return status;
            }
            else if ((buttons[6].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[4] = "drake";
                return status;
            }
            else if ((buttons[7].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[4] = "gobelin";
                return status;
            }
            else if ((buttons[8].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[4] = "paladin";
                return status;
            }
            else if ((buttons[9].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[4] = "balista";
                return status;
            }
            else if ((buttons[10].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[4] = "catapult";
                return status;
            }
            else if ((buttons[4].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                game.Status = "game";
                return status;
            }
            else
            {
                return status;
            }
        }

        internal void HandlerEnd(RectangleShape[] buttons)
        {
            //play
            if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "preGame";
            }//QUIT
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "close";
            }
        }
    }
}