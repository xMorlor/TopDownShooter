using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooterFinal
{
    static class Cursor
    {
        public static int posX;
        public static int posY;
        public static Vector2 position = new Vector2();
        public static Rectangle mouseRectangle;
        public static Vector2 mousePosition;

        public static void Update(GameTime gameTime)
        {
            posX = Mouse.GetState().X;
            posY = Mouse.GetState().Y;

            mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            position = new Vector2(posX, posY);
            //source: https://community.monogame.net/t/solved-how-can-i-get-the-world-coords-of-the-mouse-2d/11263
            Matrix invertedMatrix = Matrix.Invert(Game1.camera.Transform);
            position = Vector2.Transform(position, invertedMatrix);

            posX = (int)position.X;
            posY = (int)position.Y;

            mouseRectangle = new Rectangle(posX, posY, 1, 1);
        }
    }
}
