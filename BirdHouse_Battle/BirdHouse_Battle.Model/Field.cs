using System;
using System.Collections.Generic;
using System.Drawing;

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

        public void Init()
        {
            StartingGeneration();
            AddElements();
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

        public bool TryFindTile(int i, int j, out Tile tile)
        {
            if (i > Arena.Height || i < -Arena.Height ||
                j > Arena.Width || j < -Arena.Width) throw new ArgumentException("Location out of Arena");

            tile = _tiles[i + 250, j + 250];
            if (tile.Obstacle != "None" || tile.Height != 0)
            {
                return true;
            }

            tile = null;

            return false;
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

        internal void GenerationRockInGame(int x, int y)
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
                TryAddElement(tile);
            }
        }

        internal void TryAddElement(Tile tile)
        {
            if (!Elements.TryGetValue($"{tile.X}:{tile.Y}", out Tile faketile))
            {
                Elements.Add($"{tile.X}:{tile.Y}", tile);
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

        internal void StartingGeneration()
        {
            Random random = new Random();

            //GenerationMountains(random);
            GenerationObstacles(random);
            
            DiamondSquare();
            //DiamondSquare(true);
        }

        public void AddElements()
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.Obstacle != "None")
                {
                    _elements.Add($"{tile.X}:{tile.Y}", tile);
                }
            }
        }

// Renvoie un décalage aléatoire proportionnel à la hauteur
        internal double offset(double height, int roughness)
        {
            // On calcule la valeur aléatoire dans une plage de 2h
            // et ensuite on soustrait h de façon à ce que la valeur finale
            // soit dans l'intervalle [-h, +h]

            return (new Random().NextDouble() * 2 - 1) * height * roughness;
        } // offset

// Normalise la valeur pour s'assurer qu'elle reste dans les limites
        internal double normalize(double value)
        {
            return Math.Round(Math.Max(Math.Min(value, 14), 0));
        } // normalize

        public void DiamondSquare()
        {
            Random rdm = new Random();
            int X = 0;
            int Y = 0;

            int MAP_SIZE = 513;
            bool wrap = false;
            int roughness = 1;

            double[,] map = new double[MAP_SIZE, MAP_SIZE];
            int nw = (wrap ? 0 : 1); // indicateur non répétable

            // Initialise les coins de la carte
            map[0, 0] = rdm.Next(0, 12); // haut gauche
            map[0, MAP_SIZE - 1] = rdm.Next(0, 12); // bas gauche
            map[MAP_SIZE - 1, 0] = rdm.Next(0, 12); // haut droite
            map[MAP_SIZE - 1, MAP_SIZE - 1] = rdm.Next(0, 12); // bas droite

            double h = 7; // la plage (-h -> +h) pour le décalage moyen

            // sideLength est la longueur d'un côté d'un carré
            // ou la longueur de la diagonale d'un losange
            for (int sideLength = MAP_SIZE - 1; sideLength >= 2; sideLength /= 2)
            {
                h /= 2.0;
                // la moitié de la longueur d'un carré
                // ou la distance du centre d'un losange à un coin
                // (juste pour rendre les calculs ci-dessous un peu plus clairs)
                int halfSide = sideLength / 2;

                // génère les nouvelles valeurs du carré
                for (int x = 0; x < MAP_SIZE - 1; x += sideLength)
                {
                    for (int y = 0; y < MAP_SIZE - 1; y += sideLength)
                    {
                        // x, y est en haut à gauche du carré
                        // calcule la moyenne des coins existants
                        double avg = map[x, y] + //top left
                                  map[x + sideLength, y] + // top right
                                  map[x, y + sideLength] + // lower left
                                  map[x + sideLength, y + sideLength]; // lower right
                        avg /= 4.0;

                        // le centre c'est la moyenne plus un décalage aléatoire
                        map[x + halfSide, y + halfSide] = normalize(avg + offset(h, roughness));
                    } // for y
                } // for x 

                // génère les valeurs du losange
                // puisque les losanges sont en quinconce on se déplace en x
                // uniquement de halfSide.
                // NOTE : si la carte ne doit pas se répéter, alors x < MAP_SIZE
                // pour générer les valeurs du bord
                for (int x = 0; x < MAP_SIZE - 1 + nw; x += halfSide)
                {
                    // et y est x décalé de halfSide, mais translaté
                    // de la pleine longueur du côté
                    // NOTE : si la carte ne doit pas se répéter, alors y < MAP_SIZE
                    // pour générer les valeurs du bord
                    for (int y = (x + halfSide) % sideLength; y < MAP_SIZE - 1 + nw; y += sideLength)
                    {
                        // x, y est le centre du losange
                        // à noter que nous devons utiliser le modulo et l'ajout de MAP_SIZE pour la soustraction
                        // de façon à parcourir cycliquement le tableau pour trouver les coins
                        double avg =
                            map[(x - halfSide + MAP_SIZE - 1) % (MAP_SIZE - 1), y] + // gauche du centre
                            map[(x + halfSide) % (MAP_SIZE - 1), y] + // droite du centre
                            map[x, (y + halfSide) % (MAP_SIZE - 1)] + // bas du centre
                            map[x, (y - halfSide + MAP_SIZE - 1) % (MAP_SIZE - 1)]; // haut du centre

                        avg /= 4.0;

                        // nouvelle valeur = moyenne + décalage aléatoire
                        avg = normalize(avg + offset(h, roughness));
                        // met à jour la valeur pour le centre du losange
                        map[x, y] = avg;

                        // duplique les valeurs sur les bords si carte répétable
                        if (wrap)
                        {
                            if (x == 0) map[MAP_SIZE - 1, y] = avg;
                            if (y == 0) map[x, MAP_SIZE - 1] = avg;
                        }
                    } // for y
                } // for x
            } // for sideLength

            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    _tiles[x, y].HeightAssign(uint.Parse($"{map[x, y]}"));
                }
            }

            GetDataPicture(map);
        } // heightmapDiamondSquare

        public void GetDataPicture(double[,] data)
        {
            Bitmap pic = new Bitmap(500, 500, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Color c;

            for (int x = 0; x < 500; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    int arrayIndex = y * 500 + x;

                    switch (data[x, y])
                    {
                        case 1:
                            c = Color.FromArgb(110, 48, 12);
                            break;
                        case 2:
                            c = Color.FromArgb(109, 60, 12);
                            break;
                        case 3:
                            c = Color.FromArgb(108, 73, 11);
                            break;
                        case 4:
                            c = Color.FromArgb(107, 85, 11);
                            break;
                        case 5:
                            c = Color.FromArgb(107, 97, 10);
                            break;
                        case 6:
                            c = Color.FromArgb(102, 106, 10);
                            break;
                        case 7:
                            c = Color.FromArgb(88, 105, 9);
                            break;
                        case 8:
                            c = Color.FromArgb(75, 105, 9);
                            break;
                        case 9:
                            c = Color.FromArgb(61, 104, 8);
                            break;
                        case 10:
                            c = Color.FromArgb(47, 103, 8);
                            break;
                        case 11:
                            c = Color.FromArgb(34, 103, 7);
                            break;
                        case 12:
                            c = Color.FromArgb(20, 102, 7);
                            break;
                        case 13:
                            c = Color.FromArgb(7, 101, 6);
                            break;
                        default:
                            c = Color.FromArgb(6, 101, 18);
                            break;
                    }
                    pic.SetPixel(x, y, c);
                }
            }

            pic.Save("../../../../res/DiamondBackground.png");
        }
    }
}
