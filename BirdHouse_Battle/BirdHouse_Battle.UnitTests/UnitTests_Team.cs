using NUnit.Framework;
using BirdHouse_Battle.Model;
using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    public class UnitTest_Team
    {
        

        [Test]
        public void add_an_archer()
        {
            Arena arena = new Arena();
            Team t1 = arena.CreateTeam("RED");

            t1.AddArcher(1);

            Unit[] tab = t1.Find();
            Unit u = t1.FindUnitByName(tab[0].Name);

            Assert.That(u, Is.SameAs(tab[0]));
        }

        /// <summary>
        /// Goblin is actually added to the team 
        /// </summary>
        [Test]
        public void add_a_goblin()
        {

            Arena arena = new Arena();
            Team t1 = arena.CreateTeam("RED");

            t1.AddGobelin(1);

            Unit[] tab = t1.Find();
            Unit u = t1.FindUnitByName(tab[0].Name);

            Assert.That(u, Is.SameAs(tab[0]));
        }

        [Test]
        public void add_a_paladin()
        {

            Arena arena = new Arena();
            Team t = arena.CreateTeam("RED");

            t.AddPaladin(1);

            Unit[] tab = t.Find();
            Unit u = t.FindUnitByName(tab[0].Name);

            Assert.That(u, Is.SameAs(tab[0]));
        }

        [Test]
        public void archer_count_is_exact()
        {
            Arena a4 = new Arena();
            Team t4 = a4.CreateTeam("RED");

            t4.AddArcher(10);

            Assert.That(t4.UnitCount, Is.EqualTo(10));
        }

        [Test]
        public void unit_count_is_exact()
        {
            Arena a4 = new Arena();
            Team t4 = a4.CreateTeam("RED");

            t4.AddArcher(10);
            t4.AddPaladin(5);
            t4.AddGobelin(3);

            Assert.That(t4.UnitCount, Is.EqualTo(18));
        }

        [Test]
        public void remove_unit_from_team()
        {
            Arena ab = new Arena();
            Team t = ab.CreateTeam("Yellow");

            t.AddArcher(2);
            Unit[] tab = t.Find();
            Unit u = t.FindUnitByName(tab[0].Name);
         
            Assert.That(u, Is.SameAs(tab[0]));
            t.RemoveUnit(u);
             
            Assert.That(t.UnitCount, Is.EqualTo(1));
            Assert.That(t.Acount, Is.EqualTo(1));
        }

        [Test]
        public void gold_calculation()
        {
            //prices archer = 10.0, Gob = 3.0, pal = 12.5

            Arena ag = new Arena();
            Team tb = ag.CreateTeam("Blue");
            Team tr = ag.CreateTeam("RED");

            tb.AddArcher(5);
            tr.AddArcher(5);//50.0
            tr.AddGobelin(10);//30.0
            tr.AddPaladin(6);//75.0
            double r1 = tb.GoldCalculation(50.0);
            double r2 = tr.GoldCalculation(200.0);
            double r3 = tb.GoldCalculation(0.0);

            Assert.That(r1, Is.EqualTo(0.0));
            Assert.That(r2, Is.EqualTo(45.0));
        }

        [Test]
        public void team_is_wiped_when_there_is_no_unit_left()
        {
            Arena a = new Arena();
            Team t = a.CreateTeam("RED");
            Team t2 = a.CreateTeam("BLUE");

            t.AddArcher(0);
            t.AddGobelin(1);
            t2.AddArcher(1);
            Unit[] tab = t.Find();
            Unit u = t.FindUnitByName(tab[0].Name);
            t.RemoveUnit(u);

            Assert.That(t.UnitCount, Is.EqualTo(0));
            Assert.That(t.IsWiped, Is.True);
            Assert.That(t2.UnitCount, Is.EqualTo(t2.Acount));
            Assert.That(t2.UnitCount, Is.EqualTo(1));
            Assert.That(t2.IsWiped, Is.False);
        }

        [Test]
        public void team_limit_is_effective()
        {
            Arena a = new Arena();
            Team t = a.CreateTeam("RED");
            Team t1 = a.CreateTeam("Green");
            t1.AddGobelin(125);

            Assert.Throws<ArgumentException>(() => t.AddArcher(126));
            Assert.Throws<ArgumentException>(() => t1.AddGobelin(1));
            Assert.Throws<ArgumentException>(() => t.AddPaladin(260));
        }

        [Test]
        public void serialize_a_team()
        {
            Arena arena = new Arena();
            Team sut = arena.CreateTeam("blue");
            sut.AddArcher(2);
            //sut.AddBalista(2);
            sut.AddCatapult(2);
            sut.AddDrake(2);
            sut.AddGobelin(2);
            sut.AddPaladin(2);

            JToken jToken = sut.Serialize();

            Team result = new Team(arena, "blue", jToken);
            IEnumerable<Unit> units = result.GetUnits();
            Assert.That(units.Count(), Is.EqualTo(10));
            Assert.That(units.Any(u => u.Life == 12.0 && u.Speed == 1.8));
            
            Assert.That(units.All(u => u.Team == result));
        }
    }
}