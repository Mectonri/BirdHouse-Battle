using BirdHouse_Battle.Model;
using System.Collections.Generic;
using System;
using SFML.Graphics;
using SFML.System;

namespace BirdHouse_Battle.UI
{
    /// <summary>
    ///  This class will be used to only draw units
    /// </summary>
    public class Drawer
    {
        RenderWindow _window;
        Shape _bShape;
        Texture _terain;
        Vector2f _size;
        Texture _startButton;
        Texture _creditButton;
        Texture _settingButton;
        Texture _historyButton;
        Texture _continueButton;
        Texture _restartButton;
        Texture _quitButton;
        Text _message;
        Font _font;
        RenderStates rs;

            #region Properties
        public Drawer(RenderWindow win)
        {
            Texturer();
            _terain = new Texture("../../../../res/terrain1.jpeg");
            _startButton = new Texture("../../../../res/button_start.png");

            _creditButton = new Texture("../../../../res/button_credits.png");
            _settingButton = new Texture("../../../../res/button_setting.png");
            _historyButton = new Texture("../../../../res/button_history.png");

            _continueButton = new Texture("../../../../res/button_continue.png");
            _restartButton = new Texture("../../../../res/button_restart.png");
            _quitButton = new Texture("../../../../res/button_quit.png");

            

            _window = win;
           
            _size = new Vector2f(512, 512);
            _bShape = new RectangleShape(_size);
            _bShape.Texture = _terain;
            win.Draw(Bshape);
        }
        #endregion

        public Shape Bshape { get { return _bShape; } }

        public Texture Terain { get { return _terain; } }

        public Vector2f Size { get { return _size; } }

        #region

        public void BackGroundGame()
        {
            _terain = new Texture("../../../../res/terrain1.jpeg");
            _size = new Vector2f(512, 512);
            _bShape = new RectangleShape(_size);
            _bShape.Texture = _terain;
            _window.Draw(Bshape);

            RectangleShape legend = new RectangleShape(new Vector2f(512, 200));
            legend.Texture = new Texture("../../../../res/LEGEND.png");
            legend.Position = new Vector2f(0, 512);
            _window.Draw(legend);
        }

        /// <summary>
        /// Display archer with the corresponding color
        /// </summary>
        /// <param name="unit"></param>
        static Shape DisplayArcher(Unit unit)
        {
            CircleShape archDis = new CircleShape(7);
            archDis.SetPointCount(3);
            archDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(archDis, unit);

            return archDis;
        }

        /// <summary>
        /// Displays Goblin and color it in function of the team
        /// </summary>
        /// <param name="unit"></param>
        static CircleShape DisplayGobelin(Unit unit)
        {
            CircleShape gobDis = new CircleShape(6);
            gobDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(gobDis, unit);

            return gobDis;
        }

        static RectangleShape DisplayPaladin(Unit unit)
        {
            Vector2f vect = new Vector2f(10, 10);
            RectangleShape palDis = new RectangleShape(vect);
            palDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(palDis, unit);

            return palDis;
        }

        /// <summary>
        /// Display drake with the corresponding color
        /// </summary>
        /// <param name="unit"></param>
        static Shape DisplayDrake(Unit unit)
        {
            CircleShape drakDis = new CircleShape(8);
            drakDis.SetPointCount(5);
            drakDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(drakDis, unit);

            return drakDis;
        }

        static Shape DisplayBalista(Unit unit)
        {
            CircleShape balistDis = new CircleShape(8);
            balistDis.SetPointCount(6);
            balistDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(balistDis, unit);

            return balistDis;
        }

        static Shape DisplayCatapult(Unit unit)
        {
            CircleShape cataDis = new CircleShape(8);
            cataDis.SetPointCount(7);
            cataDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            Coloring(cataDis, unit);

            return cataDis;
        }

        static Shape DisplayArrow(Projectile projectile)
        {
            CircleShape arrDis = new CircleShape(2);
            arrDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return arrDis;
        }

        static Shape DisplayBoulder(Projectile projectile)
        {
            CircleShape boulDis = new CircleShape(6);
            boulDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return boulDis;
        }

        static Shape DisplayBalisticAmmo(Projectile projectile)
        {
            CircleShape balisDis = new CircleShape(4);
            balisDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return balisDis;
        }

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

        #endregion

        /// <summary>
        /// Creates the Texture for the differents buttons 
        /// </summary>
        static void Texturer()
        {
           
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

        /// <summary>
        /// Display Units and projectiles
        /// </summary>
        /// <param name="arena"></param>
        public void UnitDisplay(Arena arena)
        {
            Shape shape = null;

            foreach (KeyValuePair<String, Tile> tile in arena.Field.Elements)
            {
                shape = DisplayField(tile.Value);
                _window.Draw(shape);
            }
            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                foreach (KeyValuePair<int, Unit> unit in team.Value.Units)
                {
                    string s = unit.Value.ToString();
                    switch (s)
                    {
                        case "BirdHouse_Battle.Model.Archer":
                            shape = DisplayArcher(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Goblin":
                            shape = DisplayGobelin(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Drake":
                            shape = DisplayDrake(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Balista":
                            shape = DisplayBalista(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Catapult":
                            shape = DisplayCatapult(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Paladin":
                            shape = DisplayPaladin(unit.Value);
                            break;
                    }

                    _window.Draw(shape);
                }
            }
            foreach (KeyValuePair<int, Projectile> projectile in arena.Projectiles)
            {
                string s = projectile.Value.ToString();
                if (s == "BirdHouse_Battle.Model.Arrow") shape = DisplayArrow(projectile.Value);
                else if (s == "BirdHouse_Battle.Model.Boulder") shape = DisplayBoulder(projectile.Value);
                else shape = DisplayBalisticAmmo(projectile.Value);
                _window.Draw(shape);
            }
        }

        public RectangleShape[] MenuDisplay()
        {
            RectangleShape[] buttons = new RectangleShape[5];

            RectangleShape buttonPreGame = new RectangleShape(new Vector2f(100, 25));
            buttonPreGame.Texture = _startButton;
            buttonPreGame.Position = new Vector2f(200, 50);
            
            
            RectangleShape buttonHistory = new RectangleShape(new Vector2f(100, 25));
            buttonHistory.Texture = _historyButton;
            buttonHistory.Position = new Vector2f(200, 100);


            RectangleShape buttonParameter = new RectangleShape(new Vector2f(100, 25));
            buttonParameter.Texture = _settingButton;
            buttonParameter.Position = new Vector2f(200, 150);
  

            RectangleShape buttonCredit = new RectangleShape(new Vector2f(100, 25));
            
            buttonCredit.Texture = _creditButton;
            buttonCredit.Position = new Vector2f(200, 200);

            RectangleShape buttonQuit = new RectangleShape(new Vector2f(100, 25));
            buttonQuit.Texture = _quitButton;
            buttonQuit.Position = new Vector2f(200, 250);

            buttons[0] = buttonPreGame;
            buttons[1] = buttonHistory;
            buttons[2] = buttonParameter;
            buttons[3] = buttonCredit;
            buttons[4] = buttonQuit;

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            return buttons;
        }

        /// <summary>
        /// This function will launch and draw the pause menu when the game is on pause.
        /// </summary>
        public RectangleShape[] PauseDisplay()
        {
            RectangleShape[] button = new RectangleShape[4];

            Vector2f Bsize = new Vector2f(100, 25);
            RectangleShape buttonContinue = new RectangleShape(Bsize);
            buttonContinue.Texture = _continueButton;
            buttonContinue.Position = new Vector2f(200, 50);

            RectangleShape buttonRestart = new RectangleShape(Bsize);
            buttonRestart.Texture = _restartButton;
            buttonRestart.Position = new Vector2f(200, 100);

            RectangleShape buttonSetting = new RectangleShape(Bsize);
            buttonSetting.Texture = _settingButton;
            buttonSetting.Position = new Vector2f(200, 150);

            RectangleShape buttonQuit = new RectangleShape(Bsize);
            buttonQuit.Texture = _quitButton;
            buttonQuit.Position = new Vector2f(200, 200);

            button[0] = buttonContinue;
            button[1] = buttonRestart;
            button[2] = buttonSetting;
            button[3] = buttonQuit;

            foreach (var t in button)
            {
                _window.Draw(t);
            }
            return button;
        }







        public RectangleShape[] PreGameDisplay(string[] status, int[,] teamComposition)
        {
            RenderStates rs = new RenderStates();
            Font font = new Font("../../../../res/GreatVibes-Regular.ttf");


            RectangleShape[] buttons = new RectangleShape[11];
            Text[] messages = new Text[11];


            RectangleShape buttonPlay = new RectangleShape(new Vector2f(75, 25));
            buttonPlay.Position = new Vector2f(380, 125);
            Text messagePlay = new Text("play", font, 25);
            messagePlay.FillColor = new Color(0, 0, 0);
            messagePlay.Position = new Vector2f(400, 116);


            RectangleShape buttonArcher = new RectangleShape(new Vector2f(75, 25));
            buttonArcher.Position = new Vector2f(10,30);
            Text messageArcher = new Text("archer", font, 25);
            messageArcher.FillColor = new Color(0, 0, 0);
            messageArcher.Position = new Vector2f(20, 21);

            RectangleShape buttonBalista = new RectangleShape(new Vector2f(75, 25));
            buttonBalista.Position = new Vector2f(105, 30);
            Text messageBalista = new Text("balista", font, 25);
            messageBalista.FillColor = new Color(0, 0, 0);
            messageBalista.Position = new Vector2f(115, 21);

            RectangleShape buttonCatapult = new RectangleShape(new Vector2f(75, 25));
            buttonCatapult.Position = new Vector2f(105, 95);
            Text messageCatapult = new Text("catapult", font, 25);
            messageCatapult.FillColor = new Color(0, 0, 0);
            messageCatapult.Position = new Vector2f(115, 84);

            RectangleShape buttonDrake = new RectangleShape(new Vector2f(75, 25));
            buttonDrake.Position = new Vector2f(10, 95);
            Text messageDrake = new Text("drake", font, 25);
            messageDrake.FillColor = new Color(0, 0, 0);
            messageDrake.Position = new Vector2f(20, 86);


            RectangleShape buttonGobelin = new RectangleShape(new Vector2f(75, 25));
            buttonGobelin.Position = new Vector2f(10, 160);
            Text messageGobelin = new Text("gobelin", font, 25);
            messageGobelin.FillColor = new Color(0, 0, 0);
            messageGobelin.Position = new Vector2f(20, 151);


            RectangleShape buttonPaladin = new RectangleShape(new Vector2f(75, 25));
            buttonPaladin.Position = new Vector2f(10, 225);
            Text messagePaladin = new Text("paladin", font, 25);
            messagePaladin.FillColor = new Color(0, 0, 0);
            messagePaladin.Position = new Vector2f(20, 216);




            RectangleShape buttonAddTeam1 = new RectangleShape(new Vector2f(118, 190));
            buttonAddTeam1.Position = new Vector2f(5, 317);
            buttonAddTeam1.OutlineThickness = 5;
            buttonAddTeam1.OutlineColor = new Color(0,0,250);
            if (status[0]== "selected")
            {
                buttonAddTeam1.FillColor = new Color(255, 160, 122);
            }
            Text messageTeam1 = new Text("archer : "+teamComposition[0,0].ToString()+"\n drake : "+teamComposition[0,1].ToString()+"\n gobelin : "+teamComposition[0,2].ToString()+"\n paladin : "+teamComposition[0,3].ToString()+"\n balista : "+teamComposition[0,4].ToString()+"\n catapult : "+teamComposition[0,5].ToString(), font, 15);
            messageTeam1.FillColor = new Color(0, 0, 0);
            messageTeam1.Position = new Vector2f(15, 322);

            RectangleShape buttonAddTeam2 = new RectangleShape(new Vector2f(118, 190));
            buttonAddTeam2.Position = new Vector2f(132, 317);
            buttonAddTeam2.OutlineThickness = 5;
            buttonAddTeam2.OutlineColor = new Color(250, 0, 0);
            if (status[1] == "selected")
            {
                buttonAddTeam2.FillColor = new Color(255, 160, 122);
            }
            Text messageTeam2 = new Text("archer : " + teamComposition[1, 0].ToString() + "\n drake : " + teamComposition[1, 1].ToString() + "\n gobelin : " + teamComposition[1, 2].ToString() + "\n paladin : " + teamComposition[1, 3].ToString() + "\n balista : " + teamComposition[1, 4].ToString() + "\n catapult : " + teamComposition[1, 5].ToString(), font, 15);
            messageTeam2.FillColor = new Color(0, 0, 0);
            messageTeam2.Position = new Vector2f(142, 322);


            RectangleShape buttonAddTeam3 = new RectangleShape(new Vector2f(118, 190));
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
                messageTeam3 = new Text("archer : " + teamComposition[2, 0].ToString() + "\n drake : " + teamComposition[2, 1].ToString() + "\n gobelin : " + teamComposition[2, 2].ToString() + "\n paladin : " + teamComposition[2, 3].ToString() + "\n balista : " + teamComposition[2, 4].ToString() + "\n catapult : " + teamComposition[2, 5].ToString(), font, 15);
                messageTeam3.FillColor = new Color(0, 0, 0);
                messageTeam3.Position = new Vector2f(271, 322);
            }

            RectangleShape buttonAddTeam4 = new RectangleShape(new Vector2f(118, 190));
            buttonAddTeam4.Position = new Vector2f(389, 317);
            buttonAddTeam4.OutlineThickness = 5;
            buttonAddTeam4.OutlineColor = new Color(250, 250, 0);
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
                messageTeam4 = new Text("archer : " + teamComposition[3, 0].ToString() + "\n drake : " + teamComposition[3, 1].ToString() + "\n gobelin : " + teamComposition[3, 2].ToString() + "\n paladin : " + teamComposition[3, 3].ToString() + "\n balista : " + teamComposition[3, 4].ToString() + "\n catapult : " + teamComposition[3, 5].ToString(), font, 15);
                messageTeam4.FillColor = new Color(0, 0, 0);
                messageTeam4.Position = new Vector2f(399, 322);
            }







            buttons[0] = buttonAddTeam1;
            buttons[1] = buttonAddTeam2;
            buttons[2] = buttonAddTeam3;
            buttons[3] = buttonAddTeam4;
            buttons[4] = buttonPlay;
            buttons[5] = buttonArcher;
            buttons[6] = buttonDrake;
            buttons[7] = buttonGobelin;
            buttons[8] = buttonPaladin;
            buttons[9] = buttonBalista;
            buttons[10] = buttonCatapult;

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }


            messages[0] = messageArcher;
            messages[1] = messageDrake;
            messages[2] = messageGobelin;
            messages[3] = messagePaladin;
            messages[4] = messagePlay;
            messages[5] = messageTeam1;
            messages[6] = messageTeam2;
            messages[7] = messageTeam3;
            messages[8] = messageTeam4;
            messages[9] = messageBalista;
            messages[10] = messageCatapult;

            foreach(var t in messages)
            {
                t.Draw(_window, rs);
                _window.Draw(t);
            }

            return buttons;
        }
    }
}