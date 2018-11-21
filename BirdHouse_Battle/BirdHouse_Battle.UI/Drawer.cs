using BirdHouse_Battle.Model;
using System.Collections.Generic;
using System;
//using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;

namespace BirdHouse_Battle.UI
{
    /// <summary>
    ///  This class will be used to only draw units
    /// </summary>
    public class Drawer
    {
        readonly RenderWindow _window;
        Shape _bShape;
        Texture _terain;
        Vector2f _size;
        
            #region Properties
        public Drawer(RenderWindow win)
        {
            _window = win;
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
        /// This function will launch and draw the pause menu when the game is on pause.
        /// </summary>
        public void PauseMenu()
        {
            /*PauseMenu should have the difrenet options:
                restart
                quit
                settings
             */
            throw new NotImplementedException();
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
                    if (s == "BirdHouse_Battle.Model.Archer") shape = DisplayArcher(unit.Value);
                    else if (s == "BirdHouse_Battle.Model.Gobelin") shape = DisplayGobelin(unit.Value);
                    else if (s == "BirdHouse_Battle.Model.Drake") shape = DisplayDrake(unit.Value);
                    else { shape = DisplayPaladin(unit.Value); }

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
            buttonGame.Position = new Vector2f(200, 50);


            RectangleShape buttonHistory = new RectangleShape(new Vector2f(100, 25));
            buttonHistory.Position = new Vector2f(200, 100);


            RectangleShape buttonParameter = new RectangleShape(new Vector2f(100, 25));
            buttonParameter.Position = new Vector2f(200, 150);
  

            RectangleShape buttonCredit = new RectangleShape(new Vector2f(100, 25));
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
    }
}