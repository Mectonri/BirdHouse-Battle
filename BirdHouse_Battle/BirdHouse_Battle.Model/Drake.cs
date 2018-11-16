using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdHouse_Battle.Model
{
    public class Drake : Unit
    {
        public Drake(Team team, Arena arena, int NbUnit)
            : base(team, arena, 10.0, 1.5, 18.0, 15.0, 7, 0, 
                   "Chaos", true, true, NbUnit)
        {
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            SearchTarget();

            if (!IsDead() && DumpCantFly == false)
            {
                if (InRange())
                {
                    SetMouvementZero();
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
                    Mouvement = Vector.Move(Speed, Direction);
                    Vector NewLocation = Vector.Add(Mouvement, Location);

                    if (!Arena.Collision(NewLocation)) Location = NewLocation;
                }

                SpecialEffect();
            }
            else if (!IsDead())
            {
                DumpFlyAway();
            }
        }
    }
}
