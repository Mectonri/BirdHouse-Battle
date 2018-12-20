using System;
using System.Collections.Generic;

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
        readonly Field _field;

        public Arena()
        {
            _teams = new Dictionary<string, Team>();
            _deadTeams = new Dictionary<string, Team>();
            _projectiles = new Dictionary<int, Projectile>();
            _deadProjectiles = new Dictionary<int, Projectile>();
            _height = 250;
            _width = 250;
            _counter = 0;
            _field = new Field(this, -_height, _height, -_width, _width);

            if (true) // Créer le bool de nouvelle partie / rejouer partie
            {
                _field.Init();
            }
        }

        public Dictionary<string, Team> Teams
        {
            get { return _teams; }
        }

        public Dictionary<int, Projectile> Projectiles
        {
            get { return _projectiles; }
        }

        public Dictionary<string, Team> DeadTeams
        {
            get { return _deadTeams; }
        }

        public Dictionary<int, Projectile> DeadProjectiles
        {
            get { return _deadProjectiles; }
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

        public Field Field
        {
            get { return _field; }
        }

        public Team CreateTeam(string name)
        {
            if (_teams.ContainsKey(name)) throw new ArgumentException("A team with this name already exists.", nameof(name));
            if (_teams.Count >= 4) throw new Exception("You already have 4 team and cannot create any more");
            Team team = new Team(this, name, 100);
            _teams.Add(name, team);
            return team;
        }

        public void RemoveTeam(string name)
        {
            if (_teams.ContainsKey(name) == false) throw new ArgumentException("A team with name does not exist", nameof(name));

            _teams.Remove(name);
        }

        public bool FindTeam(string name)
        {
            return _teams.TryGetValue(name, out Team result);
        }

        public int TeamCount
        {
            get { return _teams.Count; }
        }

        //public bool Collision(Unit unit)
        //{
        //    double x = unit.Location.X;
        //    double y = unit.Location.Y;
        //    bool doesCollide = false;
        //    foreach (KeyValuePair<string, Team> kv in _teams)
        //    {
        //        foreach (KeyValuePair<int, Unit> kv2 in kv.Value._units)
        //        {
        //            if (x == kv2.Value.Location.X && y == kv2.Value.Location.Y && kv2.Value.Name != unit.Name) doesCollide = true;
        //        }
        //    }
        //    return doesCollide;
        //}

        public bool Collision(Unit unit, Vector vector, double speed)
        {
            bool doesCollide = false;
            bool isOriginalXGood = unit.Location.X <= unit.Arena.Height - 1;
            bool isOriginalYGood = unit.Location.Y <= unit.Arena.Height - 1;

            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<int, Unit> kv2 in kv.Value._units)
                {
                    if (vector.X == kv2.Value.Location.X && vector.Y == kv2.Value.Location.Y) doesCollide = true;
                }
            }

            Tile tile = Field.FindTile(int.Parse($"{Math.Round(vector.X)}"), int.Parse($"{Math.Round(vector.Y)}"));

            if (tile.Obstacle == "Rock" || tile.Obstacle == "Tree") doesCollide = unit.MissObstacle(vector, speed, isOriginalXGood, isOriginalYGood);

            return doesCollide;
        }

        public bool Collision(Unit unit, Vector vector, double speed, bool isOriginalXGood, bool isOriginalYGood)
        {
            bool doesCollide = false;

            foreach (KeyValuePair<string, Team> kv in _teams)
            {
                foreach (KeyValuePair<int, Unit> kv2 in kv.Value._units)
                {
                    if (vector.X == kv2.Value.Location.X && vector.Y == kv2.Value.Location.Y) doesCollide = true;
                }
            }

            Tile tile = Field.FindTile(int.Parse($"{Math.Round(vector.X)}"), int.Parse($"{Math.Round(vector.Y)}"));

            if (tile.Obstacle == "Rock" || tile.Obstacle == "Tree") doesCollide = unit.MissObstacle(vector, speed, isOriginalXGood, isOriginalYGood);

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
            Vector vector = new Vector(0, 0);
            double x;
            double y;
            Random random = new Random();
            int i = 0;
            

            foreach (KeyValuePair<string, Team> kv in _teams)
            {

                foreach (KeyValuePair<int, Unit> kv2 in kv.Value._units)
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
                foreach (KeyValuePair<int, Unit> kv2 in kv.Value._units)
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
                    foreach (KeyValuePair<int, Unit> units in team.Value._units)
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

        public Unit NearestEnemy(Vector vector, string teamName)
        {
            double x = vector.X;
            double y = vector.Y;
            double distance = 0;
            Unit ennemyUnit = null;

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                if (team.Value.Name != teamName)
                {
                    foreach (KeyValuePair<int, Unit> units in team.Value._units)
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
                    foreach (KeyValuePair<int, Unit> units in team.Value._units)
                    {
                        if (!units.Value.Fly)
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

        public Unit NearestEnemyFlying(Unit unit)
        {
            double x = unit.Location.X;
            double y = unit.Location.Y;
            double distance = 0;
            Unit ennemyUnit = null;

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                if (team.Value != unit.Team)
                {
                    foreach (KeyValuePair<int, Unit> units in team.Value._units)
                    {
                        if (units.Value.Fly)
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

        public void InitArrow(Vector start, Vector end, int NbFram)
        {
            Arrow arrow = new Arrow(this, start, end, Counter, NbFram);
            _projectiles.Add(Counter, arrow);
            _counter++;
        }

        public void InitBoulder(Vector start, Vector end, int NbFram)
        {
            Boulder boulder = new Boulder(this, start, end, Counter, NbFram);
            _projectiles.Add(Counter, boulder);
            _counter++;
        }

        public void InitBalisticAmmo(Vector start, Vector end, int NbFram)
        {
            BalisticAmmo balisticAmmo = new BalisticAmmo(this, start, end, Counter, NbFram);
            _projectiles.Add(Counter, balisticAmmo);
            _counter++;
        }

        public void IsUnitInRangeAoe(Projectile projectile, int damages)
        {
            Vector UnitPosition;

            foreach (KeyValuePair<string, Team> team in _teams)
            {
                foreach (KeyValuePair<int, Unit> units in team.Value._units)
                {
                    UnitPosition = units.Value.Location;

                    if (Vector.Soustract(UnitPosition, projectile.Position).Magnitude <= projectile.Range)
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
                foreach (KeyValuePair<int, Unit> units in team.Value._units)
                {
                    UnitPosition = units.Value.Location;
                    Vector location = Vector.Soustract(UnitPosition, projectile.Position);

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

        public void SpawnRock(int i, int j)
        {
            Field.GenerationRockInGame(i, j);
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
                if (FindTeam(team.Key) != false) RemoveTeam(team.Key);
            }
        }
    }
}
