using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    class MovementManager
    {
        public void Move(IMovable movable)
        {
            var direction = movable.InputReader.ReadInput();
            if (movable.InputReader.IsDestinationInput)
            {
                direction -= movable.Position;
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }
            }

            var afstand = direction * movable.Speed;

            Vector2 oldPositie = movable.Position;

            movable.Position += afstand;

            movable.Richting = Richting.Idle;



            if (oldPositie.X > movable.Position.X)
            {
                //naar links
                movable.Richting = Richting.Left;
            }

            if (oldPositie.X < movable.Position.X)
            {
                //naar rechts
                movable.Richting = Richting.Right;
            }

            if (movable.Position.Y < oldPositie.Y)
            {
                //naar boven
                movable.Richting = Richting.Up;
            }

            if (oldPositie.Y < movable.Position.Y)
            {
                //naar onder
                movable.Richting = Richting.Down;
            }


        }
    }
}
