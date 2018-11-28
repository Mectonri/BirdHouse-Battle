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
        readonly Shape _bShape;
        Texture _terain;
        Vector2f _size;
        
            #region Properties
        public Drawer(RenderWindow win)
        {

            _window = win;
            _terain = new Texture("../../../../res/terrain1.jpeg");
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

            RectangleShape buttonPreGame = new RectangleShape(new Vector2f(100, 25));
            buttonPreGame.Position = new Vector2f(200, 50);


            RectangleShape buttonHistory = new RectangleShape(new Vector2f(100, 25));
            buttonHistory.Position = new Vector2f(200, 100);


            RectangleShape buttonParameter = new RectangleShape(new Vector2f(100, 25));
            buttonParameter.Position = new Vector2f(200, 150);
  

            RectangleShape buttonCredit = new RectangleShape(new Vector2f(100, 25));
            buttonCredit.Position = new Vector2f(200, 200);
            

           

            buttons[0] = buttonPreGame;
            buttons[1] = buttonHistory;
            buttons[2] = buttonParameter;
            buttons[3] = buttonCredit;



            foreach (var t in buttons)
            {
                _window.Draw(t);
            }

            return buttons;
        }







        public RectangleShape[] PreGameDisplay(string[] status)
        {
            RenderStates rs = new RenderStates();
            Font font = new Font("../../../../res/GreatVibes-Regular.ttf");


            RectangleShape[] buttons = new RectangleShape[9];
            Text[] messages = new Text[5];


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

            RectangleShape buttonAddTeam2 = new RectangleShape(new Vector2f(118, 190));
            buttonAddTeam2.Position = new Vector2f(132, 317);
            buttonAddTeam2.OutlineThickness = 5;
            buttonAddTeam2.OutlineColor = new Color(250, 0, 0);
            if (status[1] == "selected")
            {
                buttonAddTeam2.FillColor = new Color(255, 160, 122);
            }


            RectangleShape buttonAddTeam3 = new RectangleShape(new Vector2f(118, 190));
            buttonAddTeam3.Position = new Vector2f(261, 317);
            buttonAddTeam3.OutlineThickness = 5;
            buttonAddTeam3.OutlineColor = new Color(0, 250, 0);
            if (status[2] == "inactive")
            {
                buttonAddTeam3.FillColor = new Color(128,128,128);
            }
            else if (status[2] == "selected")
            {
                buttonAddTeam3.FillColor = new Color(255, 160, 122);
            }

            RectangleShape buttonAddTeam4 = new RectangleShape(new Vector2f(118, 190));
            buttonAddTeam4.Position = new Vector2f(389, 317);
            buttonAddTeam4.OutlineThickness = 5;
            buttonAddTeam4.OutlineColor = new Color(250, 250, 0);
            if (status[3]== "inactive")
            {
                buttonAddTeam4.FillColor = new Color(128, 128, 128);
            }
            if (status[3] == "selected")
            {
                buttonAddTeam4.FillColor = new Color(255, 160, 122);
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

            foreach (var t in buttons)
            {
                _window.Draw(t);
            }


            messages[0] = messageArcher;
            messages[1] = messageDrake;
            messages[2] = messageGobelin;
            messages[3] = messagePaladin;
            messages[4] = messagePlay;

            foreach(var t in messages)
            {
                t.Draw(_window, rs);
                _window.Draw(t);
            }



            return buttons;
        }
    }
}