using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;

namespace TopDownShooterFinal
{
    //internal class MuzzleFlashAnimation
    static class MuzzleFlashAnimation
    {
        public static Texture2D texture = Textures.MuzzleFlash1;
        private static Rectangle destinationRectangle;
        public static bool draw = false;
        private static int num = 0;

        public static void Update()
        {
            destinationRectangle = new Rectangle((int)(Player.playerAnimation.originBullet.X + Player.position.X), (int)(Player.playerAnimation.originBullet.Y + Player.position.Y), texture.Width, texture.Height);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (draw)
            {
                num++;
                if(Player.rifleEquiped ||Player.shotgunEquiped) spriteBatch.Draw(texture, destinationRectangle, null, Color.White, MathHelper.ToRadians((float)Player.playerAnimation.angle2), new Vector2(texture.Width / 3.8f, texture.Height / 1.9f), SpriteEffects.None, 1);
                else spriteBatch.Draw(texture, destinationRectangle, null, Color.White, MathHelper.ToRadians((float)Player.playerAnimation.angle2), new Vector2(texture.Width / 3.8f, texture.Height / 2.2f), SpriteEffects.None, 1);
                if (!Player.handgunEquiped)
                {
                    if (texture == Textures.MuzzleFlash1 && num == 2)
                    {
                        num = 0;
                        texture = Textures.MuzzleFlash2;
                    }
                    else if (texture == Textures.MuzzleFlash2 && num == 2)
                    {
                        num = 0;
                        texture = Textures.MuzzleFlash3;
                    }
                    else if (texture == Textures.MuzzleFlash3 && num == 2)
                    {
                        num = 0;
                        texture = Textures.MuzzleFlash1;
                        draw = false;
                    }
                }
                else
                {
                    draw = false;
                    texture = Textures.MuzzleFlash1;
                    num = 0;
                }
            }
        }
    }
}
