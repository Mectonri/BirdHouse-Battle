using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.Model
{
    public struct Vector
    {
        double _x;
        double _y;

        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }

        public double Magnitude
        {
            get { return Math.Sqrt(_x * _x + _y * _y); }
        }

        public Vector Multiply(double d)
        {
            return new Vector(_x * d, _y * d);
        }

        public Vector Add(Vector vector)
        {
            return new Vector(_x + vector._x, _y + vector._y);
        }

        public Vector Soustract(Vector vector)
        {
            
            return new Vector(_x - vector._x, _y - vector._y);
        }

        public Vector Limit(double min, double max)
        {
            return new Vector(Math.Min(max, Math.Max(min, _x)), Math.Min(max, Math.Max(min, _y)));
        }
    }
}