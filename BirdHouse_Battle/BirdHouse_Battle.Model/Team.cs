using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    class Team
    {


        //Team number is associated with a color, so creating a team take the color as argument.
        //Couleur de Team à rajouter
        string _name;
        int _teamNumber;
        int _unitCount;
        int _typeCount;
        int _gold;
        int _limitNbUnit;
        //Arena _context; // Rajputer le contexte des equipes qui est l'arene
        Dictionary<string, Unit> _units;

        internal Team(string Name, int LimitNbUntit)
        {
            _name = Name;
            _teamNumber = TeamNumber;
            _unitCount = UnitCount;
            _typeCount = TypeCount;
         //_ctx = Context;
            _limitNbUnit = LimitNbUntit;
            _units = new Dictionary<string, Unit>();
        }

        public string Name
        {
            get { return _name; }
        }
        public int TeamNumber
        {
            get { return _teamNumber; }
        }

        public int UnitCount
        {
            get { return _unitCount; }
        }
       

        public int TypeCount
        {
            get { return _typeCount; }
        }

        //public Arena Context
        //{
        //    get { return _context; }
        //}
       

        //void AddUnit( Team c, Archer, UnitCount)
        //{
        //    Archer archer = new Archer(this, UnitCount);
        //    _teams.Add(archer);
        //}

        //void AddUnit(Team c, Goblelin, UnitCount)
        //{
        //    Gobelin goblin = new Gobelin(this, UnitCount);
        //    _teams.Add(goblin);
        //}

        //void AddUnit( Team c, Paladin, UnitCount)
        //{
        //    Paladin paladin = New Paladin(this, UnitCount);
        //    _teams.Add(paladin);
        //}


        //void RemoveUnit(Unit name)
        //{

        //   _units.Remove(name);
        //}


        //void GoldCalculation( int Gold)
        //{
        //    return _gold = Gold - UnitPrice * UnitCount;
        //}

    }
}
