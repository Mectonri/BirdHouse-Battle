﻿using System;
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


        //basic version i'll return on it later 
        public Unit SpawnUnit(Unit unit)
        {
            bool IsSpawnable = false;
            Vector vector;
            double x;
            double y;
            Random random = new Random();

            //For later 
            //random.NextDouble() * (maximum - minimum) + minimum;

            do
            {
                x = random.NextDouble() * (_width);
                y = random.NextDouble() * (_height);
                vector = new Vector(x, y);
                if (ValidSpawnLocation(vector) == true) {
                    IsSpawnable = true;
                }
                
            } while (IsSpawnable == false);

            unit.Location = vector;

            return unit;
        }

        public Unit SpawnUnit(Unit unit, double x, double y)
        {
            bool isSpawnable;
            Vector vector = new Vector(x, y);
            isSpawnable = ValidSpawnLocation(vector);
            if (isSpawnable == true)
            {
                unit.Location = vector;
                return unit;
            }
            else
            {
                throw new ArgumentException("The given coordinates are not valid");
            }
            
        }


        private bool ValidSpawnLocation(Vector vector)
        {
            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> kv2 in kv.Value._units)
                {
                    if(kv2.Value.Location.X==vector.X && kv2.Value.Location.Y == vector.Y)
                    {
                        return false;
                    }
                }
            }
            return true;
        }




        public Unit NearestEnemy(Unit unit)
        {
            double x = unit.Location.X;
            double y = unit.Location.Y;
            double distance = 0;
            Unit ennemyUnit = null;
            Team unitTeam = unit.Team;
            foreach (KeyValuePair<string, Team> team in _teams)
            {
                if (team.Value != unitTeam ) {
                    foreach (KeyValuePair<Guid, Unit> units in team.Value._units)
                    {
                        if (distance == 0)
                        {
                            distance = Math.Sqrt(Math.Pow(x - units.Value.Location.X, 2) + Math.Pow(y - units.Value.Location.Y, 2));
                            ennemyUnit = units.Value;
                        }
                        else
                        {
                            if (distance > Math.Sqrt(Math.Pow(x - units.Value.Location.X, 2) + Math.Pow(y - units.Value.Location.Y, 2)))
                            {
                                distance = Math.Sqrt(Math.Pow(x - units.Value.Location.X, 2) + Math.Pow(y - units.Value.Location.Y, 2));
                                ennemyUnit = units.Value;
                            }

                        }
                    }
                }
            }
            return ennemyUnit;
        }

        public Unit GiveDamage (Unit unit, double damage)
        {
            unit.TakeDamages(damage);
            return unit;
        }


        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}
