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
            throw new ArgumentNullException();
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
