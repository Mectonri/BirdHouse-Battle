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
        readonly Shape _bShape;
        Texture _terain;
        Vector2f _size;
        Texture _startButton;
        Texture _creditButton;
        Texture _settingButton;
        Texture _historyButton;
        Texture _continueButton;
        Texture _restartButton;
        Texture _quitButton;
        
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
           
           // _window = win;
           
            
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
        /// Displays Gobelin and color it in function of the team
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

        static Shape DisplayArrow(Projectile projectile)
        {
            CircleShape arrDis = new CircleShape(2);
            arrDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return arrDis;
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
            arrField.Position = new Vector2f((float)tile.X + 250, (float)tile.Y + 250);

            if (tile.Obstacle != "None")
            {
                switch (tile.Obstacle)
                {
                    case "Rock":
                        arrField.FillColor = new Color(187, 187, 187);
                        break;

                    case "Tree":
                        arrField.FillColor = new Color(0, 67, 5);
                        break;

                    case "River":
                        arrField.FillColor = new Color();
                        break;
                }
            }
            else if (tile.Height != 0)
            {
                switch (tile.Height)
                {
                    case 1:
                        arrField.FillColor = new Color(45, 45, 45);
                        break;

                    case 2:
                        arrField.FillColor = new Color(75, 75, 75);
                        break;

                    case 3:
                        arrField.FillColor = new Color(105, 105, 105);
                        break;

                    case 4:
                        arrField.FillColor = new Color(135, 135, 135);
                        break;

                    case 5:
                        arrField.FillColor = new Color(165, 165, 165);
                        break;

                    case 6:
                        arrField.FillColor = new Color(195, 195, 195);
                        break;

                    case 7:
                        arrField.FillColor = new Color(225, 225, 225);
                        break;

                    case 8:
                        arrField.FillColor = new Color(255, 255, 255);
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
            Shape shape;

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
                        case "BirdHouse_Battle.Model.Gobelin":
                            shape = DisplayGobelin(unit.Value);
                            break;
                        case "BirdHouse_Battle.Model.Drake":
                            shape = DisplayDrake(unit.Value);
                            break;
                        default:
                            shape = DisplayPaladin(unit.Value);
                            break;
                    }

                    _window.Draw(shape);
                }
            }
            foreach (KeyValuePair<int, Projectile> projectile in arena.Projectiles)
            {
                //string s = u.Value.ToString();
                //if (s == "BirdHouse_Battle.Model.Arrow")
                shape = DisplayArrow(projectile.Value);
                _window.Draw(shape);
            }
        }

        public RectangleShape[] MenuDisplay()
        {
            RectangleShape[] buttons = new RectangleShape[4];

            RectangleShape buttonGame = new RectangleShape(new Vector2f(100, 25));
            buttonGame.Texture = _startButton;
            buttonGame.Position = new Vector2f(200, 50);
            
            


            RectangleShape buttonHistory = new RectangleShape(new Vector2f(100, 25));
            buttonHistory.Texture = _historyButton;
            buttonHistory.Position = new Vector2f(200, 100);


            RectangleShape buttonParameter = new RectangleShape(new Vector2f(100, 100));
            buttonParameter.Texture = _settingButton;
            buttonParameter.Position = new Vector2f(0, 0);
  

            RectangleShape buttonCredit = new RectangleShape(new Vector2f(100, 25));
            
            buttonCredit.Texture = _creditButton;
            buttonCredit.Position = new Vector2f(200, 200);

            buttons[0] = buttonGame;
            buttons[1] = buttonHistory;
            buttons[2] = buttonParameter;
            buttons[3] = buttonCredit;

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            return buttons;
        }

        /// <summary>
        /// This function will launch and draw the pause menu when the game is on pause.
        /// </summary>
        public RectangleShape[] PauseMenu()
        {
            RectangleShape[] buttons = new RectangleShape[4];

            Vector2f Bsize = new Vector2f(100, 25);
            RectangleShape buttonContinue = new RectangleShape(Bsize);
            buttonContinue.Texture = _continueButton;
            buttonContinue.Position = new Vector2f(200, 50);

            RectangleShape buttonRestart = new RectangleShape(Bsize);
            buttonContinue.Texture = _restartButton;
            buttonContinue.Position = new Vector2f(200, 100);

            RectangleShape buttonSetting = new RectangleShape(Bsize);
            buttonContinue.Texture = _settingButton;
            buttonContinue.Position = new Vector2f();

            RectangleShape buttonQuit = new RectangleShape(Bsize);
            buttonContinue.Texture = _quitButton;
            buttonContinue.Position = new Vector2f(200, 150);

            buttons[0] = buttonContinue;
            buttons[1] = buttonRestart;
            buttons[2] = buttonSetting;
            buttons[3] = buttonQuit;

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }
            return buttons;
        }
    }
}