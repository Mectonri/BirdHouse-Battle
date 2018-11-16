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

        public Archer(Team team, Arena arena, int NbUnit)
            : base(team, arena, 12.0, 1.8, 135.0, 10.0, 4, 1, 
                   "Order", false, true, NbUnit)
        {
            _callDown = 0;
        }

        public int CallDown
        {
            get { return _callDown; }
        }

        public void BowAttack(int NbFram)
        {
            Vector End = Vector.Add(Vector.Multiply(NbFram, Target.Mouvement), Target.Location);
            End.Limit(-Arena.Height, Arena.Height);

            if (Vector.Soustract(Location, End).Magnitude < (Range / 2))
            {
                if (Vector.Soustract(Location, End).Magnitude < (Range / 4))
                {
                    End = Vector.Add(Vector.Multiply(0, Target.Mouvement), Target.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitArrow(Location, End, (NbFram / 4));
                    _callDown = (NbFram / 4);
                }
                else
                {
                    End = Vector.Add(Vector.Multiply((NbFram / 2), Target.Mouvement), Target.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitArrow(Location, End, (NbFram / 2));
                    _callDown = (NbFram / 2);
                }
            }
            else
            {
                Arena.InitArrow(Location, End, NbFram);
                _callDown = NbFram;
            }
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
                    if (_callDown == 0)
                    {
                        BowAttack(24);
                    }
                    else
                    {
                        _callDown--;
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