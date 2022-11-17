using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Input;

namespace TestGame.Characters
{
    internal class Hero : IGameObject
    {
        private Texture2D _texture;
        private int schuifOp_X = 0;
        private int schuifOp_Y = 0;

        Animation animation;
        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 versnelling;

        private IInputReader _inputReader;

        public Hero(Texture2D texture, IInputReader inputReader)
        {
            this._texture = texture;

            animation = new Animation();
            #region add frames
            //rij 0
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 128, 128)));
            //rij 1
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 128, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 128, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 128, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 128, 128, 128)));
            //rij 2
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 256, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 256, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 256, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 256, 128, 128)));
            //rij 3
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 384, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 384, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 384, 128, 128)));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 384, 128, 128)));
            #endregion

            positie = new Vector2(0, 0);
            snelheid = new Vector2(5, 5);
            versnelling = new Vector2(0.1f, 0.1f);

            _inputReader = inputReader;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, positie, animation.CurrentFrame.SourceRectangle, Color.White);
            //Debug.WriteLine(positie);

        }

        public void Update(GameTime gameTime)
        {
            var direction = _inputReader.ReadInput();
            direction *= snelheid;
            Vector2 oldPositie = positie;
            positie += direction;
            Richting nieuweRichting = Richting.Idle;

            //niet buiten het scherm gaan
            //kijk of de volgende positie buiten het scherm zou vallen
            //als de X of Y te groot of te klein zouden worden
            if (positie.X > 800 - 128 || positie.X < 0)
            {
                positie.X = oldPositie.X;
            }

            if (positie.Y > 480 - 128 || positie.Y < 0)
            {
                positie.Y = oldPositie.Y;
                
            }


            if (positie.X < oldPositie.X)
            {
                //naar links
                nieuweRichting = Richting.Left;
            }

            if (oldPositie.X < positie.X)
            {
                //naar rechts
                nieuweRichting = Richting.Right;
            }

            if (positie.Y < oldPositie.Y)
            {
                //naar boven
                nieuweRichting = Richting.Up;
            }

            if (oldPositie.Y < positie.Y)
            {
                //naar onder
                nieuweRichting = Richting.Down;
            }

            




            animation.Update(gameTime, nieuweRichting);
            //Move();   //moved het vanzelf

        }

        public void Move()
        {
            
            positie += snelheid;
            //snelheid += versnelling;
            //extra: limit
            //float maximaleSnelheid = 10;
            //snelheid = Limit(snelheid, maximaleSnelheid);
            //
            if (positie.X > 800-128 || positie.X < 0)
            {
                snelheid.X *= -1;
                //versnelling.X *= -1;
            }
            
            if (positie.Y > 480-128 || positie.Y < 0)
            {
                snelheid.Y *= -1;
                //versnelling *= -1;
            }
            //Debug.WriteLine(positie);


        }

        private Vector2 Limit(Vector2 v, float max)
        {
            if (v.Length() > max)
            {
                var ratio = max / v.Length();
                v.X *= ratio;
                v.Y *= ratio;
            }
            return v;
        }

        private void MoveWithMouse()
        {
            MouseState state = Mouse.GetState();
            Vector2 mouseVector = new Vector2(state.X, state.Y);

            var richting = mouseVector - positie;
            richting.Normalize();

            //zonder extra
            /*
            var afTeLeggenAfstand = richting * snelheid;
            positie += afTeLeggenAfstand;
            */

            //met extra
            richting = Vector2.Multiply(richting, 0.1f);
            snelheid += richting;
            snelheid = Limit(snelheid, 10);
            positie += snelheid;


        }


    }
}
