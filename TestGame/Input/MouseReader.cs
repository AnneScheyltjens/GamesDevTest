using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Input
{
    class MouseReader : IInputReader
    {
        public bool IsDestinationInput => true;

        public Vector2 ReadInput()
        {
            MouseState state = Mouse.GetState();
            Vector2 directionMouse = new Vector2(state.X, state.Y);
            //kijkt of de muis bewogen heeft
            /*if (directionMouse != Vector2.Zero)
            {
                //normaliseert de vector zodat we enkel de richting over houden
                directionMouse.Normalize();
            }*/
            return directionMouse;
        }
    }
}
