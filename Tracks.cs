using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    class Tracks : MotherClass
    {
        private Texture2D texture;
        int fadeAfter;
        bool fade;
        private Rectangle destRectangle;
        float angle;
        Color color;

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
                /*poud nebude fungovat:
                if (fadeColor.R > 0) fadeColor.R--;
                if (fadeColor.G > 0) fadeColor.G--;
                if (fadeColor.B > 0) fadeColor.B--;
                if (fadeColor.A > 0) fadeColor.A--;
                 */
                if(color.A == 0)
                {
                    Manager.tracksToDelete.Add(this);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {                                                         //otočená textura
            spriteBatch.Draw(texture, destRectangle, null, color, angle - 270, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 1);
        }
    }
}
