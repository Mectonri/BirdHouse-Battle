using BirdHouse_Battle.Model;
using NUnit.Framework;
using System;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class UnitTests_Unit
    {
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

            t_red[0] = arena.SpawnUnit(t_red[0]/*, 0, 1*/);
            t_blue[0] = arena.SpawnUnit(t_blue[0]/*, 5, 5*/);
            t_blue[1] = arena.SpawnUnit(t_blue[1]/*, 7, 7*/);
            t_green[0] = arena.SpawnUnit(t_green[0]/*, 11, 11*/);

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
    }
}
