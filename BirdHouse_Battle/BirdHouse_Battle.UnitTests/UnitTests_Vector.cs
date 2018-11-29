using BirdHouse_Battle.Model;
using NUnit.Framework;
using System;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    public class UnitTests_Vector
    {
        [Test]
        public void Effect_Var_To_X_And_Y()
        {
            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;

            Vector v = new Vector(x, y);

            Assert.That(v.X, Is.EqualTo(x));
            Assert.That(v.Y, Is.EqualTo(y));

            Assert.That(v.X <= 1, Is.True);
            Assert.That(v.Y <= 1, Is.True);
            Assert.That(v.X >= -1, Is.True);
            Assert.That(v.Y >= -1, Is.True);

            x = rdm1.NextDouble() * 2 - 1;
            y = rdm1.NextDouble() * 2 - 1;

            Assert.That(v.X == x, Is.False);
            Assert.That(v.Y == y, Is.False);

            v = new Vector(x, y);

            Assert.That(v.X == x, Is.True);
            Assert.That(v.Y == y, Is.True);
        }

        [Test]
        public void Magnitude_Test()
        {
            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;

            Vector v = new Vector(x, y);

            Assert.That(v.Magnitude, Is.EqualTo(Math.Sqrt(x * x + y * y)));
        }

        [Test]
        public void Mutiply_Test()
        {
            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;

            Vector v = new Vector(x, y);

            double d = rdm1.NextDouble() * 2 - 1;
      

            Assert.That(v.X, Is.EqualTo(x * d));
            Assert.That(v.Y, Is.EqualTo(y * d));
        }

        [Test]
        public void Add_Test()
        {
            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;
            double x2 = rdm1.NextDouble() * 2 - 1;
            double y2 = rdm1.NextDouble() * 2 - 1;

            Vector v = new Vector(x, y);
            Vector v2 = new Vector(x2, y2);
            
            v = v.Add(v2);

            Assert.That(v.X, Is.EqualTo(x + x2));
            Assert.That(v.Y, Is.EqualTo(y + y2));
        }

        [Test]
        public void Soustract_Test()
        {
            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;
            double x2 = rdm1.NextDouble() * 2 - 1;
            double y2 = rdm1.NextDouble() * 2 - 1;

            Vector v = new Vector(x, y);
            Vector v2 = new Vector(x2, y2);


            Assert.That(v.X, Is.EqualTo(x - x2));
            Assert.That(v.Y, Is.EqualTo(y - y2));
        }
    }
}