using Newtonsoft.Json.Linq;
using System;

namespace BirdHouse_Battle.Model
{
    public abstract class Unit
    {
        
        Team _team;
        Arena _arena;

        public int _nameUnit;

        Vector _location;
        Vector _direction;
        Vector _mouvement;

        public double _life;
        public double _speed;
        public double _range;
        public double _unitPrice;
        public int _strength;
        public int _armor;
        public int _burn;

        public string _disposition;
        public string _troop;

        public bool _fly;
        public bool _distance;
        public bool _distanceOnly;
        public bool _teamPlay;
        public bool _dumpCantFly;
        public bool _dumpCantWalk;

        protected Unit(Arena arena, Team team,  double life,
                       double speed, double range, double unitPrice,
                       int strength, int armor, string disposition, 
                       bool fly, bool distance, bool distanceOnly, int name, string troop)
        {
            _team = team;
            _arena = arena;

            _life = life;
            _speed = speed;
            _range = range;
            _unitPrice = unitPrice;
            _strength = strength;
            _armor = armor;
            _disposition = disposition;
            _fly = fly;
            _distance = distance;
            _distanceOnly = distanceOnly;
            _nameUnit = name;
            _troop = troop;

            _dumpCantFly = false;
            _teamPlay = false;
            _burn = 0;

        }


        #region De & Serialization 

        /// <summary>
        /// Deserialize a unit using a JToken
        /// </summary>
        /// <param name="team"></param>
        /// <param name="jToken"></param>
        public Unit(Arena arena, Team team, JToken jToken)
        {
            _arena = arena;
            _team = team;

            _life = jToken["Life"].Value<double>();
            _speed = jToken["Speed"].Value<double>();
            _range = jToken["Range"].Value<double>();
            _unitPrice = jToken["UnitPrice"].Value<double>();
            _strength = jToken["Strength"].Value<int>();
            _armor = jToken["Armor"].Value<int>();
            _disposition = jToken["Disposition"].Value<string>();
            _fly = jToken["Fly"].Value<bool>();
            _distance = jToken["Distance"].Value<bool>();
            _distanceOnly= jToken["DistanceOnly"].Value<bool>();
            _nameUnit = jToken["Name"].Value<int>();
            _troop = jToken["Troop"].Value<string>();

            _dumpCantFly = false;
            _teamPlay = false;
            _burn = 0;
        }

        /// <summary>
        /// Serialize a Unit
        /// </summary>
        /// <returns></returns>
        public JToken Serialize()
        {
            return new JObject(
                new JProperty("Life", _life),
                new JProperty("Speed", _speed),
                new JProperty("Range", _range),
                new JProperty("UnitPrice", _unitPrice),
                new JProperty("Strength", _strength), 
                new JProperty("Armor", _armor),
                new JProperty("Disposition", _disposition),
                new JProperty("Fly", _fly),
                new JProperty("Distance", _distance),
                new JProperty("DistanceOnly", _distanceOnly),
                new JProperty("Name", _nameUnit),
                new JProperty("Troop", _troop)
                );
        }

#endregion

        #region Getter & Setter

        public Team Team { get { return _team; } }

        public Arena Arena { get { return _arena; } }

        public int Name { get { return _nameUnit; } }

        public string Troop => _troop;

        public Unit Target { get; set; }

        public Vector Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Vector Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        public Vector Mouvement
        {
            get { return _mouvement; }
            set { _mouvement = value; }
        }

        public double Life { get { return _life; } }
        
        public double Speed { get { return _speed; } }

        public double Range { get { return _range; } }

        public double UnitPrice { get { return _unitPrice; } }

        public int Strength { get { return _strength; } }

        public int Armor { get { return _armor; } }

        public int Burn { get { return _burn; } }

        public string Disposition { get { return _disposition; } }

        public bool IsDead()
        {
            return _life <= 0;
        }

        public bool Fly { get { return _fly; } }

        public bool Distance { get { return _distance; } }

        public bool DistanceOnly { get { return _distanceOnly; } }

        public bool TeamPlay { get { return _teamPlay; } }

        public bool DumpCantFly { get { return _dumpCantFly; } }

        public bool DumpCantWalk { get { return _dumpCantWalk; } }

        #endregion

        /// <summary>
        /// Search the nearest enemy.
        /// </summary>
        public void SearchTarget()
        {
            Target = Arena.NearestEnemy(this);
            if (Target == null) throw new ArgumentNullException("A location couldn't be Null");
            NewDirection();
        }

        public void SearchTargetNotFlying()
        {
            Target = Arena.NearestEnemyNotFlying(this);
            if (Target == null)
            {
                _dumpCantFly = true;
            }
            else
            {
                NewDirection();
            }
        }
        
        public void SearchTargetFlying()
        {
            Target = Arena.NearestEnemyFlying(this);
            if (Target == null)
            {
                _dumpCantWalk = true;
            }
            else
            {
                NewDirection();
            }
        }

        public void DumpFlyAway()
        {
            _dumpCantFly = false;
        }

        public void DumpWalkAway()
        {
            _dumpCantWalk = false;
        }

        /// <summary>
        /// Get new Direction.
        /// </summary>
        /// <returns></returns>
        public void NewDirection()
        {
            _direction = Vector.Soustract(Location, Target.Location);
            _mouvement = Vector.Move(Speed, Direction);
        }

        public void NewDirection(Unit target)
        {
            _direction = Vector.Soustract(Location, target.Location);
            _mouvement = Vector.Move(Speed, Direction);
        }

        /// <summary>
        /// When a Unit Die.
        /// </summary>
        public void DieNullContext()
        {
            _team = null;
        }

        /// <summary>
        /// A corriger
        /// </summary>
        public bool InRange(double range)
        {
            Vector newV = Vector.Soustract(Target.Location, Location);
            return newV.Magnitude <= range;
        }

        /// <summary>
        /// Loose life point(s).
        /// </summary>
        /// <param name="damages"></param>
        /// <returns></returns>
        public virtual void TakeDamages(double damages)
        {
            if (damages < 0) throw new ArgumentException("Couldn't take negative damages.");
            _life = _life - Math.Max(damages - Armor, 0);
        }

        public void Fired(int fire)
        {
            if (fire <= 0) throw new ArgumentException("fire can't be negative or null");
            _burn = _burn + fire;
        }

        public void Burning()
        {
            _life = _life - 3;
            _burn--;
        }

        public void SpecialEffect()
        {
            if (Burn > 0) Burning();
        }

        public void SetMouvementZero()
        {
            _mouvement.SetZero();
        }

        public void BoostArmor()
        {
            _armor += 100;
        }

        public void UnboostArmor()
        {
            _armor -= 100;
        }

        public void TeamPlayOn()
        {
            _teamPlay = true;
        }

        public void TeamPlayOff()
        {
            _teamPlay = false;
        }

        public void FieldEffects(Vector location, Vector targetLocation, double speed, double range, out double speedF, out double rangeF)
        {
            speedF = speed;
            rangeF = range;
            if (Arena.Field.TryFindTile(int.Parse($"{Math.Round(location.X)}"), int.Parse($"{Math.Round(location.Y)}"), out Tile tile))
            {
                if (tile.Obstacle == "River")
                {
                    speedF -= 0.4;
                }
                else if (tile.Height != 0)
                {
                    switch (tile.Height)
                    {
                        case 1: rangeF += 1; break;
                        case 2: rangeF += 2; break;
                        case 3: rangeF += 3; break;
                        case 4: rangeF += 4; break;
                        case 5: rangeF += 5; break;
                        case 6: rangeF += 6; break;
                        case 7: rangeF += 7; break;
                        case 8: rangeF += 8; break;
                        case 9: rangeF += 9; break;
                        case 10: rangeF += 10; break;
                        case 11: rangeF += 11; break;
                        case 12: rangeF += 12; break;
                        case 13: rangeF += 13; break;
                        case 14: rangeF += 14; break;
                    }
                }
            }
            if (Arena.Field.TryFindTile(int.Parse($"{Math.Round(targetLocation.X)}"), int.Parse($"{Math.Round(targetLocation.Y)}"), out Tile targetTile))
            {
                if (targetTile.Height != 0)
                {
                    switch (targetTile.Height)
                    {
                        case 1: rangeF -= 1; break;
                        case 2: rangeF -= 2; break;
                        case 3: rangeF -= 3; break;
                        case 4: rangeF -= 4; break;
                        case 5: rangeF -= 5; break;
                        case 6: rangeF -= 6; break;
                        case 7: rangeF -= 7; break;
                        case 8: rangeF -= 8; break;
                        case 9: rangeF -= 9; break;
                        case 10: rangeF -= 10; break;
                        case 11: rangeF -= 11; break;
                        case 12: rangeF -= 12; break;
                        case 13: rangeF -= 13; break;
                        case 14: rangeF -= 14; break;
                    }
                }
            }
        }

        public bool MissObstacle(Vector vector, double speed, bool isOriginalXGood, bool isOriginalYGood)
        {
            bool DoesCollide = false;

            bool isXSmallest = Vector.Orientation(vector, Location);

            Vector NewLocation;

            if (isXSmallest)
            {
                if (isOriginalXGood)
                {
                    _location.X += 1;

                    NewLocation = Vector.Add(Vector.Move(speed, Direction), Location);

                    DoesCollide = Arena.Collision(this, NewLocation, speed, true, isOriginalYGood);

                    if (!DoesCollide) Location = NewLocation;
                }
                else
                {
                    _location.X -= 1;

                    NewLocation = Vector.Add(Vector.Move(speed, Direction), Location);

                    DoesCollide = Arena.Collision(this, NewLocation, speed, true, isOriginalYGood);

                    if (!DoesCollide) Location = NewLocation;
                }
            }
            else
            {
                if (isOriginalYGood)
                {
                    _location.X += 1;

                    NewLocation = Vector.Add(Vector.Move(speed, Direction), Location);

                    DoesCollide = Arena.Collision(this, NewLocation, speed, isOriginalXGood, true);

                    if (!DoesCollide) Location = NewLocation;
                }
                else
                {
                    _location.X -= 1;

                    NewLocation = Vector.Add(Vector.Move(speed, Direction), Location);

                    DoesCollide = Arena.Collision(this, NewLocation, speed, isOriginalXGood, true);

                    if (!DoesCollide) Location = NewLocation;
                }
            }

            return DoesCollide;
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public virtual void Update() { }
    }
}