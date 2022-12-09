using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    internal class Position
    {
        public Vector2 Positie { get; set; }
        public Richting Richting { get; set; }
        //public Vector2 Snelheid { get; set; }
        public Vector2 HitboxPositie { get; set; }
        public Rectangle HitboxRectangle { get; set; }
    }
}
