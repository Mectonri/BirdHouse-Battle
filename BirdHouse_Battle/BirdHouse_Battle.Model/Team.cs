using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BirdHouse_Battle.Model
{
    public class Team
    {
        /// <summary>
        /// Archer is a unit type same as Paladin and Goblin 
        /// </summary>
        
        //Team number is associated with a color, so creating a team take the color as argument
        readonly string _name;
        readonly int _teamNumber; //number is used to assign a color to a team
        int _unitCount; // Le nombre total d'unites  dans une equipes

        int _aToAdd; // le nombre d' archer a ajouter a une equipe
        int _pToAdd;
        int _gToAdd;
        int _dToAdd;
        int _cToAdd;
        int _bToAdd;

        int _aCount; //le nombre total d' Archer dans l'equipe
        int _pCount; // le nombre total de paladin dans une equipe
        int _gCount; // le nombre total de Goblin dans une equipe.
        int _dCount; // le nombre total de Dragons dans une equipe.
        int _cCount; // number of Catapult in a team
        int _bCount;

        bool _isWiped;

        double _goldAmount;
        readonly int _limitNbUnit; // unit limit by team
        Arena _arena; // Rajouter le contexte des equipes qui est l'arene
        internal Dictionary<int, Unit> _deadUnits;
        internal Dictionary<int, Unit> _units;

        Unit _goblinsTarget;
        Unit _archersTarget;
        int _goblinsAttack;
        int _archersAttack;

        /// <summary>
        /// Team Constructor
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="Name"></param>
        /// <param name="LimitNbUntit"></param>
        public Team(Arena Context, string Name, int LimitNbUntit)
        {
            _isWiped = false;

            _name = Name;

            _arena = Context;
            _teamNumber = _arena.TeamCount;
            _limitNbUnit = 125;
            _units = new Dictionary<int, Unit>();
            _deadUnits = new Dictionary<int, Unit>();
        }

        #region relevent to serialization

        /// <summary>
        /// Serialize a team
        /// </summary>
        /// <returns></returns>
        public JToken Serialize()
        {
            return new JObject(
                new JProperty("Units", _units.Select(kv => kv.Value.Serialize())));
        }

        /// <summary>
        /// Create Team from JToken
        /// </summary>
        ///<param name = "jToken" ></ param >
        public Team(Arena Arena, string Name, JToken jToken)
        {
            _isWiped = false;
            _name = Name;
            _arena = Arena;
            _teamNumber = _arena.TeamCount;
            _limitNbUnit = 125;

            _units = new Dictionary<int, Unit>();
            JArray jUnits = (JArray)jToken["units"];

           // IEnumerable<Unit> units = jUnits.Select(u => new Archer(this, u));
            
            //foreach (Unit unit in units)
            //{
            //    _units.Add(unit.Name, unit);
            //}

            _deadUnits = new Dictionary<int, Unit>();
        }

        public IEnumerable<Unit> GetUnits()
        {
            return _units.Values;
        }

        #endregion

        #region Getters & Setters

        public string Name => _name;

        public int TeamNumber => _teamNumber;

        public int UnitCount
        {
            get
            {
                if (_unitCount > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of troops for this team");

                return _unitCount = _aCount + _gCount + _pCount + _dCount + _cCount + _bCount;
            }
        }

        public Arena Context => _arena;

        public int Acount => _aCount;

        public int Gcount => _gCount;

        public int Pcount => _pCount;

        public int Dcount => _dCount;

        public int Ccount => _cCount;

        public int Bcount => _bCount;

        public double GoldAmount
        {
            get => _goldAmount;
            set
            {
                if (value < 0.0) throw new ArgumentException("You don't have enought gold", nameof(value));
                _goldAmount = value;
            }
        }

        public int AToAdd
        {
            get => _aToAdd;
            set
            {
                if (_aToAdd < 0 || _aToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maxin4mun number for this team", nameof(_aToAdd));
                _aToAdd = value;
            }
        }

        public int GToAdd
        {
            get => _gToAdd;
            set
            {
                if (_gToAdd < 0 || _gToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maxin4mun number for this team", nameof(_gToAdd));
                _gToAdd = value;
            }
        }

        public int PToAdd
        {
            get => _pToAdd;
            set
            {
                if (_pToAdd < 0 || _pToAdd > _limitNbUnit) throw new ArgumentException("The number of Troups must be positive", nameof(_pToAdd));
                _pToAdd = value;
            }
        }

        public int DToAdd
        {
            get => _dToAdd;
            set
            {
                if (_dToAdd < 0 || _dToAdd > _limitNbUnit) throw new ArgumentException("The number of Troups must be positive", nameof(_dToAdd));
                _dToAdd = value;
            }
        }

        public int CToAdd
        {
            get => _cToAdd;
            set
            {
                if (_cToAdd < 0 || _cToAdd > _limitNbUnit) throw new ArgumentException("The number of Troups must be positive", nameof(_dToAdd));
                _cToAdd = value;
            }
        }

        public int BToAdd
        {
            get => _bToAdd;
            set
            {
                if (_bToAdd < 0 || _bToAdd > _limitNbUnit) throw new ArgumentException("The number of Troups must be positive", nameof(_dToAdd));
                _bToAdd = value;
            }
        }

        public Dictionary<int, Unit> Units => _units;

        public Unit GoblinsTarget => _goblinsTarget;

        public Unit ArchersTarget => _archersTarget;

        public int GoblinsAttack => _goblinsAttack;

        public int ArchersAttack => _archersAttack;

        #endregion

        #region AddUnit
        /// <summary>
        /// Add an archer to a team
        /// </summary>
        /// <param name="AToAdd"></param>
        public void AddArcher(int AToAdd)
        {
            if (UnitCount >= _limitNbUnit || AToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of unit in this team", nameof(_unitCount));
            for (int i = 0; i < AToAdd; i++)
            {
                Archer archer = new Archer(this, _arena, UnitCount);
                _aCount++;
                _units.Add(archer.Name, archer);
            }
        }

        /// <summary>
        /// Add Drake to a team
        /// </summary>
        /// <param name="DToAdd"></param>
        public void AddDrake(int DToAdd)
        {
            if (UnitCount >= _limitNbUnit || DToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of unit in this team", nameof(_unitCount));
            for (int i = 0; i < DToAdd; i++)
            {
                Drake drake = new Drake(this, _arena, UnitCount);
                _dCount++;
                _units.Add(drake.Name, drake);
            }
        }

        public void AddCatapult(int CToAdd)
        {
            if (UnitCount >= _limitNbUnit || CToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of unit in this team", nameof(_unitCount));
            for (int i = 0; i < CToAdd; i++)
            {
                Catapult catapult = new Catapult(this, _arena, UnitCount);
                _cCount++;
                _units.Add(catapult.Name, catapult);
            }
        }

        public void AddBalista(int BToAdd)
        {
            if (UnitCount >= _limitNbUnit || BToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of unit in this team", nameof(_unitCount));
            for (int i = 0; i < BToAdd; i++)
            {
                Balista balista = new Balista(this, _arena, UnitCount);
                _bCount++;
                _units.Add(balista.Name, balista);
            }
        }

        /// <summary>
        /// Add Goblin to a team
        /// </summary>
        /// <param name="GToAdd"></param>
        public void AddGobelin(int GToAdd)
        {
            if (UnitCount >= _limitNbUnit || GToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun numebr of unit in this team", nameof(_unitCount));
            for (int i = 0; i < GToAdd; i++)
            {
                Goblin goblin = new Goblin(this, _arena, UnitCount);
                _gCount++;
                _units.Add(goblin.Name, goblin);
            }
        }

        /// <summary>
        /// Ass a Palladin to a team
        /// </summary>
        /// <param name="PToAdd"></param>
        public void AddPaladin(int PToAdd)
        {
            if (PToAdd > _limitNbUnit || UnitCount >= _limitNbUnit) throw new ArgumentException("You've exceeded the maximun numebr of unit in this team", nameof(_unitCount));
            for (int i = 0; i < PToAdd; i++)
            {
                Paladin paladin = new Paladin(this, _arena, UnitCount);
                _pCount++;
                _units.Add(paladin.Name, paladin);
            }
        }

        #endregion


        public Unit[] Find()
        {
            Unit[] Tableau = new Unit[_units.Count];
            int z = 0;
            foreach (KeyValuePair<int, Unit> i in _units)
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
        public Unit FindUnitByName(int name)
        {
            _units.TryGetValue(name, out Unit u);
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
                else if (s == "BirdHouse_Battle.Model.Goblin")
                {
                    _gCount--;
                }
                else if (s == "BirdHouse_Battle.Model.Drake")
                {
                    _dCount--;
                }
                else if (s == "BirdHouse_Battle.Model.Catapult")
                {
                    _cCount--;
                }
                else if (s == "BirdHouse_Battle.Model.Balista")
                {
                    _bCount--;
                }
                else { _pCount--; }
            }
        }

        /// <summary>
        /// Calculate the amount of gold left. Can only be superieo or equals to 0
        /// </summary>
        /// <param name="Gold"></param>
        /// <returns></returns>
        public double GoldCalculation(double Gold)
        {
            double result = 0.0;
            foreach (KeyValuePair<int, Unit> kv in _units)
            {
                result = result + kv.Value.UnitPrice;
                if (result < 0.0) throw new ArgumentException("You dont have enought gold ", nameof(Gold));
            }
            return _goldAmount = Gold - result;
        }

        public bool IsGobelinsAlone()
        {
            foreach (KeyValuePair<int, Unit> unit in Units)
            {
                if (unit.Value.GetType().ToString() != "BirdHouse_Battle.Model.Goblin")
                {
                    return false;
                }
            }
            return true;
        }

        public Vector GoblinsLocation()
        {
            Vector GlobalLocation = new Vector(0, 0);
            int Count = 0;

            foreach (KeyValuePair<int, Unit> unit in Units)
            {
                if (unit.Value.GetType().ToString() == "BirdHouse_Battle.Model.Goblin")
                {
                    GlobalLocation.Add(unit.Value.Location);
                    Count++;
                }
            }
            return Vector.Division(GlobalLocation, Count);
        }

        public Vector ArchersLocation()
        {
            Vector GlobalLocation = new Vector(0, 0);
            int Count = 0;

            foreach (KeyValuePair<int, Unit> unit in Units)
            {
                if (unit.Value.GetType().ToString() == "BirdHouse_Battle.Model.Archer")
                {
                    GlobalLocation.Add(unit.Value.Location);
                    Count++;
                }
            }
            return Vector.Division(GlobalLocation, Count);
        }

        public void GetUnitsGlobalStrength()
        {
            _goblinsAttack = 0;
            _archersAttack = 0;

            foreach (KeyValuePair<int, Unit> unit in Units)
            {
                string type = unit.Value.GetType().ToString();

                if (type == "BirdHouse_Battle.Model.Goblin" || type == "BirdHouse_Battle.Model.Archer")
                {
                    Vector location = unit.Value.Location;

                    Vector globalLocation;

                    if (type == "BirdHouse_Battle.Model.Goblin")
                    {
                        globalLocation = Vector.Soustract(GoblinsTarget.Location, unit.Value.Location);
                    }
                    else
                    {
                        globalLocation = Vector.Soustract(ArchersTarget.Location, unit.Value.Location);
                    }

                    if (unit.Value.Target != null)
                    {
                        location = Vector.Soustract(unit.Value.Target.Location, unit.Value.Location);

                        if (location.Magnitude * 2 >= globalLocation.Magnitude && type == "BirdHouse_Battle.Model.Goblin")
                        {
                            _goblinsAttack += unit.Value.Strength;
                            unit.Value.TeamPlayOn();
                        }
                        else if (location.Magnitude * 2 >= globalLocation.Magnitude && type == "BirdHouse_Battle.Model.Archer")
                        {
                            _archersAttack += unit.Value.Strength;
                            unit.Value.TeamPlayOn();
                        }
                        else
                        {
                            unit.Value.TeamPlayOff();
                        }
                    }
                }
            }
        }

        public void UnitTypeEnemys()
        {
            Vector GobelinsLocation = this.GoblinsLocation();
            Vector ArchersLocation = this.ArchersLocation();
            _goblinsTarget = _arena.NearestEnemy(GobelinsLocation, Name);
            _archersTarget = _arena.NearestEnemy(ArchersLocation, Name);
            GetUnitsGlobalStrength();
        }

        /// <summary>
        /// True if all units in the team died
        /// </summary>
        public bool IsWiped
        {
            get
            {
                if (UnitCount == 0 || _units.Count == 0) return true;
                return _isWiped;
            }
        }

        public void Update()
        {
            //update units and add dead units in deadunit dic 
            foreach (Unit unit in _units.Values)
            {
                UnitTypeEnemys();
                unit.Update();
                if (unit.IsDead()) _deadUnits.Add(unit.Name, unit);
            }
        }

        public void UpdateDead()
        {
            //remove dead units only if they still exist
            foreach (KeyValuePair<int, Unit> i in _deadUnits)
            {
                if (FindUnitByName(i.Key) != null) RemoveUnit(i.Value);
            }
        }
    }
}