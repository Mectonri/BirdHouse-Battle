﻿using System;
using System.Collections.Generic;
using System.Text;
using BirdHouse_Battle.Model;
using NUnit.Framework;

namespace BirdHouse_Battle.Model
{
    public class Archer : Unit
    {
        public Archer(Team team, Arena arena)
            : base(team, arena, 12.0, 1.8, 35.0, 10.0, 4, 1, "Order", false, true)
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