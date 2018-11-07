using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdHouse_Battle.Model
{
    public class Drake : Unit
    {
        public Drake(Team team, Arena arena)
            : base(team, arena, 10.0, 1.5, 10.0, 15.0, 7, 0, "Chaos", true, true)
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
                    if (new Random().NextDouble() < 0.5)
                    {
                        Arena.GiveDamage(Target, Strength);
                    }
                    else
                    {
                        Arena.GiveFire(Target, 2);
                    }
                }
                else
                {
                    Mouvement = Direction.Move(Speed);
                    Vector NewLocation = Location.Add(Mouvement);

                    if (!Arena.Collision(NewLocation)) Location = NewLocation;
                }

                if (Burn > 0) Burning();

                SearchTarget();
            }
            else if (!IsDead())
            {
                DumpFlyAway();
                SearchTarget();
            }
        }
    }
}
