using BirdHouse_Battle.Model;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace BirdHouse_Battle.UI
{
    /// <summary>
    ///  This class will be used to only draw units
    /// </summary>
    public class Drawer
    {
        internal RenderWindow _window;

        readonly Shape _winnerButton;
        readonly Texture _redTeam;
        readonly Texture _blueTeam;
        readonly Texture _yellowTeam;
        readonly Texture _greenTeam;

        public Drawer(RenderWindow Window)
        {
            _redTeam = new Texture("../../../../res/RedTeam.png");
            _blueTeam = new Texture("../../../../res/BlueTeam.png");
            _greenTeam = new Texture("../../../../res/GreenTeam.png");
            _yellowTeam = new Texture("../../../../res/YellowTeam.png");

            _window = Window;
            
            _winnerButton = new RectangleShape()
            {
                Size = new Vector2f(250, 100),
                Position = new Vector2f(128, 374)
            };         
            _window.Draw(CreateShape(512, 512, "../../../../res/terrain1.jpeg", 0, 0));
        }

        /// <summary>
        /// Displays the game background
        /// </summary>
        public void BackGroundGame()
        {
            _window.Draw( CreateShape(512, 512, "../../../../res/terrain1.jpeg", 0, 0));

            //Displays the legend
            _window.Draw(CreateShape(512, 200, "../../../../res/LEGEND.png", 0, 512));
        }

        #region CreateButton & overloads

        internal Shape CreateShape(Vector2f Size, string Link, Vector2f position)
        {
            RectangleShape button = new RectangleShape()
            {
                Size = Size,
                Texture =  new Texture(Link),
                Position = position,
            };
            return button;
        }

        internal Shape CreateShape(int X, int Y, string Link, int PosX, int PosY)
        {
            RectangleShape button = new RectangleShape()
            {
                Size = new Vector2f(X, Y),
                Texture = new Texture(Link),
                Position = new Vector2f(PosX, PosY)
            };
            return button;
        }

        internal Shape CreateShape(Vector2f Size, string Link, int PosX, int PosY)
        {
            RectangleShape shape = new RectangleShape()
            {
                Size = Size,
                Position = new Vector2f(PosX, PosY),
                Texture = new Texture(Link)
            };
            return shape;
        }
   
        internal static CircleShape CreateShape(Unit unit, int size, uint point)
        {
            CircleShape shape = new CircleShape(size)
            {
                Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250),
            };
            shape.SetPointCount(point);
            Coloring(shape, unit);

            return shape;
        }

        internal static CircleShape CreateShape(Projectile projectile, int size)
        {
            CircleShape shape = new CircleShape(size)
            {
                Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250),
            };
            return shape;
        }

        #endregion

        public static Shape Coloring(Shape shape, Unit unit)
        {
            switch (unit.Team.TeamNumber)
            {
                case 0:
                    shape.FillColor = new Color(0, 71, 96);
                    break;
                case 1:
                    shape.FillColor = new Color(138, 19, 0);
                    break;
                case 2:
                    shape.FillColor = new Color(32, 121, 0);
                    break;
                case 3:
                    shape.FillColor = new Color(213, 205, 0);
                    break;
            }
            return shape;
        }

        static Shape DisplayPaladin(Unit unit)
        {
            RectangleShape shape = new RectangleShape(new Vector2f(10, 10))
            {
                Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250)
            };

            Coloring(shape, unit);

            return shape;
        }

        public Shape DisplayField(Tile tile)
        {
            CircleShape arrField = new CircleShape(5);
            arrField.SetPointCount(6);
            arrField.Position = new Vector2f((float)tile.X + 250, (float)tile.Y + 250);

            if (tile.Height != 0)
            {
                switch (tile.Height)
                {
                    case 1: arrField.FillColor = new Color(10, 30, 27); break;
                    case 2: arrField.FillColor = new Color(30, 40, 28); break;
                    case 3: arrField.FillColor = new Color(50, 50, 29); break;
                    case 4: arrField.FillColor = new Color(70, 60, 30); break;
                    case 5: arrField.FillColor = new Color(90, 65, 31); break;
                    case 6: arrField.FillColor = new Color(110, 70, 32); break;
                    case 7: arrField.FillColor = new Color(115, 75, 33); break;
                    case 8: arrField.FillColor = new Color(120, 80, 34); break;
                    case 9: arrField.FillColor = new Color(125, 84, 35); break;
                    case 10: arrField.FillColor = new Color(130, 87, 36); break;
                    case 11: arrField.FillColor = new Color(135, 90, 37); break;
                    case 12: arrField.FillColor = new Color(140, 94, 38); break;
                    case 13: arrField.FillColor = new Color(145, 97, 39); break;
                    case 14: arrField.FillColor = new Color(150, 100, 40); break;
                }
            }
            else if (tile.Obstacle != "None")
            {
                switch (tile.Obstacle)
                {
                    case "Rock":
                        arrField.FillColor = new Color(195, 180, 157);
                        break;

                    case "Tree":
                        arrField.FillColor = new Color(0, 67, 5);
                        break;

                    case "River":
                        arrField.FillColor = new Color(30, 38, 129);
                        break;
                }
            }

            return arrField;
        }

        static Shape DisplayUnit(Unit unit )
        {
            Shape shape = null;

            if ( unit is Archer) { shape = shape = CreateShape(unit, 7, 3); }
            else if ( unit is Balista ) { shape = CreateShape(unit, 8, 6); }
            else if ( unit is Catapult ) { shape = CreateShape(unit, 8, 7); }
            else if ( unit is Drake ) { shape = CreateShape(unit, 8, 5); }
            else if ( unit is Goblin ) { shape = CreateShape(unit, 6, 0);}
            else { shape  = DisplayPaladin(unit);}// case paladin

            return shape;
        }

        static Shape DisplayProj(Projectile p)
        {
            Shape shape = null;
            if (p is Arrow) shape = CreateShape(p, 2);
            else if (p is Boulder) shape = CreateShape(p, 6);
            else { shape = CreateShape(p, 4); }

            return shape;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arena"></param>
        public void UnitDisplay(Arena arena)
        {
            Shape shape = null;

            foreach (KeyValuePair<string, Tile> tile in arena.Field.Elements)
            {
                shape = DisplayField(tile.Value);
                _window.Draw(shape);
            }
            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                foreach (KeyValuePair<int, Unit> unit in team.Value.Units)
                {
                    _window.Draw(shape = DisplayUnit(unit.Value));
                }
            }
            foreach (KeyValuePair<int, Projectile> projectile in arena.Projectiles)
            {
                _window.Draw(shape = DisplayProj(projectile.Value));
            }
        }

        #region Menu

        internal Shape[] EndDisplay(int Winner)
        {
            _window.Draw( CreateShape(512, 712, "../../../../res/end.png", 0, 0));
            Vector2f Bsize = new Vector2f(100, 25);
            Shape[] buttons = new RectangleShape[2];

            buttons[0] = CreateShape(Bsize, "../../../../res/button_again.png", 100, 500); // take us to pregame screen
            buttons[1] = CreateShape(Bsize, "../../../../res/button_quit.png", 300, 500); ; // take us to main menu

            switch (Winner)
            {
                case 1://red won
                    _winnerButton.Texture = _blueTeam;
                    break;

                case 2://blue won
                    _winnerButton.Texture = _redTeam;
                    break;

                case 3:
                    _winnerButton.Texture = _greenTeam;
                    break;

                case 4:
                    _winnerButton.Texture = _yellowTeam;
                    break;
            }

            _window.Draw(_winnerButton);
            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            return buttons;
        }

        /// <summary>
        /// Display the main menu
        /// </summary>
        /// <returns></returns>
        public Shape[] MenuDisplay()
        {
            Vector2f Bsize = new Vector2f(100, 25);
            Shape[] buttons = new RectangleShape[5];

            Shape MenuBackground = CreateShape(512, 712, "../../../../res/main.png",0 , 0);
            _window.Draw(MenuBackground);
           
            buttons[0] = CreateShape(Bsize, "../../../../res/button_start.png", 200, 100); // take us to pregame sreen
            buttons[1] = CreateShape(Bsize, "../../../../res/button_history.png", 200, 150);// to history mode
            buttons[2] = CreateShape(Bsize, "../../../../res/button_setting.png", 200, 200);// to settings
            buttons[3] = CreateShape(Bsize, "../../../../res/button_credits.png", 200, 250); // to credits
            buttons[4] = CreateShape(Bsize, "../../../../res/button_quit.png", 200, 300); //take us to exit screen

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            return buttons;
        }

        /// <summary>
        /// Asks for cofirmation before quitting
        /// </summary>
        /// <returns></returns>
        public Shape[] ExitDisplay()
        {
            Shape ExitBackground = CreateShape(512, 712, "../../../../res/exit.png", 0 ,0);
            _window.Draw(ExitBackground);

            Shape[] button = new RectangleShape[2];
            Vector2f Bsize = new Vector2f(100, 50);

            button[0] = CreateShape(Bsize, "../../../../res/button_yes.png", 100, 200); // 
            button[1] = CreateShape(Bsize, "../../../../res/button_no.png", 300, 200); // take us to the precedent screen

            foreach (var t in button)
            {
               _window.Draw(t);
            }
            return button;
        }

        /// <summary>
        /// Display the pause menu
        /// </summary>
        public Shape[] PauseDisplay()
        {
            Shape[] button = new RectangleShape[4];

            Vector2f Bsize = new Vector2f(100, 25);
           
            button[0] = CreateShape(Bsize, "../../../../res/button_continue.png", 200, 50);//take us to the game screen
            button[1] = CreateShape(Bsize, "../../../../res/button_restart.png", 200, 100); // take us to pregame screen
            button[2] = CreateShape(Bsize, "../../../../res/button_setting.png", 200, 150); // to settings
            button[3] = CreateShape(Bsize, "../../../../res/button_quit.png", 200, 200); // take us to exist screen

            foreach (var t in button)
            {
                _window.Draw(t);
            }
            return button;
        }

        public Shape[] PreGameDisplay(string[] status, int[,] teamComposition)
        {
            Vector2f Bsize = new Vector2f(75, 25);//button size
            Vector2f Tsize = new Vector2f(118, 190); //team button  size

            RenderStates rs = new RenderStates();
            Font font = new Font("../../../../res/Overlock-Regular.ttf");//font for the text

            Shape[] buttons = new RectangleShape[12];
            Text[] messages = new Text[4];
            
            RectangleShape buttonAddTeam1 = new RectangleShape(Tsize);
            buttonAddTeam1.Position = new Vector2f(5, 317);
            buttonAddTeam1.OutlineThickness = 5;
            buttonAddTeam1.OutlineColor = new Color(0,0,250);
            if (status[0]== "selected")
            {
                buttonAddTeam1.FillColor = new Color(255, 160, 122);
            }
            Text messageTeam1 = new Text("ARCHER : "+teamComposition[0,0].ToString()+"\n DRAKE : "+teamComposition[0,1].ToString()+"\n GOBLIN : "+teamComposition[0,2].ToString()+"\n PALADIN : "+teamComposition[0,3].ToString()+"\n BALISTA : "+teamComposition[0,4].ToString()+"\n CATAPULT : "+teamComposition[0,5].ToString(), font, 15);
            messageTeam1.FillColor = new Color(0, 0, 0);
            messageTeam1.Position = new Vector2f(15, 322);

            RectangleShape buttonAddTeam2 = new RectangleShape(Tsize);
            buttonAddTeam2.Position = new Vector2f(132, 317);
            buttonAddTeam2.OutlineThickness = 5;
            buttonAddTeam2.OutlineColor = new Color(250, 0, 0);
            if (status[1] == "selected")
            {
                buttonAddTeam2.FillColor = new Color(255, 160, 122);
            }
            Text messageTeam2 = new Text("ARCHER : " + teamComposition[1, 0].ToString() + "\n DRAKE : " + teamComposition[1, 1].ToString() + "\n GOBLIN : " + teamComposition[1, 2].ToString() + "\n PALADIN : " + teamComposition[1, 3].ToString() + "\n BALISTA : " + teamComposition[1, 4].ToString() + "\n CATAPULT : " + teamComposition[1, 5].ToString(), font, 15);
            messageTeam2.FillColor = new Color(0, 0, 0);
            messageTeam2.Position = new Vector2f(142, 322);


            RectangleShape buttonAddTeam3 = new RectangleShape(Tsize);
            buttonAddTeam3.Position = new Vector2f(261, 317);
            buttonAddTeam3.OutlineThickness = 5;
            buttonAddTeam3.OutlineColor = new Color(0, 250, 0);
            Text messageTeam3 = new Text("", font, 25);
            if (status[2] == "inactive")
            {
                buttonAddTeam3.FillColor = new Color(128, 128, 128);
            }
            else
            {
                if (status[2] == "selected")
                {
                    buttonAddTeam3.FillColor = new Color(255, 160, 122);
                }
                messageTeam3 = new Text("ARCHER : " + teamComposition[2, 0].ToString() + "\n DRAKE : " + teamComposition[2, 1].ToString() + "\n GOBLIN : " + teamComposition[2, 2].ToString() + "\n PALADIN : " + teamComposition[2, 3].ToString() + "\n BALISTA : " + teamComposition[2, 4].ToString() + "\n CAPAPULT : " + teamComposition[2, 5].ToString(), font, 15);
                messageTeam3.FillColor = new Color(0, 0, 0);
                messageTeam3.Position = new Vector2f(271, 322);
            }

            RectangleShape buttonAddTeam4 = new RectangleShape(Tsize)
            {
                Position = new Vector2f(389, 317),
                OutlineThickness = 5,
                OutlineColor = new Color(250, 250, 0)
            };
            Text messageTeam4 = new Text("", font, 25);
            if (status[3] == "inactive")
            {
                buttonAddTeam4.FillColor = new Color(128, 128, 128);
            }
            else
            {
                if (status[3] == "selected")
                {
                    buttonAddTeam4.FillColor = new Color(255, 160, 122);
                }
                messageTeam4 = new Text("ARCHER : " + teamComposition[3, 0].ToString() + "\n DRAKE : " + teamComposition[3, 1].ToString() + "\n GOBLIN : " + teamComposition[3, 2].ToString() + "\n PALADIN : " + teamComposition[3, 3].ToString() + "\n BALISTA : " + teamComposition[3, 4].ToString() + "\n CATAPULT : " + teamComposition[3, 5].ToString(), font, 15);
                messageTeam4.FillColor = new Color(0, 0, 0);
                messageTeam4.Position = new Vector2f(399, 322);
            }
            
            buttons[0] = buttonAddTeam1;
            buttons[1] = buttonAddTeam2;
            buttons[2] = buttonAddTeam3;
            buttons[3] = buttonAddTeam4;
            buttons[4] = CreateShape(Bsize, "../../../../res/button_play.png", 380, 125);
            buttons[5] = CreateShape(Bsize, "../../../../res/button_archer.png", 10, 30);
            buttons[6] = CreateShape(Bsize, "../../../../res/button_drake.png", 10, 95);
            buttons[7] = CreateShape(Bsize, "../../../../res/button_goblin.png", 10, 160);
            buttons[8] = CreateShape(Bsize, "../../../../res/button_paladin.png", 10, 225);
            buttons[9] = CreateShape(Bsize, "../../../../res/button_balista.png", 105, 30);
            buttons[10] = CreateShape(Bsize, "../../../../res/button_catapult.png", 105, 95);
            buttons[11] = CreateShape(Bsize, "../../../../res/button_random.png", 380, 225);

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            
            messages[0] = messageTeam1;
            messages[1] = messageTeam2;
            messages[2] = messageTeam3;
            messages[3] = messageTeam4;
          
            foreach (var t in messages)
            {
                t.Draw(_window, rs);
                _window.Draw(t);
            }

            return buttons;
        }
        #endregion
    }
}