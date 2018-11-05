using BirdHouse_Battle.Model;
using NUnit.Framework;
using System;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class UnitTests_Unit
    {
        [Test]
        public void Distance_Error()
        {
            Arena arena = new Arena();
            Team red = arena.CreateTeam("Red");
            Team green = arena.CreateTeam("Green");

            red.AddArcher(1);
            green.AddPaladin(1);

            Unit[] t_red = red.Find();
            Unit[] t_green = green.Find();

            t_red[0] = arena.SpawnUnit(t_red[0], 0, 1);
            t_green[0] = arena.SpawnUnit(t_green[0], 110, 110);

            t_red[0].SearchTarget();
            t_green[0].SearchTarget();

            for ( int i = 0; i < 10; i++ ) arena.Update();
        }

        [Test]
        public void Nearest_Enemy()
        {
            Arena arena = new Arena();
            Team red = arena.CreateTeam("Red");
            Team blue = arena.CreateTeam("Blue");
            Team green = arena.CreateTeam("Green");

            red.AddArcher(1);
            blue.AddGobelin(2);
            green.AddPaladin(1);

            Unit[] t_red = red.Find();
            Unit[] t_blue = blue.Find();
            Unit[] t_green = green.Find();

            t_red[0] = arena.SpawnUnit(t_red[0], 0, 1);
            t_blue[0] = arena.SpawnUnit(t_blue[0], 5, 5);
            t_blue[1] = arena.SpawnUnit(t_blue[1], 7, 7);
            t_green[0] = arena.SpawnUnit(t_green[0], 11, 11);

            t_red[0].SearchTarget();
            t_blue[0].SearchTarget();
            t_blue[1].SearchTarget();
            t_green[0].SearchTarget();

            Assert.That(t_red[0].Target, Is.EqualTo(t_blue[0]));
            Assert.That(t_blue[0].Target, Is.EqualTo(t_red[0]));
            Assert.That(t_blue[1].Target, Is.EqualTo(t_green[0]));
            Assert.That(t_green[0].Target, Is.EqualTo(t_blue[1]));
        }

        [Test]
        public void Die_Null_Context()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("red");

            Paladin Pal = new Paladin(team, arena);
            Gobelin Gob = new Gobelin(team, arena);
            Archer Arc = new Archer(team, arena);

            Assert.That(Pal.Team, Is.EqualTo(team));
            Pal.DieNullContext();
            Assert.That(Pal.Team, Is.EqualTo(null));

            Assert.That(Gob.Team, Is.EqualTo(team));
            Gob.DieNullContext();
            Assert.That(Gob.Team, Is.EqualTo(null));

            Assert.That(Arc.Team, Is.EqualTo(team));
            Arc.DieNullContext();
            Assert.That(Arc.Team, Is.EqualTo(null));
        }

        [Test]
        public void Try_Update_Horizon()
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

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(0, 3.09)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(0, 11)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(0, 8.82)));
        }

        [Test]
        public void Try_Update_Vertical()
        {
            Arena arena = new Arena();
            Team red = arena.CreateTeam("red");
            Team blue = arena.CreateTeam("blue");

            red.AddPaladin(1);
            blue.AddPaladin(1);

            Unit[] t_red = red.Find();
            Unit[] t_blue = blue.Find();

            t_red[0] = arena.SpawnUnit(t_red[0], 0, 1);
            t_blue[0] = arena.SpawnUnit(t_blue[0], 10, 1);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(2.09, 1)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(10, 1)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(7.82, 1)));
        }

        [Test]
        public void Try_Update_Diagonal_Positive()
        {
            Arena arena = new Arena();
            Team red = arena.CreateTeam("red");
            Team blue = arena.CreateTeam("blue");

            red.AddPaladin(1);
            blue.AddPaladin(1);

            Unit[] t_red = red.Find();
            Unit[] t_blue = blue.Find();

            t_red[0] = arena.SpawnUnit(t_red[0], 0, 1);
            t_blue[0] = arena.SpawnUnit(t_blue[0], 5, 6);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(1.05, 2.05)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(5, 6)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(3.91, 4.91)));
        }

        [Test]
        public void Try_Update_Diagonal_Negative()
        {
            Arena arena = new Arena();
            Team red = arena.CreateTeam("red");
            Team blue = arena.CreateTeam("blue");

            red.AddPaladin(1);
            blue.AddPaladin(1);

            Unit[] t_red = red.Find();
            Unit[] t_blue = blue.Find();

            t_red[0] = arena.SpawnUnit(t_red[0], 0, 1);
            t_blue[0] = arena.SpawnUnit(t_blue[0], -5, 6);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(-1.05, 2.05)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(-5, 6)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(-3.91, 4.91)));
        }
    }
}
