using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    class Paladin : Unit
    {
        public Paladin(Team team, Arena arena)
        {
            _team = team;
            _arena = arena;
            _isDead = false;
            _inRange = false;
            _life = 5.0;
            _speed = 1.0;
            _range = 2.0;
            _unitPrice = 12.5;
            _strength = 5;
            _armor = 4;
            _disposition = "Order";
        }
    }
}
