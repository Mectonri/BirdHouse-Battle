using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Archer : Unit
    {
        double _life;
        double _speed;
        double _range;
        double _unitPrice;
        int _strength;
        int _armor;
        string _disposition;
        bool _isDead;
        bool _inRange;

        public Archer(Team team, Arena arena)
            : base(team, arena)
        {
            _life = 2.0;
            _speed = 2.0;
            _range = 5.0;
            _unitPrice = 10.0;
            _strength = 2;
            _armor = 1;
            _isDead = false;
            _disposition = "Order";
        }

        public double Life { get { return _life; } }

        public double Speed { get { return _speed; } }

        public double Range { get { return _range; } }

        public double UnitPrice { get { return _unitPrice; } }

        public int Strength { get { return _strength; } }

        public int Armor { get { return _armor; } }

        public string Disposition { get { return _disposition; } }

        public bool IsDead
        {
            get
            {
                return _life <= 0;
            }
        }

        /// <summary>
        /// A corriger
        /// </summary>
        public bool InRange
        {
            get
            {
                Vector newV = Location.Soustract(Target.Location);
                return newV.Magnitude <= Range;
            }
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

        /// <summary>
        /// Game Loop in Unit
        /// </summary>
        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}