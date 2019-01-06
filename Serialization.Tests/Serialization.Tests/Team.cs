using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Serialization.Tests
{
    public class Team
    {
        readonly Dictionary<string, Unit> _units;

        public Team()
        {
            _units = new Dictionary<string, Unit>();
        }

        public Team( JToken jToken )
            : this()
        {
            JArray jUnits = ( JArray )jToken[ "Units" ];
            IEnumerable<Unit> units = jUnits.Select<JArray, JToken>
            //IEnumerable<Unit> units = jUnits.Select( u => new Unit( this, u ) );
            foreach(Unit unit in units)
            {
                _units.Add( Guid.NewGuid().ToString(), unit );
            }
        }

        public IEnumerable<Unit> GetUnits()
        {
            return _units.Values;
        }

        
        public JToken Serialize()
        {
            return new JObject(
                new JProperty( "Units", _units.Select( kv => kv.Value.Serialize() ) ) );
        }

        public Unit CreateUnit( double health, double speed )
        {
            Unit unit = new Unit( this, health, speed );
            _units.Add( Guid.NewGuid().ToString(), unit );
            return unit;
        }
    }
}
