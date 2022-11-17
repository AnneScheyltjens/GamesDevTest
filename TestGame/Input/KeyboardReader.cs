using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Input
{
    internal class KeyboardReader : IInputReader
    {
        private Keys left = Keys.Left;
        private Keys right = Keys.Right;
        private Keys up = Keys.Up;
        private Keys down = Keys.Down;

        public bool IsDestinationInput => false;

        public Vector2 ReadInput()
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;
            if (state.IsKeyDown(left))
            {
                direction.X -= 1;
            }
            if (state.IsKeyDown(right))
            {
                direction.X += 1;
            }

            if (state.IsKeyDown(up))
            {
                direction.Y -= 1;
            }
            if (state.IsKeyDown(down))
            {
                direction.Y += 1;
            }
            return direction;
        }
    }
}
