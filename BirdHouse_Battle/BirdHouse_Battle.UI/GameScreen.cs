using System;
using BirdHouse_Battle.Model;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace BirdHouse_Battle.UI
{
    public class GameScreen
    {
        RenderWindow _window;

        public GameScreen()
        {
            SFML.SystemNative.Load();
            SFML.WindowNative.Load();
            SFML.GraphicsNative.Load();
            SFML.AudioNative.Load();
            _window = new RenderWindow(new VideoMode(1080, 700), "SFML window");
        }

        public RenderWindow window {
            get { return _window; }
            private set { }
        }

        //Close the window when the On close event is received
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static CircleShape DisplayPaladin( Unit unit)
        {
            CircleShape palDis = new CircleShape(5);
            palDis.Position = new Vector2f((float)unit.Location.X, (float)unit.Location.Y);

            //Assign a defalut color to a unit in a team 
            //That way, team 1 is yellow, 2 is blue, 3 is Red, 4 is green
            switch (unit.Team.TeamNumber)
            {
                case 1:
                    palDis.FillColor = new Color(Color.Yellow);
                    break;
                case 2:
                    palDis.FillColor = new Color(Color.Blue);
                    break;
                case 3:
                    palDis.FillColor = new Color(Color.Red);
                    break;
                case 4:
                    palDis.FillColor = new Color(Color.Green);
                    break;
            }
            return palDis;
        }


        /// <summary>
        /// Displays Goblin and color it in function of the team
        /// </summary>
        /// <param name="unit"></param>
        static CircleShape DisplayGobelin( Unit unit)
        {
            CircleShape gobDis = new CircleShape(5, 4);
            gobDis.Position = new Vector2f((float)unit.Location.X, (float)unit.Location.Y);

            switch (unit.Team.TeamNumber)
            {
                case 1:
                    gobDis.FillColor = new Color(Color.Yellow);
                    break;
                case 2:
                    gobDis.FillColor = new Color(Color.Blue);
                    break;
                case 3:
                    gobDis.FillColor = new Color(Color.Red);
                    break;
                case 4:
                    gobDis.FillColor = new Color(Color.Green);
                    break;
            }
            return gobDis;
        }


        /// <summary>
        /// Display archer with the coresponding color
        /// </summary>
        /// <param name="unit"></param>
        static CircleShape DisplayArcher(Unit unit )
        {
                CircleShape archDis = new CircleShape(5, 3);
                archDis.Position = new Vector2f((float)unit.Location.X, (float)unit.Location.Y);

            switch (unit.Team.TeamNumber)
            {
                case 1:
                    archDis.FillColor = new Color(Color.Yellow);
                    break;
                case 2:
                    archDis.FillColor = new Color(Color.Blue);
                    break;
                case 3:
                    archDis.FillColor = new Color(Color.Red);
                    break;
                case 4:
                    archDis.FillColor = new Color(Color.Green);
                    break;
            }
            return archDis;
        }

        /// <summary>
        /// Display Units 
        /// </summary>
        /// <param name="arena"></param>
        public void UnitDisplay(Arena arena)
        {
            CircleShape Shape;

            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                foreach (KeyValuePair<Guid, Unit> u in team.Value.Unit)
                {
                    string s = u.GetType().ToString();
                    if (s == "BirdHouse_Battle.Model.Archer") Shape = DisplayArcher(u.Value);
                    else if (s == "BirdHouse_Battle.Model.Gobelin") Shape = DisplayGobelin(u.Value);
                    else { Shape = DisplayPaladin(u.Value); }

                    window.Draw(Shape);
                }
            }
        }
    }
}
