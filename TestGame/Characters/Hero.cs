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

namespace TestGame.Characters
{
    internal class Hero : IGameObject
    {
        private Texture2D _texture;
        private Rectangle _deelRectangle;
        private int schuifOp_X = 0;
        private int schuifOp_Y = 0;

        Animation animation;
        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 versnelling;

        public Hero(Texture2D texture)
        {
            this._texture = texture;
            _deelRectangle = new Rectangle(schuifOp_X, schuifOp_Y, 128, 128);

            animation = new Animation();
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

            positie = new Vector2(0, 0);
            snelheid = new Vector2(5, 5);
            versnelling = new Vector2(0.1f, 0.1f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_texture, positie, animation.CurrentFrame.SourceRectangle, Color.White);
            //Debug.WriteLine(positie);

        }

        public void Update(GameTime gameTime)
        {
            
            animation.Update(gameTime);
            Move();

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
