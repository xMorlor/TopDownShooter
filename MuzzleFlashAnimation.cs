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
    static class MuzzleFlashAnimation
    {
        public static Texture2D texture { get; set; }
        public static bool draw { get; set; }
        private static int num = 0;
        private static Vector2 position;

        static MuzzleFlashAnimation()
        {
            draw = false;
            texture = Textures.MuzzleFlash1;
        }

        public static void Update()
        {
            position = new Vector2(Player.playerAnimation.originBullet.X + Player.position.X, Player.playerAnimation.originBullet.Y + Player.position.Y);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (draw)
            {
                num++;
                if(Player.rifleEquiped ||Player.shotgunEquiped) spriteBatch.Draw(texture, position, null, Color.White, MathHelper.ToRadians((float)Player.playerAnimation.angle2), new Vector2(texture.Width / 3.8f, texture.Height / 1.9f), 1, SpriteEffects.None, 1);
                else spriteBatch.Draw(texture, position, null, Color.White, MathHelper.ToRadians((float)Player.playerAnimation.angle2), new Vector2(texture.Width / 3.8f, texture.Height / 2.2f), 1, SpriteEffects.None, 1);
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
