using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    internal class AnimationFrame
    {
        public Rectangle SourceRectangle { get; set; }
        //public Rectangle HitboxRectangle { get; set; }

        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
            
        }

        /*public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }*/
    }
}
