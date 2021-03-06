﻿using BirdHouse_Battle.Model;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using System;



namespace BirdHouse_Battle.UI
{


    /// <summary>
    ///  This class will be used to only draw 
    /// </summary>
    public class Drawer
    {
        internal RenderWindow _window;
        readonly Shape _winnerButton;
        readonly Texture _redTeam;
        readonly Texture _blueTeam;
        readonly Texture _yellowTeam;
        readonly Texture _greenTeam;
        readonly RenderStates _rs;
        internal Font _font;

        public Drawer(RenderWindow Window)
        {
            _redTeam = new Texture("../../../../res/RedTeam.png");
            _blueTeam = new Texture("../../../../res/BlueTeam.png");
            _greenTeam = new Texture("../../../../res/GreenTeam.png");
            _yellowTeam = new Texture("../../../../res/YellowTeam.png");

            _rs = new RenderStates();
            _font = new Font("../../../../res/Overlock-Regular.ttf"); //font for the text

            _window = Window;

            Window.Draw(CreateShape(512, 512, "../../../../res/terrain1.jpeg", 0, 0));
        }

        /// <summary>
        /// Displays the game background
        /// </summary>
        public void BackGroundGame(string mode, string path)
        {
            _window.Draw(CreateShape(512, 512, "../../../../res/terrain1.jpeg", 0, 0));
            if (mode != "replay")
            {
                _window.Draw(CreateShape(512, 512, "../../../../res/DiamondBackground.png", 0, 0));
            }
            else
            {
                _window.Draw(CreateShape(512, 512, "../../../../saveStates/" + path + "/BackGround.png", 0, 0));
            }

            _window.Draw(CreateShape(512, 200, "../../../../res/LEGEND.png", 0, 512));
        }

        public void Healthbar(double[] StartingHealth, Arena arena)
        {
            double[] healths = new double[4];
            int i = 0;
            foreach (KeyValuePair<string, Team> kv in arena.Teams)
            {
                healths[i] = kv.Value.Health / StartingHealth[1] * 100;
            }

            Text health1 = WritteText($"healths[1]", 15, 128, 200);
        }

        #region CreataShape & overloads

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
                Position = new Vector2f((float) unit.Location.X + 250, (float) unit.Location.Y + 250),
            };
            shape.SetPointCount(point);
            Coloring(shape, unit);

            return shape;
        }

        internal static CircleShape CreateShape(Projectile projectile, int size)
        {
            CircleShape shape = new CircleShape(size)
            {
                Position = new Vector2f((float) projectile.Position.X + 250, (float) projectile.Position.Y + 250),
            };
            return shape;
        }

        #endregion

        internal Text WritteText(string message, uint size, int PosX, int PosY)
        {
            Text text = new Text()
            {
                DisplayedString = message,
                Font = _font,
                CharacterSize = size,
                Position = new Vector2f(PosX, PosY),
                FillColor = Color.Black,
            };
            return text;
        }

        #region Displays

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
                Position = new Vector2f((float) unit.Location.X + 250, (float) unit.Location.Y + 250)
            };

            Coloring(shape, unit);

            return shape;
        }

        public Shape DisplayField(Tile tile)
        {
            CircleShape arrField = new CircleShape(3);
            arrField.Position = new Vector2f((float) tile.X + 250, (float) tile.Y + 250);

            if (tile.Obstacle != "None")
            {
                arrField.SetPointCount(6);
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

        static Shape DisplayUnit(Unit unit)
        {
            Shape shape = null;

            if (unit is Archer)
            {
                shape = CreateShape(unit, 7, 3);
            }
            else if (unit is Balista)
            {
                shape = CreateShape(unit, 8, 6);
            }
            else if (unit is Catapult)
            {
                shape = CreateShape(unit, 8, 7);
            }
            else if (unit is Drake)
            {
                shape = CreateShape(unit, 8, 5);
            }
            else if (unit is Goblin)
            {
                shape = CreateShape(unit, 6, 30);
            }
            else
            {
                shape = DisplayPaladin(unit);
            } // case paladin

            return shape;
        }

        static Shape DisplayProj(Projectile projectile)
        {
            Shape shape = null;
            if (projectile is Arrow) shape = CreateShape(projectile, 2);
            else if (projectile is Boulder) shape = CreateShape(projectile, 6);
            else
            {
                shape = CreateShape(projectile, 4);
            }

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

        internal Shape[] ResultDisplay(int Winner)
        {

            Shape[] buttons = new RectangleShape[2];

            buttons[0] =
                CreateShape(100, 25, "../../../../res/button_again.png", 100, 500); // take us to pregame screen
            buttons[1] = CreateShape(100, 25, "../../../../res/button_quit.png", 300, 500);
            ; // take us to main menu

            switch (Winner)
            {
                case 0: //blue won
                    _window.Draw(CreateShape(512, 712, "../../../../res/winnerblue.png", 0, 0));
                    break;

                case 1: //red won
                    _window.Draw(CreateShape(512, 712, "../../../../res/winnerred.png", 0, 0));
                    break;

                case 2: // Green won
                    _window.Draw(CreateShape(512, 712, "../../../../res/winnergreen.png", 0, 0));
                    break;

                case 3: // yellow won
                    _window.Draw(CreateShape(512, 712, "../../../../res/winneryellow.png", 0, 0));
                    break;
            }

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
            Shape[] buttons = new RectangleShape[7];

            Shape MenuBackground = CreateShape(512, 712, "../../../../res/main.png", 0, 0);
            _window.Draw(MenuBackground);

            _window.Draw(buttons[0] =
                CreateShape(Bsize, "../../../../res/button_start.png", 200, 150)); // take us to pregame sreen
            _window.Draw(buttons[1] =
                CreateShape(Bsize, "../../../../res/button_quick-game.png", 200, 200)); //quick game
            _window.Draw(buttons[2] = CreateShape(Bsize, "../../../../res/button_historic.png", 200, 250)); // Historic
            _window.Draw(buttons[3] =
                CreateShape(Bsize, "../../../../res/button_history.png", 200, 300)); // to history mode
            buttons[4] = CreateShape(Bsize, "../../../../res/button_setting.png", 200, 350); // to settings
            _window.Draw(buttons[5] = CreateShape(Bsize, "../../../../res/button_credits.png", 200, 350)); // to credits
            _window.Draw(buttons[6] =
                CreateShape(Bsize, "../../../../res/button_quit.png", 200, 500)); //take us to exit screen

            //foreach (var t in buttons)
            //{
            //    _window.Draw(t);
            //}
            return buttons;
        }

        public Shape[] ElderGameDisplay(string[] dossiers, string[] winner, string[] tours)
        {

            uint size = 20;
            Vector2f Bsize = new Vector2f(75, 25);
            Shape[] buttons = new RectangleShape[11];
            Text[] saves = new Text[10];
            Shape MenuBackground = CreateShape(512, 712, "../../../../res/main.png", 0, 0);
            _window.Draw(MenuBackground);
            int PosX1 = 392;
            int PosX2 = 75;
            int PosY = 150;

            for (int i = 0; i < 10; i++)
            {

                buttons[i] = CreateShape(Bsize, "../../../../res/button_replay.png", PosX1, PosY + i * 50);
                saves[i] = WritteText(dossiers[i] + "\t" + winner[i] + "\t" + tours[i], size, PosX2, PosY + i * 50);
                saves[i].FillColor = Color.White;
                saves[i].Draw(_window, _rs);

                if (dossiers[i] != null)
                {
                    _window.Draw(buttons[i]);
                    _window.Draw(saves[i]);
                }

            }

            buttons[10] = CreateShape(Bsize, "../../../../res/button_main-menu.png", 206, 630);
            _window.Draw(buttons[10]);

            return buttons;
        }

        internal Shape[] CreditDisplay()
        {
            Shape CreditBacground = CreateShape(512, 712, "../../../../res/credit.png", 0, 0);
            _window.Draw(CreditBacground);

            Shape[] button = new RectangleShape[1];

            button[0] = CreateShape(100, 50, "../../../../res/return.png", 400, 650);

            foreach (var t in button)
            {
                _window.Draw(t);
            }

            return button;
        }

        /// <summary>
        /// Ask For confirmation before returning
        /// </summary>
        /// <returns></returns>
        public Shape[] ReturnDisplay()
        {
            Shape ExitBG = CreateShape(512, 712, "../../../../res/return1.png", 0, 0);
            _window.Draw(ExitBG);

            Vector2f Tsize = new Vector2f(118, 390); //team button  size

            Shape[] button = new RectangleShape[2];

            button[0] = CreateShape(75, 25, "../../../../res/button_yes.png", 100, 200); // 
            button[1] = CreateShape(75, 25, "../../../../res/button_no.png", 300,
                200); // take us to the precedent screen
            RectangleShape[] buttons = new RectangleShape[17];

            Shape buttonPlay = CreateShape(75, 25, "../../../../res/button_play.png", 380, 125);

            foreach (var t in button)
            {
                _window.Draw(t);
            }

            return button;
        }

        /// <summary>
        /// Display 
        /// </summary>
        /// <returns></returns>
        internal Shape[] QuitDisplay()
        {
            Shape[] button = new RectangleShape[2];

            Shape ExitBackground = CreateShape(512, 712, "../../../../res/exit.png", 0, 0);
            _window.Draw(ExitBackground);

            button[0] = CreateShape(75, 25, "../../../../res/button_yes.png", 100, 200); // 
            button[1] = CreateShape(75, 25, "../../../../res/button_no.png", 300, 200);

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

            _window.Draw(button[0] =
                CreateShape(Bsize, "../../../../res/button_continue.png", 200, 50)); //take us to the game screen
            button[1] = CreateShape(Bsize, "../../../../res/button_restart.png", 200, 100); // take us to pregame screen
            button[2] = CreateShape(Bsize, "../../../../res/button_setting.png", 200, 150); // to settings
            button[3] = CreateShape(Bsize, "../../../../res/button_quit.png", 200, 200); // take us to exist screen

            //foreach (var t in button)
            //{
            //    _window.Draw(t);
            //}
            return button;
        }

        /// <summary>
        /// Display the PreGame Menu
        /// </summary>
        /// <param name="status"></param>
        /// <param name="teamComposition"></param>
        /// <returns></returns>
        public Shape[] PreGameDisplay(string[] status, int[,] teamComposition)
        {
            Vector2f Bsize = new Vector2f(75, 25); //button size
            Vector2f Tsize = new Vector2f(118, 390); //team button  size

            Shape[] buttons = new RectangleShape[18];
            Text[] messages = new Text[10];

            RectangleShape buttonAddRemove = new RectangleShape(Bsize);
            buttonAddRemove.Position = new Vector2f(275, 30);
            buttonAddRemove.FillColor = new Color(211, 211, 211);
            Text textAddRemove = new Text("", _font, 25);

            if (status[5] == "add")
            {
                textAddRemove = new Text("ADD", _font, 20);
            }
            else
            {
                textAddRemove = new Text("REM", _font, 20);
            }

            textAddRemove.Position = new Vector2f(295, 27);
            textAddRemove.FillColor = new Color(0, 0, 0);

            RectangleShape button1 = new RectangleShape(new Vector2f(30, 25));
            Text text1 = new Text("1", _font, 15);
            button1.Position = new Vector2f(360, 30);
            text1.Position = new Vector2f(370, 30);
            text1.FillColor = new Color(0, 0, 0);
            if (status[6] == "1")
            {
                button1.FillColor = new Color(100, 100, 100);
            }
            else
            {
                button1.FillColor = new Color(200, 200, 200);
            }

            RectangleShape button10 = new RectangleShape(new Vector2f(30, 25));
            Text text10 = new Text("10", _font, 15);
            button10.Position = new Vector2f(390, 30);
            text10.Position = new Vector2f(390, 30);
            text10.FillColor = new Color(0, 0, 0);
            if (status[6] == "10")
            {
                button10.FillColor = new Color(100, 100, 100);
            }
            else
            {
                button10.FillColor = new Color(200, 200, 200);
            }

            RectangleShape button100 = new RectangleShape(new Vector2f(30, 25));
            Text text100 = new Text("100", _font, 15);
            button100.Position = new Vector2f(420, 30);
            text100.Position = new Vector2f(420, 30);
            text100.FillColor = new Color(0, 0, 0);
            if (status[6] == "100")
            {
                button100.FillColor = new Color(100, 100, 100);
            }
            else
            {
                button100.FillColor = new Color(200, 200, 200);
            }

            RectangleShape buttonAddTeam1 = new RectangleShape(Tsize)
            {
                Position = new Vector2f(5, 317),
                OutlineThickness = 5,
                OutlineColor = new Color(250, 0, 0)
            };
            if (status[0] == "selected")
            {
                buttonAddTeam1.FillColor = new Color(255, 160, 122);
            }

            Text messageTeam1 = new Text(
                "ARCHER : " + teamComposition[0, 0].ToString() + "\n DRAKE : " + teamComposition[0, 1].ToString() +
                "\n GOBLIN : " + teamComposition[0, 2].ToString() + "\n PALADIN : " + teamComposition[0, 3].ToString() +
                "\n BALISTA : " + teamComposition[0, 4].ToString() + "\n CATAPULT : " +
                teamComposition[0, 5].ToString(), _font, 15);
            messageTeam1.FillColor = new Color(0, 0, 0);
            messageTeam1.Position = new Vector2f(15, 322);

            RectangleShape buttonAddTeam2 = new RectangleShape(Tsize);
            buttonAddTeam2.Position = new Vector2f(132, 317);
            buttonAddTeam2.OutlineThickness = 5;
            buttonAddTeam2.OutlineColor = new Color(0, 0, 250);

            if (status[1] == "selected")
            {
                buttonAddTeam2.FillColor = new Color(255, 160, 122);
            }

            Text messageTeam2 = new Text(
                "ARCHER : " + teamComposition[1, 0].ToString() + "\n DRAKE : " + teamComposition[1, 1].ToString() +
                "\n GOBLIN : " + teamComposition[1, 2].ToString() + "\n PALADIN : " + teamComposition[1, 3].ToString() +
                "\n BALISTA : " + teamComposition[1, 4].ToString() + "\n CATAPULT : " +
                teamComposition[1, 5].ToString(), _font, 15);
            messageTeam2.FillColor = new Color(0, 0, 0);
            messageTeam2.Position = new Vector2f(142, 322);

            RectangleShape buttonAddTeam3 = new RectangleShape(Tsize);
            RectangleShape buttonSupprTeam3 = new RectangleShape(new Vector2f(20, 20));
            buttonAddTeam3.Position = new Vector2f(261, 317);
            buttonSupprTeam3.Position = new Vector2f(360, 317);
            buttonAddTeam3.OutlineThickness = 5;
            buttonAddTeam3.OutlineColor = new Color(0, 250, 0);
            Text messageTeam3 = new Text("", _font, 25);
            Text messageSuppr3 = new Text(" ", _font, 25);
            if (status[2] == "inactive")
            {
                buttonAddTeam3.FillColor = new Color(128, 128, 128);
                buttonSupprTeam3.FillColor = new Color(Color.Transparent);
            }
            else
            {
                buttonSupprTeam3.FillColor = new Color(255, 0, 0);
                messageSuppr3 = new Text("X", _font, 25);
                messageSuppr3.FillColor = new Color(0, 0, 0);
                messageSuppr3.Position = new Vector2f(362, 310);
                if (status[2] == "selected")
                {
                    buttonAddTeam3.FillColor = new Color(255, 160, 122);

                }

                messageTeam3 = new Text(
                    "ARCHER : " + teamComposition[2, 0].ToString() + "\n DRAKE : " + teamComposition[2, 1].ToString() +
                    "\n GOBLIN : " + teamComposition[2, 2].ToString() + "\n PALADIN : " +
                    teamComposition[2, 3].ToString() + "\n BALISTA : " + teamComposition[2, 4].ToString() +
                    "\n CAPAPULT : " + teamComposition[2, 5].ToString(), _font, 15);
                messageTeam3.FillColor = new Color(0, 0, 0);
                messageTeam3.Position = new Vector2f(271, 322);
            }

            RectangleShape buttonAddTeam4 = new RectangleShape(Tsize)
            {
                Position = new Vector2f(389, 317),
                OutlineThickness = 5,
                OutlineColor = new Color(250, 250, 0)
            };

            Text messageTeam4 = new Text("", _font, 25);
            Text messageSuppr4 = new Text("", _font, 25);
            RectangleShape buttonSupprTeam4 = new RectangleShape(new Vector2f(20, 20));
            buttonSupprTeam4.Position = new Vector2f(488, 317);

            if (status[3] == "inactive")
            {
                buttonAddTeam4.FillColor = new Color(128, 128, 128);
                buttonSupprTeam4.FillColor = new Color(Color.Transparent);
            }
            else
            {

                buttonSupprTeam4.FillColor = new Color(255, 0, 0);
                messageSuppr4 = new Text("X", _font, 25);
                messageSuppr4.FillColor = new Color(0, 0, 0);
                messageSuppr4.Position = new Vector2f(490, 310);
                if (status[3] == "selected")
                {
                    buttonAddTeam4.FillColor = new Color(255, 160, 122);
                }

                messageTeam4 = new Text(
                    "ARCHER : " + teamComposition[3, 0].ToString() + "\n DRAKE : " + teamComposition[3, 1].ToString() +
                    "\n GOBLIN : " + teamComposition[3, 2].ToString() + "\n PALADIN : " +
                    teamComposition[3, 3].ToString() + "\n BALISTA : " + teamComposition[3, 4].ToString() +
                    "\n CATAPULT : " + teamComposition[3, 5].ToString(), _font, 15);
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
            buttons[11] = buttonSupprTeam3;
            buttons[12] = buttonSupprTeam4;
            buttons[13] = buttonAddRemove;
            buttons[14] = button1;
            buttons[15] = button10;
            buttons[16] = button100;
            buttons[17] = CreateShape(Bsize, "../../../../res/button_fill.png", 380, 225);

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }

            messages[0] = messageTeam1;
            messages[1] = messageTeam2;
            messages[2] = messageTeam3;
            messages[3] = messageTeam4;
            messages[4] = messageSuppr3;
            messages[5] = messageSuppr4;
            messages[6] = text1;
            messages[7] = text10;
            messages[8] = text100;
            messages[9] = textAddRemove;

            foreach (var t in messages)
            {
                t.Draw(_window, _rs);
                _window.Draw(t);
            }

            return buttons;
        }

        internal Shape[] HistoryResultDisplay(int winner)
        {
            if (winner == 1)
            {
                _window.Draw(CreateShape(512, 712, "../../../../res/victory.jpg", 0, 0)); //BG
            }
            else
            {
                _window.Draw(CreateShape(512, 712, "../../../../res/defeat.jpg", 0, 0));
            }

            Shape[] buttons = new RectangleShape[2];

            buttons[0] =
                CreateShape(100, 25, "../../../../res/button_levels.png", 100, 500); // take us to LvSelection Menu
            buttons[1] = CreateShape(100, 25, "../../../../res/button_main-menu.png", 300, 500); // take us to Main Menu

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }

            return buttons;
        }


        public Shape[] PlacementDisplay(Arena arena)
        {
            Vector2f Bsize = new Vector2f(75, 25); //button size

            Shape[] buttons = new Shape[9];
            RenderStates rs = new RenderStates();
            RectangleShape team1 = new RectangleShape(Bsize);
            RectangleShape team2 = new RectangleShape(Bsize);
            RectangleShape team3 = new RectangleShape(Bsize);
            RectangleShape team4 = new RectangleShape(Bsize);
            RectangleShape font = new RectangleShape(new Vector2f(560, 200));
            RectangleShape over1 = new RectangleShape(new Vector2f(512, 128));
            RectangleShape over2 = new RectangleShape(new Vector2f(512, 128));
            if (arena.FindTeam("green") == true)
            {
                over1 = new RectangleShape(new Vector2f(128, 128));
                over2 = new RectangleShape(new Vector2f(128, 128));
            }

            RectangleShape over3 = new RectangleShape(new Vector2f(128, 128));
            RectangleShape over4 = new RectangleShape(new Vector2f(128, 128));

            team1.Position = new Vector2f(0, 0);
            team2.Position = new Vector2f(0, 0);
            font.Position = new Vector2f(0, 510);

            if (arena.FindTeam("green") == true)
            {
                team3.Position = new Vector2f(0, 0);
            }

            if (arena.FindTeam("yellow") == true)
            {
                team4.Position = new Vector2f(0, 0);
            }

            buttons[0] = team1;
            buttons[1] = team2;
            buttons[2] = team3;
            buttons[3] = team4;
            buttons[4] = font;
            buttons[5] = over1;
            buttons[6] = over2;
            buttons[7] = over3;
            buttons[8] = over4;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].FillColor = new Color(250, 250, 250);
                //buttons[i].Position = new Vector2f(0,300);
            }

            foreach (var t in buttons)
            {
                t.Draw(_window, rs);
                _window.Draw(t);
            }

            return buttons;
        }

        #region History

        internal Shape[] HistoryLvSeletionDisplay()
        {
            Shape HistoryLvSelectionBG =
                CreateShape(512, 712, "../../../../res/history.png", 0, 0); // History mode Background
            _window.Draw(HistoryLvSelectionBG);

            Shape[] buttons = new RectangleShape[6];

            buttons[0] = CreateShape(75, 25, "../../../../res/button_main-menu.png", 50, 612); // return button
            buttons[1] = CreateShape(75, 25, "../../../../res/button_level1.png", 218, 200); // Level button
            buttons[2] = CreateShape(75, 25, "../../../../res/button_level2.png", 218, 250); // Level button
            buttons[3] = CreateShape(75, 25, "../../../../res/button_level3.png", 218, 300); // Level button
            buttons[4] = CreateShape(75, 25, "../../../../res/button_level4.png", 218, 350); // Level button
            buttons[5] = CreateShape(75, 25, "../../../../res/button_level5.png", 218, 400); //level Button

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }

            return buttons;
        }

        internal Shape[] HistoryPreGameDisplay( /*string[] status,*/ int[,] teamComposition)
        {
            Vector2f Bsize = new Vector2f(75, 25); //button size
            Vector2f Tsize = new Vector2f(246, 405); //team button  size

            Shape[] buttons = new RectangleShape[9];
            Text[] messages = new Text[9];

            RectangleShape teamButton = new RectangleShape(Tsize)
            {
                Position = new Vector2f(5, 300),
                OutlineThickness = 5,
                OutlineColor = new Color(0, 0, 250),
                FillColor = new Color(255, 160, 122)
            };

            Text messageTeam1 = new Text(
                "ARCHER : " + teamComposition[0, 0].ToString() + "\n DRAKE : " + teamComposition[0, 3].ToString() +
                "\n GOBLIN : " + teamComposition[0, 4].ToString() + "\n PALADIN : " + teamComposition[0, 5].ToString() +
                "\n BALISTA : " + teamComposition[0, 1].ToString() + "\n CATAPULT : " +
                teamComposition[0, 2].ToString(), _font, 15)
            {
                FillColor = new Color(0, 0, 0),
                Position = new Vector2f(100, 322)
            };

            RectangleShape team1Button = new RectangleShape(Tsize)
            {
                Position = new Vector2f(261, 300),
                OutlineThickness = 5,
                OutlineColor = new Color(250, 0, 0)
            };

            Text messageTeam2 = new Text(
                "ARCHER : " + teamComposition[1, 0].ToString() + "\n DRAKE : " + teamComposition[1, 3].ToString() +
                "\n GOBLIN : " + teamComposition[1, 4].ToString() + "\n PALADIN : " + teamComposition[1, 5].ToString() +
                "\n BALISTA : " + teamComposition[1, 1].ToString() + "\n CATAPULT : " +
                teamComposition[1, 2].ToString(), _font, 15)
            {
                FillColor = new Color(0, 0, 0),
                Position = new Vector2f(350, 322)
            };
            int Gold = teamComposition[0, 6];
            Text messageGold = new Text()
            {
                DisplayedString = "Remaining Gold :" + Gold.ToString(),
                Font = _font,
                CharacterSize = 20,
                Position = new Vector2f(200, 225),
                FillColor = Color.Black,
                Style = Text.Styles.Bold,
            };
            // Archer price
            Text price1 = new Text()
            {
                DisplayedString = "Price: 10",
                Font = _font,
                CharacterSize = 18,
                Position = new Vector2f(10, 55),
                FillColor = Color.Black,
            };
            // Balista price
            Text price2 = new Text()
            {
                DisplayedString = "Price: 30",
                Font = _font,
                CharacterSize = 18,
                Position = new Vector2f(105, 55),
                FillColor = Color.Black,
            };
            // Catapult price
            Text price3 = new Text()
            {
                DisplayedString = "Price: 40",
                Font = _font,
                CharacterSize = 18,
                Position = new Vector2f(200, 55),
                FillColor = Color.Black,
            };
            // Drake price
            Text price4 = new Text()
            {
                DisplayedString = "Price: 15",
                Font = _font,
                CharacterSize = 18,
                Position = new Vector2f(10, 120),
                FillColor = Color.Black,
            };
            //Goblin price
            Text price5 = new Text()
            {
                DisplayedString = "Price: 3",
                Font = _font,
                CharacterSize = 18,
                Position = new Vector2f(105, 120),
                FillColor = Color.Black,
            };
            // Paladin price
            Text price6 = new Text()
            {
                DisplayedString = "Price: 12",
                Font = _font,
                CharacterSize = 18,
                Position = new Vector2f(200, 120),
                FillColor = Color.Black,
            };

            buttons[0] = teamButton;
            buttons[1] = team1Button;
            buttons[2] = CreateShape(Bsize, "../../../../res/button_play.png", 380, 125);
            buttons[3] = CreateShape(Bsize, "../../../../res/button_archer.png", 10, 30);
            buttons[4] = CreateShape(Bsize, "../../../../res/button_drake.png", 10, 95);
            buttons[5] = CreateShape(Bsize, "../../../../res/button_goblin.png", 105, 95);
            buttons[6] = CreateShape(Bsize, "../../../../res/button_paladin.png", 200, 95);
            buttons[7] = CreateShape(Bsize, "../../../../res/button_balista.png", 105, 30);
            buttons[8] = CreateShape(Bsize, "../../../../res/button_catapult.png", 200, 30);

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }

            messages[0] = messageTeam1;
            messages[1] = messageTeam2;
            messages[2] = messageGold;
            messages[3] = price1;
            messages[4] = price2;
            messages[5] = price3;
            messages[6] = price4;
            messages[7] = price5;
            messages[8] = price6;

            foreach (var t in messages)
            {
                t.Draw(_window, _rs);
                _window.Draw(t);
            }

            return buttons;
        }

        #endregion

        #endregion




        public Shape[] PlacementDisplay(Arena arena, string[] status, int[,] teamCompo, int[,] compoLeft,
            Unit[] unitToDraw)
        {


            Vector2f Bsize = new Vector2f(75, 25); //button size
            Font textfont = new Font("../../../../res/Overlock-Regular.ttf");

            RectangleShape font = new RectangleShape(new Vector2f(1000, 1000));
            font.Position = new Vector2f(0, 512);

            RectangleShape blueFont = new RectangleShape(new Vector2f(256, 512));
            RectangleShape redFont = new RectangleShape(new Vector2f(256, 512));
            RectangleShape yellowFont = new RectangleShape(new Vector2f(0, 0));
            RectangleShape greenFont = new RectangleShape(new Vector2f(0, 0));

            yellowFont.FillColor = new Color(0, 0, 0, 255);
            greenFont.FillColor = new Color(0, 0, 0, 255);

            RectangleShape button1 = new RectangleShape(new Vector2f(30, 25));
            RectangleShape button10 = new RectangleShape(new Vector2f(30, 25));
            RectangleShape button100 = new RectangleShape(new Vector2f(30, 25));
            Text txtButton1 = new Text("10", textfont, 15);
            Text txtButton10 = new Text("30", textfont, 15);
            Text txtButton100 = new Text("50", textfont, 15);

            RectangleShape selectionRed = new RectangleShape(Bsize);
            RectangleShape selectionBlue = new RectangleShape(Bsize);
            RectangleShape selectionGreen = new RectangleShape(new Vector2f(0, 0));
            RectangleShape selectionYellow = new RectangleShape(new Vector2f(0, 0));

            button1.Position = new Vector2f(330, 520);
            button10.Position = new Vector2f(360, 520);
            button100.Position = new Vector2f(390, 520);
            txtButton1.Position = new Vector2f(340, 520);
            txtButton10.Position = new Vector2f(360, 520);
            txtButton100.Position = new Vector2f(390, 520);
            txtButton1.FillColor = new Color(0, 0, 0);
            txtButton10.FillColor = new Color(0, 0, 0);
            txtButton100.FillColor = new Color(0, 0, 0);

            selectionRed.Position = new Vector2f(90, 520);
            selectionRed.FillColor = new Color(255, 0, 0);
            selectionBlue.Position = new Vector2f(10, 520);
            selectionBlue.FillColor = new Color(0, 0, 255);

            Shape buttonUnitSelect = new CircleShape(5);
            buttonUnitSelect.FillColor = new Color(255, 0, 0);

            if (status[2] == "archer")
            {
                buttonUnitSelect.Position = new Vector2f(5, 560);
            }
            else if (status[2] == "drake")
            {
                buttonUnitSelect.Position = new Vector2f(5, 600);
            }
            else if (status[2] == "goblin")
            {
                buttonUnitSelect.Position = new Vector2f(5, 640);
            }
            else if (status[2] == "paladin")
            {
                buttonUnitSelect.Position = new Vector2f(120, 560);
            }
            else if (status[2] == "balista")
            {
                buttonUnitSelect.Position = new Vector2f(120, 600);
            }
            else if (status[2] == "catapult")
            {
                buttonUnitSelect.Position = new Vector2f(120, 640);
            }

            if (arena.FindTeam("green"))
            {
                selectionGreen = new RectangleShape(Bsize);
                selectionGreen.Position = new Vector2f(170, 520);
                selectionGreen.FillColor = new Color(0, 255, 0);
            }

            if (arena.FindTeam("yellow"))
            {
                selectionYellow = new RectangleShape(Bsize);
                selectionYellow.Position = new Vector2f(250, 520);
                selectionYellow.FillColor = new Color(255, 255, 0);
            }



            if (status[1] == "10")
            {
                button1.FillColor = new Color(100, 100, 100);
                button10.FillColor = new Color(200, 200, 200);
                button100.FillColor = new Color(200, 200, 200);
            }
            else if (status[1] == "30")
            {
                button1.FillColor = new Color(200, 200, 200);
                button10.FillColor = new Color(100, 100, 100);
                button100.FillColor = new Color(200, 200, 200);
            }
            else
            {
                button1.FillColor = new Color(200, 200, 200);
                button10.FillColor = new Color(200, 200, 200);
                button100.FillColor = new Color(100, 100, 100);
            }




            font.FillColor = new Color(255, 255, 255);
            font.Position = new Vector2f(0, 512);


            redFont.Position = new Vector2f(256, 0);
            blueFont.Position = new Vector2f(0, 0);

            if (!arena.FindTeam("green"))
            {
                if (status[0] == "red")
                {
                    redFont.FillColor = new Color(255, 0, 0, 50);
                    blueFont.FillColor = new Color(50, 50, 50, 150);
                }
                else if (status[0] == "blue")
                {
                    redFont.FillColor = new Color(50, 50, 50, 150);
                    blueFont.FillColor = new Color(0, 0, 255, 50);
                }
            }
            else
            {
                redFont = new RectangleShape(new Vector2f(256, 256));
                blueFont = new RectangleShape(new Vector2f(256, 256));
                greenFont = new RectangleShape(new Vector2f(256, 256));

                redFont.Position = new Vector2f(0, 256);
                blueFont.Position = new Vector2f(0, 0);
                greenFont.Position = new Vector2f(256, 0);

                selectionGreen.FillColor = new Color(0, 255, 0);

                if (arena.FindTeam("yellow"))
                {

                    yellowFont = new RectangleShape(new Vector2f(256, 256));
                    yellowFont.Position = new Vector2f(256, 256);
                    selectionYellow.FillColor = new Color(255, 255, 0);
                    yellowFont.FillColor = new Color(50, 50, 50, 150);
                }

                if (status[0] == "red")
                {
                    redFont.FillColor = new Color(255, 0, 0, 50);
                    blueFont.FillColor = new Color(50, 50, 50, 150);
                    greenFont.FillColor = new Color(50, 50, 50, 150);
                }
                else if (status[0] == "blue")
                {
                    blueFont.FillColor = new Color(0, 0, 255, 50);
                    redFont.FillColor = new Color(50, 50, 50, 150);
                    greenFont.FillColor = new Color(50, 50, 50, 150);
                }
                else if (status[0] == "green")
                {
                    greenFont.FillColor = new Color(0, 255, 0, 50);
                    redFont.FillColor = new Color(50, 50, 50, 150);
                    blueFont.FillColor = new Color(50, 50, 50, 150);
                }
                else if (status[0] == "yellow")
                {
                    yellowFont.FillColor = new Color(255, 255, 0, 50);
                    redFont.FillColor = new Color(50, 50, 50, 150);
                    blueFont.FillColor = new Color(50, 50, 50, 150);
                    greenFont.FillColor = new Color(50, 50, 50, 150);
                }
            }

            RectangleShape aPoint = new RectangleShape(new Vector2f(0, 0));
            RectangleShape bPoint = new RectangleShape(new Vector2f(0, 0));

            if (status[3] != "NA")
            {
                aPoint = new RectangleShape(new Vector2f(4, 4));
                aPoint.Position = new Vector2f(Int32.Parse(status[3]), Int32.Parse(status[4]));
                aPoint.FillColor = new Color(0, 0, 0);
            }

            if (status[5] != "NA")
            {
                bPoint = new RectangleShape(new Vector2f(4, 4));
                bPoint.Position = new Vector2f(Int32.Parse(status[5]), Int32.Parse(status[6]));
                bPoint.FillColor = new Color(0, 0, 0);
            }


            Shape[] buttons = new Shape[23];
            RenderStates rs = new RenderStates();
            RectangleShape randomPlacement = new RectangleShape(Bsize);
            randomPlacement.FillColor = new Color(90, 90, 90);
            randomPlacement.Position = new Vector2f(380, 600);

            buttons[0] = font;
            buttons[1] = redFont;
            buttons[2] = blueFont;
            buttons[3] = selectionRed;
            buttons[4] = selectionBlue;
            buttons[5] = greenFont;
            buttons[6] = yellowFont;
            buttons[7] = selectionGreen;
            buttons[8] = selectionYellow;
            buttons[9] = button1;
            buttons[10] = button10;
            buttons[11] = button100;
            buttons[12] = CreateShape(Bsize, "../../../../res/button_archer.png", 10, 560);
            buttons[13] = CreateShape(Bsize, "../../../../res/button_drake.png", 10, 600);
            buttons[14] = CreateShape(Bsize, "../../../../res/button_goblin.png", 10, 640);
            buttons[15] = CreateShape(Bsize, "../../../../res/button_paladin.png", 125, 560);
            buttons[16] = CreateShape(Bsize, "../../../../res/button_balista.png", 125, 600);
            buttons[17] = CreateShape(Bsize, "../../../../res/button_catapult.png", 125, 640);
            buttons[18] = buttonUnitSelect;
            buttons[19] = aPoint;
            buttons[20] = bPoint;
            buttons[21] = CreateShape(Bsize, "../../../../res/button_play.png", 380, 640);
            buttons[22] = randomPlacement;

            Text txtArcher = new Text("*", textfont, 15);
            Text txtDrake = new Text("*", textfont, 15);
            Text txtGobelin = new Text("*", textfont, 15);
            Text txtPaladin = new Text("*", textfont, 15);
            Text txtBalista = new Text("*", textfont, 15);
            Text txtCatapult = new Text("*", textfont, 15);
            Text random = new Text("random", textfont, 15);
            random.Position = new Vector2f(390, 600);

            if (status[0] == "red")
            {
                txtArcher = new Text("*" + compoLeft[0, 0].ToString(), textfont, 15);
                txtDrake = new Text("*" + compoLeft[0, 1].ToString(), textfont, 15);
                txtGobelin = new Text("*" + compoLeft[0, 2].ToString(), textfont, 15);
                txtPaladin = new Text("*" + compoLeft[0, 3].ToString(), textfont, 15);
                txtBalista = new Text("*" + compoLeft[0, 4].ToString(), textfont, 15);
                txtCatapult = new Text("*" + compoLeft[0, 5].ToString(), textfont, 15);
            }
            else if (status[0] == "blue")
            {
                txtArcher = new Text("*" + compoLeft[1, 0].ToString(), textfont, 15);
                txtDrake = new Text("*" + compoLeft[1, 1].ToString(), textfont, 15);
                txtGobelin = new Text("*" + compoLeft[1, 2].ToString(), textfont, 15);
                txtPaladin = new Text("*" + compoLeft[1, 3].ToString(), textfont, 15);
                txtBalista = new Text("*" + compoLeft[1, 4].ToString(), textfont, 15);
                txtCatapult = new Text("*" + compoLeft[1, 5].ToString(), textfont, 15);

            }
            else if (status[0] == "green")
            {
                txtArcher = new Text("*" + compoLeft[2, 0].ToString(), textfont, 15);
                txtDrake = new Text("*" + compoLeft[2, 1].ToString(), textfont, 15);
                txtGobelin = new Text("*" + compoLeft[2, 2].ToString(), textfont, 15);
                txtPaladin = new Text("*" + compoLeft[2, 3].ToString(), textfont, 15);
                txtBalista = new Text("*" + compoLeft[2, 4].ToString(), textfont, 15);
                txtCatapult = new Text("*" + compoLeft[2, 5].ToString(), textfont, 15);

            }
            else
            {
                txtArcher = new Text("*" + compoLeft[3, 0].ToString(), textfont, 15);
                txtDrake = new Text("*" + compoLeft[3, 1].ToString(), textfont, 15);
                txtGobelin = new Text("*" + compoLeft[3, 2].ToString(), textfont, 15);
                txtPaladin = new Text("*" + compoLeft[3, 3].ToString(), textfont, 15);
                txtBalista = new Text("*" + compoLeft[3, 4].ToString(), textfont, 15);
                txtCatapult = new Text("*" + compoLeft[3, 5].ToString(), textfont, 15);

            }


            txtArcher.Position = new Vector2f(90, 560);
            txtDrake.Position = new Vector2f(90, 600);
            txtGobelin.Position = new Vector2f(90, 640);
            txtPaladin.Position = new Vector2f(205, 560);
            txtBalista.Position = new Vector2f(205, 600);
            txtCatapult.Position = new Vector2f(205, 640);

            Text[] text = new Text[10];

            text[0] = txtButton1;
            text[1] = txtButton10;
            text[2] = txtButton100;
            text[3] = txtArcher;
            text[4] = txtDrake;
            text[5] = txtGobelin;
            text[6] = txtPaladin;
            text[7] = txtBalista;
            text[8] = txtCatapult;
            text[9] = random;

            for (int i = 3; i < text.Length; i++)
            {
                text[i].FillColor = new Color(0, 0, 0);
            }

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }

            foreach (var t2 in text)
            {
                t2.Draw(_window, rs);
                _window.Draw(t2);
            }

            for (int i = 0; i < 800; i++)
            {
                if (!(unitToDraw[i] is null))
                {
                    Shape shape;
                    _window.Draw(shape = DisplayUnit(unitToDraw[i]));
                }
            }

            return buttons;
        }
    }
}
