using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    class Tracks
    {
        private Texture2D texture;
        private int fadeAfter;
        private bool fade;
        private Rectangle destRectangle;
        private float angle;
        private Color color;
        public Vector2 position { get; set; }

        public Tracks(int index, float ang)
        {
            texture = Manager.trackTextures[index];
            position = new Vector2(Player.hitboxCircle.Center.X - texture.Width / 2 + 15, Player.hitboxCircle.Center.Y - texture.Height / 2 + 15);
            fade = false;
            fadeAfter = 300;
            angle = ang;
            
            color = Color.White;
            destRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            destRectangle.X = (int)position.X;
            destRectangle.Y = (int)position.Y;
            
            fadeAfter--;
            if (fadeAfter == 0)
            {
                fade = true;
            }
            if (fade)
            {
                color.R--;
                color.G--;
                color.B--;
                color.A--;
                if(color.A == 0)
                {
                    Manager.tracksToDelete.Add(this);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {                                                         //otočená textura
            spriteBatch.Draw(texture, position, null, color, angle - 270, new Vector2(texture.Width / 2, texture.Height / 2), 1, SpriteEffects.None, 1);
        }
    }
}
