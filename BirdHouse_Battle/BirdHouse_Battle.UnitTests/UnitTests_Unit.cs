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
            Team team = arena.CreateTeam("red");
            Team team2 = arena.CreateTeam("blue");
            Team team3 = arena.CreateTeam("green");

            team.AddArcher(1);
            team2.AddPaladin(2);
            team3.AddGobelin(1);

            Unit[] tab = team.Find();// Useless but no other option.
            Unit[] tab2 = team2.Find();// Useless but no other option.
            Unit[] tab3 = team3.Find();// Useless but no other option.

            tab[0].Location = new Vector(0, 0);// Can't give real location to the unit.

            tab2[0].Location = new Vector(5, 5);// Can't give real location to the unit.
            tab2[1].Location = new Vector(7, 7);// Can't give real location to the unit.

            tab3[0].Location = new Vector(8, 8);// Can't give real location to the unit.

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
