using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace TopDownShooterFinal
{
    class GridTile
    {
        public bool blocked { get; set; }
        public Vector2 position { get; set; }
        public GridTile parent { get; set; }
        public bool visited { get; set; }
        public Rectangle collisionRectangle { get; set; }
        public Circle collisionCircle { get; set; }

        public bool mapRec { get; set; }

        public int f { get; set; }
        public int g { get; set; }
        public int h { get; set; }

        public GridTile()
        {
            this.blocked = false;
            parent = null;
            visited = false;
            mapRec = false;
        }
    }
}
