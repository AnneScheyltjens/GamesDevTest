using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Input;

namespace TestGame
{
    interface IMovable
    {
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }
        public Richting Richting { get; set; }
    }
}
