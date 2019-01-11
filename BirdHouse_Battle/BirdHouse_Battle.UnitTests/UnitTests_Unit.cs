﻿using BirdHouse_Battle.Model;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


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

            Paladin Pal = new Paladin(arena, team,  1);
            Goblin Gob = new Goblin(arena, team,  1);
            Archer Arc = new Archer(arena, team, 1);

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
            t_blue[0] = arena.SpawnUnit(t_blue[0], 0, 31);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(0, 3.16)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(0, 31)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(0, 28.66)));
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
            t_blue[0] = arena.SpawnUnit(t_blue[0], 30, 1);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(2.16, 1)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(30, 1)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(27.66, 1)));
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
            t_blue[0] = arena.SpawnUnit(t_blue[0], 15, 16);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(1.08, 2.08)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(15, 16)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(13.83, 14.83)));
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
            t_blue[0] = arena.SpawnUnit(t_blue[0], -15, 16);

            t_red[0].SearchTarget();
            t_red[0].Update();

            Assert.That(t_red[0].Location, !Is.EqualTo(new Vector(0, 1)));
            Assert.That(t_red[0].Location, Is.EqualTo(new Vector(-1.3, 2.3)));

            t_blue[0].SearchTarget();
            t_blue[0].Update();

            Assert.That(t_blue[0].Location, !Is.EqualTo(new Vector(-15, 16)));
            Assert.That(t_blue[0].Location, Is.EqualTo(new Vector(-13.620000000000001, 14.620000000000001)));
        }

        [Test]
        public void Serialize_a_unit_as_archer()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("blue");

            Archer sut = new Archer(arena, team, 1);

            JToken jToken = sut.Serialize();
        }

        [Test]
        public void Deserialize_a_unit()
        {
            Arena arena = new Arena(); 
            Team team = arena.CreateTeam("blue");
            
            Archer a = new Archer(arena, team, 1);
           
            string fileName = Path.GetTempFileName();

            {
                JToken jToken = a.Serialize();
                using (FileStream fs = File.OpenWrite(fileName))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    jToken.WriteTo(jw);
                }
            }

            {
                using (FileStream fs = File.OpenRead(fileName))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonTextReader jr = new JsonTextReader(sr))
                {
                    JToken jToken = JToken.ReadFrom(jr);
                    Archer archer = new Archer(arena, team, jToken);
                    Archer archer1 = new Archer(arena, team, 22);

                    Assert.That(archer.Life, Is.EqualTo(12.0));
                    Assert.That(archer.Troop, Is.EqualTo("archer"));
                    Assert.That(archer.Equals(archer1), Is.False);
                    Assert.That(archer.Name, Is.Not.EqualTo(archer1.Name));
                }
            }
        }

        public static void CheckEquals(Unit u1, Unit u2)
        {
            Assert.That(u1.GetType(), Is.SameAs(u2.GetType()));
            Assert.That(u1.Troop, Is.SameAs(u2.Troop));
            //if (u1 is Balista b1 && u2 is Balista b2)
            //{

            //}
            //else if (u1 is Paladin p1 && u2 is Paladin p2)
            //{

            //}
            //else
            //{

            //}

        }
    }
}
