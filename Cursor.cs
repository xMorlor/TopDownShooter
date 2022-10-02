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

        public static void Update(GameTime gameTime)
        {
            posX = Mouse.GetState().X;
            posY = Mouse.GetState().Y;
            position = new Vector2(posX, posY);
            mouseRectangle = new Rectangle(posX, posY, 1, 1);
        }
    }
}
