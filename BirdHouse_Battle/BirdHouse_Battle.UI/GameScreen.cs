using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using BirdHouse_Battle.Model;
using System.Collections.Generic;

namespace BirdHouse_Battle.UI
{
    public class GameScreen
    {
        //Close the window when the On close event is received
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void DisplayPaladin( Unit unit)
        {
            CircleShape palDis = new CircleShape(3);
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

        }


        /// <summary>
        /// Displays Goblin and color it in function of the team
        /// </summary>
        /// <param name="unit"></param>
        static void DisplayGoblin( Unit unit)
        {
            CircleShape gobDis = new CircleShape(80, 3);
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
        }

        /// <summary>
        /// Display archer with the coresponding color
        /// </summary>
        /// <param name="unit"></param>
        static void DisplayArcher(Unit unit )
        {
                CircleShape archDis = new CircleShape(80, 3);
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
        }

        /// <summary>
        /// Display Units 
        /// </summary>
        /// <param name="arena"></param>
        static void UnitDisplay(Arena arena)
        {
            foreach (KeyValuePair<string, Team> team in arena.Teams)
            {
                foreach (KeyValuePair<Guid, Unit> u in team.Value.Unit)
                {
                    string s = u.GetType().ToString();
                    if (s == "BirdHouse_Battle.Model.Archer") DisplayArcher(u.Value);
                    else if (s == "BirdHouse_Battle.Model.Goblinr") DisplayGoblin(u.Value);
                    else { DisplayPaladin(u.Value); }                 
                }
            }
        }
        static void render(Arena arena)
        {
            
                    //Create a winwow
                    RenderWindow window = new RenderWindow(new VideoMode(1000, 1000), "SFML window");
                    UnitDisplay(arena);

                    //window.Display();
            
        }
    }
}
