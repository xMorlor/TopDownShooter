using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    static class Map
    {
        public static List<Tile> walls = new List<Tile>();
        private static List<Tile> allTiles = new List<Tile>();
        public static List<Tile> drawListGround;
        public static List<Tile> drawListWalls;
        private static List<Point> housePoints = new List<Point>();

        public static void CreateHouses()
        {
            Point point = new Point(1200, 600);
            housePoints.Add(point);
            CreateHouse(1200, 600);
            
            Random rnd = new Random();
            for(int i = 0; i < 100; i++)
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
            //allTiles.Add(tile1);
            walls.Add(tile1);
            tile1.CreateHitboxRectangle1((int)corePos.X, (int)corePos.Y, 28, 356);
            tile1.CreateHitboxRectangle2((int)corePos.X, (int)corePos.Y, 89, 28);

            Tile tile2 = new Tile(new Vector2(corePos.X, corePos.Y + Textures.tTile1.Height), Textures.tTile5);
            //allTiles.Add(tile2);
            walls.Add(tile2);

            Tile tile3 = new Tile(new Vector2(tile2.position.X, tile2.position.Y + tile2.texture.Height), Textures.tTile5);
            //allTiles.Add(tile3);
            walls.Add(tile3);

            Tile tile4 = new Tile(new Vector2(tile3.position.X, tile3.position.Y + tile3.texture.Height), Textures.tTile4);
            //allTiles.Add(tile4);
            walls.Add(tile4);
            tile4.CreateHitboxRectangle2((int)tile4.position.X, (int)tile4.position.Y + 61, 89, 28);

            Tile tile5 = new Tile(new Vector2(tile1.position.X + 163, tile1.position.Y), Textures.tTile2);
            //allTiles.Add(tile5);
            walls.Add(tile5);
            tile5.CreateHitboxRectangle1((int)tile5.position.X, (int)tile5.position.Y, 89, 28);
            tile5.CreateHitboxRectangle2((int)tile5.position.X + 61, (int)tile5.position.Y, 28, 356);

            Tile tile6 = new Tile(new Vector2(corePos.X + 224, corePos.Y + 89), Textures.tTile5);
            //allTiles.Add(tile6);
            walls.Add(tile6);

            Tile tile7 = new Tile(new Vector2(corePos.X + 224, corePos.Y + 178), Textures.tTile5);
            //allTiles.Add(tile7);
            walls.Add(tile7);

            Tile tile8 = new Tile(new Vector2(corePos.X + 163, corePos.Y + 265), Textures.tTile3);
            //allTiles.Add(tile8);
            walls.Add(tile8);
            tile8.CreateHitboxRectangle2((int)tile8.position.X, (int)tile8.position.Y + 62, 89, 29);
        }

        /*
         pak implementovat algo a optimalizovat pokud to bude dělat bordel
         */

        public static bool CheckCollisionsWithWallsLeft(GameTime gameTime)
        {
            Rectangle hitboxRec = Player.hitboxRectangle;
            hitboxRec.X -= (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;

            foreach (var k in walls)
            {
                if (hitboxRec.Intersects(k.hitboxRectangle1) || hitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionsWithWallsRight(GameTime gameTime)
        {
            Rectangle hitboxRec = Player.hitboxRectangle;
            hitboxRec.X += (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;
            foreach (var k in walls)
            {
                if (hitboxRec.Intersects(k.hitboxRectangle1) || hitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionsWithWallsTop(GameTime gameTime)
        {
            Rectangle hitboxRec = Player.hitboxRectangle;
            hitboxRec.Y -= (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 2;

            foreach (var k in walls)
            {
                if (hitboxRec.Intersects(k.hitboxRectangle1) || hitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckCollisionsWithWallsBottom(GameTime gameTime)
        {
            Rectangle hitboxRec = Player.hitboxRectangle;
            hitboxRec.Y += (int)(176.78f * 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;

            foreach (var k in walls)
            {
                if (hitboxRec.Intersects(k.hitboxRectangle1) || hitboxRec.Intersects(k.hitboxRectangle2))
                {
                    return true;
                }
            }
            return false;
        }

        public static void InitializeGround()
        {
            for(int x = -25000; x < 25040; x += 139)
            {
                for(int y = -25000; y < 25040; y += 139)
                {
                    if(x == -25000 && y == -25000)
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
                    else if(x == -25000 && y == 24901)
                    {
                        Tile tile = new Tile(new Vector2(x, y), Textures.groundTile8);
                        allTiles.Add(tile);
                    }
                    else if(x == 24901 && y == -25000)
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
                    else if(x <= 24861)
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
