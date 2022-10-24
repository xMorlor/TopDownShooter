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
        public static bool houseCreated = false;
        public static List<Tile> house = new List<Tile>();
        private static List<Tile> allTiles = new List<Tile>();
        public static List<Tile> drawList;


        public static void CreateHouse()
        {
            Vector2 corePos = new Vector2(600, 600);

            Tile tile1 = new Tile(corePos, Textures.tTile1);
            allTiles.Add(tile1);
            house.Add(tile1);

            tile1.CreateHitboxRectangle1((int)corePos.X, (int)corePos.Y, 28, 356);
            tile1.CreateHitboxRectangle2((int)corePos.X, (int)corePos.Y, 89, 28);

            Tile tile2 = new Tile(new Vector2(corePos.X, corePos.Y + Textures.tTile1.Height), Textures.tTile5);
            allTiles.Add(tile2);
            house.Add(tile2);

            Tile tile3 = new Tile(new Vector2(tile2.position.X, tile2.position.Y + tile2.texture.Height), Textures.tTile5);
            allTiles.Add(tile3);
            house.Add(tile3);

            Tile tile4 = new Tile(new Vector2(tile3.position.X, tile3.position.Y + tile3.texture.Height), Textures.tTile4);
            allTiles.Add(tile4);
            house.Add(tile4);
            tile4.CreateHitboxRectangle2((int)tile4.position.X, (int)tile4.position.Y + 61, 89, 28);

            Tile tile5 = new Tile(new Vector2(tile1.position.X + 163, tile1.position.Y), Textures.tTile2);
            allTiles.Add(tile5);
            house.Add(tile5);
            tile5.CreateHitboxRectangle1((int)tile5.position.X, (int)tile5.position.Y, 89, 28);
            tile5.CreateHitboxRectangle2((int)tile5.position.X + 61, (int)tile5.position.Y, 28, 356);

            Tile tile6 = new Tile(new Vector2(corePos.X + 224, corePos.Y + 89), Textures.tTile5);
            allTiles.Add(tile6);
            house.Add(tile6);

            Tile tile7 = new Tile(new Vector2(corePos.X + 224, corePos.Y + 178), Textures.tTile5);
            allTiles.Add(tile7);
            house.Add(tile7);

            Tile tile8 = new Tile(new Vector2(corePos.X + 163, corePos.Y + 265), Textures.tTile3);
            allTiles.Add(tile8);
            house.Add(tile8);
            tile8.CreateHitboxRectangle2((int)tile8.position.X, (int)tile8.position.Y + 62, 89, 29);
        }

        public static void ChechCollisionBetweenPlayerAndObjectsAndWalls(GameTime gameTime)
        {

        }

        public static bool CheckCollisionsWithWallsLeft(GameTime gameTime)
        {
            Rectangle hitboxRec = Player.hitboxRectangle;
            hitboxRec.X -= (int)(176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;

            foreach (var k in house)
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
            hitboxRec.X += (int)(176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;
            foreach (var k in house)
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
            hitboxRec.Y -= (int)(176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 2;

            foreach (var k in house)
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
            hitboxRec.Y += (int)(176.78f * (float)gameTime.ElapsedGameTime.TotalSeconds) + 1;

            foreach (var k in house)
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
            drawList = allTiles.FindAll(i => Vector2.Distance(Player.position, i.position) < 1650);
        }

        public static void Update(GameTime gameTime)
        {
            CheckTilesForDrawing();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            
            foreach (var k in drawList)
            {
                k.Draw(spriteBatch);
            }
        }
    }
}
