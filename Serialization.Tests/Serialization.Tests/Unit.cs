using Newtonsoft.Json.Linq;

namespace Serialization.Tests
{
    public class Unit
    {
        readonly Team _team;
        double _health;
        double _speed;

        internal Unit( Team team, double health, double speed )
        {
            _team = team;
            _health = health;
            _speed = speed;
        }

        internal Unit( Team team, JToken jToken )
        {
            _team = team;
            _health = jToken[ "Health" ].Value<double>();
            _speed = jToken[ "Speed" ].Value<double>();
        }

        public JToken Serialize()
        {
            return new JObject(
                new JProperty( "Health", _health ),
                new JProperty( "Speed", _speed ) );
        }

        public double Speed => _speed;

        public double Health => _health;

        public Team Team => _team;
    }
}
