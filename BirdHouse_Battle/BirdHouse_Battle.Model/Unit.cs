﻿using System;
using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    public abstract class Unit
    {
        Team _team;
        Arena _arena;
        Unit _target;
        readonly Guid _name;
        Vector _location;
        Vector _direction;
        Vector _mouvement;

        double _life;
        double _speed;
        double _range;
        double _unitPrice;
        int _strength;
        int _armor;
        int _burn;
        string _disposition;

        bool _fly;
        bool _distance;
        bool _dumpCantFly;

        protected Unit(Team team, Arena arena, double life,
                       double speed, double range, double unitPrice,
                       int strength, int armor, string disposition, bool fly, bool distance)
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
            _name = Guid.NewGuid();
            _burn = 0;
            _fly = fly;
            _distance = distance;
            _dumpCantFly = false;
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

        public bool DumpCantFly { get { return _dumpCantFly; } }

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

        public void DumpFlyAway()
        {
            _dumpCantFly = false;
        }

        /// <summary>
        /// Get new Direction.
        /// </summary>
        /// <returns></returns>
        public void NewDirection()
        {
            _direction = Target.Location.Soustract(Location);
            _mouvement = _direction.Move(Speed);
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
        public bool InRange()
        {
            Vector newV = Location.Soustract(Target.Location);
            return newV.Magnitude <= Range;
        }

        /// <summary>
        /// Loose life point(s).
        /// </summary>
        /// <param name="damages"></param>
        /// <returns></returns>
        public void TakeDamages(double damages)
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

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public virtual void Update() { }
    }
}