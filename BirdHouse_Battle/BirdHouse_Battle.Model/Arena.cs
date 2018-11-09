using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public class Arena
    {
        readonly Dictionary<string, Team> _teams;
        readonly Dictionary<string, Team> _deadTeams;
        readonly Dictionary<int, Projectile> _projectiles;
        readonly Dictionary<int, Projectile> _deadProjectiles;
        readonly int _height;
        readonly int _width;
        int _counter;
        
        public Arena()
        {
            _teams = new Dictionary<string, Team>();
            _deadTeams = new Dictionary<string, Team>();
            _projectiles = new Dictionary<int, Projectile>();
            _deadProjectiles = new Dictionary<int, Projectile>();
            _height = 250;
            _width = 250;
            _counter = 0;
        }

        public Dictionary<string, Team> Teams
        {
            get { return _teams; }
        }

        public Dictionary<int, Projectile> Projectiles
        {
            get { return _projectiles; }
        }

        public int Height
        {
            get { return _height; }
        }
        
        public int Width
        {
            get { return _width; }
        }

        public int Counter
        {
            get { return _counter; }
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

        public int TeamCount
        {
            get { return _teams.Count; }
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
                    if (x == kv2.Value.Location.X && y == kv2.Value.Location.Y && kv2.Value.Name != unit.Name) doesCollide = true;
                }
            }
            return doesCollide;
        }

        public bool Collision(Vector vector)
        {
            bool doesCollide = false;
            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> kv2 in kv.Value._units)
                {
                    if (vector.X == kv2.Value.Location.X && vector.Y == kv2.Value.Location.Y) doesCollide = true;
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
                x = random.NextDouble() * (_width * 2) - _width;
                y = random.NextDouble() * (_height * 2) - _height;
                vector = new Vector(x, y);
                if (ValidSpawnLocation(vector) == true) {
                    IsSpawnable = true;
                }

            } while (IsSpawnable == false);

            unit.Location = vector;

            return unit;
        }

        public void SpawnUnit()
        {
            Vector vector =new Vector(0,0);
            double x;
            double y;
            Random random = new Random();
            int i = 0;
            

            foreach (KeyValuePair<string, Team> kv in _teams)
            {

                foreach (KeyValuePair<Guid, Unit> kv2 in kv.Value._units)
                {
                    bool IsSpawnable = false;

                    do
                    {
                        if (_teams.Count == 2)
                        {
                            if (i == 0)
                            {
                                x = random.NextDouble() * _width -_width;
                                y = random.NextDouble() * (_height*2) - _height;
                                vector = new Vector(x, y);
                                if (ValidSpawnLocation(vector) == true)
                                {
                                    IsSpawnable = true;
                                }
                            }
                            else
                            {
                                x = random.NextDouble() * _width;
                                y = random.NextDouble() * (_height*2)- _height;
                                vector = new Vector(x, y);
                                if (ValidSpawnLocation(vector) == true)
                                {
                                    IsSpawnable = true;
                                }
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                x = random.NextDouble() * _width - _width;
                                y = random.NextDouble() * _height - _height;
                                vector = new Vector(x, y);
                                if (ValidSpawnLocation(vector) == true)
                                {
                                    IsSpawnable = true;
                                }
                            }
                            else if (i == 1)
                            {
                                x = random.NextDouble() * _width - _width;
                                y = random.NextDouble() * _height;
                                vector = new Vector(x, y);
                                if (ValidSpawnLocation(vector) == true)
                                {
                                    IsSpawnable = true;
                                }
                            }
                            else if (i == 2)
                            {
                                x = random.NextDouble() * _width;
                                y = random.NextDouble() * _height - _height;
                                vector = new Vector(x, y);
                                if (ValidSpawnLocation(vector) == true)
                                {
                                    IsSpawnable = true;
                                }
                            }
                            else
                            {
                                x = random.NextDouble() * _width;
                                y = random.NextDouble() * _height;
                                vector = new Vector(x, y);
                                if (ValidSpawnLocation(vector) == true)
                                {
                                    IsSpawnable = true;
                                }
                            }
                        }

                    } while (IsSpawnable == false);

                    kv2.Value.Location = vector;
                }
                i++;
            }
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
                throw new ArgumentException("The given coordinates are not valid, either they are equal to {0,0}, a unit is already on those coordonates or the given coordonates are outside the board");
            }

        }


        private bool ValidSpawnLocation(Vector vector)
        {
            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> kv2 in kv.Value._units)
                {
                    if (kv2.Value.Location.X == vector.X && kv2.Value.Location.Y == vector.Y || vector.X == 0 && vector.Y == 0 || vector.X > _width || vector.Y > _height)
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

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                if (team.Value != unit.Team) {
                    foreach (KeyValuePair<Guid, Unit> units in team.Value._units)
                    {
                        double dX = x - units.Value.Location.X;
                        double dY = y - units.Value.Location.Y;

                        if (distance == 0)
                        {
                            distance = Math.Sqrt(dX * dX) + Math.Sqrt(dY * dY);
                            ennemyUnit = units.Value;
                        }
                        else
                        {
                            if (distance > Math.Sqrt(dX * dX) + Math.Sqrt(dY * dY))
                            {
                                distance = Math.Sqrt(dX * dX) + Math.Sqrt(dY * dY);
                                ennemyUnit = units.Value;
                            }

                        }
                    }
                }
            }
            return ennemyUnit;
        }

        public Unit NearestEnemyNotFlying(Unit unit)
        {
            double x = unit.Location.X;
            double y = unit.Location.Y;
            double distance = 0;
            Unit ennemyUnit = null;

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                if (team.Value != unit.Team)
                {
                    foreach (KeyValuePair<Guid, Unit> units in team.Value._units)
                    {
                        if (units.Value.Fly == false)
                        {
                            double dX = x - units.Value.Location.X;
                            double dY = y - units.Value.Location.Y;

                            if (distance == 0)
                            {
                                distance = Math.Sqrt(dX * dX) + Math.Sqrt(dY * dY);
                                ennemyUnit = units.Value;
                            }
                            else
                            {
                                if (distance > Math.Sqrt(dX * dX) + Math.Sqrt(dY * dY))
                                {
                                    distance = Math.Sqrt(dX * dX) + Math.Sqrt(dY * dY);
                                    ennemyUnit = units.Value;
                                }

                            }
                        }
                    }
                }
            }
            return ennemyUnit;
        }

        public Unit GiveDamage(Unit unit, double damage)
        {
            unit.TakeDamages(damage);
            return unit;
        }

        public Unit GiveFire(Unit unit, int damage)
        {
            unit.Fired(damage);
            return unit;
        }

        public void InitArrow(Vector start, Vector end)
        {
            Arrow arrow = new Arrow(this, start, end, Counter);
            _projectiles.Add(Counter, arrow);
            _counter++;
        }

        public void IsUnitInRangeAoe(Projectile projectile, int damages)
        {
            Vector UnitPosition;

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> units in team.Value._units)
                {
                    UnitPosition = units.Value.Location;

                    if (projectile.Position.Soustract(UnitPosition).Magnitude <= projectile.Range)
                    {
                        units.Value.TakeDamages(damages);
                    }
                }
            }
        }

        public bool IsUnitInRange(Projectile projectile, int damages)
        {
            Vector UnitPosition;

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                foreach (KeyValuePair<Guid, Unit> units in team.Value._units)
                {
                    UnitPosition = units.Value.Location;
                    Vector location = projectile.Position.Soustract(UnitPosition);

                    if (location.Magnitude <= projectile.Range)
                    {
                        units.Value.TakeDamages(damages);
                        return true;
                    }
                }
            }
            return false;
        }
        
        private bool FindProjectile(int name)
        {
            return _projectiles.TryGetValue(name, out Projectile projectile);
        }

        public void UpdateTeams()
        {
            foreach (Team team in _teams.Values)
            {
                team.Update();
            }
            foreach (Team team in _teams.Values)
            {
                team.UpdateDead();
                if (team.IsWiped == true) _deadTeams.Add(team.Name, team);
            }
        }

        public void UpdateProjectiles()
        {
            foreach (Projectile projectile in _projectiles.Values)
            {
                projectile.Update();

                if (projectile.Arrived)
                {
                    _deadProjectiles.Add(projectile.Name, projectile);
                }
            }
            foreach (Projectile projectile in _deadProjectiles.Values)
                {
                if (FindProjectile(projectile.Name))
                {
                    _projectiles.Remove(projectile.Name);
                    projectile.Dispose();
                }
            }
        }

        public void Update()
        {
            UpdateTeams();
            UpdateProjectiles();
            
            foreach (KeyValuePair<string, Team> team in _deadTeams)
            {
                if (FindTeam(team.Key) != null) RemoveTeam(team.Key);
            }
        }
    }
}
