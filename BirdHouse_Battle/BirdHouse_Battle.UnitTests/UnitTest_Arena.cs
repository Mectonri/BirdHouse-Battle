using BirdHouse_Battle.Model;
using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class UnitTest_Arena
    {
        //[Test]
        //public void create_team()
        //{
        //    Arena arena = new Arena();

        //    Team firstTeam = arena.CreateTeam("firstTeam");
        //    Assert.That(firstTeam.Name, Is.EqualTo("firstTeam"));

        //    Team team = arena.FindTeam("firstTeam");
        //    Assert.That(team, Is.EqualTo(firstTeam));
        //}

        //[Test]
        //public void remove_team()
        //{
        //    Arena arena = new Arena();


        //    Assert.Throws<ArgumentException>(() => arena.RemoveTeam("Titi"));




        //    Team firstTeam = arena.CreateTeam("firstTeam");
        //    Team team = arena.FindTeam("firstTeam");
        //    Assert.That(team, Is.EqualTo(firstTeam));


        //    arena.RemoveTeam("firstTeam");
        //    Assert.Throws<ArgumentException>(() => arena.RemoveTeam("firstTeam"));

        //}


        [Test]
        public void Spawn_unit()
        {
            Arena arena = new Arena();
            Team firstTeam = arena.CreateTeam("firstTeam");
            firstTeam.AddGobelin(1);

            Unit[] tab = firstTeam.Find();
            Unit u = firstTeam.FindUnitByName(tab[0].Name);

            u = arena.SpawnUnit(u);
            Vector testVector = new Vector(0, 0);
            Vector vector = u.Location;
            Assert.That(vector, !Is.EqualTo(testVector));


        }

        [Test]
        public void specific_spawn_location()
        {
            Arena arena = new Arena();
            Team firstTeam = arena.CreateTeam("firstTeam");
            firstTeam.AddArcher(2);

            Unit[] tab = firstTeam.Find();
            Unit u = firstTeam.FindUnitByName(tab[0].Name);
            Unit u2 = firstTeam.FindUnitByName(tab[1].Name);
            
           
            Assert.Throws<ArgumentException>(() => u = arena.SpawnUnit(u, 0, 0));
            Assert.Throws<ArgumentException>(() => u = arena.SpawnUnit(u, 2000, 0));
            Assert.Throws<ArgumentException>(() => u = arena.SpawnUnit(u, 0, 20000));

            u2 = arena.SpawnUnit(u2, 1, 1);
            Assert.Throws<ArgumentException>(() => u = arena.SpawnUnit(u, 1, 1));
        }

        //[Test]
        //public void unit_cannot_move_on_a_already_used_location()
        //{
        //    Arena arena = new Arena();
        //    Team firstTeam = arena.CreateTeam("firstTeam");
        //    firstTeam.AddArcher(2);

        //    Unit[] tab = firstTeam.Find();
        //    Unit u = firstTeam.FindUnitByName(tab[0].Name);
        //    Unit u2 = firstTeam.FindUnitByName(tab[1].Name);

        //    Vector vector = new Vector(1, 1);
        //    Vector vector2 = new Vector(7, 7);

        //    u = arena.SpawnUnit(u, 1, 1);
        //    bool a = arena.Collision(vector);
        //    Assert.That(a, Is.EqualTo(true));
 

        //    a = arena.Collision(vector2);
        //    Assert.That(a, Is.EqualTo(false));

        //}

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


            secondTeam.AddGobelin(1);
            tab2 = secondTeam.Find();
            Unit thirdUnit = secondTeam.FindUnitByName(tab2[1].Name);

            Vector vector1 = new Vector(0, 0);
            Vector vector2 = new Vector(0, 1);
            Vector vector3 = new Vector(0, 2);
            firstUnit.Location = vector1;
            secondUnit.Location = vector2;
            thirdUnit.Location = vector3;

            Unit testUnit2 = arena.NearestEnemy(firstUnit);
            Assert.That(testUnit2, Is.EqualTo(secondUnit));
        }

        [Test]
        public void serialization_of_arena()
        {
            Arena sut = new Arena();

            sut.CreateTeam("red");
            Team team = new Team(sut, "blue", 125);

            team.AddArcher(2);
            team.AddPaladin(2);

            JToken jToken = sut.Serialise();

            Arena result = new Arena(jToken);

            IEnumerable<Team> teams =  result.GetTeams();

            Assert.That(teams.Count(), Is.EqualTo(2) );
            Assert.That(teams.Any(t => t.Name == "red"));
            Assert.That(teams.All(t => t.Context == result));
        }


    }
}
