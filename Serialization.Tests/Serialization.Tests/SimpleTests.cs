using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Serialization.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void serialize()
        {
            Team sut = new Team();
            sut.CreateUnit( 20.0, 20.0 );
            sut.CreateUnit( 10.0, 15.0 );

            JToken token = sut.Serialize();

            Team result = new Team( token );
            IEnumerable<Unit> units = result.GetUnits();
            Assert.That( units.Count(), Is.EqualTo( 2 ) );
            Assert.That( units.Any( u => u.Health == 20.0 && u.Speed == 20.0 ) );
            Assert.That( units.Any( u => u.Health == 10.0 && u.Speed == 15.0 ) );
            Assert.That( units.All( u => u.Team == result ) );
        }

        [Test]
        public void persist()
        {
            Team sut = new Team();
            sut.CreateUnit( 25.0, 30.0 );
            sut.CreateUnit( 15.0, 10.0 );
            string fileName = Path.GetTempFileName();

            {
                JToken token = sut.Serialize();
                using( FileStream fs = File.OpenWrite( fileName ) )
                using( StreamWriter sw = new StreamWriter( fs ) )
                using( JsonTextWriter jw = new JsonTextWriter( sw ) )
                {
                    token.WriteTo( jw );
                }
            }

            {
                using( FileStream fs = File.OpenRead( fileName ) )
                using( StreamReader sr = new StreamReader( fs ) )
                using( JsonTextReader jr = new JsonTextReader( sr ) )
                {
                    JToken token = JToken.ReadFrom( jr );
                    Team team = new Team( token );
                    IEnumerable<Unit> units = team.GetUnits();
                    Assert.That( units.Count(), Is.EqualTo( 2 ) );
                    Assert.That( units.Any( u => u.Health == 25.0 && u.Speed == 30.0 ) );
                    Assert.That( units.Any( u => u.Health == 15.0 && u.Speed == 10.0 ) );
                    Assert.That( units.All( u => u.Team == team ) );
                }
            }
        }
    }
}
