using System;
using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    public class Team
    {
        /// <summary>
        /// Archer is a unit type same as Paladin and Goblin 
        /// </summary>
        
        //Team number is associated with a color, so creating a team take the color as argument.
        //Couleur de Team à rajouter
        readonly string _name;
        readonly int _teamNumber; //number is used to assign a color to a team
        int _unitCount; // Le nombre total d'unites  dans une equipes

        int _aToAdd; // le nombre d' archer a ajouter a une equipe
        int _pToAdd;
        int _gToAdd;
        

        int _aCount; //le nombre total d' Archer dans l'equipe
        int _pCount; // le nombre total de paladin dans une equipe
        int _gCount; // le nombre total de Gobelin dans une equipe.

        bool _isWiped;
        
        double _goldAmount;
        readonly int _limitNbUnit; // unit limit by team
        Arena _arena; // Rajouter le contexte des equipes qui est l'arene
        internal Dictionary<Guid, Unit> _deadUnits; 
        internal Dictionary<Guid, Unit> _units;

        public Team ( Arena Context, string Name, int LimitNbUntit )
        {
            _isWiped = false;
            _aCount = 0;
            _gCount = 0;
            _pCount = 0;

            _name = Name;
            
            _arena = Context;
            _teamNumber = _arena.TeamCount;
            _limitNbUnit = LimitNbUntit;
            _units = new Dictionary<Guid, Unit>();
            _deadUnits = new Dictionary<Guid, Unit>();
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
            get { return _unitCount = _aCount + _gCount + _pCount; }
        }


        public Arena Context
        {
            get { return _arena; }
        }

        public int Acount
        {
            get { return _aCount; }
        }

        public int Gcount
        {
            get { return _gCount; }
        }

        public int Pcount
        {
            get { return _pCount; }
        }

        public double GoldAmount
        {
            get { return _goldAmount; }
            set
            {
                if ( value < 0.0) throw new ArgumentException("You don't have enought gold", nameof(value));
                _goldAmount = value;

            }
        }

        public int AToAdd
        {
            get { return _aToAdd; }
            set
            {
                if (_aToAdd < 0) throw new ArgumentException("Le nombre de troupe a rajouter doit etre positif", nameof(_aToAdd));
                _aToAdd = value;
            }
        }

        public int GToAdd
        {
            get { return _gToAdd; }
            set
            {
                if (_gToAdd < 0) throw new ArgumentException("Le nombre de troupe a rajouter doit etre positif", nameof(_gToAdd));
                _gToAdd = value;
            }
        }

        public int PToAdd
        {
            get { return _pToAdd; }
            set
            {
                if (_pToAdd < 0) throw new ArgumentException("The number of Troups must be positive", nameof(_pToAdd));
                _pToAdd = value;
            }
        }

        // Add a Unit type to a team

        public void AddArcher(int AToAdd)
        {

            for (int i = 0; i < AToAdd; i++)
            {
                Archer archer = new Archer(this, _arena);
                _aCount++;
                _units.Add(archer.Name, archer);
            }
        }

        public void AddGobelin(int GToAdd)
        {
            for (int i = 0; i < GToAdd; i++)
            {
                Gobelin gobelin = new Gobelin(this, _arena);
                _gCount++;
                _units.Add(gobelin.Name, gobelin);
            }
        }

        public void AddPaladin(int PToAdd)
        {
            for (int i = 0; i < PToAdd; i++)
            {
                Paladin paladin = new Paladin(this, _arena);
                _pCount++;
                _units.Add(paladin.Name, paladin);
            }
        }

        public Unit[] Find()
        {
            Unit[] Tableau = new Unit[_units.Count];
            int z = 0;
            foreach (KeyValuePair<Guid, Unit> i in _units)
            {
                Tableau[z] = i.Value;
                z++;
            }
            return Tableau;
        }

        /// <summary>
        /// Find Unit by it name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Unit FindUnitByName(Guid name)
        {
            if (!_units.TryGetValue(name, out Unit u)) throw new ArgumentException("There is no Unit with this name", nameof(name));
            return u;
        }


        /// <summary>
        ///Removes a unit from a team and decrement the UnitCount
        /// </summary>
        /// <param name="unit"></param>
        public void RemoveUnit(Unit u)
        {

            if (_units.TryGetValue(u.Name, out u))
            {
                u.DieNullContext();
                _units.Remove(u.Name);
                string s = u.GetType().ToString();
                if (s == "BirdHouse_Battle.Model.Archer")
                {
                    _aCount--;
                }
                else if (s == "BirdHouse_Battle.Model.Gobelin")
                {
                    _gCount--;
                }
                else { _pCount--; }
            }
        }

        /// <summary>
        /// Calculate the amount of gold left. Can only be superieo or equals to 0
        /// </summary>
        /// <param name="Gold"></param>
        /// <returns></returns>
        public double GoldCalculation( double Gold)
        {
            
            double result = 0.0;
            foreach (KeyValuePair<Guid, Unit> kv in _units)
            {
                result = result + kv.Value.UnitPrice;
                if (result < 0.0) throw new ArgumentException("You dont have enought gold ", nameof(Gold));   
            }
            return _goldAmount = Gold - result;
        }

        /// <summary>
        /// True if all units in the team died
        /// </summary>
        public bool IsWiped
        {
            get
            {
                if ( UnitCount == 0 || _units.Count == 0 ) return true;
                return _isWiped;
            }
        }
        
        public void Update()
        {
            //update units and add dead units in deadunit dic 
            foreach (Unit unit in _units.Values)
            {
                unit.Update();
                if (unit.IsDead()) _deadUnits.Add(unit.Name, unit);
            }//remove dead units only if they still exist
            foreach (KeyValuePair<Guid, Unit> i in _deadUnits)
            {
                if( FindUnitByName(i.Key) != null ) RemoveUnit( i.Value );
            }
        }

        public Dictionary<Guid, Unit> Unit
        {
            get { return _units; }
        }
    }
}