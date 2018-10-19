using BirdHouse_Battle.Model;
using NUnit.Framework;
using System;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class UnitTests_Archer
    {
        [Test]
        public void Try_Take_Damages()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Archer Arc = new Archer(team, arena);

            Assert.Throws<ArgumentException>(() => Arc.TakeDamages(-1));

            double Life = Arc.Life;

            Arc.TakeDamages(Arc.Armor + 1);
            Assert.That(Arc.Life, Is.EqualTo(Life - 1));

            Life--;

            Arc.TakeDamages(0);
            Assert.That(Arc.Life, Is.EqualTo(Life));
            Arc.TakeDamages(Arc.Armor);
            Assert.That(Arc.Life, Is.EqualTo(Life));
            Arc.TakeDamages(Arc.Life + Arc.Armor);
            Assert.That(Arc.Life, Is.EqualTo(0));
        }

        [Test]
        public void Try_Die()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Archer Arc = new Archer(team, arena);

            Arc.TakeDamages(Arc.Life + Arc.Armor);
            Assert.That(Arc.IsDead, Is.True);

            Arc = new Archer(team, arena);

            Arc.TakeDamages(Arc.Life + Arc.Armor + 1);
            Assert.That(Arc.IsDead, Is.True);

            Arc = new Archer(team, arena);

            Arc.TakeDamages(Arc.Life + Arc.Armor - 1);
            Assert.That(Arc.IsDead, Is.False);
        }

        [Test]
        public void Try_New_Direction()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Archer Arc = new Archer(team, arena);

            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;

            Arc.Location = new Vector(x, y);

            Archer target = new Archer(team, arena);

            double x2 = rdm1.NextDouble() * 2 - 1;
            double y2 = rdm1.NextDouble() * 2 - 1;

            target.Location = new Vector(x2, y2);
            Arc.Target = target;

            Arc.NewDirection();
            Assert.That(Arc.Direction, Is.EqualTo(new Vector(x - x2, y - y2)));
        }

        [Test]
        public void Try_InRange()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Archer Arc = new Archer(team, arena);
            Arc.Location = new Vector(0, 0);

            Archer target = new Archer(team, arena);
            Random rdm = new Random();
            double x;
            double y;

            for (int i = 0; i < 42; i++)
            {
                x = rdm.NextDouble() * 2 - 1;
                y = rdm.NextDouble() * 2 - 1;

                target.Location = new Vector(x, y);
                Arc.Target = target;

                Arc.NewDirection();

                if (Arc.Location.Soustract(Arc.Target.Location).Magnitude <= Arc.Range) Assert.That(Arc.InRange, Is.True);
                if (Arc.Location.Soustract(Arc.Target.Location).Magnitude > Arc.Range) Assert.That(Arc.InRange, Is.False);
            }
        }
    }
}