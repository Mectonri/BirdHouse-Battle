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


        }

        [Test]
        public void Aquire_a_target()
        {

        }


    }
}
