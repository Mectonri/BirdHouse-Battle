using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Gobelin : Unit
    {
        public Gobelin(Team team, Arena arena, int NbUnit)
            : base(team, arena, 8.0, 2.5, 4.0, 3.0, 15, 0, 
                   "Chaos", false, false, NbUnit)
        {
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            SearchTargetNotFlying();

            if (!IsDead() && DumpCantFly == false)
            {
                if (Target.Life > Life && Life < 5)
                {
                    Mouvement = Vector.MoveAndRunAway(Speed, Direction);
                    Vector NewLocation = Vector.Add(Mouvement, Location);
                    NewLocation.Limit(-Arena.Height, Arena.Height);

                    if (!Arena.Collision(NewLocation)) Location = NewLocation;
                }
                else
                {
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
