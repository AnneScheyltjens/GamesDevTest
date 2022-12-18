using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGame.Collision;
using TestGame.Input;

namespace TestGame.Enemies
{
    internal class Wolf : IGameObject, IMovingObject
    {
        public Texture2D Texture { get; set; }
        public Texture2D HitboxTexture { get; set; }

        public Animation Animation { get; set; }

        public Position CurrentPositie { get; set; }
        public Position NextPositie { get; set; }

        public Vector2 Snelheid { get; set; }

        public float Scale { get; set; }

        public int GoesRight { get; set; }

        public Vector2 HitboxBreedNr { get; set; }

        public Wolf(Texture2D texture, GraphicsDevice graphics, Vector2 beginPositie) 
        {
            Texture = texture;
            HitboxBreedNr = new Vector2( 58, 40);
            Scale = 1.3f;
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            CurrentPositie = new Position();
            NextPositie = new Position();

            CurrentPositie.Positie = new Vector2(beginPositie.X, beginPositie.Y + 3);
            CurrentPositie.Richting = Richting.Left;
            GoesRight = -1;
            Snelheid = new Vector2(2, 2);

            NextPositie.Positie = CurrentPositie.Positie;
            NextPositie.Richting = Richting.Left;
            UpdateHitbox();
            //CurrentPositie = NextPositie;
            //NextPositie = CurrentPositie;

            Animation = new Animation(0);

            Animation.AddFrame(new AnimationFrame(new Rectangle(144, 0, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(192, 0, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(240, 0, 48, 48)));

            Animation.AddFrame(new AnimationFrame(new Rectangle(144, 48, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(192, 48, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(240, 48, 48, 48)));

            Animation.AddFrame(new AnimationFrame(new Rectangle(144, 144, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(192, 144, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(240, 144, 48, 48)));

            Animation.AddFrame(new AnimationFrame(new Rectangle(144, 96, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(192, 96, 48, 48)));
            Animation.AddFrame(new AnimationFrame(new Rectangle(240, 96, 48, 48)));

            

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(HitboxTexture, CurrentPositie.HitboxPositie, CurrentPositie.HitboxRectangle, Color.OrangeRed);
            //spriteBatch.Draw(Texture, CurrentPosition.Positie, Animation.CurrentFrame.SourceRectangle, Color.White);
            spriteBatch.Draw(Texture, CurrentPositie.Positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);

        }

        public void Update(GameTime gameTime)
        {
            NextPositie = new Position();

            NextPositie.Positie = new Vector2(CurrentPositie.Positie.X + GoesRight, CurrentPositie.Positie.Y);
            if (GoesRight == -1)
            {
                NextPositie.Richting = Richting.Left;
            } else
            {
                NextPositie.Richting = Richting.Right;
            }
            //Animation.Update(gameTime, Richting.Left, 3, 0);
            UpdateHitbox();

            //screen size
            /*if (NextPositie.Positie.X > 800 - NextPositie.HitboxRectangle.Height || NextPositie.Positie.X < 0 - NextPositie.HitboxRectangle.Height)
            {
                //Positie.X = OldPosition.X;
                NextPositie.Positie = new Vector2(CurrentPositie.Positie.X, NextPositie.Positie.Y);
                //versnelling.X *= -1;
            }*/

            Animation.Update(gameTime, CurrentPositie.Richting, 3, 0);


        }

        private void UpdateHitbox()
        {            
            if (NextPositie.Richting == Richting.Left)
            {
                NextPositie.HitboxPositie = new Vector2(
                    (int)NextPositie.Positie.X, // + scaleXAdd,// -5,
                    (int)NextPositie.Positie.Y + 22); // + scaleYAdd);

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
                    (int)NextPositie.Positie.X , // + scaleXAdd,//-5,
                    (int)NextPositie.Positie.Y + 22); // + scaleYAdd);

                NextPositie.HitboxRectangle = new Rectangle(
                    (int)NextPositie.HitboxPositie.X,
                    (int)NextPositie.HitboxPositie.Y,
                    (int)HitboxBreedNr.X+2,
                    (int)HitboxBreedNr.Y);
                //NextRectagle = new Rectangle((int)positie.X + (int)HitboxBreedNr.X, (int)positie.Y + (int)HitboxBreedNr.Y, (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);
            }
        }
        public void UpdateWithoutPositionRetrieve(GameTime gameTime)
        {
            UpdateHitbox();

            //Move();
            Animation.Update(gameTime, CurrentPositie.Richting, 3, 0);

            //MoveOld();   //moved het vanzelf


            //HitboxPosition = new Vector2((int)positie.X + 40, (int)positie.Y + 40);
        }
    }
}
