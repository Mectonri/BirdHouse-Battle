﻿using NUnit.Framework;
using BirdHouse_Battle.Model;
using System;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    public class TeamTests
    {
        

        [Test]
        public void add_an_archer()
        {
            Arena arena = new Arena();
            Team t = arena.CreateTeam("RED");

            t.AddArcher(1);

            Unit[] tab = t.Find();
            Unit u = t.FindUnitByName(tab[0].Name);

            Assert.That(u, Is.SameAs(tab[0]));
        }

        /// <summary>
        /// Goblin is actually added to the team 
        /// </summary>
        [Test]
        public void add_a_goblin()
        {

            Arena arena = new Arena();
            Team t = arena.CreateTeam("RED");

            t.AddGobelin(1);

            Unit[] tab = t.Find();
            Unit u = t.FindUnitByName(tab[0].Name);

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
            //prices archer = 10.0, Gob = 5.0, pal = 12.5

            Arena ag = new Arena();
            Team tb = ag.CreateTeam("Blue");
            Team tr = ag.CreateTeam("RED");

            tb.AddArcher(5);
            tr.AddArcher(5);//50.0
            tr.AddGobelin(10);//50.0
            tr.AddPaladin(6);//75.0
            double r1 = tb.GoldCalculation(50.0);
            double r2 = tr.GoldCalculation(200.0);
            double r3 = tb.GoldCalculation(0.0);

            Assert.That(r1, Is.EqualTo(0.0));
            Assert.That(r2, Is.EqualTo(25.0));
        }
    }
}