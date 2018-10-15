using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    class Gobelin : Unit
    {
        public Gobelin(Team team, Arena arena)
        {
            _team = team;
            _arena = arena;
            _isDead = false;
            _inRange = false;
            _life = 1.0;
            _speed = 4.0;
            _range = 1.0;
            _unitPrice = 5.0;
            _strength = 3;
            _armor = 1;
            _disposition = "Chaos";
        }
    }
}
