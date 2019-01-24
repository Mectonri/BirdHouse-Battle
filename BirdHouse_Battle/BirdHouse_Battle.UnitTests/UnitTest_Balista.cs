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
    class UnitTest_Balista
    {

        internal UnitTests_Unit UTT;

        [Test]
        public void add_an_balista()
        {
            Arena arena = new Arena();
            Team t1 = arena.CreateTeam("RED");

            t1.AddBalista(1);

            Unit[] tab = t1.Find();
            Unit u = t1.FindUnitByName(tab[0].Name);

            Assert.That(u, Is.SameAs(tab[0]));
        }

        [Test]
        public void serialize_a_balista()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("blue");


            Balista sut = new Balista(arena, team, 1);

            JToken jToken = sut.Serialize();

            Balista result = new Balista(arena, team, jToken);

            UnitTests_Unit.CheckEquals(sut, result);
        }

        [Test]
        public void deserialize_a_balista()
        {
            Arena arena = new Arena();
            Team team = arena.CreateTeam("blue");

            Balista sut = new Balista(arena, team, 1);

            string fileName = Path.GetTempFileName();

            {
                JToken jToken = sut.Serialize();
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
                    Balista balista = new Balista(arena, team, jToken);
                    Balista balista1 = new Balista(arena, team, 22);

                    Assert.That(balista.Life, Is.EqualTo(25.0));
                    Assert.That(balista.Troop, Is.EqualTo("balista"));
                    Assert.That(balista.Equals(balista1), Is.False);
                    Assert.That(balista.Name, Is.EqualTo(1));
                    Assert.That(balista.Name, Is.Not.EqualTo(balista1.Name));
                }
            }
        }
    }
}
