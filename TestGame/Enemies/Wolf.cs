using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGame.Input;

namespace TestGame.Enemies
{
    internal class Wolf : IGameObject
    {
        public Texture2D Texture { get; set; }
        public Texture2D HitboxTexture { get; set; }

        public Animation Animation { get; set; }

        public Position CurrentPosition { get; set; }
        public Position NextPositie { get; set; }

        public Vector2 Snelheid { get; set; }

        public float Scale { get; set; }

        public int GoesRight { get; set; }

        public Vector2 HitboxBreedNr { get; set; }

        public Wolf(Texture2D texture, GraphicsDevice graphics, Vector2 beginPositie) 
        {
            Texture = texture;
            HitboxBreedNr = new Vector2( 56, 40);
            Scale = 1.3f;
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            CurrentPosition = new Position();
            NextPositie = new Position();

            CurrentPosition.Positie = new Vector2(beginPositie.X, beginPositie.Y + 3);
            CurrentPosition.Richting = Richting.Left;
            GoesRight = -1;
            Snelheid = new Vector2(2, 2);

            NextPositie.Positie = CurrentPosition.Positie;
            NextPositie.Richting = Richting.Left;
            UpdateHitbox();
            CurrentPosition = NextPositie;

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
            spriteBatch.Draw(HitboxTexture, CurrentPosition.HitboxPositie, CurrentPosition.HitboxRectangle, Color.OrangeRed);
            //spriteBatch.Draw(Texture, CurrentPosition.Positie, Animation.CurrentFrame.SourceRectangle, Color.White);
            spriteBatch.Draw(Texture, CurrentPosition.Positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);

        }

        public void Update(GameTime gameTime)
        {
            NextPositie.Positie = new Vector2(CurrentPosition.Positie.X + GoesRight, CurrentPosition.Positie.Y);

            //Animation.Update(gameTime, Richting.Left, 3, 0);

            Animation.Update(gameTime, CurrentPosition.Richting, 3, 0);


            UpdateHitbox();
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
    }
}
