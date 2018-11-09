using System;
using System.Collections.Generic;
using System.Text;
using BirdHouse_Battle.Model;
using NUnit.Framework;

namespace BirdHouse_Battle.Model
{
    public class Archer : Unit
    {

        int _callDown;

        public Archer(Team team, Arena arena)
            : base(team, arena, 12.0, 1.8, 135.0, 10.0, 4, 1, "Order", false, true)
        {
            _callDown = 0;
        }

        public int CallDown
        {
            get { return _callDown; }
        }

        public void BowAttack()
        {
            Vector End = Target.Location.Add(Target.Mouvement.Multiply(3));
            End.Limit(-Arena.Height, Arena.Height);
            Arena.InitArrow(Location, End);
            _callDown = 3;
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
                    if (_callDown == 0)
                    {
                        BowAttack();
                    }
                    else
                    {
                        _callDown--;
                    }
                }
                else
                {
                    Mouvement = Direction.Move(Speed);
                    Vector NewLocation = Location.Add(Mouvement);

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