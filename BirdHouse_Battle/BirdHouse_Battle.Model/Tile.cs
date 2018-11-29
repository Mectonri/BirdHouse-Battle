using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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