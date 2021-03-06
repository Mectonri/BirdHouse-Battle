﻿using Newtonsoft.Json.Linq;

namespace BirdHouse_Battle.Model
{
    public class Catapult : Unit
    {
        int _coolDown;

        public Catapult(Arena arena, Team team, int nameUnit)
            : base(arena, team,  30.0, 0.50, 250.0, 40.0, 20, 3,
                "Order", false, false, false, nameUnit, "catapult")
        {
            _coolDown = 0;
        }

        public Catapult(Arena arena, Team team, JToken jToken)
            : base(arena, team, jToken)
        {
            _coolDown = 0;
        }
        public int CoolDown => _coolDown;

        public void BoulderAttack(int NbFram, Unit finalTarget)
        {
            Vector End = Vector.Add(Vector.Multiply(NbFram, finalTarget.Mouvement), finalTarget.Location);
            End.Limit(-Arena.Height, Arena.Height);

            if (Vector.Soustract(Location, End).Magnitude < (Range / 2))
            {
                if (Vector.Soustract(Location, End).Magnitude < (Range / 4))
                {
                    End = Vector.Add(Vector.Multiply(0, finalTarget.Mouvement), finalTarget.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitBoulder(Location, End, (NbFram / 4));
                }
                else
                {
                    End = Vector.Add(Vector.Multiply((NbFram / 2), finalTarget.Mouvement), finalTarget.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitBoulder(Location, End, (NbFram / 2));
                }
            }
            else
            {
                Arena.InitBoulder(Location, End, NbFram);
            }
            _coolDown = NbFram;
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            // Observation

            bool globalAttack = TeamPlay && Team.ArchersAttack > 2 * Team.ArchersTarget.Life;

            SearchTargetNotFlying();

            bool canMove = !IsDead() && !DumpCantFly;

            // Affectations terrain

            double speed = 0;
            double range = 0;

            if (!DumpCantFly) FieldEffects(Location, Target.Location, Speed, Range, out speed, out range);

            // Action

            if (canMove)
            {
                if (InRange(range))
                {
                    SetMouvementZero();
                    if (_coolDown == 0)
                    {
                        BoulderAttack(48, Target);
                    }
                    else
                    {
                        _coolDown--;
                    }
                }
                else
                {
                    Mouvement = Vector.Move(speed, Direction);
                    Vector NewLocation = Vector.Add(Mouvement, Location);

                    if (!Arena.Collision(this, NewLocation, speed)) Location = NewLocation;
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