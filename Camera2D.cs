using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    public class Camera2D
    {
        //source: https://www.youtube.com/watch?v=ceBCDKU_mNw


        public Matrix Transform { get; private set; }

        public void Follow(GameTime gameTime)
        {
            var position = Matrix.CreateTranslation(
                -Player.position.X - 5,
                -Player.position.Y - 25,
                0);

            var offset = Matrix.CreateTranslation(
                Utils.screenWidth / 2,
                Utils.screenHeigth / 2,
                0);

            var position2 = Matrix.CreateTranslation(
               -Cursor.mousePosition.X / 3f + 326,
               -Cursor.mousePosition.Y / 3f + 206,
               0);

            Transform = position * offset * position2;
        }
    }
}
