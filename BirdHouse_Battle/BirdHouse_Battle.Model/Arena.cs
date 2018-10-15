using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    class Arena
    {
        //readonly Dictionary<string, Teams> _teams;
        readonly int _height;
        readonly int _width;
        public Arena()
        {
            //_teams = new Dictionary<string, Teams>();
            _height = 100;
            _width = 100;
        }




        //public Team CreateTeam(string name)
        //{
        //    if (_teams.ContainsKey(name)) throw new ArgumentException("A team with this name already exists.", nameof(name));
        //    if (_teams.Count >= 4) throw new Exception("You already have 4 team and cannot create any more");
        //    Team team = new Team(this, name);
        //    _teams.Add(name, team);
        //    return team;
        //}

        //public void RemoveTeam(string name)
        //{
        //    if (_teams.ContainsKey(name) == false) throw new ArgumentException("A team with name does not exist", nameof(name));

        //    _teams.Remove(name);
        //}

        //public Team FindTeam(string name)
        //{
        //    Team result;
        //    _teams.TryGetValue(name, out result);
        //    Team cat = result as Team;
        //    return Team;
        //}



        //public bool Collision(Unit unit)
        //{
        //    int x = unit.position.x;
        //    int y = unit.position.y;
        //    bool doesCollide = false;
        //    foreach (Team team in _teams)
        //    {
        //        foreach (Unit unitBis in _teams._units)
        //        {
        //            if (x == unitBis.position.x && y == unitBis.position.y && unitBis != unit) doesCollide = true;
        //        }
        //    }
        //    return doesCollide;
        //}

        public Unit SpawnUnit(Unit unit)
        {
            
        }



        //public Unit NearestEnnemy (Unit unit)
        //{
        //    int x = unit.position.x;
        //    int y = unit.position.y;
        //    double distance=0;
        //    Unit ennemyUnit;
        //    foreach (Team team in _teams)
        //    {
        //        foreach (Unit unitBis in _teams._units)
        //        {
        //            if (distance == 0)
        //            {
        //                distance = Math.Sqrt(Math.Pow(x - unitBis.position.x, 2) + Math.Pow(y - unitBis.position.y, 2));
        //                ennemyUnit = unitBis;
        //            }
        //            else
        //            {
        //                if (distance > Math.Sqrt(Math.Pow(x - unitBis.position.x, 2) + Math.Pow(y - unitBis.position.y, 2)))
        //                {
        //                    ennemyUnit = unitBis;
        //                }
        //            }  
        //        }
        //    }
        //    return ennemyUnit;
        //}

        



        


        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}
