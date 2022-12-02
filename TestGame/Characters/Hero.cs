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

        //private Animation animation;

        public Animation Animation { get; set; }


        
        private Texture2D hitbox;
        public Rectangle HitboxRectangle { get; set; }   //deze steeds opnieuw maken met de geupdate hitboxPosition
        public Vector2 HitboxPosition { get; set; }

        public Vector2 HitboxDownNr { get; set; }
        public Vector2 HitboxUpNr { get; set; }
        public Vector2 HitboxBreedNr { get; set; }

        public Vector2 SourceRectNr { get; set; }

        public Rectangle NextRectagle { get; set; }
        public Vector2 OldPosition { get; set; }

        //old, but needed for new
        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 versnelling;
        private Richting richting;
        private IInputReader _inputReader;

        private float scale = 1;// (float)1.5;
        private int scaleXAdd = 0;//24;
        private int scaleYAdd = 0;//23;
        

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

            //old 
            positie = new Vector2(240, 240);
            snelheid = new Vector2(5, 5);
            versnelling = new Vector2(0.1f, 0.1f);
            //new
            //movementManager = new MovementManager();

            SourceRectNr = new Vector2(128, 128);
            HitboxBreedNr = new Vector2(50, 45);
            HitboxDownNr = new Vector2(29, 42);
            HitboxUpNr = new Vector2(29, 50);



            Animation = new Animation();
            #region add frames
            //rij 0
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(128, 0, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(256, 0, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(384, 0, 128, 128)));
            //rij 1
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 128, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(128, 128, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(256, 128, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(384, 128, 128, 128)));
            //rij 2
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 256, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(128, 256, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(256, 256, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(384, 256, 128, 128)));
            //rij 3
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 384, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(128, 384, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(256, 384, 128, 128)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(384, 384, 128, 128)));
            #endregion
            
            

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
            //HitboxPosition = new Vector2((int)positie.X + (int)HitboxDownNr.X + scaleXAdd, (int)positie.Y + (int)HitboxDownNr.Y + scaleYAdd);
            _inputReader = inputReader;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(hitbox, HitboxPosition, animation.CurrentFrame.HitboxRectangle, Color.Green);
            spriteBatch.Draw(hitbox, HitboxPosition, HitboxRectangle, Color.Green, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0) ;
            //spriteBatch.Draw(Texture, positie, animation.CurrentFrame.SourceRectangle, Color.White);
            spriteBatch.Draw(Texture, positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            //Debug.WriteLine(positie);

        }

        public void Update(GameTime gameTime)
        {

            Move();
            Animation.Update(gameTime, richting);

            //MoveOld();   //moved het vanzelf

            updateRectangles();

            //HitboxPosition = new Vector2((int)positie.X + 40, (int)positie.Y + 40);


        }

        private void updateRectangles()
        {
            //check richting
            //small if idle, up, down

            if (richting == Richting.Idle)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxDownNr.X + 20 + scaleXAdd,
                    (int)positie.Y + (int)HitboxDownNr.Y + 3 + scaleYAdd);
                HitboxRectangle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);
                //NextRectagle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);

            }
            else if (richting == Richting.Down)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxDownNr.X + 20 + scaleXAdd,
                    (int)positie.Y + (int)HitboxDownNr.Y + 3 + scaleYAdd);
                HitboxRectangle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);
                //NextRectagle = new Rectangle((int)positie.X + (int)HitboxDownNr.X, (int)positie.Y + (int)HitboxDownNr.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);

            }
            else if (richting == Richting.Up)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxUpNr.X + 20 + scaleXAdd,
                    (int)positie.Y + (int)HitboxUpNr.Y - 10 + scaleYAdd);
                HitboxRectangle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxUpNr.X, (int)HitboxUpNr.Y);
                //NextRectagle = new Rectangle((int)positie.X + (int)HitboxUpNr.X, (int)positie.Y + (int)HitboxUpNr.Y, (int)HitboxUpNr.X, (int)HitboxUpNr.Y);


            }
            else if (richting == Richting.Left)
            {
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxBreedNr.X - 12 + scaleXAdd,// -5,
                    (int)positie.Y + (int)HitboxBreedNr.Y - 5 + scaleYAdd);
                HitboxRectangle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);
                //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);


            }
            else
            {
                //schuif positie nog een beetje op
                HitboxPosition = new Vector2(
                    (int)positie.X + (int)HitboxBreedNr.X - 8 + scaleXAdd,//-5,
                    (int)positie.Y + (int)HitboxBreedNr.Y - 5 + scaleYAdd);
                HitboxRectangle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);
                //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);

            }
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


            OldPosition = positie;
            positie += direction;
            richting = Richting.Idle;

            if (positie.X > 800 - 88 || positie.X < 0-40)
            {
                positie.X = OldPosition.X;
                //versnelling.X *= -1;
            }

            if (positie.Y > 480 - 88 || positie.Y < 0-40)
            {
                positie.Y = OldPosition.Y;
                //versnelling *= -1;
            }


            if (OldPosition.X > positie.X)
            {
                //naar links
                richting = Richting.Left;
            }

            if (OldPosition.X < positie.X)
            {
                //naar rechts
                richting = Richting.Right;
            }

            if (positie.Y < OldPosition.Y)
            {
                //naar boven
                richting = Richting.Up;
            }

            if (OldPosition.Y < positie.Y)
            {
                //naar onder
                richting = Richting.Down;
            }

            #endregion

        }

        public void UpdateToOldPostion(GameTime gameTime)
        {
            positie = OldPosition;
            Animation.Update(gameTime, richting);
            updateRectangles();
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
