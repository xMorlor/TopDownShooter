using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace TopDownShooterFinal
{
    //internal class Blood
    class Blood
    {
        private int num, randomNum, randomAngle, maxCircleRange, range, fadeOutNum, increaseRange;
        private Color[] colors1D, defaultColors;
        private Color color;
        private Random rnd;
        public Texture2D texture { get; set; }
        private Vector2 origin, position;
        private Rectangle destRectangle;
        public Zombie zombie { get; set; }
        private bool fadeOut, justHit;
        public bool update { get; set; }
        private int posX, posY;

        public Blood(bool justHit, Vector2 position, Zombie zombie, int num, GraphicsDevice graphicsDevice)
        {
            increaseRange = 0;
            fadeOut = false;
            fadeOutNum = 3600;
            this.zombie = zombie;
            update = true;
            range = 1;
            rnd = new Random();
            randomNum = rnd.Next(1, 3);
            this.justHit = justHit;
            if (justHit) update = false;
            randomAngle = rnd.Next(1, 361);
            this.position = position;
            color = DayNight.color;
            if(!justHit) this.position += zombie.direction * num;

            if (randomNum == 1 && !justHit)
            {
                texture = Textures.BloodMedium;
                destRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Textures.BloodMedium.Width, Textures.BloodMedium.Height);
            }
            else if (randomNum == 2 && !justHit)
            {
                texture = Textures.BloodLarge;
                destRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Textures.BloodLarge.Width, Textures.BloodLarge.Height);
            }
            else if (justHit)
            {
                texture = Textures.BloodSmall;
                destRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Textures.BloodSmall.Width, Textures.BloodSmall.Height);
            }
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            if (texture.Height > texture.Width)
            {
                maxCircleRange = texture.Height / 2;
            }
            else
            {
                maxCircleRange = texture.Width / 2;
            }
            //https://stackoverflow.com/questions/5162405/image-rectangle-partial-fade-in-xna
            if(texture != Textures.BloodSmall)
            {
                defaultColors = new Color[texture.Width * texture.Height];
                texture.GetData<Color>(defaultColors);

                colors1D = new Color[texture.Width * texture.Height];
                texture.GetData<Color>(colors1D);

                for (int x = 0; x < colors1D.Length; x++)
                {
                    colors1D[x].A = 0;
                    colors1D[x].R = 0;
                    colors1D[x].G = 0;
                    colors1D[x].B = 0;
                }
                texture = new Texture2D(graphicsDevice, texture.Width, texture.Height); 
            }
        }

        public void Update()
        {
            if(!fadeOut)color = DayNight.color;
            if (zombie.zombieAnimation.fadeOut || texture == Textures.BloodSmall)
            {
                fadeOutNum--;
                if(fadeOutNum == 0)
                {
                    fadeOut = true;
                }
                if (fadeOut)
                {
                    if (color.A > 0) color.A--;
                    if (color.R > 0) color.R--;
                    if (color.G > 0) color.G--;
                    if (color.B > 0) color.B--;
                    if(color.A == 0 && color.R == 0 && color.G == 0 && color.B == 0)
                    {
                        Manager.bloodToRemove.Add(this);
                    }
                }
            }
        }

        public void UpdateMediumAndLargeTexture()
        {
            if (update)
            {
                colors1D = new Color[texture.Width * texture.Height];
                texture.GetData<Color>(colors1D);
                posX = 0;
                posY = 0;
                for (int x = 0; x < this.colors1D.Length; x++)
                {
                    posX++;
                    if (posX == this.texture.Width)
                    {
                        posY++;
                        posX = 0;
                    }
                    if (defaultColors[x].A == 0 && defaultColors[x].R == 0 && defaultColors[x].G == 0 && defaultColors[x].B == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (Vector2.Distance(new Vector2(posX, posY) + position - new Vector2(texture.Width / 2, texture.Height / 2), position) < range)
                        {
                            if (defaultColors[x].A > colors1D[x].A)
                            {
                                colors1D[x].A++;
                            }
                            if (defaultColors[x].R > colors1D[x].R)
                            {
                                colors1D[x].R++;
                            }
                            if (defaultColors[x].G > colors1D[x].G)
                            {
                                colors1D[x].G++;
                            }
                            if (defaultColors[x].B > colors1D[x].B)
                            {
                                colors1D[x].B++;
                            }
                        }
                    }
                }
                texture.SetData<Color>(colors1D);
                increaseRange++;
                if (range < maxCircleRange && increaseRange % 10 == 0) range++;
                num++;
                if (num == 540)
                {
                    update = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, randomAngle, origin, 1, SpriteEffects.None, 1);
        }
    }
}
