using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    static class WeaponIcons
    {
        private static Vector2 knifePos = new Vector2(Utils.screenWidth - 20 - Textures.KnifeLogo.Width, Utils.screenHeigth - 138);
        private static Vector2 handgunPos = new Vector2(Utils.screenWidth - 20 - Textures.HandgunLogo.Width, Utils.screenHeigth - 196);
        private static Vector2 shotgunPos = new Vector2(Utils.screenWidth - 20 - Textures.ShotgunLogo.Width, Utils.screenHeigth - 254);
        private static Vector2 riflePos = new Vector2(Utils.screenWidth - 20 - Textures.AkLogo.Width, Utils.screenHeigth - 312);

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (Player.knifeEquiped)
            {
                spriteBatch.Draw(Textures.KnifeLogo, knifePos, Color.White);
                spriteBatch.Draw(Textures.HandgunLogo, handgunPos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.ShotgunLogo, shotgunPos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.AkLogo, riflePos, new Color(255, 255, 255, 100));
            }
            else if (Player.handgunEquiped)
            {
                spriteBatch.Draw(Textures.KnifeLogo, knifePos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.HandgunLogo, handgunPos, Color.White);
                spriteBatch.Draw(Textures.ShotgunLogo, shotgunPos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.AkLogo, riflePos, new Color(255, 255, 255, 100));
            }
            else if (Player.shotgunEquiped)
            {
                spriteBatch.Draw(Textures.KnifeLogo, knifePos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.HandgunLogo, handgunPos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.ShotgunLogo, shotgunPos, Color.White);
                spriteBatch.Draw(Textures.AkLogo, riflePos, new Color(255, 255, 255, 100));
            }
            else if (Player.rifleEquiped)
            {
                spriteBatch.Draw(Textures.KnifeLogo, knifePos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.HandgunLogo, handgunPos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.ShotgunLogo, shotgunPos, new Color(255, 255, 255, 100));
                spriteBatch.Draw(Textures.AkLogo, riflePos, Color.White);
            }

            //stamina bar
            spriteBatch.Draw(Textures._1x1, new Rectangle(Utils.screenWidth - 20 - 102, Utils.screenHeigth - 80,102, 22), null, Color.DarkGray, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(Textures._1x1, new Rectangle(Utils.screenWidth - 20 - 101, Utils.screenHeigth - 79, Player.stamina, 20), null, new Color(0, 23, 39), 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
