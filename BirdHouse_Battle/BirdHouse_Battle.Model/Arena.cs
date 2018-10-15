using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    class Arena
    {
        readonly Dictionary<string, Teams> _teams;
        readonly int _height;
        readonly int _width;
        public Arena(int heigth, int width)
        {
            _teams = new Dictionary<string, Teams>();
            _height = heigth;
            _width = width;
        }




        public Team CreateTeam(string name)
        {
            if (_teams.ContainsKey(name)) throw new ArgumentException("A team with this name already exists.", nameof(name));

            Team team = new Team(this, name);
            _teams.Add(name, team);
            return team;
        }

        public Team FindTeam(string name)
        {
            Team result;
            _teams.TryGetValue(name, out result);
            Team cat = result as Team;
            return Team;
        }





        
        //public void SpawnUnit()
        //{

        //}


        public Unit NearestEnnemy (Unit unit)
        {
            int x = unit.position.x;
            int y = unit.position.y;
            double distance=0;
            Unit ennemyUnit;
            foreach (Team team in _teams)
            {
                foreach (Unit unitBis in _teams._units)
                {
                    if (distance == 0)
                    {
                        distance = Math.Sqrt(Math.Pow(x - unitBis.position.x, 2) + Math.Pow(y - unitBis.position.y, 2));
                        ennemyUnit = unitBis;
                    }
                    else
                    {
                        if (distance > Math.Sqrt(Math.Pow(x - unitBis.position.x, 2) + Math.Pow(y - unitBis.position.y, 2)))
                        {
                            ennemyUnit = unitBis;
                        }
                    }  
                }
            }
            return ennemyUnit;
        }

        



        //internal vector nextrandomposition()
        //{
        //    return new vector(nextrandomdouble(-1.0, 1.0), nextrandomdouble(-1.0, 1.0));
        //}

        //internal vector nextrandomdirection()
        //{
        //    double x = nextrandomdouble(-1.0, 1.0);
        //    double alpha = math.acos(x);
        //    double y = math.sin(alpha);
        //    if (_random.nextdouble() < 0.5) y = y * -1.0;
        //    return new vector(x, y);
        //}

        //internal vector nextrandomdirection(vector direction, double alpha)
        //{
        //    double beta = nextrandomdouble(-1.0 * alpha, alpha);
        //    double newx = math.cos(beta + math.acos(direction.x));
        //    double newy = math.sin(beta + math.asin(direction.y));

        //    vector vector = new vector(newx, newy);
        //    return vector.multiply(1.0 / vector.magnitude);
        //}


        public void Update()
        {
            throw new ArgumentNullException();
        }
    }
}
