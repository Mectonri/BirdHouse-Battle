using System;
using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    public class Team
    {


        //Team number is associated with a color, so creating a team take the color as argument.
        //Couleur de Team à rajouter
        string _name;
        int _teamNumber;
        int _unitCount;
        int _typeCount;
        double _gold;
        int _limitNbUnit;
        Arena _arena; // Rajputer le contexte des equipes qui est l'arene
        internal Dictionary<Guid, Unit> _units;

        internal Team(Arena Context, string Name, int LimitNbUntit)
        {
            _name = Name;
            _teamNumber = TeamNumber;
            _unitCount = UnitCount;
            _typeCount = TypeCount;
            _arena = Context;
            _limitNbUnit = LimitNbUntit;
            _units = new Dictionary<Guid, Unit>();
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

        public Arena Context
        {
            get { return _arena; }
        }


        void AddArcher(int UnitCount)
        {
            Archer archer = new Archer(this, _arena);
            _units.Add(archer.Name, archer);
        }

        void AddGobelin(int UnitCount)
        {
            Gobelin gobelin = new Gobelin(this, _arena);
            _units.Add(gobelin.Name, gobelin);
        }

        void AddPaladin(int UnitCount)
        {
            Paladin paladin = new Paladin(this, _arena);
            _units.Add(paladin.Name, paladin);
        }


        void RemoveUnit(Unit unit)
        {

            _units.Remove(unit.Name);
        }

        double GoldCalculation(Unit unit, double Gold)
        {
            return _gold = Gold - unit.UnitPrice * UnitCount;
        }

    }
}
