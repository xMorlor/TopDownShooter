using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    class Tile
    {
        public Vector2 position { get; set; }
        public Texture2D texture { get; set; }
        public Rectangle hitboxRectangle1 = new Rectangle(0, 0, 0, 0);
        public Rectangle hitboxRectangle2 = new Rectangle(0, 0, 0, 0);
        private float angle;
        private Random rnd = new Random();

        public Tile(Vector2 position, Texture2D texture)
        {
            angle = 0;
            this.position = position;
            this.texture = texture;
        }

        public void SetAngle()
        {
            angle = rnd.Next(1, 361);
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
            spriteBatch.Draw(texture, position, null, Color.White, angle, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
