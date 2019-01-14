using System;
using Newtonsoft.Json.Linq;

namespace BirdHouse_Battle.Model
{
    public class Tile
    {
        int _x;
        int _y;
        uint _height;
        enum _obstacles { None, Rock, Tree, River};
        string _obstacle;

        public Tile(int x, int y)
        {
            _x = x;
            _y = y;
            _obstacle = Enum.GetName(typeof(_obstacles), 0);
        }

        public Tile(JToken jToken)
        {
            _x = jToken["X"].Value<int>();
            _y = jToken["Y"].Value<int>();
            _obstacle = jToken["Obstacle"].Value<string>();
        }

        public JToken Serialize()
        {
            return new JObject(
                new JProperty("X", _x),
                new JProperty("Y", _y),
                new JProperty("Obstacle", Obstacle)
                );
        }

        public int X { get { return _x; } }

        public int Y { get { return _y; } }

        public uint Height { get { return _height; } }

        public string Obstacle { get { return _obstacle; } }

        internal void HeightAssign(uint height)
        {
            _height = height;
        }

        public void ObstacleAssign(int NbObstacle)
        {
            if (NbObstacle < 0 || NbObstacle > 3) throw new ArgumentException("This obstacle doesn't exist !");
            _obstacle = Enum.GetName(typeof(_obstacles), NbObstacle);
        }
    }
}