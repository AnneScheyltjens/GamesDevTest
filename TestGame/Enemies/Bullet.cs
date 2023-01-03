using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Collision;

namespace TestGame.Enemies
{
    internal class Bullet : IMovingObject, IGameObject
    {
        public Position CurrentPositie { get; set; }
        public Position NextPositie { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D HitboxTexture { get; set; }
        public Animation Animation { get; set; }
        public float Scale { get; set; }
        public Vector2 HitboxBreedNr { get; set; }
        //public Vector2 Snelheid { get; set; }


        public Bullet(Texture2D texture, GraphicsDevice graphics, Vector2 beginPositie)
        {
            Texture = texture;
            Scale = 0.3f;
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            HitboxBreedNr = new Vector2(64, 28);

            CurrentPositie = new Position();
            NextPositie = new Position();

            CurrentPositie.Positie = new Vector2(beginPositie.X, beginPositie.Y);
            CurrentPositie.Richting = Richting.Right;
            NextPositie.Positie = CurrentPositie.Positie;
            NextPositie.Richting = Richting.Right;
            UpdateHitbox();
          
            //Snelheid = new Vector2(2, 2);


            Animation = new Animation(0);
            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 64, 64)));

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

            NextPositie.Positie = new Vector2(CurrentPositie.Positie.X + 1.0f, CurrentPositie.Positie.Y);

            UpdateHitbox();
            CurrentPositie = NextPositie;
            
        }

        private void UpdateHitbox()
        {
            
            NextPositie.HitboxPositie = new Vector2(
                (int)NextPositie.Positie.X,
                (int)NextPositie.Positie.Y+6);

            NextPositie.HitboxRectangle = new Rectangle(
                (int)NextPositie.HitboxPositie.X,
                (int)NextPositie.HitboxPositie.Y,
                (int)HitboxBreedNr.X - 45,
                (int)HitboxBreedNr.Y - 20);           
        }
    }
}
