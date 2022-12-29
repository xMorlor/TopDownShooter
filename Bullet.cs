using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    class Bullet : MotherClass
    {
        public Texture2D texture;
        private float posX;
        private float posY;
        private double angle;
        private int duration;

        public Circle hitboxCircle;
        public Rectangle hitboxRectangle;

        public bool shotFromRifle; 
        public bool shotFromHandgun;  
        public bool shotFromShotgun;  

        public Bullet(Vector2 pos, Texture2D t, double angl, GameTime gameTime)
        {
            angle = angl;
            position = pos;
            texture = t;
            posX = ((float)(Math.Cos(angl / 360.0 * 2 * Math.PI) * 13));
            posY = ((float)(Math.Sin(angl / 360.0 * 2 * Math.PI) * 13));
            duration = 0;

            position.X -= posX * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y -= posY * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            hitboxCircle = new Circle(position, 2);
            hitboxRectangle = new Rectangle((int)(hitboxCircle.Center.X - hitboxCircle.Radius * 6), (int)(hitboxCircle.Center.Y - hitboxCircle.Radius * 6), (int)(hitboxCircle.Radius * 12), (int)(hitboxCircle.Radius * 12));
        }

        public void Update(GameTime gameTime)
        {
            position.X += posX * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += posY * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            hitboxCircle.Update(position);
            hitboxRectangle = new Rectangle((int)(hitboxCircle.Center.X - hitboxCircle.Radius * 3), (int)(hitboxCircle.Center.Y - hitboxCircle.Radius * 3), (int)(hitboxCircle.Radius * 6), (int)(hitboxCircle.Radius * 6));


            duration++;
            if(duration == 65)
            {
                Manager.bulletsToDelete.Add(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {                                                                                         //textura je otočená tak proto -90 angle
            spriteBatch.Draw(texture, position, null, Color.White, MathHelper.ToRadians((float)angle - 90), Vector2.Zero, 0.25f, SpriteEffects.None, 1);
        }
    }
}
