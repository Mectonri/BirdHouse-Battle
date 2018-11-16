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
            #region Fields
        RenderWindow _window;
        Shape Bshape;
            #endregion
        
            #region Properties
        public Drawer(RenderWindow win)
        {

            _window = win;
            Texture terain = new Texture("../../../../res/terrain1.jpeg");
            Vector2f size = new Vector2f(512, 512);
            Bshape = new RectangleShape(size);
            Bshape.Texture = terain;
            win.Draw(Bshape);

        }
        #endregion

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

            switch (unit.Team.TeamNumber)
            {
                case 0:
                    archDis.FillColor = new Color(Color.Blue);
                    break;
                case 1:
                    archDis.FillColor = new Color(Color.Red);
                    break;
                case 2:
                    archDis.FillColor = new Color(Color.Green);
                    break;
                case 3:
                    archDis.FillColor = new Color(Color.Yellow);
                    break;
            }
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

            switch (unit.Team.TeamNumber)
            {
                case 0:
                    gobDis.FillColor = new Color(Color.Blue);
                    break;
                case 1:
                    gobDis.FillColor = new Color(Color.Red);
                    break;
                case 2:
                    gobDis.FillColor = new Color(Color.Green);
                    break;
                case 3:
                    gobDis.FillColor = new Color(Color.Yellow);
                    break;
            }
            return gobDis;
        }

        static RectangleShape DisplayPaladin(Unit unit)
        {
            Vector2f vect = new Vector2f(10, 10);
            RectangleShape palDis = new RectangleShape(vect);
            palDis.Position = new Vector2f((float)unit.Location.X + 250, (float)unit.Location.Y + 250);

            //Assign a defalut color to a unit in a team 
            //That way, team 1 is yellow, 2 is blue, 3 is Red, 4 is green
            switch (unit.Team.TeamNumber)
            {
                case 0:
                    palDis.FillColor = new Color(Color.Blue);
                    break;
                case 1:
                    palDis.FillColor = new Color(Color.Red);
                    break;
                case 2:
                    palDis.FillColor = new Color(Color.Green);
                    break;
                case 3:
                    palDis.FillColor = new Color(Color.Yellow);
                    break;
            }
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

            switch (unit.Team.TeamNumber)
            {
                case 0:
                    drakDis.FillColor = new Color(Color.Blue);
                    break;
                case 1:
                    drakDis.FillColor = new Color(Color.Red);
                    break;
                case 2:
                    drakDis.FillColor = new Color(Color.Green);
                    break;
                case 3:
                    drakDis.FillColor = new Color(Color.Yellow);
                    break;
            }
            return drakDis;
        }

        static Shape DisplayArrow(Projectile projectile)
        {
            CircleShape arrDis = new CircleShape(2);
            arrDis.Position = new Vector2f((float)projectile.Position.X + 250, (float)projectile.Position.Y + 250);

            return arrDis;
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

        /// <summary>
        /// Display Units and projectiles
        /// </summary>
        /// <param name="arena"></param>
        public void UnitDisplay(Arena arena)
        {
            Shape Shape;

            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                foreach (KeyValuePair<Guid, Unit> u in team.Value.Unit)
                {
                    string s = u.Value.ToString();
                    if (s == "BirdHouse_Battle.Model.Archer") Shape = DisplayArcher(u.Value);
                    else if (s == "BirdHouse_Battle.Model.Gobelin") Shape = DisplayGobelin(u.Value);
                    else if (s == "BirdHouse_Battle.Model.Drake") Shape = DisplayDrake(u.Value);
                    else { Shape = DisplayPaladin(u.Value); }

                    _window.Draw(Shape);
                }
            }
            foreach (KeyValuePair<int, Projectile> u in arena.Projectiles)
            {
                string s = u.Value.ToString();
                /*if (s == "BirdHouse_Battle.Model.Arrow")*/
                Shape = DisplayArrow(u.Value);
                _window.Draw(Shape);
            }
        }
    }
}
