﻿using Newtonsoft.Json.Linq;
using System;

namespace BirdHouse_Battle.Model
{
    public class Drake : Unit
    {
        public Drake(Arena arena, Team team,  int nameUnit)
            : base( arena, team, 10.0, 1.5, 18.0, 15.0, 7, 0, 
                   "Chaos", true, true, false, nameUnit, "drake")
        {

        }

        public Drake(Arena arena,  Team team, JToken jToken)
            : base(arena, team, jToken)
        {

        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            // Observation

            SearchTarget();

            bool canMove = !IsDead() && DumpCantFly == false;

            bool willBurn = new Random().NextDouble() < 0.5;

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
                    if (willBurn)
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
