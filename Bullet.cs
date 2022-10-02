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
        private Rectangle destinationRectangle;
        private double angle;
        private int duration;

        public Circle hitboxCircle;
        private Vector2 positionForCircle;

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

            /*positionForCircle = position;
            positionForCircle.X += posX * 2.3f;
            positionForCircle.Y += posY * 2.3f;*/
            hitboxCircle = new Circle(position, 2);
        }

        public void Update(GameTime gameTime)
        {
            position.X += posX * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += posY * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //positionForCircle.X += posX * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //positionForCircle.Y += posY * 150 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitboxCircle.Update(position);

            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 4, 20);
            
            duration++;
            if(duration == 65)
            {
                Manager.bulletsToDelete.Add(this);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {                                                                                         //textura je otočená tak proto -90 angle
            spriteBatch.Draw(texture, destinationRectangle, null, Color.White, MathHelper.ToRadians((float)angle - 90), Vector2.Zero, SpriteEffects.None, 1);

            //spriteBatch.Draw(Textures.exp, new Vector2(position.X - 2, position.Y - 2), Color.White);
        }
    }
}
