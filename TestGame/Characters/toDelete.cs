using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Input;
using TestGame;
using Microsoft.Xna.Framework;

namespace TestGame.Characters
{
    /*internal class toDelete
    {
        var direction = _inputReader.ReadInput();

            if (_inputReader.IsDestinationInput)
            {
                direction -= CurrentPositie.Positie;
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }
}

direction *= Snelheid;


var oldPosition = CurrentPositie.Positie;
CurrentPositie.Positie += direction;
Richting = Richting.Idle;

//screen size
if (CurrentPositie.Positie.X > 800 - 88 || CurrentPositie.Positie.X < 0 - 40)
{
    //Positie.X = OldPosition.X;
    CurrentPositie.Positie = new Vector2(oldPosition.X, CurrentPositie.Positie.Y);
    //versnelling.X *= -1;
}

//screen size
if (CurrentPositie.Positie.Y > 480 - 88 || CurrentPositie.Positie.Y < 0 - 40)
{
    //Positie.Y = OldPosition.Y;
    CurrentPositie.Positie = new Vector2(CurrentPositie.Positie.X, oldPosition.Y);
    //versnelling *= -1;
}


if (oldPosition.X > CurrentPositie.Positie.X)
{
    //naar links
    richting = Richting.Left;
}

if (oldPosition.X < CurrentPositie.Positie.X)
{
    //naar rechts
    richting = Richting.Right;
}

if (CurrentPositie.Positie.Y < oldPosition.Y)
{
    //naar boven
    richting = Richting.Up;
}

if (oldPosition.Y < CurrentPositie.Positie.Y)
{
    //naar onder
    richting = Richting.Down;
}

//if richting.up -> spring en daarna vallen

if (richting == Richting.Up)
{
    //spring
    YBeweging = -5;
}
else if (YBeweging < 0)
{
    YBeweging += Gravity;
}
//Positie.Y += YBeweging;
CurrentPositie.Positie = new Vector2(CurrentPositie.Positie.X, CurrentPositie.Positie.Y + YBeweging);

    }*/
}
