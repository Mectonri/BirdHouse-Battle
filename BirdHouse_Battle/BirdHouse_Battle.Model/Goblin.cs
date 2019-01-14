using Newtonsoft.Json.Linq;

namespace BirdHouse_Battle.Model
{
    public class Goblin : Unit
    {
        public Goblin(Arena arena, Team team,  int nameUnit)
            : base( arena, team, 8.0, 2.5, 4.0, 3.0, 15, 0, 
                   "Chaos", false, false, false, nameUnit, "goblin")
        {
        }

        public Goblin(Arena arena, Team team, JToken jToken)
            : base(arena, team, jToken)
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

            bool runAway = finalTarget != null && finalTarget.Life > Life && Life < 5 && !GoblinsAlone();

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

                    if (!Arena.Collision(this, NewLocation, speed)) Location = NewLocation;
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

                        if (!Arena.Collision(this, NewLocation, speed)) Location = NewLocation;
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
