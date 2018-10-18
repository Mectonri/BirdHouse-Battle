using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Arena
    {
        readonly Dictionary<string, Team> _teams;
        readonly int _height;
        readonly int _width;

        public Arena()
        {
            _teams = new Dictionary<string, Team>();
            _height = 100;
            _width = 100;
        }

        public Team CreateTeam(string name)
        {
            if (_teams.ContainsKey(name)) throw new ArgumentException("A team with this name already exists.", nameof(name));
            if (_teams.Count >= 4) throw new Exception("You already have 4 team and cannot create any more");
            Team team = new Team(this, name, 250);//POUR L'INSTANT AXEL
            _teams.Add(name, team);
            return team;
        }

        public void RemoveTeam(string name)
        {
            if (_teams.ContainsKey(name) == false) throw new ArgumentException("A team with name does not exist", nameof(name));

            _teams.Remove(name);
        }

        public Team FindTeam(string name)
        {
            Team result;
            _teams.TryGetValue(name, out result);
            return result;
        }



        public bool Collision(Unit unit)
        {
            double x = unit.Location.X;
            double y = unit.Location.Y;
            bool doesCollide = false;
            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> kv2 in kv.Value._units)
                {
                    if (x == kv2.Value.Location.X && y == kv2.Value.Location.Y && kv2.Value != unit) doesCollide = true;
                }
            }
            return doesCollide;
        }

        public Unit SpawnUnit(Unit unit)
        {
            throw new ArgumentNullException();
        }



        public Unit NearestEnemy(Unit unit)
        {
            double x = unit.Location.X;
            double y = unit.Location.Y;
            double distance = 0;
            Unit ennemyUnit = new Unit();
            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> kv2 in kv.Value._units)
                {
                    if (distance == 0)
                    {
                        distance = Math.Sqrt(Math.Pow(x - kv2.Value.Location.X, 2) + Math.Pow(y - kv2.Value.Location.Y, 2));
                        ennemyUnit = kv2.Value;
                    }
                    else
                    {
                        if (distance > Math.Sqrt(Math.Pow(x - kv2.Value.Location.X, 2) + Math.Pow(y - kv2.Value.Location.Y, 2)))
                        {
                            distance = Math.Sqrt(Math.Pow(x - kv2.Value.Location.X, 2) + Math.Pow(y - kv2.Value.Location.Y, 2));
                            ennemyUnit = kv2.Value;
                        }

                    }
                }
            }
            return ennemyUnit;
        }








        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}
