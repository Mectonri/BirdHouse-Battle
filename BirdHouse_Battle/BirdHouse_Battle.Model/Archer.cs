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
            : base(team, arena, 12.0, 1.8, 35.0, 10.0, 4, 1, "Order")
        {
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            if (!IsDead())
            {
                if (InRange())
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