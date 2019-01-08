namespace BirdHouse_Battle.Model
{
    public class Paladin : Unit
    {
        double _pastedLife;
        int _coldDown;

        public Paladin(Team team, Arena arena, int nameUnit)
            : base(team, arena, 18.0, 1.2, 15.0, 12.5, 5, 4, 
                   "Order", false, false, false, nameUnit, "paladin")
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
            // Observation

            SearchTargetNotFlying();

            bool canMove = !IsDead() && DumpCantFly == false;

            double currentLife = Life;

            bool coldDownDown = PastedLife - currentLife > 5 && ColdDown == 0;

            //Affectations terrain

            double speed = 0;
            double range = 0;

            if (!DumpCantFly) FieldEffects(Location, Target.Location, Speed, Range, out speed, out range);

            // Action

            if (canMove)
            {
                if (coldDownDown)
                {
                    BoostArmor();
                    _coldDown = 3;
                }
                if (InRange(range))
                {
                    SetMouvementZero();
                    Arena.GiveDamage(Target, Strength);
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