using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace BirdHouse_Battle.Model
{
    public class Field
    {
        Arena _arena;
        Dictionary<string, Tile> _tiles;
        Dictionary<string, Tile> _elements;

        public Field(Arena arena, int startX, int endX, int startY, int endY)
        {
            _arena = arena;
            _tiles = new Dictionary<string, Tile>();
            _elements = new Dictionary<string, Tile>();
            SpawnTiles(startX, endX, startY, endY);
            StartingGeneration();
            AddElements();
        }

        public Arena Arena
        {
            get { return _arena; }
        }

        public Dictionary<string, Tile> Tiles
        {
            get { return _tiles; }
        }

        public Dictionary<string, Tile> Elements
        {
            get { return _elements; }
        }

        internal void SpawnTiles(int startX, int endX, int startY, int endY)
        {
            for ( int i = startX ; i < endX+1 ; i++ )
            {
                for (int j = startY; j < endY+1; j++)
                {
                    _tiles.Add(String.Format("{0}:{1}", i, j), new Tile(i, j));
                }
            }
        }

        public Tile FindTile(int i, int j)
        {
            if (i > Arena.Height || i < -Arena.Height ||
                j > Arena.Width  || j < -Arena.Width) throw new ArgumentException("Location out of Arena");

            _tiles.TryGetValue(String.Format("{0}:{1}", i, j), out Tile tile);

            return tile;
        }

        internal void SpawnHeights( int i, int j, uint height )
        {
            Tile tile = FindTile(i, j);

            if (tile.Height <= height)
            {
                tile.HeightAssign(height);

                if (height > 1)
                {
                    if (i - 1 >= -Arena.Height)
                    {
                        SpawnHeights(i - 1, j, height - 1);
                    }
                    if (i + 1 <= Arena.Height)
                    {
                        SpawnHeights(i + 1, j, height - 1);
                    }
                    if (j - 1 >= -Arena.Width)
                    {
                        SpawnHeights(i, j - 1, height - 1);
                    }
                    if (j + 1 <= Arena.Width)
                    {
                        SpawnHeights(i, j + 1, height - 1);
                    }
                }
            }
        }

        internal void GenerationMountains (Random random)
        {
            int x;
            int y;

            for (int i = 0; i < 4; i++)
            {
                x = random.Next(-Arena.Height, Arena.Height);
                y = random.Next(-Arena.Width, Arena.Width);

                SpawnHeights(x, y, uint.Parse(String.Format("{0}", random.Next(4, 8))));
            }
        }

        internal void GenerationRock(int x, int y)
        {
            Tile[] tiles = new Tile[5];
            tiles[0] = FindTile(x, y);
            tiles[1] = FindTile(x - 1, y - 1);
            tiles[2] = FindTile(x + 1, y - 1);
            tiles[3] = FindTile(x - 1, y + 1);
            tiles[4] = FindTile(x + 1, y + 1);

            foreach (Tile tile in tiles)
            {
                tile.ObstacleAssign(1);
            }
        }
            

        internal void GenerationTree(int x, int y)
        {
            Tile[] tiles = new Tile[9];
            tiles[0] = FindTile(x, y);
            tiles[1] = FindTile(x - 1, y);
            tiles[2] = FindTile(x + 1, y);
            tiles[3] = FindTile(x, y - 1);
            tiles[4] = FindTile(x, y + 1);
            tiles[5] = FindTile(x - 1, y - 1);
            tiles[6] = FindTile(x + 1, y - 1);
            tiles[7] = FindTile(x - 1, y + 1);
            tiles[8] = FindTile(x + 1, y + 1);

            foreach (Tile tile in tiles)
            {
                tile.ObstacleAssign(2);
            }
        }

        internal void GenerationRiver(int x, int y) { }

        internal void GenerationObstacles(Random random)
        {
            int x;
            int y;
            int obstacle;

            for (int i = 0; i < 4; i++)
            {
                x = random.Next(-Arena.Height+2, Arena.Height-2);
                y = random.Next(-Arena.Width+2, Arena.Width-2);
                obstacle = random.Next(1, 3);

                if (obstacle == 1) { GenerationRock(x, y); }
                else if (obstacle == 2) { GenerationTree(x, y); }
            }

            x = random.Next(-Arena.Height, Arena.Height);
            y = random.Next(-Arena.Width, Arena.Width);

            GenerationRiver(x, y);
        }

        internal void StartingGeneration ()
        {
            Random random = new Random();

            GenerationMountains(random);
            GenerationObstacles(random);
        }

        public void AddElements()
        {
            foreach (KeyValuePair<string, Tile> tile in Tiles)
            {
                if (tile.Value.Height > 0 || tile.Value.Obstacle != "None")
                {
                    _elements.Add(tile.Key, tile.Value);
                }
            }
        }
    }
}
