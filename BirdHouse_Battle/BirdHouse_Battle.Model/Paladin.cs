using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Paladin : Unit
    {
        double _pastedLife;
        int _coldDown;

        public Paladin(Team team, Arena arena, int NbUnit)
            : base(team, arena, 18.0, 1.2, 15.0, 12.5, 5, 4, 
                   "Order", false, false, NbUnit)
        {
        }

        public double PastedLife
        {
            get { return _pastedLife; }
        }

        public int ColdDown
        {
            get { return _coldDown; }
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            SearchTargetNotFlying();
            double currentLife = Life;

            if (!IsDead() && DumpCantFly == false)
            {
                if (PastedLife - currentLife > 5 && ColdDown == 0)
                {
                    BoostArmor();
                    _coldDown = 3;
                }
                if (InRange())
                {
                    SetMouvementZero();
                    Arena.GiveDamage(Target, Strength);
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
            if (ColdDown > 0)
            {
                _coldDown--;
                if (ColdDown == 2)
                {
                    UnboostArmor();
                }
            }
        }
    }
}