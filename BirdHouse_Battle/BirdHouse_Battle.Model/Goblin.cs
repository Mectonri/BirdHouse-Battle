using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Goblin : Unit
    {
        public Goblin(Team team, Arena arena, int nameUnit)
            : base(team, arena, 8.0, 2.5, 4.0, 3.0, 15, 0, 
                   "Chaos", false, false, false, nameUnit)
        {
        }

        public bool GoblinsAlone()
        {
            return Team.IsGobelinsAlone();
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            // Observation

            bool globalAttack = TeamPlay && Team.GoblinsAttack > 2 * Team.GoblinsTarget.Life;

            Unit finalTarget = null;

            SearchTargetNotFlying();

            if (globalAttack)
            {
                finalTarget = Team.GoblinsTarget;
                NewDirection(finalTarget);
            }
            else
            {
                finalTarget = Target;
            }

            bool canMove = !IsDead() && DumpCantFly == false;

            bool runAway = finalTarget.Life > Life && Life < 5 && !GoblinsAlone();

            // Affectations terrain

            double speed = 0;
            double range = 0;

            if (!DumpCantFly) FieldEffects(Location, finalTarget.Location, Speed, Range, out speed, out range);

            // Action

            if (canMove)
            {
                if (runAway && !TeamPlay)
                {
                    Mouvement = Vector.MoveAndRunAway(Speed, Direction);
                    Vector NewLocation = Vector.Add(Mouvement, Location);
                    NewLocation.Limit(-Arena.Height, Arena.Height);

                    if (!Arena.Collision(NewLocation)) Location = NewLocation;
                }
                else
                {
                    if (InRange(range))
                    {
                        SetMouvementZero();
                        Arena.GiveDamage(finalTarget, Strength);
                    }
                    else
                    {
                        Mouvement = Vector.Move(speed, Direction);
                        Vector NewLocation = Vector.Add(Mouvement, Location);

                        if (!Arena.Collision(NewLocation)) Location = NewLocation;
                    }
                }

                SpecialEffect();
            }
            else if (!IsDead())
            {
                SetMouvementZero();
                DumpFlyAway();
            }
        }
    }
}
