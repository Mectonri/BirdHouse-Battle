using BirdHouse_Battle.Model;
using NUnit.Framework;
using System;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class UnitTests_Paladin
    {
        [Test]
        public void Try_Take_Damages()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Paladin Pal = new Paladin(team, arena);

            Assert.Throws<ArgumentException>(() => Pal.TakeDamages(-1));

            double Life = Pal.Life;

            Pal.TakeDamages(Pal.Armor + 1);
            Assert.That(Pal.Life, Is.EqualTo(Life - 1));

            Life--;

            Pal.TakeDamages(0);
            Assert.That(Pal.Life, Is.EqualTo(Life));
            Pal.TakeDamages(Pal.Armor);
            Assert.That(Pal.Life, Is.EqualTo(Life));
            Pal.TakeDamages(Pal.Life + Pal.Armor);
            Assert.That(Pal.Life, Is.EqualTo(0));
        }

        [Test]
        public void Try_Die()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Paladin Pal = new Paladin(team, arena);

            Pal.TakeDamages(Pal.Life+Pal.Armor);
            Assert.That(Pal.IsDead, Is.True);

            Pal = new Paladin(team, arena);

            Pal.TakeDamages(Pal.Life + Pal.Armor + 1);
            Assert.That(Pal.IsDead, Is.True);

            Pal = new Paladin(team, arena);

            Pal.TakeDamages(Pal.Life + Pal.Armor - 1);
            Assert.That(Pal.IsDead, Is.False);
        }

        [Test]
        public void Try_New_Direction()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Paladin Pal = new Paladin(team, arena);

            Random rdm1 = new Random();
            double x = rdm1.NextDouble() * 2 - 1;
            double y = rdm1.NextDouble() * 2 - 1;

            Pal.Location = new Vector(x, y);

            Paladin target = new Paladin(team, arena);
            
            double x2 = rdm1.NextDouble() * 2 - 1;
            double y2 = rdm1.NextDouble() * 2 - 1;

            target.Location = new Vector(x2, y2);
            Pal.Target = target;

            Pal.NewDirection();
            Assert.That(Pal.Direction, Is.EqualTo(new Vector(x - x2, y - y2)));
        }

        [Test]
        public void Try_InRange()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Paladin Pal = new Paladin(team, arena);
            Pal.Location = new Vector(0, 0);

            Paladin target = new Paladin(team, arena);
            Random rdm = new Random();
            double x;
            double y;

            for (int i = 0; i < 42; i++)
            {
                x = rdm.NextDouble() * 2 - 1;
                y = rdm.NextDouble() * 2 - 1;

                target.Location = new Vector(x, y);
                Pal.Target = target;

                Pal.NewDirection();

                if (Pal.Location.Soustract(Pal.Target.Location).Magnitude <= Pal.Range) Assert.That(Pal.InRange, Is.True);
                if (Pal.Location.Soustract(Pal.Target.Location).Magnitude > Pal.Range) Assert.That(Pal.InRange, Is.False);
            }
        }

        [Test]
        public void Try_Update()
        {
            Arena arena = new Arena();
            Team red = arena.CreateTeam("red");
            Team blue = arena.CreateTeam("blue");

            red.AddPaladin(1);
            blue.AddPaladin(1);

            Unit[] t_red = red.Find();
            Unit[] t_blue = blue.Find();

            t_red[0] = arena.SpawnUnit(t_red[0], 0, 1);
            t_blue[0] = arena.SpawnUnit(t_blue[0], 0, 11);

            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));

            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(0, 11)));
        }
    }
}