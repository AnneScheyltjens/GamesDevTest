using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Animatie;

namespace TestGame.Collision
{
    public class Position
    {
        public Vector2 Positie { get; set; }
        public Richting Richting { get; set; }
        public Vector2 HitboxPositie { get; set; }
        public Rectangle HitboxRectangle { get; set; }

        public Position()
        {
        }

        public Position(Vector2 positie, Rectangle hitboxRectangle)
        {
            Positie = positie;
            HitboxRectangle = hitboxRectangle;
        }
    }
}
