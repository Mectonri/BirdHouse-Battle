using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    class Arena
    {
        //readonly Dictionary<string, Teams> _teams;
        public Arena()
        {
            //_teams = new Dictionary<string, Teams>();
        }




        //public Team CreateTeam()
        //{

        //}
        //public Team FindTeam()
        //{

        //}
        //internal List<Team> GetTeams()
        //{

        //}




        //public Unit CreateUnit()
        //{
        //    Unit unit = new Unit();
        //    return unit;
        //}
        //public void SpawnUnit()
        //{

        //}




        public void Update()
        {

        }



        //internal Vector NextRandomPosition()
        //{
        //    return new Vector(NextRandomDouble(-1.0, 1.0), NextRandomDouble(-1.0, 1.0));
        //}

        //internal Vector NextRandomDirection()
        //{
        //    double x = NextRandomDouble(-1.0, 1.0);
        //    double alpha = Math.Acos(x);
        //    double y = Math.Sin(alpha);
        //    if (_random.NextDouble() < 0.5) y = y * -1.0;
        //    return new Vector(x, y);
        //}

        //internal Vector NextRandomDirection(Vector direction, double alpha)
        //{
        //    double beta = NextRandomDouble(-1.0 * alpha, alpha);
        //    double newX = Math.Cos(beta + Math.Acos(direction.X));
        //    double newY = Math.Sin(beta + Math.Asin(direction.Y));

        //    Vector vector = new Vector(newX, newY);
        //    return vector.Multiply(1.0 / vector.Magnitude);
        //}
    }
}
