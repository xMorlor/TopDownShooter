using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TopDownShooterFinal
{
    static class Map
    {
        public static List<Tile> walls = new List<Tile>();
        public static List<Rectangle> wallsForPathFinder = new List<Rectangle>();   //asi odstranit
        private static List<Tile> allTiles = new List<Tile>();
        public static List<Tile> drawListGround;
        public static List<Tile> drawListWalls;
        private static List<Point> housePoints = new List<Point>();
        public static List<Texture2D> groundCracks = new();
        public static List<Rectangle> rectanglesAroundHouses = new List<Rectangle>();
        private static Rectangle playerHitboxRec;

        public static void CreateHouses()
        {
            Point point = new Point(1200, 600);
            housePoints.Add(point);
            CreateHouse(1200, 600);
            
            Random rnd = new Random();
            for(int i = 0; i < 500; i++)
            {
                bool condition = true;
                while (condition)
                {
                    int x = rnd.Next(-24000, 24000);
                    int y = rnd.Next(-24000, 24000);
                    if(x % 30 == 0 && y % 30 == 0)
                    {
                        condition = false;
                        foreach(var k in housePoints)
                        {
                            if(Vector2.Distance(new Vector2(x, y), k.ToVector2()) < 800)
                            {
                                condition = true;
                                break;
                            }
                        }
                        if (!condition)
                        {
                            CreateHouse(x, y);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        private static void CreateHouse(int x, int y)
        {
            Point p = new Point(x, y);
            housePoints.Add(p);
            Vector2 corePos = new Vector2(p.X, p.Y);

            Tile tile1 = new Tile(corePos, Textures.tTile1);

            walls.Add(tile1);
            tile1.CreateHitboxRectangle1((int)corePos.X, (int)corePos.Y, 28, 356);
            tile1.CreateHitboxRectangle2((int)corePos.X, (int)corePos.Y, 89, 28);

            Tile tile2 = new Tile(new Vector2(corePos.X, corePos.Y + Textures.tTile1.Height), Textures.tTile5);
            walls.Add(tile2);

            Tile tile3 = new Tile(new Vector2(tile2.position.X, tile2.position.Y + tile2.texture.Height), Textures.tTile5);
            walls.Add(tile3);

            Tile tile4 = new Tile(new Vector2(tile3.position.X, tile3.position.Y + tile3.texture.Height), Textures.tTile4);
            walls.Add(tile4);
            tile4.CreateHitboxRectangle2((int)tile4.position.X, (int)tile4.position.Y + 61, 89, 28);

            Tile tile5 = new Tile(new Vector2(tile1.position.X + 163, tile1.position.Y), Textures.tTile2);
            walls.Add(tile5);
            tile5.CreateHitboxRectangle1((int)tile5.position.X, (int)tile5.position.Y, 89, 28);
            tile5.CreateHitboxRectangle2((int)tile5.position.X + 61, (int)tile5.position.Y, 28, 356);

            Tile tile6 = new Tile(new Vector2(corePos.X + 224, corePos.Y + 89), Textures.tTile5);
            walls.Add(tile6);

            Tile tile7 = new Tile(new Vector2(corePos.X + 224, corePos.Y + 178), Textures.tTile5);
            walls.Add(tile7);

            Tile tile8 = new Tile(new Vector2(corePos.X + 163, corePos.Y + 265), Textures.tTile3);
            walls.Add(tile8);
            tile8.CreateHitboxRectangle2((int)tile8.position.X, (int)tile8.position.Y + 62, 89, 29);

            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X - 120, (int)tile1.position.Y - 120, 120, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X - 120, (int)tile1.position.Y, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X - 120, (int)tile1.position.Y + 80, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X - 120, (int)tile1.position.Y + 160, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X - 120, (int)tile1.position.Y + 240, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X - 120, (int)tile1.position.Y + 320, 120, 156));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X + 89, (int)tile1.position.Y - 120, 74, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X + 89, (int)tile1.position.Y, 74, 160));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X + 89, (int)tile1.position.Y + 160, 74, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X + 89, (int)tile1.position.Y + 240, 74, 144));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X, (int)tile1.position.Y - 120, 89, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X, (int)tile5.position.Y - 120, 89, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X + 89, (int)tile5.position.Y - 120, 120, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X + 89, (int)tile5.position.Y, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X + 89, (int)tile5.position.Y + 80, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X + 89, (int)tile5.position.Y + 160, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X + 89, (int)tile5.position.Y + 240, 120, 80));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X + 89, (int)tile5.position.Y + 320, 120, 156));
            rectanglesAroundHouses.Add(new Rectangle((int)tile4.position.X, (int)tile4.position.Y + 89, 89, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile4.position.X + 89, (int)tile4.position.Y + 89, 74, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile4.position.X + 163, (int)tile4.position.Y + 89, 89, 120));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X + 28, (int)tile1.position.Y + 28, 61, 160));
            rectanglesAroundHouses.Add(new Rectangle((int)tile1.position.X + 28, (int)tile1.position.Y + 188, 61, 140));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X, (int)tile5.position.Y + 28, 61, 160));
            rectanglesAroundHouses.Add(new Rectangle((int)tile5.position.X, (int)tile5.position.Y + 188, 61, 140));
        }
        
        public static bool CheckCollisionsForPlayerWithWallsLeft(GameTime gameTime)
        {
            playerHitboxRec = Player.hitboxRectangle;
            playerHitboxRec.X -= (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;

            foreach (var k in walls)
            {
                if (playerHitboxRec.Intersects(k.hitboxRectangle1) || playerHitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionsForPlayerWithWallsRight(GameTime gameTime)
        {
            playerHitboxRec = Player.hitboxRectangle;
            playerHitboxRec.X += (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;
            foreach (var k in walls)
            {
                if (playerHitboxRec.Intersects(k.hitboxRectangle1) || playerHitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionsForPlayerWithWallsTop(GameTime gameTime)
        {
            playerHitboxRec = Player.hitboxRectangle;
            playerHitboxRec.Y -= (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 2;

            foreach (var k in walls)
            {
                if (playerHitboxRec.Intersects(k.hitboxRectangle1) || playerHitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionsForPlayerWithWallsBottom(GameTime gameTime)
        {
            playerHitboxRec = Player.hitboxRectangle;
            playerHitboxRec.Y += (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;

            foreach (var k in walls)
            {
                if (playerHitboxRec.Intersects(k.hitboxRectangle1) || playerHitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        private static void HandleGrassTiles(int x, int y)
        {
            if (x == -25000 && y == -25000)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass2);
                allTiles.Add(tile);
            }
            else if (x == -25000 && y != -25000 && y != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass9);
                allTiles.Add(tile);
            }
            else if (y == -25000 && x != -25000 && x != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass3);
                allTiles.Add(tile);
            }
            else if (x == -25000 && y == 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass8);
                allTiles.Add(tile);
            }
            else if (x == 24901 && y == -25000)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass4);
                allTiles.Add(tile);
            }
            else if (x == 24901 && y != -25000 && y != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass5);
                allTiles.Add(tile);
            }
            else if (x == 24901 && y == 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass6);
                allTiles.Add(tile);
            }
            else if (y == 24901 && x != -25000 && x != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grass7);
                allTiles.Add(tile);
            }
            else
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.grassFull);
                allTiles.Add(tile);
            }
        }

        private static void HandleGroundTiles(int x, int y)
        {
            Random rnd = new Random();
            if (x == -25000 && y == -25000)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile2);
                allTiles.Add(tile);
            }
            else if (x == -25000 && y != -25000 && y != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile9);
                allTiles.Add(tile);
            }
            else if (y == -25000 && x != -25000 && x != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile3);
                allTiles.Add(tile);
            }
            else if (x == -25000 && y == 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile8);
                allTiles.Add(tile);
            }
            else if (x == 24901 && y == -25000)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile4);
                allTiles.Add(tile);
            }
            else if (x == 24901 && y != -25000 && y != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile5);
                allTiles.Add(tile);
            }
            else if (x == 24901 && y == 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile6);
                allTiles.Add(tile);
            }
            else if (y == 24901 && x != -25000 && x != 24901)
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile7);
                allTiles.Add(tile);
            }
            else
            {
                Tile tile = new Tile(new Vector2(x, y), Textures.groundTile1);
                allTiles.Add(tile);

                if (rnd.Next(1, 15) == 1)
                {
                    Texture2D tex = groundCracks[rnd.Next(1, groundCracks.Count)];
                    Tile t = new Tile(new Vector2(x + 70 - tex.Width / 2, y + 70 - tex.Height / 2), tex);
                    t.SetAngle();
                    allTiles.Add(t);
                }
            }
        }

        public static void InitializeGround()
        {
            List<Rectangle> zonesGrass = new List<Rectangle>();
            List<Rectangle> zonesGround = new List<Rectangle>();
            Random rnd = new Random();

            for (int x = -25000; x < 25000; x += 10000)
            {
                for(int y = -25000; y < 25000; y += 10000)
                {
                    if(rnd.Next(1, 3) == 1)
                    {
                        zonesGrass.Add(new Rectangle(x, y, 12500, 12500));
                    }
                    else
                    {
                        zonesGround.Add(new Rectangle(x, y, 12500, 12500));
                    }
                }
            }
            
            for(int x = -25000; x < 25040; x += 139)
            {
                for(int y = -25000; y < 25040; y += 139)
                {
                    bool inZoneGrass = false;
                    bool inZoneGround = false;
                    foreach(var k in zonesGrass)
                    {
                        if (new Rectangle(x, y, 1, 1).Intersects(k))
                        {
                            inZoneGrass = true;
                        }
                    }
                    foreach(var k in zonesGround)
                    {
                        if (new Rectangle(x, y, 1, 1).Intersects(k))
                        {
                            inZoneGround = true;
                        }
                    }
                    
                    if(inZoneGrass && inZoneGround)
                    {
                        if(rnd.Next(1, 3) == 1)
                        {
                            HandleGroundTiles(x, y);
                        }
                        else
                        {
                            HandleGrassTiles(x, y);
                        }
                    }
                    else if (inZoneGround)
                    {
                        HandleGroundTiles(x, y);
                    }
                    else if (inZoneGrass)
                    {
                        HandleGrassTiles(x, y);
                    }
                }
            }
        }

        public static void InitializeWater()
        {
            for(int x = -26900; x < 26900; x += 139)
            {
                for(int y = -26900; y < 26900; y += 139)
                {
                    if(y <= -24861)
                    {
                        Tile tile = new Tile(new Vector2(x, y), Textures.waterTile1);
                        allTiles.Add(tile);
                    }
                    else if(x <= -24861)
                    {
                        Tile tile = new Tile(new Vector2(x, y), Textures.waterTile1);
                        allTiles.Add(tile);
                    }
                    else if(y >= 24861)
                    {
                        Tile tile = new Tile(new Vector2(x, y), Textures.waterTile1);
                        allTiles.Add(tile);
                    }
                    else if(x >= 24861)
                    {
                        Tile tile = new Tile(new Vector2(x, y), Textures.waterTile1);
                        allTiles.Add(tile);
                    }
                }
            }
        }

        public static void CheckTilesForDrawing()
        {
            drawListGround = allTiles.FindAll(i => Vector2.Distance(Player.position, i.position) < 1650);
            drawListWalls = walls.FindAll(i => Vector2.Distance(Player.position, i.position) < 1650);
        }

        public static void Update(GameTime gameTime)
        {
            CheckTilesForDrawing();
        }

        public static void DrawWalls(SpriteBatch spriteBatch)
        {
            foreach(var k in drawListWalls)
            {
                k.Draw(spriteBatch);
            }
        }

        public static void DrawGround(SpriteBatch spriteBatch)
        {
            foreach (var k in drawListGround)
            {
                k.Draw(spriteBatch);
            }
        }
    }
}
