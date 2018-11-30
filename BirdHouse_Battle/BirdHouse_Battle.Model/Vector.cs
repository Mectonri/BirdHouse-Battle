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
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Get size of the vector.
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt(_x * _x + _y * _y); }
        }

        public static Vector Multiply(double d, Vector vector)
        {
            vector.X *= d;
            vector.Y *= d;

            return vector;
        }

        public static Vector Division(Vector vector, int number)
        {
            vector.X = Math.Round(vector.X /= number, 0);
            vector.Y = Math.Round(vector.Y /= number, 0);

            return vector;
        }

        public Vector Add(Vector vector_two)
        {
            Vector vector = this;
            vector.X = _x + vector_two.X;
            vector.Y = _y + vector_two.Y;
            return vector;
        }

        public static Vector Add(Vector vector_two, Vector vector)
        {
            vector.X += vector_two.X;
            vector.Y += vector_two.Y;
            return vector;
        }

        public static Vector Soustract(Vector vector_two, Vector vector)
        {
            vector.X -= vector_two.X;
            vector.Y -= vector_two.Y;
            return vector;
        }

        /// <summary>
        /// Limit values of X and Y.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public void Limit(double min, double max)
        {
            if (_x < min) _x = min;
            if (_x > max) _x = max;
            if (_y < min) _y = min;
            if (_y > max) _y = max;
        }

        public static Vector Move(double Speed, Vector vector)
        {
            double x = vector.X;
            double y = vector.Y;
            double Rx = vector.X / 100;
            double Ry = vector.Y / 100;

            do
            {
                x = x - Rx;
                y = y - Ry;
            }
            while (Math.Sqrt(x * x) + Math.Sqrt(y * y) > 2);

            x = Math.Round(x * Speed, 2);
            y = Math.Round(y * Speed, 2);

            if ( x + y == 0)
            {
                Rx = Rx / 10;
                Ry = Ry / 10;

                while (Math.Sqrt(x * x) + Math.Sqrt(y * y) < 2)
                {
                    x = x + Rx;
                    y = y + Ry;
                }

                x = Math.Round(x * Speed, 2);
                y = Math.Round(y * Speed, 2);
            }
            vector.X = x;
            vector.Y = y;

            return vector;
        }

        public static Vector MoveAndRunAway(double Speed, Vector vector)
        {
            double x = vector.X;
            double y = vector.Y;
            double Rx = x / 100;
            double Ry = y / 100;

            do
            {
                x = x - Rx;
                y = y - Ry;
            }
            while (Math.Sqrt(x * x) + Math.Sqrt(y * y) > 2);

            x = Math.Round(x * Speed, 2);
            y = Math.Round(y * Speed, 2);

            if (x + y == 0)
            {
                Rx = Rx / 10;
                Ry = Ry / 10;

                while (Math.Sqrt(x * x) + Math.Sqrt(y * y) < 2)
                {
                    x = x + Rx;
                    y = y + Ry;
                }

                x = Math.Round(x * Speed, 2);
                y = Math.Round(y * Speed, 2);
            }

            vector.X = -x;
            vector.Y = -y;

            return vector;
        }

        public static Vector MoveProjectile(int NbFram, Vector vector)
        {
            vector.X = Math.Round(vector.X / NbFram, 0);
            vector.Y = Math.Round(vector.Y / NbFram, 0);

            return vector;
        }

        public void SetZero()
        {
            _x = 0;
            _y = 0;
        }
    }
}