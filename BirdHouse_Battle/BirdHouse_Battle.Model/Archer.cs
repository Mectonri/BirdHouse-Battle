using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Archer : Unit
    {
        public Archer(Team team, Arena arena)
        {
            _team = team;
            _arena = arena;
            _isDead = false;
            _inRange = false;
            _life = 2.0;
            _speed = 2.0;
            _range = 5.0;
            _unitPrice = 10.0;
            _strength = 2;
            _armor = 1;
            _disposition = "Order";
        }
    }
}