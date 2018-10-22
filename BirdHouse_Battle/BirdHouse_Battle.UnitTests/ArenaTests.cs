using BirdHouse_Battle.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class ArenaTests
    {
        [Test]
        public void create_team()
        {
            Arena arena = new Arena();

            Team firstTeam = arena.CreateTeam("firstTeam");
            Assert.That(firstTeam.Name, Is.EqualTo("firstTeam"));

            Team team = arena.FindTeam("firstTeam");
            Assert.That(team, Is.EqualTo(firstTeam));
        }

        [Test]
        public void remove_team()
        {
            Arena arena = new Arena();


            Assert.Throws<ArgumentException>(() => arena.RemoveTeam("Titi"));




            Team firstTeam = arena.CreateTeam("firstTeam");
            Team team = arena.FindTeam("firstTeam");
            Assert.That(team, Is.EqualTo(firstTeam));


            arena.RemoveTeam("firstTeam");
            Assert.Throws<ArgumentException>(() => arena.RemoveTeam("firstTeam"));

        }


        [Test]
        public void Spawn_unit()
        {
            Arena arena = new Arena();
            Team firstTeam = arena.CreateTeam("firstTeam");
            firstTeam.AddGobelin(1);

            Unit[] tab = firstTeam.Find();
            Unit u = firstTeam.FindUnitByName(tab[0].Name);

           u= arena.SpawnUnit(u);
            Vector testVector = new Vector(0,0);
            Vector vector = u.Location;
            Assert.That(vector, !Is.EqualTo(testVector) );
        }

        [Test]
        public void Aquire_a_target()
        {
            Arena arena = new Arena();
            Team firstTeam = arena.CreateTeam("firstTeam");
            Team secondTeam = arena.CreateTeam("secondTeam");
            firstTeam.AddGobelin(1);
            secondTeam.AddGobelin(1);

            Unit[] tab = firstTeam.Find();
            Unit firstUnit = firstTeam.FindUnitByName(tab[0].Name);

            firstUnit = arena.SpawnUnit(firstUnit);



            Unit[] tab2 = secondTeam.Find();
            Unit secondUnit = secondTeam.FindUnitByName(tab2[0].Name);

            secondUnit = arena.SpawnUnit(secondUnit);

            Unit testUnit = arena.NearestEnemy(firstUnit);

            Assert.That(testUnit, Is.EqualTo(secondUnit));

        }


    }
}
