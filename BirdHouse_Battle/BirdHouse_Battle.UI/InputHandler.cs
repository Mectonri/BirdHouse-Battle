using System;
using BirdHouse_Battle.Model;
using SFML.Graphics;
using SFML.Window;

namespace BirdHouse_Battle.UI
{
    class InputHandler
    {
        Game game;

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
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                game.Switch("RETURN");
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.P))
            {
                game.Switch("P");
            }
            if(Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                game.Switch("Right");
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                game.Switch("Left");
            }
        }

        /// <summary>
        /// Handler to Pause
        /// </summary>
        /// <param name="buttons"></param>
        public void HandlerPause(Shape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Switch("ESC");
            }
            else if ((Keyboard.IsKeyPressed(Keyboard.Key.P)))
            {
                Console.WriteLine("pause : p");
                game.Switch("P");
            }//CONTINUE button
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Switch("P");
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
                game.Status = "return";
            }
        }

        /// <summary>
        /// Handler to Return
        /// </summary>
        /// <param name="buttons"></param>
        public void HandlerReturn(Shape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "main";
            }//YES
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }//NO
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "return";
            }
        }

        /// <summary>
        /// Handler to MainMenu
        /// </summary>
        /// <param name="buttons"></param>
        public void HandlerMain(Shape [] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "quit";
            }//play
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "preGame";
            }//Quick game
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "quickGame";
            }
            //history
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "history";
            }//settings
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                //game.Status = "preGame";
            }//credit
            else if (buttons[4].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "credit";
            }//Quit
            else if (buttons[5].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "quit";
            }
        }

        public void HandlerElderGame(Shape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "quit";
            }//play
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "preGame";
            }//Quick game
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "quickGame";
            }
            //history
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "history";
            }//settings
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                //game.Status = "game";
            }//credit
            else if (buttons[4].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "credit";
            }//Quit
            else if (buttons[5].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "quit";
            }
        }

        /// <summary>
        /// Handler to PreGame
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string[] HandlerPreGame(Shape[] buttons, string[] status)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "close";
                return status;
            }
            else if ((buttons[13].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[5] == "add"))
            {
                status[5] = "remove";
                return status;
            }
            else if ((buttons[14].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[6] = "1";
                return status;
            }
            else if ((buttons[15].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[6] = "10";
                return status;
            }
            else if ((buttons[16].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) ))
            {
                status[6] = "100";
                return status;
            }
            else if ((buttons[13].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[5] == "remove"))
            {
                status[5] = "add";
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
                    if (status[i] == "selected")
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
            else if ((buttons[11].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                if (status[3] == "inactive")
                {
                    status[2] = "inactive";
                }
                else
                {
                    status[2] = "inactiveTemp";
                }
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
            else if ((buttons[12].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[3] = "inactive";
                return status;
            }
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[3] == "inactive" && status[2]!="inactive")
            {
                status[3] = "active";
                return status;
            }
            else if ((buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left) && status[3] == "active" && status[2]!="inactive"))
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
            }//Fill button
            else if ((buttons[17].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left)))
            {
                status[7] = "no";                
                return status ;
            }

            else { return status; }
        }

        /// <summary>
        /// History Page Handler
        /// </summary>
        /// <param name="buttons"></param>
        internal void HandlerLVSelection(Shape[] buttons)
        {
            //ESC Key 
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "main";
            }//RETURN 
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }// Level
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {

                game.LevelLoad(0);
                
                
            }//PLAY
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
            }
        }

        internal int[,] HandlerHistoryPreGame(int[,] teamComp, Team team, Shape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "close";
                return teamComp;
            } //
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                return teamComp;
            } // 
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                return teamComp;
            } // PLAY
            else if (buttons[2].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "game";
                return teamComp;
            } // Add Archer
            else if (buttons[3].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                team.AddArcher(1);
                teamComp[0, 6] = (int)team.GoldCalculation(teamComp[0, 6]);

                teamComp[0, 0]++;
                return teamComp;
            } // Add Drake
            else if (buttons[4].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                team.AddDrake(1);
                teamComp[0, 3]++;
                return teamComp;
            } // Add Goblin
            else if (buttons[5].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                team.AddGoblin(1);
                teamComp[0, 4]++;
                return teamComp;
            } // Add Paladin
            else if (buttons[6].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                team.AddPaladin(1);
                teamComp[0, 6] = (int)team.GoldCalculation(teamComp[0, 6]);

                teamComp[0, 5]++;
                return teamComp;
            }// Add Balista
            else if (buttons[7].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                team.AddBalista(1);
                teamComp[0, 1]++;
                return teamComp;
            } //Add Catapult
            else if (buttons[8].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                team.AddCatapult(1);
                teamComp[0, 2]++;
                return teamComp;

            }
            else { return teamComp;  }
        }

        /// <summary>
        /// Handler to Quit the game
        /// </summary>
        /// <param name="buttons"></param>
        internal void HandlerQuit(Shape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.Status = "main";
                
            }//OUI
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Window.Close();
               
            }// NO
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
               
            }

        }

        /// <summary>
        /// Handler to Credit
        /// </summary>
        /// <param name="buttons"></param>
        internal void HandlerCredit(Shape[] buttons)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            { 
                game.Status = "main";
            }//return
            else if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }
        }

        /// <summary>
        /// Handler to End
        /// </summary>
        /// <param name="buttons"></param>
        internal void HandlerResulsult(Shape[] buttons)
        {
            //play
            if (buttons[0].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "preGame";
            }//QUIT 
            else if (buttons[1].GetGlobalBounds().Contains(Mouse.GetPosition(game.Window).X, Mouse.GetPosition(game.Window).Y) == true && Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                game.Status = "main";
            }
        }
    }
}