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
using TestGame.Animatie;
using TestGame.Blocks;
using TestGame.Collision;
using TestGame.Input;

namespace TestGame.Characters
{
    internal class Hero : IGameObject, IMovingObject
    {
        public Texture2D Texture { get; set; }
        public Animation Animation { get; set; }

        //current + next postions
        public Position CurrentPositie { get; set; }
        public Position NextPositie { get; set; }


        public Texture2D hitbox { get; set; }
        public Vector2 HitboxDownNr { get; set; }
        public Vector2 HitboxUpNr { get; set; }
        public Vector2 HitboxBreedNr { get; set; }
        public Vector2 SourceRectNr { get; set; }

        public int YBeweging { get; set; }
        public int Gravity { get; set; }
        public int AmountOfJumps { get; set; }
        public bool OnGround { get; set; }


        public int NrOfLivesLeft { get; set; }
        public List<Live> Lives { get; set; }
        public bool HasBeenHit { get; set; }
        public int NotTakingDamagaTime { get; set; }
        public bool Showing { get; set; }


        public Vector2 Snelheid { get; set; }

        private IInputReader _inputReader;

        private float scale = 1;
        private int scaleXAdd = 0;
        private int scaleYAdd = 0;
        

        public Hero(Texture2D texture, IInputReader inputReader, GraphicsDevice graphics, Vector2 beginPositie)
        {
            
            Texture = texture;
            hitbox = new Texture2D(graphics, 1, 1);
            hitbox.SetData(new[] { Color.White });

            YBeweging = -1;
            Gravity = 2;
            AmountOfJumps = 0;
            NrOfLivesLeft = 4;
            Lives = new List<Live>();

            for (int i = NrOfLivesLeft-1; i > 0; i--)
            {
                Lives.Add(new Live(Texture, new Vector2(1700 + (i * 30), 40)));
            }

            CurrentPositie = new Position();
            NextPositie = new Position();
            OnGround = true;    //if false, hero has jumped
            HasBeenHit = false;
            NotTakingDamagaTime = 0;
            Showing = true;

            CurrentPositie.Positie = beginPositie;
            CurrentPositie.Richting = Richting.Idle;
            Snelheid = new Vector2(5, 5);

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
            
            _inputReader = inputReader;

            NextPositie.Richting = Richting.Idle;
            NextPositie.Positie = CurrentPositie.Positie;
            UpdateHitbox();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Live live in Lives)
            {
                live.Draw(spriteBatch);
            }

            if (NotTakingDamagaTime % 2 == 0)
            {
                Showing = true;
            } else
            {
                Showing = false;
            }

            //spriteBatch.Draw(hitbox, CurrentPositie.HitboxPositie, CurrentPositie.HitboxRectangle, Color.Green, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0) ;

            if (HasBeenHit && NotTakingDamagaTime > 0 && Showing)
            {
                spriteBatch.Draw(Texture, CurrentPositie.Positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);

            }
            else if (HasBeenHit && NotTakingDamagaTime > 0 && !Showing) { 
                //nothing
            }
            else
            {
                spriteBatch.Draw(Texture, CurrentPositie.Positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);

            }
        }

        public void UpdateWithoutPositionRetrieve(GameTime gameTime)
        {
            UpdateHitbox();

            Animation.Update(gameTime, CurrentPositie.Richting, 4, 9);
        }

        public void Update(GameTime gameTime)
        {
            NextPositie = new Position();

            //get + save next position
            GetNextPosition();  
            //nextPosition is nu correct ingesteld

            UpdateHitbox();

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
                        (int)NextPositie.Positie.X + (int)HitboxDownNr.X + 20,
                        (int)NextPositie.Positie.Y + (int)HitboxDownNr.Y + 3);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxDownNr.X,
                        (int)HitboxDownNr.Y);
                }
                else if (NextPositie.Richting == Richting.Down)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxDownNr.X + 20, 
                        (int)NextPositie.Positie.Y + (int)HitboxDownNr.Y + 3); 

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxDownNr.X,
                        (int)HitboxDownNr.Y);
                }
                else if (NextPositie.Richting == Richting.Up)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxUpNr.X + 20, 
                        (int)NextPositie.Positie.Y + (int)HitboxUpNr.Y - 10); 

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxUpNr.X,
                        (int)HitboxUpNr.Y);
                }
                else if (NextPositie.Richting == Richting.Left)
                {
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxBreedNr.X - 13,
                        (int)NextPositie.Positie.Y + (int)HitboxBreedNr.Y - 2.5f);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxBreedNr.X,
                        (int)HitboxBreedNr.Y);
                }
                else
                {
                    //schuif positie nog een beetje op
                    NextPositie.HitboxPositie = new Vector2(
                        (int)NextPositie.Positie.X + (int)HitboxBreedNr.X - 8,
                        (int)NextPositie.Positie.Y + (int)HitboxBreedNr.Y - 2.5f);

                    NextPositie.HitboxRectangle = new Rectangle(
                        (int)NextPositie.HitboxPositie.X,
                        (int)NextPositie.HitboxPositie.Y,
                        (int)HitboxBreedNr.X,
                        (int)HitboxBreedNr.Y);
                }
            } 
            else
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
                if (OnGround)
                {
                    //naar boven
                    NextPositie.Richting = Richting.Up;
                } else
                {
                    NextPositie.Richting = Richting.Idle;
                    NextPositie.Positie = CurrentPositie.Positie;
                }
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
                YBeweging = -17;    //moet oneven zijn
            }
            else if (YBeweging < 0)
            {
                YBeweging += Gravity;
            }
            NextPositie.Positie = new Vector2(NextPositie.Positie.X, NextPositie.Positie.Y + YBeweging);
        }
    }
}
