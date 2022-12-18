using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SharpDX.Direct2D1;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TestGame.Blocks;
using TestGame.Collision;
using TestGame.Input;

namespace TestGame.Characters
{
    internal class Hero : IGameObject, IMovingObject  //, IMovable
    {
        //private Texture2D _texture;
        public Texture2D Texture { get; set; }
        private int schuifOp_X = 0;
        private int schuifOp_Y = 0;

        //private Animation animation;

        public Animation Animation { get; set; }


        //for position stuff
        //public Vector2 CurrentPosition { get; set; }
        //public Vector2 NextPosition { get; set; }
        //public Richting Direction { get; set; }
        //public Vector2 Snelheid { get; set; }
        //public Rectangle CurrentHitboxRectangle { get; set; }
        //public Rectangle NextHitboxRectangle { get; set; }
        //public Vector2 CurrentHitboxPosition { get; set; }
        //public Vector2 NextHitboxPosition { get; set; }
        //end position stuff

        //current + next postions
        public Position CurrentPositie { get; set; }
        public Position NextPositie { get; set; }




        private Texture2D hitbox;
        //public Rectangle HitboxRectangle { get; set; }   //deze steeds opnieuw maken met de geupdate hitboxPosition
        //public Vector2 HitboxPosition { get; set; }

        public Vector2 HitboxDownNr { get; set; }
        public Vector2 HitboxUpNr { get; set; }
        public Vector2 HitboxBreedNr { get; set; }

        public Vector2 SourceRectNr { get; set; }

        //public Rectangle NextRectagle { get; set; }
        //public Vector2 NextPosition { get; set; }

        public int YBeweging { get; set; }
        public int Gravity { get; set; }

        //old, but needed for new
        //public Vector2 Positie { get; set; }
        //private Vector2 snelheid;
        public Vector2 Snelheid { get; set; }
        private Vector2 versnelling;
        //private Richting richting;
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


        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphics, Vector2 beginPositie)
        {
            Texture = texture;
            hitbox = new Texture2D(graphics, 1, 1);
            hitbox.SetData(new[] { Color.White });

            YBeweging = -1;
            Gravity = 2;

            CurrentPositie = new Position();
            NextPositie = new Position();

            //old 
            //Positie = new Vector2(280, 280);
            CurrentPositie.Positie = beginPositie;
            CurrentPositie.Richting = Richting.Idle;
            Snelheid = new Vector2(5, 5);
            versnelling = new Vector2(0.1f, 0.1f);
            //new
            //movementManager = new MovementManager();

            SourceRectNr = new Vector2(128, 128);
            HitboxBreedNr = new Vector2(50, 45);
            HitboxDownNr = new Vector2(29, 42);
            HitboxUpNr = new Vector2(29, 50);



            Animation = new Animation(9);
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
            //HitboxPosition = new Vector2((int)Positie.X + (int)HitboxDownNr.X, (int)Positie.Y + (int)HitboxDownNr.Y);
            //HitboxPosition = new Vector2((int)positie.X + (int)HitboxDownNr.X + scaleXAdd, (int)positie.Y + (int)HitboxDownNr.Y + scaleYAdd);
            _inputReader = inputReader;

            /*CurrentPositie.HitboxPositie = new Vector2(
                    (int)CurrentPositie.Positie.X + (int)HitboxDownNr.X + scaleXAdd,
                    (int)CurrentPositie.Positie.Y + (int)HitboxDownNr.Y + scaleYAdd);

            CurrentPositie.HitboxRectangle = new Rectangle((int)CurrentPositie.HitboxPositie.X, (int)CurrentPositie.HitboxPositie.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);
            */

            NextPositie.Richting = Richting.Idle;
            NextPositie.Positie = CurrentPositie.Positie;
            UpdateHitbox();
            //NextPositie = CurrentPositie;
            //Animation.InitialUpdate(CurrentPositie.Richting);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hitbox, CurrentPositie.HitboxPositie, CurrentPositie.HitboxRectangle, Color.Green);
            
            
            //spriteBatch.Draw(hitbox, CurrentPositie.HitboxPositie, CurrentPositie.HitboxRectangle, Color.Green, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0) ;
            //spriteBatch.Draw(Texture, positie, animation.CurrentFrame.SourceRectangle, Color.White);
            
            spriteBatch.Draw(Texture, CurrentPositie.Positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
            //Debug.WriteLine(positie);

        }

        public void UpdateWithoutPositionRetrieve(GameTime gameTime)
        {
            UpdateHitbox();

            //Move();
            Animation.Update(gameTime, CurrentPositie.Richting, 4, 9);

            //MoveOld();   //moved het vanzelf


            //HitboxPosition = new Vector2((int)positie.X + 40, (int)positie.Y + 40);
        }

        public void Update(GameTime gameTime)
        {
            NextPositie = new Position();
            //get + save next position

            GetNextPosition();  //nextPosition is nu correct ingesteld


            //hitbox
            UpdateHitbox();

            //screen size
            /*if (NextPositie.Positie.X > 800|| NextPositie.Positie.X < 0)
            {
                //Positie.X = OldPosition.X;
                NextPositie.Positie = new Vector2(CurrentPositie.Positie.X, NextPositie.Positie.Y);
                //versnelling.X *= -1;
            }

            //screen size
            if (CurrentPositie.Positie.Y > 480 || CurrentPositie.Positie.Y < 0 )
            {
                //Positie.Y = OldPosition.Y;
                CurrentPositie.Positie = new Vector2(NextPositie.Positie.X, CurrentPositie.Positie.Y);
                //versnelling *= -1;
            }
            */
            Animation.Update(gameTime, NextPositie.Richting, 4, 9);


        }

        private void UpdateHitbox()
        {
            if (scale == 1)
            {
                //check richting
                //small if idle, up, down

                if (NextPositie.Richting == Richting.Idle)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxDownNr.X + 20, // + scaleXAdd,
                        (int)NextPositie.Positie.Y + (int)HitboxDownNr.Y + 3); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxDownNr.X,
                        (int)HitboxDownNr.Y);
                    //NextRectagle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);

                }
                else if (NextPositie.Richting == Richting.Down)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxDownNr.X + 20, // + scaleXAdd,
                        (int)NextPositie.Positie.Y + (int)HitboxDownNr.Y + 3); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxDownNr.X,
                        (int)HitboxDownNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxDownNr.X, (int)positie.Y + (int)HitboxDownNr.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);

                }
                else if (NextPositie.Richting == Richting.Up)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxUpNr.X + 20, // + scaleXAdd,
                        (int)NextPositie.Positie.Y + (int)HitboxUpNr.Y - 10); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxUpNr.X,
                        (int)HitboxUpNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxUpNr.X, (int)positie.Y + (int)HitboxUpNr.Y, (int)HitboxUpNr.X, (int)HitboxUpNr.Y);


                }
                else if (NextPositie.Richting == Richting.Left)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxBreedNr.X - 13, // + scaleXAdd,// -5,
                        (int)NextPositie.Positie.Y + (int)HitboxBreedNr.Y - 2.5f); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxBreedNr.X,
                        (int)HitboxBreedNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);


                }
                else
                {
                    //schuif positie nog een beetje op
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxBreedNr.X - 8, // + scaleXAdd,//-5,
                        (int)NextPositie.Positie.Y + (int)HitboxBreedNr.Y - 2.5f); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxBreedNr.X,
                        (int)HitboxBreedNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);

                }
            } else
            {
                //scale == 1.5
                //check richting
                //small if idle, up, down

                if (NextPositie.Richting == Richting.Idle)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + ((int)HitboxDownNr.X + 20), // + scaleXAdd,
                        (int)NextPositie.Positie.Y + ((int)HitboxDownNr.Y + 3)); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxDownNr.X * 2,
                        (int)HitboxDownNr.Y * 2);
                    //NextRectagle = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);

                }
                else if (NextPositie.Richting == Richting.Down)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxDownNr.X*scale + 20, // + scaleXAdd,
                        (int)NextPositie.Positie.Y + (int)HitboxDownNr.Y*scale + 3); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxDownNr.X,
                        (int)HitboxDownNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxDownNr.X, (int)positie.Y + (int)HitboxDownNr.Y, (int)HitboxDownNr.X, (int)HitboxDownNr.Y);

                }
                else if (NextPositie.Richting == Richting.Up)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxUpNr.X * scale + 20, // + scaleXAdd,
                        (int)NextPositie.Positie.Y + (int)HitboxUpNr.Y * scale - 10); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxUpNr.X,
                        (int)HitboxUpNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxUpNr.X, (int)positie.Y + (int)HitboxUpNr.Y, (int)HitboxUpNr.X, (int)HitboxUpNr.Y);


                }
                else if (NextPositie.Richting == Richting.Left)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + ((int)HitboxBreedNr.X- 13) * scale, // + scaleXAdd,// -5,
                        (int)NextPositie.Positie.Y + ((int)HitboxBreedNr.Y  - 2.5f) * scale); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxBreedNr.X,
                        (int)HitboxBreedNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);


                }
                else
                {
                    //schuif positie nog een beetje op
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + ((int)HitboxBreedNr.X  - 8) * scale, // + scaleXAdd,//-5,
                        (int)NextPositie.Positie.Y + ((int)HitboxBreedNr.Y - 2.5f) * scale); // + scaleYAdd);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxBreedNr.X,
                        (int)HitboxBreedNr.Y);
                    //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);

                }
            }
            
        }

        public void GetNextPosition()
        {
            #region positie
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

            NextPositie.Positie = CurrentPositie.Positie + direction;
            NextPositie.Richting = Richting.Idle;


            //check if on screen
            //screen size
            if (NextPositie.Positie.X > 800 - 88 || NextPositie.Positie.X < 0 - 40)
            {
                //Positie.X = OldPosition.X;
                NextPositie.Positie = new Vector2(CurrentPositie.Positie.X, NextPositie.Positie.Y);
                //versnelling.X *= -1;
            }

            //screen size
            if (NextPositie.Positie.Y > 480 - 88 || NextPositie.Positie.Y < 0 - 40)
            {
                //Positie.Y = OldPosition.Y;
                NextPositie.Positie = new Vector2(NextPositie.Positie.X, CurrentPositie.Positie.Y);
                //versnelling *= -1;
            }

            //richting
            if (CurrentPositie.Positie.X > NextPositie.Positie.X)
            {
                //naar links
                NextPositie.Richting = Richting.Left;
            }

            if (CurrentPositie.Positie.X < NextPositie.Positie.X)
            {
                //naar rechts
                NextPositie.Richting = Richting.Right;
               

            }

            if (NextPositie.Positie.Y < CurrentPositie.Positie.Y)
            {
                //naar boven
                NextPositie.Richting = Richting.Up;
            }

            if (CurrentPositie.Positie.Y < NextPositie.Positie.Y)
            {
                //naar onder
                NextPositie.Richting = Richting.Down;
            }



            

            //if richting.up -> spring en daarna vallen

            if (NextPositie.Richting == Richting.Up)
            {
                //spring
                YBeweging = -5;
            }
            else if (YBeweging < 0)
            {
                YBeweging += Gravity;
            }
            //Positie.Y += YBeweging;
            NextPositie.Positie = new Vector2(NextPositie.Positie.X, NextPositie.Positie.Y + YBeweging);
            
            #endregion


        }

        /*public void Move()
        {
            //movementManager.Move(this);
            #region move
            
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
            if (CurrentPositie.Positie.X > 800 - 88 || CurrentPositie.Positie.X < 0-40)
            {
                //Positie.X = OldPosition.X;
                CurrentPositie.Positie = new Vector2(oldPosition.X, CurrentPositie.Positie.Y);
                //versnelling.X *= -1;
            }

            //screen size
            if (CurrentPositie.Positie.Y > 480 - 88 || CurrentPositie.Positie.Y < 0-40)
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
            } else if (YBeweging < 0)
            {
                YBeweging += Gravity;
            }
            //Positie.Y += YBeweging;
            CurrentPositie.Positie = new Vector2(CurrentPositie.Positie.X, CurrentPositie.Positie.Y + YBeweging);


            #endregion

        }*/

        /*public void UpdateToOldPostion(GameTime gameTime)
        {
            Positie = OldPosition;
            Animation.Update(gameTime, richting);
            updateRectangles();
        }*/

        /*public void UpdateToAllowedPosition(Vector2 allowedPosition, GameTime gameTime)
        {
            Positie = allowedPosition;
            Animation.Update(gameTime, richting);
            updateRectangles();
        }*/

        /*public void MoveOld()
        {
            
            Positie += snelheid;
            //snelheid += versnelling;
            //extra: limit
            //float maximaleSnelheid = 10;
            //snelheid = Limit(snelheid, maximaleSnelheid);
            //
            if (Positie.X > 800-128 || Positie.X < 0)
            {
                snelheid.X *= -1;
                //versnelling.X *= -1;
            }
            
            if (Positie.Y > 480-128 || Positie.Y < 0)
            {
                snelheid.Y *= -1;
                //versnelling *= -1;
            }
            //Debug.WriteLine(positie);


        }*/

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

        /*private void MoveWithMouse()
        {
            MouseState state = Mouse.GetState();
            Vector2 mouseVector = new Vector2(state.X, state.Y);

            var richting = mouseVector - Positie;
            richting.Normalize();

            //zonder extra
            *//*
            var afTeLeggenAfstand = richting * snelheid;
            positie += afTeLeggenAfstand;
            *//*

            //met extra
            richting = Vector2.Multiply(richting, 0.1f);
            snelheid += richting;
            snelheid = Limit(snelheid, 10);
            Positie += snelheid;


        }*/


    }
}
