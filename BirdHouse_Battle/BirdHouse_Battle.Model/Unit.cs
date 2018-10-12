using System;

namespace BirdHouse_Battle.Model
{
    public class Unit
    {
        Team _team;
        readonly Arena _arena;
        double _life;
        readonly double _speed;
        readonly double _range;
        readonly double _unitPrice;
        readonly int _strength;
        readonly int _armor;
        readonly char _disposition;
        Vector _location;
        Vector _direction;
        Vector _targetLocation;
        bool _isDead;
        bool _inRange;

        public Unit(Team team, Arena arena, double life,
                    double speed, double range, double unitPrice,
                    int strength, int armor, char disposition,
                    Vector location, bool isDead, bool inRange)
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
            _location = location;
            _isDead = isDead;
            _inRange = inRange;
        }

        public Team Team { get { return _team; } }

        public Arena Arena { get { return _arena; } }

        public double Life
        {
            get { return _life; }
            set { _life = value; }
        }

        public double Speed { get { return _speed; } }

        public double Range { get { return _range; } }

        public double UnitPrice { get { return _unitPrice; } }

        public int Strength { get { return _strength; } }

        public int Armor { get { return _armor; } }

        public char Disposition { get { return _disposition; } }

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

        public Vector TargetLocation
        {
            get { return _targetLocation; }
            set { _targetLocation = value; }
        }

        public bool IsDead
        {
            get
            {
                return Life > 0;
            }
            set { _isDead = value; }
        }

        public bool InRange
        {
            get
            {
                return Math.Max(Location.Magnitude, TargetLocation.Magnitude) - Math.Min(Location.Magnitude, TargetLocation.Magnitude) < Range;
            }
            set { _inRange = value; }
        }

        public double TakeDamages(double damages)
        {
            _life = _life - Math.Max(damages - _armor, 0);
            return _life;
        }

        public Vector SearchTarget()
        {
            Vector newTarget = _arena.NearestEnemy(this);
            return newTarget;
        }

        public Vector NewDirection()
        {
            throw new ArgumentNullException();
        }

        public void DieNullContext()
        {
            _team = null;
        }

        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}