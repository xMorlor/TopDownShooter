using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace TopDownShooterFinal
{
    class GridTile
    {
        public bool blocked = false;
        public Vector2 position { get; set; }
        public GridTile parent = null;
        public bool visited = false;
        public Rectangle collisionRectangle { get; set; }
        public Circle collisionCircle { get; set; }

        public bool mapRec = false;

        public int f { get; set; }
        public int g { get; set; }
        public int h { get; set; }
    }
}
