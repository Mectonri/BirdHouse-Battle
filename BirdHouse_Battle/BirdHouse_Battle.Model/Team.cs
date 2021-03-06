﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BirdHouse_Battle.Model
{
    public class Team
    {         
        //Team number is associated with a color, so creating a team take the color as argument
        readonly string _name;
        readonly int _teamNumber; //number is used to assign a color to a team
        int _unitCount; // Le nombre total d'unites  dans une equipes

        internal int _aToAdd;// Number of Archer   to add to a Team
        internal int _bToAdd;// Number of Balista  to add to a Team
        internal int _cToAdd;// Number of Catapult to add to a Team
        internal int _dToAdd;// Number of Drake    to add to a Team
        internal int _gToAdd;// Number of Goblin   to add to a Team
        internal int _pToAdd;// Number of Paladin  to add to a Team
        
        internal int _aCount; // Number of Archer   in a team
        int _bCount; // Number of Balista  in a team
        int _cCount; // Number of Catapult in a team
        int _dCount; // Number of Drake    in a team
        int _gCount; // Number of Goblin   in a team
        int _pCount; // Number of Paladin  in a team
        

        bool _isWiped;

        internal double _goldAmount; // Amount of gold given to the player in HistoryMode  
        readonly int _limitNbUnit; // unit limit by team
        internal Arena _arena; // Adds team conttext which is the Arena
        internal Dictionary<int, Unit> _deadUnits; 
        internal Dictionary<int, Unit> _units;

        internal double _health;

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
            _arena = Context;
            _name = Name;
            
            _teamNumber = _arena.TeamCount;
            _limitNbUnit = 125;
            _isWiped = false;

            _units = new Dictionary<int, Unit>();
            _deadUnits = new Dictionary<int, Unit>();
            _health = HealthCalculation();
        }

        #region relevent to serialization

        /// <summary>
        /// Serialize a team
        /// </summary>
        /// <returns></returns>
        public JToken Serialize()
        {
            return new JObject(
                new JProperty("Name", _name),
                new JProperty("Units", _units.Select(kv => kv.Value.Serialize())));
        }

        /// <summary>
        /// Create Team from JToken
        /// </summary>
        ///<param name = "jToken" ></ param >
        public Team(Arena arena, JToken jToken)
        {
            _isWiped = false;
            _name = jToken["Name"].Value<string>();
            _arena = arena;
            _teamNumber = _arena.TeamCount;
            _limitNbUnit = 125;

            _units = new Dictionary<int, Unit>();
            JArray jUnits = (JArray)jToken["Units"];

            IEnumerable<Unit> units = jUnits.Select( u =>  AddUnit( arena, this, u) );

            foreach (Unit unit in units)
            {
                _units.Add(unit.Name, unit);
            }

            _deadUnits = new Dictionary<int, Unit>();
        }

        internal Unit AddUnit(Arena arena, Team team, JToken jToken)
        {
            Unit unit = null;

            if (jToken["Troop"].Value<string>() == "archer")
            {
                 unit = new Archer(arena, team, jToken);
                team.Acount++;
            }
            else if (jToken["Troop"].Value<string>() == "balista")
            {
                 unit = new Balista(arena, team, jToken);
                team.Bcount++;
            }
            else if (jToken["Troop"].Value<string>() == "catapult")
            {
                 unit = new Catapult(arena, team, jToken);
                team.Ccount++;
            }
            else if (jToken["Troop"].Value<string>() == "drake")
            {
                 unit = new Drake(arena, team, jToken);
                team.Dcount++;
            }
            else if (jToken["Troop"].Value<string>() == "goblin")
            {
                 unit = new Goblin(arena, team, jToken);
                team.Gcount++;
            }
            else if (jToken["Troop"].Value<string>() == "paladin")
            {
                 unit = new Paladin(arena, team, jToken);
                team.Pcount++;
            }

            return unit;
        }

        internal void AddUnitToTeam(Team team, JToken u)
        {
            string troop = u["Troop"].Value<string>();

            switch (troop)
            {
                case "archer":
                    team.AddArcher(1);
                    break;
                case "balista":
                    team.AddBalista(1);
                    break;
                case "catapult":
                    team.AddCatapult(1);
                    break;
                case "drake":
                    team.AddDrake(1);
                    break;
                case "goblin":
                    team.AddGoblin(1);
                    break;
                case "paladin":
                    team.AddPaladin(1);
                    break;
            }
        }

        public IEnumerable<Unit> GetUnits()
        {
            return _units.Values;
        }

        #endregion

        #region Getters & Setters

        public double Health => _health;
        public string Name => _name;

        public int TeamNumber => _teamNumber;

        public int UnitCount
        {
            get
            {
                if (_unitCount > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of troops for this team");

                return _unitCount = _aCount + _bCount  + _cCount + _dCount + _gCount + _pCount    ;
            }
        }

        public Arena Context => _arena;

        public int Acount
        {
            get{ return _aCount; }
            set{ _aCount = value; }
        }

        public int Gcount
        {
            get { return _gCount; }
            set { _gCount = value; }
        }

        public int Pcount
        {
            get { return _pCount; }
            set { _pCount = value; }
        }
        

        public int Dcount
        {
            get { return _dCount; }
            set { _dCount = value; }
        }
        public int Ccount
        {
            get { return _cCount; }
            set { _cCount = value; }
        }
        public int Bcount
        {
            get { return _bCount; }
            set { _bCount = value; }
        }

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
                Archer archer = new Archer(_arena, this, UnitCount);
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
                Drake drake = new Drake(_arena, this,  UnitCount);
                _dCount++;
                _units.Add(drake.Name, drake);
            }
        }

        public void AddCatapult(int CToAdd)
        {
            if (UnitCount >= _limitNbUnit || CToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of unit in this team", nameof(_unitCount));
            for (int i = 0; i < CToAdd; i++)
            {
                Catapult catapult = new Catapult(_arena, this,  UnitCount);
                _cCount++;
                _units.Add(catapult.Name, catapult);
            }
        }

        public void AddBalista(int BToAdd)
        {
            if (UnitCount >= _limitNbUnit || BToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun number of unit in this team", nameof(_unitCount));
            for (int i = 0; i < BToAdd; i++)
            {
                Balista balista = new Balista(_arena, this,  UnitCount);
                _bCount++;
                _units.Add(balista.Name, balista);
            }
        }

        /// <summary>
        /// Add Goblin to a team
        /// </summary>
        /// <param name="GToAdd"></param>
        public void AddGoblin(int GToAdd)
        {
            if (UnitCount >= _limitNbUnit || GToAdd > _limitNbUnit) throw new ArgumentException("You've exceeded the maximun numebr of unit in this team", nameof(_unitCount));
            for (int i = 0; i < GToAdd; i++)
            {
                Goblin goblin = new Goblin(_arena, this,  UnitCount);
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
                Paladin paladin = new Paladin(_arena,  this,  UnitCount);
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
                
                if (u is Archer)        _aCount--;
                else if (u is Goblin)   _gCount--;
                else if (u is Drake)    _dCount--;
                else if (u is Catapult) _cCount--;
                else if (u is Balista)  _bCount--;
                else { _pCount--; }
            }
        }

        /// <summary>
        /// Returns the total health of a team
        /// </summary>
        /// <returns></returns>
        public double HealthCalculation()
        {
            double h = 0;
            foreach (KeyValuePair<int, Unit> kv in _units)
            {
                h = kv.Value.Life + h;
            }
            return h;
        }

        public double AddWithGold(int unit)
        {
            double result = 0.0;
            switch (unit)
            {
                case 1:
                    result = GoldAmount - 10.0;
                    if (result < 0) return GoldAmount;
                    else { AddArcher(1); return GoldAmount = result; }

                case 2:
                    result = GoldAmount - 30.0;
                    if (result < 0) return GoldAmount;
                    else { AddBalista(1); return GoldAmount = result; }

                case 3:
                    result = GoldAmount - 40.0;
                    if (result < 0) return GoldAmount;
                    else { AddCatapult(1); return GoldAmount = result; }

                case 4:
                    result = GoldAmount - 15.0;
                    if (result < 0) return GoldAmount;
                    else { AddDrake(1); return GoldAmount = result; }

                case 5:
                    result = GoldAmount - 3.0;
                    if (result < 0) return GoldAmount;
                    else { AddGoblin(1); return GoldAmount = result; }

                case 6:
                    result = GoldAmount - 12.0;
                    if (result < 0) return GoldAmount;
                    else { AddPaladin(1); return GoldAmount = result; }
            }
            return GoldAmount;
        }

        /// <summary>
        /// Calculate the amount of gold left. Can only be superieo or equals to 0
        /// </summary>
        /// <param name="Gold"></param>
        /// <returns></returns>
        public double GoldCalculation(double Gold)
        {
            
            foreach (KeyValuePair<int, Unit> unit in _units)
            {
                
                if (Gold < 0.0) throw new ArgumentException("You dont have enought gold ", nameof(Gold));
                Gold = Gold - unit.Value.UnitPrice;
            }
            return _goldAmount = Gold ;
        }

        public bool IsGobelinsAlone()
        {
            foreach (KeyValuePair<int, Unit> unit in Units)
            {
                if (unit.Value is Goblin)
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
                if (unit.Value is Goblin)
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
                if (unit.Value is Archer)
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
                Unit u = unit.Value;

                if (u is Goblin || u is Archer)
                {
                    Vector location = unit.Value.Location;

                    Vector globalLocation;

                    if (u is Goblin)
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

                        if (location.Magnitude * 2 >= globalLocation.Magnitude && u is Goblin)
                        {
                            _goblinsAttack += unit.Value.Strength;
                            unit.Value.TeamPlayOn();
                        }
                        else if (location.Magnitude * 2 >= globalLocation.Magnitude && u is Archer)
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