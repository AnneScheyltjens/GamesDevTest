using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Collision
{
    public interface IMovingObject
    {
        public Position CurrentPositie { get; set; }
        public Position NextPositie { get; set; }

    }
}
