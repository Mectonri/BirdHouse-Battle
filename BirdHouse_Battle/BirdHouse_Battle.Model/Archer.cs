using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Archer : Unit
    {
        public Archer(Team team, Arena arena)
            : base(team, arena, 2.0, 2.0, 5.0, 10.0, 2, 1, "Order")
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