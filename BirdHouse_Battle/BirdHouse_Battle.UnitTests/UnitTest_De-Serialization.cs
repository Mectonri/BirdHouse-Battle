using BirdHouse_Battle.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BirdHouse_Battle.UnitTests
{
    [TestFixture]
    class UnitTest_De_Serialization
    {
        [Test]
        public void serialize_an_archer()
        {
            Arena arena = new Arena();
            Team team = new Team(arena, "blue", 125);

            Archer sut = new Archer(arena, team, 1);

            team.Units.Add(sut.Name, sut);
            team.Acount++;

            JToken jToken = sut.Serialize();

            Archer result = new Archer(arena, team, jToken);

            Assert.That(sut.Arena, Is.EqualTo(arena));
            Assert.That(result.Arena, Is.EqualTo(arena));

            Assert.That(sut.Team, Is.EqualTo(team));
            Assert.That(result.Team, Is.EqualTo(team));

            Assert.That(sut.Life == 12.0 && sut.Speed == 1.8);
            Assert.That(result.Life == 12.0 && result.Speed == 1.8);

            UnitTests_Unit.CheckEquals(sut, result);
        }

        [Test]
        public void deserialize_an_archer()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("blue");

            Archer sut = new Archer(arena, team, 1);

            team.Units.Add(sut.Name, sut);
            team.Acount++;

            string fileName = Path.GetTempFileName();

            {
                JToken token = sut.Serialize();
                using (FileStream fs = File.OpenWrite(fileName))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    token.WriteTo(jw);
                }
            }

            {
                using (FileStream fs = File.OpenRead(fileName))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonTextReader jr = new JsonTextReader(sr))
                {
                    JToken token = JToken.ReadFrom(jr);
                    Archer result = new Archer(arena, team, token);

                    Assert.That(arena.TeamCount, Is.EqualTo(1));
                    Assert.That(team.UnitCount, Is.EqualTo(1));
                    Assert.That(team.Acount, Is.EqualTo(1));

                    Assert.That(sut.Arena, Is.EqualTo(arena));
                    Assert.That(result.Arena, Is.EqualTo(arena));

                    Assert.That(sut.Team, Is.EqualTo(team));
                    Assert.That(result.Team, Is.EqualTo(team));

                    Assert.That(sut.Life == 12.0 && sut.Speed == 1.8);
                    Assert.That(result.Life == 12.0 && result.Speed == 1.8);

                    Assert.That(sut.Team, Is.EqualTo(result.Team));
                }
            }
        }

        [Test]
        public void serialize_a_balista()
        {
            Arena arena = new Arena();
            Team team = new Team(arena, "blue", 125);

            Balista sut = new Balista(arena, team, 1);
            team.Units.Add(sut.Name, sut);
            team.Bcount++;

            JToken jToken = sut.Serialize();

            Balista result = new Balista(arena, team, jToken);

            Assert.That(sut.Arena, Is.EqualTo(arena));
            Assert.That(result.Arena, Is.EqualTo(arena));

            Assert.That(sut.Team, Is.EqualTo(team));
            Assert.That(result.Team, Is.EqualTo(team));

            Assert.That(sut.Life == 25.0 && sut.Speed == 0.70);
            Assert.That(result.Life == 25.0 && result.Speed == 0.70);

            //Assert.That(sut, Is.EqualTo(result));
        }

        [Test]
        public void deserialize_a_balista()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("blue");

            Balista sut = new Balista(arena, team, 1);

            team.Units.Add(sut.Name, sut);
            team.Acount++;

            string fileName = Path.GetTempFileName();

            {
                JToken token = sut.Serialize();
                using (FileStream fs = File.OpenWrite(fileName))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    token.WriteTo(jw);
                }
            }

            {
                using (FileStream fs = File.OpenRead(fileName))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonTextReader jr = new JsonTextReader(sr))
                {
                    JToken token = JToken.ReadFrom(jr);
                    Balista result = new Balista(arena, team, token);

                    Assert.That(arena.TeamCount, Is.EqualTo(1));
                    Assert.That(team.UnitCount, Is.EqualTo(1));
                    Assert.That(team.Acount, Is.EqualTo(1));

                    Assert.That(sut.Arena, Is.EqualTo(arena));
                    Assert.That(result.Arena, Is.EqualTo(arena));

                    Assert.That(sut.Team, Is.EqualTo(team));
                    Assert.That(result.Team, Is.EqualTo(team));

                    Assert.That(sut.Life == 25.0 && sut.Speed == 0.7);
                    Assert.That(result.Life == 25.0 && result.Speed == 0.7);

                    Assert.That(sut.Team, Is.EqualTo(result.Team));
                }
            }
        }

        [Test]
        public void serialize_a_unit()
        {
            Arena arena = new Arena();

            Team team = new Team(arena, "blue", 125);

            Archer archer = new Archer(arena, team, 1);
            team.Units.Add(1, archer);
            team.Acount++;
            Balista balista = new Balista(arena, team, 2);
            team.Units.Add(2, balista);
            team.Bcount++;
            Catapult catapult = new Catapult(arena, team, 3);
            team.Units.Add(3, catapult);
            team.Ccount++;
            Drake drake = new Drake(arena, team, 4);
            team.Units.Add(4, drake);
            team.Dcount++;

           

            //Team result = new Team(token);
            //IEnumerable<Unit> units = result.GetUnits();
            //Assert.That(units.Count(), Is.EqualTo(2));
            //Assert.That(units.Any(u => u.Health == 20.0 && u.Speed == 20.0));
            //Assert.That(units.Any(u => u.Health == 10.0 && u.Speed == 15.0));
            //Assert.That(units.All(u => u.Team == result));


        }

        [Test]
        public void deserialize_a_unit()
        {

        }

        [Test]
        public void serialize_a_team()
        {
            Arena arena = new Arena();
            Team sut = arena.CreateTeam("blue");

            Archer archer = new Archer(arena, sut, 1);
            sut.Units.Add(1, archer);
            sut.Acount++;
            Balista balista = new Balista(arena, sut, 2);
            sut.Units.Add(2, balista);
            sut.Bcount++;
            Catapult catapult = new Catapult(arena, sut, 3);
            sut.Units.Add(3, catapult);
            sut.Ccount++;
            Drake drake = new Drake(arena, sut, 4);
            sut.Units.Add(4, drake);
            sut.Dcount++;

            JToken jToken = sut.Serialize();

            Team result = new Team(arena, jToken);

            IEnumerable<Unit> units = result.GetUnits();
            Assert.That(units.Count(), Is.EqualTo(4));
            
            Assert.That(units.Any(u => u.Life == 12.0 && u.Speed == 1.8 && u.Troop == "archer"));
            Assert.That(units.Any(u => u.Life == 25.0 && u.Speed == 0.70 && u.Troop == "balista"));
            Assert.That(units.Any(u => u.Life == 30.0 && u.Speed == 0.50 && u.Troop == "catapult"));
            Assert.That(units.Any(u => u.Life == 10 && u.Speed == 1.5 && u.Troop == "drake"));

            Assert.That(units.All(u => u.Team == result));

        }

        [Test]
        public void deserialize_a_team()
        {
            Arena arena = new Arena();
            Team sut = new Team(arena, "blue", 125);

            Archer archer = new Archer(arena, sut, 1);
            sut.Units.Add(1, archer);
            sut.Acount++;
            Balista balista = new Balista(arena, sut, 2);
            sut.Units.Add(2, balista);
            sut.Bcount++;
            Catapult catapult = new Catapult(arena, sut, 3);
            sut.Units.Add(3, catapult);
            sut.Ccount++;
            Drake drake = new Drake(arena, sut, 4);
            sut.Units.Add(4, drake);
            sut.Dcount++;

            string fileName = Path.GetTempFileName();

            {
                JToken token = sut.Serialize();
                using (FileStream fs = File.OpenWrite(fileName))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    token.WriteTo(jw);
                }
            }

            {
                using (FileStream fs = File.OpenRead(fileName))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonTextReader jr = new JsonTextReader(sr))
                {
                    JToken token = JToken.ReadFrom(jr);
                    Team team = new Team(arena, token);
                    IEnumerable<Unit> units = team.GetUnits();
                    Assert.That(units.Count(), Is.EqualTo(sut.UnitCount));
                    Assert.That(units.Count(), Is.EqualTo(4));

                    Assert.That(units.Any(u => u.Life == 12.0 && u.Speed == 1.8 && u.Troop == "archer"));
                    Assert.That(units.Any(u => u.Life == 25.0 && u.Speed == 0.70 && u.Troop == "balista"));
                    Assert.That(units.Any(u => u.Life == 30.0 && u.Speed == 0.50 && u.Troop == "catapult"));
                    Assert.That(units.Any(u => u.Life == 10 && u.Speed == 1.5 && u.Troop == "drake"));


                    Assert.That(units.All(u => u.Team == team));
                }
            }
        }

        [Test]
        public void serialize_an_arena()
        {
            Arena sut = new Arena();

            Team team = sut.CreateTeam("blue");

            Archer archer = new Archer(sut, team, 1);
            team.Units.Add(1, archer);
            team.Acount++;

            Balista balista = new Balista(sut, team, 2);
            team.Units.Add(2, balista);
            team.Bcount++;

            Catapult catapult = new Catapult(sut, team, 3);
            team.Units.Add(3, catapult);
            team.Ccount++;

            Drake drake = new Drake(sut, team, 4);
            team.Units.Add(4, drake);
            team.Dcount++;

            JToken jToken = sut.Serialize();

            Arena result = new Arena(jToken);

            IEnumerable<Team> teams = result.GetTeams();

            Assert.That(teams.Count(), Is.EqualTo(1));
            //Assert.That(teams.Any(t => t.Name == "blue" && t.UnitCount == 4));

            Assert.That(sut.TeamCount == result.TeamCount);
            //Assert.That( sut.Teams == result.Teams);
            //Assert.That(sut == result);

        }

        [Test]
        public void deserialize_an_arena()
        {

            Arena sut = new Arena();
            Team team = sut.CreateTeam("blue");

            Archer archer = new Archer(sut, team, 1);
            team.Units.Add(archer.Name, archer);
            team.Acount++;



            string fileName = Path.GetTempFileName();

            {
                JToken token = sut.Serialize();
                using (FileStream fs = File.OpenWrite(fileName))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonTextWriter jw = new JsonTextWriter(sw))
                {
                    token.WriteTo(jw);
                }
            }

            {
                using (FileStream fs = File.OpenRead(fileName))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonTextReader jr = new JsonTextReader(sr))
                {
                    JToken token = JToken.ReadFrom(jr);
                    Arena result = new Arena();

                    Assert.That(sut.TeamCount, Is.EqualTo(1));
                    Assert.That(team.UnitCount, Is.EqualTo(1));
                    Assert.That(team.Acount, Is.EqualTo(1));

                    Assert.That(sut.Teams.Any(u => u.Key == "blue"));
                    //Assert.That(sut.Teams.SequenceEqual(result.Teams) );

                    //Assert.That();
                    //Assert.That();

                    //Assert.That();
                }
            }
        }
    }
}
