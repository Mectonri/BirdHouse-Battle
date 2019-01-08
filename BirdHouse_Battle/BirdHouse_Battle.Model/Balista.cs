namespace BirdHouse_Battle.Model
{
    public class Balista : Unit
    {
        int _callDown;

        public Balista(Team team, Arena arena, int nameUnit)
            : base(team, arena, 25.0, 0.70, 200.0, 30.0, 15, 2,
                "Order", false, false, true, nameUnit , "baliste")
        {
            _callDown = 0;
        }

        public int CallDown
        {
            get { return _callDown; }
        }

        public void BalisticAmmoAttack(int NbFram, Unit finalTarget)
        {
            Vector End = Vector.Add(Vector.Multiply(NbFram, finalTarget.Mouvement), finalTarget.Location);
            End.Limit(-Arena.Height, Arena.Height);

            if (Vector.Soustract(Location, End).Magnitude < (Range / 2))
            {
                if (Vector.Soustract(Location, End).Magnitude < (Range / 4))
                {
                    End = Vector.Add(Vector.Multiply(0, finalTarget.Mouvement), finalTarget.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitBalisticAmmo(Location, End, (NbFram / 4));
                }
                else
                {
                    End = Vector.Add(Vector.Multiply((NbFram / 2), finalTarget.Mouvement), finalTarget.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitBalisticAmmo(Location, End, (NbFram / 2));
                }
            }
            else
            {
                Arena.InitBalisticAmmo(Location, End, NbFram);
            }
            _callDown = NbFram;
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public override void Update()
        {
            // Observation

            bool globalAttack = TeamPlay && Team.ArchersAttack > 2 * Team.ArchersTarget.Life;

            SearchTargetFlying();

            bool canMove = !IsDead() && DumpCantWalk == false;

            // Affectations terrain

            double speed = 0;
            double range = 0;

            if (!DumpCantWalk) FieldEffects(Location, Target.Location, Speed, Range, out speed, out range);

            // Action

            if (canMove)
            {
                if (InRange(range))
                {
                    SetMouvementZero();
                    if (_callDown == 0)
                    {
                        BalisticAmmoAttack(24, Target);
                    }
                    else
                    {
                        _callDown--;
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
                DumpWalkAway();
            }
        }
    }
}