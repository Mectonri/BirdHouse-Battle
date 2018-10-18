using System;
using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    public abstract class Unit
    {
        Team _team;
        Arena _arena;
        Unit _target;
        readonly Guid _name = Guid.NewGuid();
        Vector _location;
        Vector _direction;

        protected Unit(Team team, Arena arena)
        {
            _team = team;
            _arena = arena;
        }

        public Team Team { get { return _team; } }

        public Arena Arena { get { return _arena; } }

        public Guid Name { get { return _name; } }

        public Unit Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public Vector Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Vector Direction
        {
            get { return _direction; }
        }

        /// <summary>
        /// Search the nearest enemy.
        /// </summary>
        /// <returns></returns>
        //public void SearchTarget()
        //{
        //    Target = Arena.NearestEnemy(this);
        //    NewDirection();
        //}

        /// <summary>
        /// Get new Direction.
        /// </summary>
        /// <returns></returns>
        public void NewDirection()
        {
            _direction = Location.Soustract(Target.Location);
        }

        /// <summary>
        /// When a Unit Die.
        /// </summary>
        public void DieNullContext()
        {
            _team = null;
        }
    }
}