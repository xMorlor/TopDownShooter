using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TopDownShooterFinal
{
    class ZombieAnimation
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        public int totalFrames;
        Color fadeColor;
        public bool fadeOut;
        bool bFade;

        public ZombieAnimation(Texture2D texture, int rows, int columns)
        {
            bFade = false;
            fadeOut = false;
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update(GameTime gameTime, Zombie zombie)
        {
            if (fadeOut == false)
            {
                if (Texture == Textures.ZombieIdle || Texture == Textures.ZombieEating || Texture == Textures.ZombieRun || Texture == Textures.ZombieSaunter || Texture == Textures.ZombieWalk)
                {
                    currentFrame++;
                    if (currentFrame == totalFrames) currentFrame = 0;
                }
                else if (Texture == Textures.ZombieDeath1 || Texture == Textures.ZombieDeath2)
                {
                    if (currentFrame < totalFrames - 2)
                    {
                        currentFrame++;
                    }
                }
                else if(Texture == Textures.ZombieAttack1 || Texture == Textures.ZombieAttack2 || Texture == Textures.ZombieAttack3)
                {
                    currentFrame++;
                    if (currentFrame == totalFrames)
                    {
                        Player.onHitFlash = true;
                        Player.health -= 5;
                        zombie.attacking = false;
                        if (zombie.walking)
                        {
                            zombie.MakeZombieWalk();
                        }
                        else
                        {
                            zombie.MakeZombieRun();
                        }
                    }
                }
                fadeColor = DayNight.color;
                //fadeColor.A = DayNight.color.R;           bez tohohle to dělá fade mrtvoly černý a s tim zas bílý
            }
            else
            {
                
                if (!bFade)
                {
                    foreach (var k in Manager.bloodList)
                    {
                        if (k.zombie == zombie && k.update == false && k.texture != Textures.BloodSmall)
                        {
                            bFade = true;
                        }
                    }
                }
                
                if(fadeOut)
                {
                    //protože se to buguje (když se nastaví fade out true, tak nestíhá dojet animace
                    if (currentFrame < totalFrames - 2)
                    {
                        currentFrame++;
                    }
                }
                if (bFade)
                {
                    if (fadeColor.R > 0) fadeColor.R--;
                    if (fadeColor.G > 0) fadeColor.G--;
                    if (fadeColor.B > 0) fadeColor.B--;
                    if (fadeColor.A > 0) fadeColor.A--;
                    if (fadeColor == new Color(0, 0, 0, 0)) Manager.deadZombieBodiesToDelete.Add(zombie);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, double angle)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            Vector2 origin = new Vector2((Texture.Width / Columns) / 2, (Texture.Height / Rows) / 2);
                                                                                                                            //+180 protože to bylo o půl kruh otočený
            if (!fadeOut) spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, DayNight.color, MathHelper.ToRadians((float)angle + 180), origin, SpriteEffects.None, 1);
            else spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, fadeColor, MathHelper.ToRadians((float)angle + 180), origin, SpriteEffects.None, 1);

            //spriteBatch.DrawString(Textures.debug, fadeColor + "", new Vector2(500, 500), Color.White);
        }
    }
}
