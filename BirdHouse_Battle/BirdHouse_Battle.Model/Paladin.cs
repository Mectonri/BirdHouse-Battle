using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Paladin : Unit
    {
        public Paladin(Team team, Arena arena)
            : base(team, arena, 5.0, 1.1, 2.0, 12.5, 5, 4, "Order")
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
