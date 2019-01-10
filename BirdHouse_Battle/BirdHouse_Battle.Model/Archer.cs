using Newtonsoft.Json.Linq;

namespace BirdHouse_Battle.Model
{
    public class Archer : Unit
    {
        int _coolDown;

        public Archer( Arena arena, Team team, int nameUnit)
            : base(arena, team,  12.0, 1.8, 135.0, 10.0, 4, 1, 
                   "Order", false, true, false, nameUnit, "archer")
        {
            _coolDown = 0;
        }

        public Archer(Arena arena, Team team, JToken jToken)
            : base(arena, team, jToken)
        {

            _life = jToken["Life"].Value<double>();
            _speed = jToken["Speed"].Value<double>();
            _range = jToken["Range"].Value<double>();
            _unitPrice = jToken["UnitPrice"].Value<double>();
            _strength = jToken["Strength"].Value<int>();
            _armor = jToken["Armor"].Value<int>();
            _disposition = jToken["Disposition"].Value<string>();
            _fly = jToken["Fly"].Value<bool>();
            _distance = jToken["Distance"].Value<bool>();
            _distanceOnly = jToken["DistanceOnly"].Value<bool>();
            _nameUnit = jToken["Name"].Value<int>();
            _troop = jToken["Troop"].Value<string>();

            _coolDown = 0;
        }

        public int CoolDown
        {
            get { return _coolDown; }
        }

        public void BowAttack(int NbFram, Unit finalTarget)
        {
            Vector End = Vector.Add(Vector.Multiply(NbFram, finalTarget.Mouvement), finalTarget.Location);
            End.Limit(-Arena.Height, Arena.Height);

            if (Vector.Soustract(Location, End).Magnitude < (Range / 2))
            {
                if (Vector.Soustract(Location, End).Magnitude < (Range / 4))
                {
                    End = Vector.Add(Vector.Multiply(0, finalTarget.Mouvement), finalTarget.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitArrow(Location, End, (NbFram / 4));
                }
                else
                {
                    End = Vector.Add(Vector.Multiply((NbFram / 2), finalTarget.Mouvement), finalTarget.Location);
                    End.Limit(-Arena.Height, Arena.Height);

                    Arena.InitArrow(Location, End, (NbFram / 2));
                }
            }
            else
            {
                Arena.InitArrow(Location, End, NbFram);
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

            Unit finalTarget = null;

            SearchTarget();

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

            // Affectations terrain

            double speed = 0;
            double range = 0;

            if (!DumpCantFly) FieldEffects(Location, finalTarget.Location, Speed, Range, out speed, out range);

            // Action

            if (canMove)
            {
                if (InRange(range))
                {
                    SetMouvementZero();
                    if (_coolDown == 0)
                    {
                        BowAttack(24, finalTarget);
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