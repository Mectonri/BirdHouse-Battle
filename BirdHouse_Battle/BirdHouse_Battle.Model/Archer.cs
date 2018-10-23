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
            : base(team, arena, 2.0, 1.5, 5.0, 10.0, 2, 1, "Order")
        {
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public void Update()
        {
            if (!IsDead)
            {
                if (InRange)
                {
                    Arena.GiveDamage(Target, Strength);
                }
                else
                {
                    Mouvement = Direction.Move(Speed);
                    Vector NewLocation = Location.Add(Mouvement);

                    if (!Arena.Collision(NewLocation)) Location = NewLocation;
                }
                SearchTarget();
            }
        }
    }
}