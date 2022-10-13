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
        public Matrix Transform { get; private set; }
        public Matrix TransformForShader { get; private set; }

        public void Follow()
        {
            var position = Matrix.CreateTranslation(
                -Cursor.position.X - (float)Math.Sin(Player.playerAnimation.angle) * 18.5f,
                -Cursor.position.Y + (float)Math.Cos(Player.playerAnimation.angle) * 18.5f,
                0);
            
            var offset = Matrix.CreateTranslation(Utils.screenWidth / 2, Utils.screenHeigth / 2, 0);

            Transform = position + offset;

            //odstranit pokud se nebude používat
            /*var offsetForShader = Matrix.CreateTranslation(Utils.screenWidth / 2, Utils.screenHeigth / 2, 1);

            TransformForShader = position + offsetForShader;*/
        }
    }
}
