using System;
using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    public class Unit
    {
        internal Team _team;
        internal Arena _arena;
        Unit _target;
        internal double _life;
        internal double _speed;
        internal double _range;
        internal double _unitPrice;
        internal int _strength;
        internal int _armor;
        readonly Guid _name = Guid.NewGuid();
        internal string _disposition;
        Vector _location;
        Vector _direction;
        internal bool _isDead;
        internal bool _inRange;

        public Team Team { get { return _team; } }

        public Arena Arena { get { return _arena; } }

        public Unit Target
        {
            get { return _target; }
            set { _target = value; }
        }


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

        public Guid Name { get { return _name; } }

        public string Disposition { get { return _disposition; } }

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
                return Math.Max(Location.Magnitude, Target.Location.Magnitude) - Math.Min(Location.Magnitude, Target.Location.Magnitude) < Range;
            }
            set { _inRange = value; }
        }

        /// <summary>
        /// Loose life point(s).
        /// </summary>
        /// <param name="damages"></param>
        /// <returns></returns>
        public double TakeDamages(double damages)
        {
            _life = _life - Math.Max(damages - _armor, 0);
            return _life;
        }

        /// <summary>
        /// Search the nearest enemy.
        /// </summary>
        /// <returns></returns>
        public Unit SearchTarget()
        {
            Unit newTarget = _arena.NearestEnemy(this);
            return newTarget;
        }

        /// <summary>
        /// Get new Direction.
        /// </summary>
        /// <returns></returns>
        public Vector NewDirection()
        {
            Vector newDirection = _location.Soustract(Target.Location);
            return newDirection;
        }

        /// <summary>
        /// When a Unit Die.
        /// </summary>
        public void DieNullContext()
        {
            _team = null;
        }

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}