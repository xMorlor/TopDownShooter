using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace TopDownShooterFinal
{
    internal class GridTile
    {
        public bool blocked = false;
        public Vector2 position;
        public GridTile parent = null;
        public bool visited = false;
        public Rectangle collisionRectangle;
        public Circle collisionCircle;

        public bool mapRec = false;

        public int f;
        public int g;
        public int h;
    }
}
