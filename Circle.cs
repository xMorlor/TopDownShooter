using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooterFinal
{
    public class Circle
    {
        public Vector2 Center;
        public float Radius;

        public Circle(Vector2 center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public void Update(Vector2 center)
        {
            this.Center = center;
        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw()
        }*/
    }
}
