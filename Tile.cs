using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    class Tile
    {
        public Vector2 position;
        public Texture2D texture;
        public Rectangle hitboxRectangle1 = new Rectangle(0, 0, 0, 0);
        public Rectangle hitboxRectangle2 = new Rectangle(0, 0, 0, 0);

        public Tile(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }

        public void Update()
        {

        }

        public void CreateHitboxRectangle1(int posX, int posY, int width, int heigth)
        {
            hitboxRectangle1 = new Rectangle(posX, posY, width, heigth);
        }

        public void CreateHitboxRectangle2(int posX, int posY, int width, int heigth)
        {
            hitboxRectangle2 = new Rectangle(posX, posY, width, heigth);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            if(hitboxRectangle1 != new Rectangle(0, 0, 0, 0))
            {
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y), Color.White);
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y), Color.White);
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y + hitboxRectangle1.Height), Color.White);
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y + hitboxRectangle1.Height), Color.White);


                
                //spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y), Color.White);
                //spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y + hitboxRectangle1.Height), Color.White);
                //spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y + hitboxRectangle1.Height), Color.White);
                /*spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y) + "", new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y), Color.White);
                spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y) + "", new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y), Color.White);
                spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y + hitboxRectangle1.Height) + "", new Vector2(hitboxRectangle1.Location.X, hitboxRectangle1.Location.Y + hitboxRectangle1.Height), Color.White);
                spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y + hitboxRectangle1.Height) + "", new Vector2(hitboxRectangle1.Location.X + hitboxRectangle1.Width, hitboxRectangle1.Location.Y + hitboxRectangle1.Height), Color.White);
                */
            }
            if (hitboxRectangle2 != new Rectangle(0, 0, 0, 0))
            {
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle2.Location.X, hitboxRectangle2.Location.Y), Color.White);
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle2.Location.X + hitboxRectangle2.Width, hitboxRectangle2.Location.Y), Color.White);
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle2.Location.X, hitboxRectangle2.Location.Y + hitboxRectangle2.Height), Color.White);
                spriteBatch.Draw(Textures.exp, new Vector2(hitboxRectangle2.Location.X + hitboxRectangle2.Width, hitboxRectangle2.Location.Y + hitboxRectangle2.Height), Color.White);

                //spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle2.Location.X, hitboxRectangle2.Location.Y) + "", new Vector2(hitboxRectangle2.Location.X, hitboxRectangle2.Location.Y), Color.White);
                //spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle2.Location.X + hitboxRectangle2.Width, hitboxRectangle2.Location.Y) + "", new Vector2(hitboxRectangle2.Location.X + hitboxRectangle2.Width, hitboxRectangle2.Location.Y), Color.White);
                //spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle2.Location.X, hitboxRectangle2.Location.Y + hitboxRectangle2.Height) + "", new Vector2(hitboxRectangle2.Location.X, hitboxRectangle2.Location.Y + hitboxRectangle2.Height), Color.White);
                //spriteBatch.DrawString(Textures.debug, new Vector2(hitboxRectangle2.Location.X + hitboxRectangle2.Width, hitboxRectangle2.Location.Y + hitboxRectangle2.Height) + "", new Vector2(hitboxRectangle2.Location.X + hitboxRectangle2.Width, hitboxRectangle2.Location.Y + hitboxRectangle2.Height), Color.White);
            }
        }
    }
}
