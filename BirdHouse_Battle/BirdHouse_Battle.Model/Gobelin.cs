using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Gobelin : Unit
    {
        public Gobelin(Team team, Arena arena)
            : base(team, arena, 1.0, 1.9, 1.0, 5.0, 3, 1, "Chaos")
        {
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}
