using System;
using System.Collections.Generic;
using System.Text;
using BirdHouse_Battle.Model;
using NUnit.Framework;

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