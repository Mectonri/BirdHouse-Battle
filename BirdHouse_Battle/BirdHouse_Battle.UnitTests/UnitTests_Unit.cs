using BirdHouse_Battle.Model;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;


namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    public class UnitTests_Unit
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
                    Archer archer1 = new Archer(arena, team, 1);
                    
                    CheckEquals(archer, archer1);

                }
            }
        }

        public static void CheckEquals(Unit u1, Unit u2)
        {
            Assert.That(u1.GetType(), Is.SameAs(u2.GetType()));
            Assert.That(u1.Troop, Is.EqualTo(u2.Troop));

            if (u1 is Archer a1 && u2 is Archer a2)
            {
                Assert.That(a1.Arena == a2.Arena);

                Assert.That(a1.Team == a2.Team);

                Assert.That(a1.Life == a2.Life && a1.Life == 12.0);
                Assert.That(a1.Speed == a2.Speed && a2.Speed == 1.80);
                Assert.That(a1.Range == a2.Range && a1.Range == 135.0);
                Assert.That(a1.UnitPrice == a2.UnitPrice && a2.UnitPrice == 10.0);
                Assert.That(a1.Strength == a2.Strength && a1.Strength == 4);
                Assert.That(a1.Armor == a2.Armor && a2.Armor == 1);
                Assert.That(a1.Disposition == a2.Disposition && a1.Disposition == "Order");
                Assert.That(a1.Fly == a2.Fly && a1.Fly == false );
                Assert.That(a1.Distance == a2.Distance && a2.Distance == true);
                Assert.That(a1.DistanceOnly == a2.DistanceOnly && a1.DistanceOnly == false);
                Assert.That(a1.Name == a2.Name);
                Assert.That(a1.Troop == a2.Troop && a2.Troop == "archer");

            }
            else if (u1 is Balista b1 && u2 is Balista b2)
            {
                Assert.That(b1.Arena == b2.Arena);

                Assert.That(b1.Team == b2.Team);

                Assert.That(b1.Life == b2.Life && b2.Life == 25.0);
                Assert.That(b1.Speed == b2.Speed && b2.Speed == 0.70);
                Assert.That(b1.Range == b2.Range && b1.Range == 200.0);
                Assert.That(b1.UnitPrice == b2.UnitPrice && b2.UnitPrice == 30.0);
                Assert.That(b1.Strength == b2.Strength && b1.Strength == 15);
                Assert.That(b1.Armor == b2.Armor && b1.Armor == 2);
                Assert.That(b1.Disposition == b2.Disposition && b1.Disposition == "order");
                Assert.That(b1.Fly == b2.Fly && b1.Fly == false);
                Assert.That(b1.Distance == b2.Distance && b2.Distance == false);
                Assert.That(b1.DistanceOnly == b2.DistanceOnly && b1.DistanceOnly == true);
                Assert.That(b1.Name == b2.Name);
                Assert.That(b1.Troop == b2.Troop && b2.Troop == "archer");
            }
            else if (u1 is Catapult c1 && u2 is Catapult c2)
            {
                Assert.That(c1.Arena == c2.Arena);

                Assert.That(c1.Team == c2.Team);

                Assert.That(c1.Life == c2.Life && c2.Life == 30.0);
                Assert.That(c1.Speed == c2.Speed && c2.Speed == 0.5);
                Assert.That(c1.Range == c2.Range && c1.Range == 250.0);
                Assert.That(c1.UnitPrice == c2.UnitPrice && c2.UnitPrice == 40.0);
                Assert.That(c1.Strength == c2.Strength && c1.Strength == 20);
                Assert.That(c1.Armor == c2.Armor && c1.Armor == 3);
                Assert.That(c1.Disposition == c2.Disposition && c1.Disposition == "order");
                Assert.That(c1.Fly == c2.Fly && c1.Fly == false);
                Assert.That(c1.Distance == c2.Distance && c2.Distance == false);
                Assert.That(c1.DistanceOnly == c2.DistanceOnly && c1.DistanceOnly == false);
                Assert.That(c1.Name == c2.Name);
                Assert.That(c1.Troop == c2.Troop && c2.Troop == "catapult");
            }
            else if (u1 is Drake d1 && u2 is Drake d2)
            {
                Assert.That(d1.Arena == d2.Arena);

                Assert.That(d1.Team == d2.Team);

                Assert.That(d1.Life == d2.Life                 && d2.Life == 10);
                Assert.That(d1.Speed == d2.Speed               && d2.Speed == 1.5);
                Assert.That(d1.Range == d2.Range               && d1.Range == 18.0);
                Assert.That(d1.UnitPrice == d2.UnitPrice       && d2.UnitPrice == 15.0);
                Assert.That(d1.Strength == d2.Strength         && d1.Strength == 7);
                Assert.That(d1.Armor == d2.Armor               && d1.Armor == 0);
                Assert.That(d1.Disposition == d2.Disposition   && d1.Disposition == "Chaos");
                Assert.That(d1.Fly == d2.Fly                   && d1.Fly == true);
                Assert.That(d1.Distance == d2.Distance         && d2.Distance == true);
                Assert.That(d1.DistanceOnly == d2.DistanceOnly && d1.DistanceOnly == false);
                Assert.That(d1.Name == d2.Name);
                Assert.That(d1.Troop == d2.Troop && d2.Troop == "drake");
            }
            else if ( u1 is Goblin g1 && u2 is Goblin g2)
            {
                Assert.That(g1.Arena == g2.Arena);

                Assert.That(g1.Team == g2.Team);

                Assert.That(g1.Life == g2.Life && g2.Life == 8.0);
                Assert.That(g1.Speed == g2.Speed && g2.Speed == 2.5);
                Assert.That(g1.Range == g2.Range && g1.Range == 4.0);
                Assert.That(g1.UnitPrice == g2.UnitPrice && g2.UnitPrice == 3.0);
                Assert.That(g1.Strength == g2.Strength && g1.Strength == 15);
                Assert.That(g1.Armor == g2.Armor && g1.Armor == 0);
                Assert.That(g1.Disposition == g2.Disposition && g1.Disposition == "Chaos");
                Assert.That(g1.Fly == g2.Fly && g1.Fly == false);
                Assert.That(g1.Distance == g2.Distance && g2.Distance == false);
                Assert.That(g1.DistanceOnly == g2.DistanceOnly && g1.DistanceOnly == false);
                Assert.That(g1.Name == g2.Name);
                Assert.That(g1.Troop == g2.Troop && g2.Troop == "goblin");
            }
            else if (u1 is Paladin p1 && u2 is Paladin p2)
            {
                Assert.That(p1.Arena == p2.Arena);

                Assert.That(p1.Team == p2.Team);

                Assert.That(p1.Life == p2.Life                 && p2.Life == 18.0);
                Assert.That(p1.Speed == p2.Speed               && p2.Speed == 1.20);
                Assert.That(p1.Range == p2.Range               && p1.Range == 15.0);
                Assert.That(p1.UnitPrice == p2.UnitPrice       && p2.UnitPrice == 12.5);
                Assert.That(p1.Strength == p2.Strength         && p1.Strength == 4);
                Assert.That(p1.Armor == p2.Armor               && p1.Armor == 4);
                Assert.That(p1.Disposition == p2.Disposition   && p1.Disposition == "Order");
                Assert.That(p1.Fly == p2.Fly                   && p1.Fly == false);
                Assert.That(p1.Distance == p2.Distance         && p2.Distance == false);
                Assert.That(p1.DistanceOnly == p2.DistanceOnly && p1.DistanceOnly == false);
                Assert.That(p1.Name == p2.Name);
                Assert.That(p1.Troop == p2.Troop               && p2.Troop == "paladin");
            }
            else
            {
                Assert.That(1 == 0);
            }

        }
    }
}
