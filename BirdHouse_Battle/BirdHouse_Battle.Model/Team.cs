using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    class Team
    {
        //Couleur de Team à rajouter
        string _name;
        int _teamNumber;
        int _troupsCount;
        int _typeCount;
        Dictionary<string, Unit> _teams;

        internal Team(string Name)
        {
            _name = Name;
            _teamNumber = TeamNumber;
            _troupsCount = TroupsCount;
            _typeCount = TypeCount;
            _teams = new Dictionary<string, Unit>();
        }

        public string Name
        {
            get { return _name; }
        }
        public int TeamNumber
        {
            get { return _teamNumber; }
        }

        public int TroupsCount
        {
            get { return _troupsCount; }
        }

        public int TypeCount
        {
            get { return _typeCount; }
        }



        void AddFighter()
        {
            throw new NotImplementedException();
        }

        void RemoveFighter()
        {
            throw new NotImplementedException();
        }

    }
}
