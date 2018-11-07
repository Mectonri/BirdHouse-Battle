using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Paladin : Unit
    {
        public Paladin(Team team, Arena arena)
            : base(team, arena, 18.0, 1.2, 15.0, 12.5, 5, 4, "Order", false, false)
        {
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            if (!IsDead() || DumpCantFly == false)
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

                if (Burn > 0) Burning();

                SearchTargetNotFlying();
            }
            else if (!IsDead())
            {
                DumpFlyAway();
                SearchTargetNotFlying();
            }
        }
    }
}