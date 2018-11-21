using System;
using System.Collections.Generic;

namespace BirdHouse_Battle.Model
{
    public class Field
    {
        Arena _arena;
        Tile[,] _tiles;
        Dictionary<string, Tile> _elements;

        public Field(Arena arena, int startX, int endX, int startY, int endY)
        {
            _arena = arena;
            _tiles = new Tile[endX * 2 + 1, endY * 2 + 1];
            _elements = new Dictionary<string, Tile>();
            SpawnTiles(startX, endX, startY, endY);
            StartingGeneration();
            AddElements();
        }

        public Arena Arena
        {
            get { return _arena; }
        }

        public Tile[,] Tiles
        {
            get { return _tiles; }
        }

        public Dictionary<string, Tile> Elements
        {
            get { return _elements; }
        }

        internal void SpawnTiles(int startX, int endX, int startY, int endY)
        {
            for (int i = startX; i < endX + 1; i++)
            {
                for (int j = startY; j < endY + 1; j++)
                {
                    _tiles[i + 250, j + 250] = new Tile(i, j);
                }
            }
        }

        public Tile FindTile(int i, int j)
        {
            if (i > Arena.Height || i < -Arena.Height ||
                j > Arena.Width || j < -Arena.Width) throw new ArgumentException("Location out of Arena");

            Tile tile = _tiles[i + 250, j + 250];

            return tile;
        }

        internal void SpawnHeights(int i, int j, uint height)
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

        internal void GenerationMountains(Random random)
        {
            int x;
            int y;

            for (int i = 0; i < 4; i++)
            {
                x = random.Next(-Arena.Height, Arena.Height);
                y = random.Next(-Arena.Width, Arena.Width);

                SpawnHeights(x, y, uint.Parse($"{random.Next(8, 14)}"));
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

        internal void SwitchRiver()
        {
            Random random = new Random();
            int x = random.Next(-Arena.Height, Arena.Height);
            int y = random.Next(-Arena.Width, Arena.Width);

            switch (random.Next(0, 4))
            {
                case 0:
                    x = random.Next(-Arena.Height, Arena.Height);
                    y = random.Next(-Arena.Height, Arena.Height);
                    GenerationRiver(new Vector(x, -250), new Vector(250, y));
                    break;
                case 1:
                    x = random.Next(-Arena.Height, Arena.Height);
                    y = random.Next(-Arena.Height, Arena.Height);
                    GenerationRiver(new Vector(x, -250), new Vector(y, 250));
                    break;
                case 2:
                    x = random.Next(-Arena.Height, Arena.Height);
                    y = random.Next(-Arena.Height, Arena.Height);
                    GenerationRiver(new Vector(-250, x), new Vector(250, y));
                    break;
                case 3:
                    x = random.Next(-Arena.Height, Arena.Height);
                    y = random.Next(-Arena.Height, Arena.Height);
                    GenerationRiver(new Vector(-250, x), new Vector(y, 250));
                    break;
            }
        }

        internal void GenerationRiver(Vector start, Vector end)
        {
            double dx = end.X - start.X;
            double dy = end.Y - start.Y;

            int swaps = 0;
            if (dy > dx)
            {
                Swap(ref dx, ref dy);
                swaps = 1;
            }

            double a = Math.Abs(dy);
            double b = -Math.Abs(dx);

            double d = 2 * a + b;
            double x = start.X;
            double y = start.Y;
            Tile tile2, tile3, tile4, tile5, tile6, tile7, tile8;
            Tile tile = FindTile(int.Parse($"{x}"), int.Parse($"{y}"));
            tile.ObstacleAssign(3);

            int s = 1;
            int q = 1;
            if (start.X > end.X) q = -1;
            if (start.Y > end.Y) s = -1;

            for (int k = 0; k < dx; k++)
            {
                if (d >= 0)
                {
                    d = 2 * (a + b) + d;
                    y = y + s;
                    x = x + q;
                }
                else
                {
                    if (swaps == 1) y = y + s;
                    else x = x + q;
                    d = 2 * a + d;
                }

                tile = FindTile(int.Parse($"{x}"), int.Parse($"{y}"));
                tile.ObstacleAssign(3);
                if (y < 244)
                {
                    tile2 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 1}"));
                    tile2.ObstacleAssign(3);
                    tile3 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 2}"));
                    tile3.ObstacleAssign(3);
                    tile4 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 3}"));
                    tile4.ObstacleAssign(3);
                    tile5 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 4}"));
                    tile5.ObstacleAssign(3);
                    tile6 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 4}"));
                    tile6.ObstacleAssign(3);
                    tile7 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 4}"));
                    tile7.ObstacleAssign(3);
                    tile8 = FindTile(int.Parse($"{x}"), int.Parse($"{y + 4}"));
                    tile8.ObstacleAssign(3);

                }
                else if (x > -247)
                {
                    tile2 = FindTile(int.Parse($"{x - 1}"), int.Parse($"{y}"));
                    tile2.ObstacleAssign(3);
                    tile3 = FindTile(int.Parse($"{x - 2}"), int.Parse($"{y}"));
                    tile3.ObstacleAssign(3);
                    tile4 = FindTile(int.Parse($"{x - 3}"), int.Parse($"{y}"));
                    tile4.ObstacleAssign(3);
                    tile5 = FindTile(int.Parse($"{x - 4}"), int.Parse($"{y}"));
                    tile5.ObstacleAssign(3);
                    tile6 = FindTile(int.Parse($"{x - 4}"), int.Parse($"{y}"));
                    tile6.ObstacleAssign(3);
                    tile7 = FindTile(int.Parse($"{x - 4}"), int.Parse($"{y}"));
                    tile7.ObstacleAssign(3);
                    tile8 = FindTile(int.Parse($"{x - 4}"), int.Parse($"{y}"));
                    tile8.ObstacleAssign(3);
                }
            } 
        }

        private void Swap(ref double x, ref double y)
        {
            double temp = x;
            x = y;
            y = temp;
        }

        internal void GenerationObstacles(Random random)
        {
            int x;
            int y;
            int obstacle;

            for (int i = 0; i < 30; i++)
            {
                x = random.Next(-Arena.Height+2, Arena.Height-2);
                y = random.Next(-Arena.Width+2, Arena.Width-2);
                obstacle = random.Next(1, 3);

                if (obstacle == 1) { GenerationRock(x, y); }
                else if (obstacle == 2) { GenerationTree(x, y); }
            }

            //GenerationRiver(new Vector(150, -250), new Vector(-250, 150));

            SwitchRiver();
        }

        internal void StartingGeneration ()
        {
            Random random = new Random();

            GenerationMountains(random);
            GenerationObstacles(random);
        }

        public void AddElements()
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.Height > 0 || tile.Obstacle != "None")
                {
                    _elements.Add($"{tile.X}:{tile.Y}", tile);
                }
            }
        }
    }
}
