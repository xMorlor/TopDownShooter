using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    static class Map
    {

        public static List<Vector2> positions = new List<Vector2>();
        
        /*private static int[,] map = {
           { },
           { },
           { },
           { },
           { },
           { }
       };*/

        //neni hotovo

        public static void SetPositions()
        {
            for(int x = 0; x < 20; x++)
            {
                for(int y = 0; y < 20; y++)
                {
                    Vector2 v = new Vector2(x * 89, y * 89);
                    positions.Add(v);
                }
            }
        }

        public static void Update(GameTime gameTime)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (Player.knifeEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X + 250 * (float)gameTime.ElapsedGameTime.TotalSeconds, positions[i].Y);

                        
                    }
                    else if (Player.handgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X + 220 * (float)gameTime.ElapsedGameTime.TotalSeconds, positions[i].Y);

                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X + 160 * (float)gameTime.ElapsedGameTime.TotalSeconds, positions[i].Y);

                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (Player.knifeEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X, positions[i].Y + 250 * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    }
                    else if (Player.handgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X, positions[i].Y + 220 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                
                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X, positions[i].Y + 160 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                        
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (Player.knifeEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X, positions[i].Y - 250 * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    }
                    else if (Player.handgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X, positions[i].Y - 220 * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X, positions[i].Y - 160 * (float)gameTime.ElapsedGameTime.TotalSeconds);

                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (Player.knifeEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X - 250 * (float)gameTime.ElapsedGameTime.TotalSeconds, positions[i].Y);

                    }
                    else if (Player.handgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X - 220 * (float)gameTime.ElapsedGameTime.TotalSeconds, positions[i].Y);

                    }
                    else if (Player.rifleEquiped || Player.shotgunEquiped)
                    {
                        positions[i] = new Vector2(positions[i].X - 160 * (float)gameTime.ElapsedGameTime.TotalSeconds, positions[i].Y);
                    }
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(var k in positions)
            {
                spriteBatch.Draw(Textures.testTile, k, Color.White);
            }
        }
    }
}
