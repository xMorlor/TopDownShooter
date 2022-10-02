using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace TopDownShooterFinal
{
    enum GameState
    {
        MainMenu,
        Gameplay,
        EndOfGame
    }

    static class Manager
    {
        public static GameState gameState = GameState.Gameplay;
        public static List<Bullet> bulletList = new List<Bullet>();
        public static List<Bullet> bulletsToDelete = new List<Bullet>();
        public static List<Zombie> zombieList = new List<Zombie>();
        public static List<Zombie> zombiesToDelete = new List<Zombie>();
        public static List<Zombie> deadZombieBodies = new List<Zombie>();
        public static List<Zombie> deadZombieBodiesToDelete = new List<Zombie>();
        public static List<Texture2D> trackTextures = new List<Texture2D>();
        public static List<Tracks> tracks = new List<Tracks>();
        public static List<Tracks> tracksToDelete = new List<Tracks>();
        public static List<Blood> bloodList = new List<Blood>();
        public static List<Blood> bloodToRemove = new List<Blood>();

        public static List<MotherClass> allObjects = new List<MotherClass>();

        public static void RemoveBlood()
        {
            foreach(var k in bloodToRemove)
            {
                bloodList.Remove(k);
            }
            bloodToRemove.Clear();
        }

        public static void UpdateBlood()
        {
            foreach(var k in bloodList)
            {
                k.Update();
            }
            //opatření proti lagům
            List<Blood> mediumAndLarge = new List<Blood>();
            foreach(var k in bloodList)
            {
                if(k.texture != Textures.BloodSmall && k.update)
                {
                    mediumAndLarge.Add(k);
                }
            }
            for(int i = 0; i < mediumAndLarge.Count; i++)
            {
                if(i < 10) //tady snižovat nebo navyšovat (větší číslo -> tím víc to bude updatovat blood textur najednou)
                {
                    mediumAndLarge[i].UpdateMediumAndLargeTexture();
                }
            }
        }

        public static void DrawBlood(SpriteBatch spriteBatch)
        {
            foreach(var k in bloodList)
            {
                k.Draw(spriteBatch);
            }
        }

        public static void UpdateTracks()
        {
            foreach(var k in tracks)
            {
                k.Update();
            }
        }

        public static void RemoveTracks()
        {
            foreach(var k in tracksToDelete)
            {
                tracks.Remove(k);
            }
            tracksToDelete.Clear();
        }

        public static void DrawTracks(SpriteBatch spriteBatch)
        {
            foreach (var k in tracks)
            {
                k.Draw(spriteBatch);
            }
        }

        public static void DrawDeadZombies(SpriteBatch spriteBatch)
        {
            foreach(var k in deadZombieBodies)
            {
                k.Draw(spriteBatch);
            }
        }

        public static void UpdateDeadZombies(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            foreach(var k in deadZombieBodies)
            {
                k.Update(gameTime, 1, 1, graphicsDevice); //neni potřeba přiřadit číselné hodnoty
            }

            /*foreach(var k in deadZombieBodies)
            {
                k.feedingNumberOfZombies = 0;
            }

            foreach(var k in deadZombieBodies)
            {
                foreach(var o in zombieList)
                {
                    if(o.zombieAnimation.Texture == Textures.ZombieEating && Utils.IntersectCircles(o.hitboxCircle, k.hitboxCircle))
                    {
                        k.feedingNumberOfZombies++;
                    }
                }
            }*/
        }

        public static void DeleteZombieBodies()
        {
            foreach(var k in deadZombieBodiesToDelete)
            {
                deadZombieBodies.Remove(k);
            }
            deadZombieBodiesToDelete.Clear();
        }

        public static void UpdateListOfAllObjects()
        {
            allObjects.Clear();
            foreach (var k in tracks)
            {
                allObjects.Add(k);
            }
            foreach (var k in zombieList)
            {
                allObjects.Add(k);
            }
            foreach(var k in bulletList)
            {
                allObjects.Add(k);
            }
            foreach(var k in deadZombieBodies)
            {
                allObjects.Add(k);
            }
            foreach(var k in bloodList)
            {
                allObjects.Add(k);
            }
        }

        public static void UpdateBullets(GameTime gameTime)
        {
            foreach(var k in bulletList)
            {
                k.Update(gameTime);
            }
        }

        public static void DrawBullets(SpriteBatch spriteBatch)
        {
            foreach(var k in bulletList)
            {
                k.Draw(spriteBatch);
            }
        }

        public static void DeleteBullets()
        {
            if(bulletsToDelete.Count >= 1)
            {
                foreach(var k in bulletsToDelete)
                {
                    bulletList.Remove(k);
                }
                bulletsToDelete.Clear();
            }
        }

        public static void UpdateZombies(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            foreach(var k in zombieList)
            {
                k.Update(gameTime, (int)Player.position.X + 40, (int)Player.position.Y + 35, graphicsDevice);
            }
        }

        public static void DrawZombies(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(var k in zombieList)
            {
                k.Draw(spriteBatch);
            }
        }

        public static void DeleteZombies()
        {
            foreach(var k in zombiesToDelete)
            {
                zombieList.Remove(k);
            }
            zombiesToDelete.Clear();
        }
    }
}
