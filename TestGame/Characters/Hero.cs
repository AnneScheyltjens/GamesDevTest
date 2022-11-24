using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct2D1;
using SharpDX.MediaFoundation;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TestGame.Input;

namespace TestGame.Characters
{
    internal class Hero : IGameObject  //, IMovable
    {
        //private Texture2D _texture;
        public Texture2D Texture { get; set; }
        private int schuifOp_X = 0;
        private int schuifOp_Y = 0;

        private Animation animation;

        


        private Texture2D hitbox;
        //public Rectangle Hitbox { get; set; }
        public Vector2 HitboxPosition { get; set; }
        public Vector2 HitboxDownNr { get; set; }
        public Vector2 HitboxUpNr { get; set; }
        public Vector2 HitboxBreedNr { get; set; }
        public Vector2 SourceRectNr { get; set; }


        //old, but needed for new
        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 versnelling;
        private Richting richting;
        private IInputReader _inputReader;
        

        #region movementManager
        //new
        /*private MovementManager movementManager;
        
        public Richting Richting
        {
            get { return richting; }
            set { richting = value; }
        }
        public Vector2 Speed
        {
            get { return snelheid; }
            set { snelheid = value; }
        }
        public Vector2 Position
        {
            get { return positie; }
            set { positie = value; }
        }
        public IInputReader InputReader
        {
            get { return _inputReader; }
            set { _inputReader = value; }
        }
        */

        #endregion


        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphics)
        {
            Texture = texture;
            hitbox = new Texture2D(graphics, 1, 1);
            hitbox.SetData(new[] { Color.White });

            SourceRectNr = new Vector2(128, 128);
            HitboxBreedNr = new Vector2(35, 47);
            HitboxDownNr = new Vector2(48, 47);
            HitboxUpNr = new Vector2(48, 40);


            Rectangle breedHitbox = new Rectangle(
                (int)positie.X + (int)HitboxBreedNr.X,
                (int)positie.Y + (int)HitboxBreedNr.Y,
                (int)SourceRectNr.X - 40 - (int)HitboxBreedNr.X,
                (int)SourceRectNr.Y - 40 - (int)HitboxBreedNr.Y);

            Rectangle upHitbox = new Rectangle(
                (int)positie.X + (int)HitboxUpNr.X,
                (int)positie.Y + (int)HitboxUpNr.Y,
                (int)SourceRectNr.X - 50 - (int)HitboxUpNr.X,
                (int)SourceRectNr.Y - 40 - (int)HitboxUpNr.Y);
            
            Rectangle downHitbox = new Rectangle(
                (int)positie.X + (int)HitboxDownNr.X,
                (int)positie.Y + (int)HitboxDownNr.Y,
                (int)SourceRectNr.X - 50 - (int)HitboxDownNr.X,
                (int)SourceRectNr.Y - 40 - (int)HitboxDownNr.Y);

            animation = new Animation();
            #region add frames
            //rij 0
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 128, 128), upHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 128, 128), upHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 128, 128), upHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 128, 128), upHitbox));
            //rij 1
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 128, 128, 128), breedHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 128, 128, 128), breedHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 128, 128, 128), breedHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 128, 128, 128), breedHitbox));
            //rij 2
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 256, 128, 128), downHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 256, 128, 128), downHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 256, 128, 128), downHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 256, 128, 128), downHitbox));
            //rij 3
            animation.AddFrame(new AnimationFrame(new Rectangle(0, 384, 128, 128), breedHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(128, 384, 128, 128), breedHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(256, 384, 128, 128), breedHitbox));
            animation.AddFrame(new AnimationFrame(new Rectangle(384, 384, 128, 128), breedHitbox));
            #endregion
            
            //old 
            positie = new Vector2(0, 0);
            snelheid = new Vector2(5, 5);
            versnelling = new Vector2(0.1f, 0.1f);
            //new
            //movementManager = new MovementManager();

            //standaard
            //Hitbox = animation.CurrentFrame.SourceRectangle;
            //kleiner
            /*Hitbox = new Rectangle(
                (int)positie.X+40,
                (int)positie.Y+40,
                animation.CurrentFrame.SourceRectangle.Width-80,
                animation.CurrentFrame.SourceRectangle.Height-80);*/

            //HitboxPosition = new Vector2((int)positie.X + 40, (int)positie.Y + 40);
            HitboxPosition = new Vector2((int)positie.X + (int)HitboxDownNr.X, (int)positie.Y + (int)HitboxDownNr.Y);
            _inputReader = inputReader;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hitbox, HitboxPosition, animation.CurrentFrame.HitboxRectangle, Color.Green);
            spriteBatch.Draw(Texture, positie, animation.CurrentFrame.SourceRectangle, Color.White);
            //Debug.WriteLine(positie);

        }

        public void Update(GameTime gameTime)
        {

            Move();
            animation.Update(gameTime, richting);
            
            //MoveOld();   //moved het vanzelf
            /*Hitbox = new Rectangle(
                (int)positie.X + 40,
                (int)positie.Y + 40,
                animation.CurrentFrame.SourceRectangle.Width - 80,
                animation.CurrentFrame.SourceRectangle.Height - 80);*/

            //check richting
            //smal if idle, up, down
            if (richting == Richting.Idle || richting == Richting.Down)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxDownNr.X,
                    (int)positie.Y + (int)HitboxDownNr.Y);
            } else if (richting == Richting.Up)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxUpNr.X,
                    (int)positie.Y + (int)HitboxUpNr.Y);
            }
            else if (richting == Richting.Left)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxBreedNr.X,
                    (int)positie.Y + (int)HitboxBreedNr.Y-5);
            } else
            {
                //schuif positie nog een beetje op
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxBreedNr.X +5,
                    (int)positie.Y + (int)HitboxBreedNr.Y-5);
            }

            //HitboxPosition = new Vector2((int)positie.X + 40, (int)positie.Y + 40);

        }

        public void Move()
        {
            //movementManager.Move(this);
            #region move
            
            var direction = _inputReader.ReadInput();

            if (_inputReader.IsDestinationInput)
            {
                direction -= positie;
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }
            }

            direction *= snelheid;


            Vector2 oldPositie = positie;
            positie += direction;
            richting = Richting.Idle;

            if (positie.X > 800 - 88 || positie.X < 0-40)
            {
                positie.X = oldPositie.X;
                //versnelling.X *= -1;
            }

            if (positie.Y > 480 - 88 || positie.Y < 0-40)
            {
                positie.Y = oldPositie.Y;
                //versnelling *= -1;
            }


            if (oldPositie.X > positie.X)
            {
                //naar links
                richting = Richting.Left;
            }

            if (oldPositie.X < positie.X)
            {
                //naar rechts
                richting = Richting.Right;
            }

            if (positie.Y < oldPositie.Y)
            {
                //naar boven
                richting = Richting.Up;
            }

            if (oldPositie.Y < positie.Y)
            {
                //naar onder
                richting = Richting.Down;
            }

            #endregion

        }

        public void MoveOld()
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
