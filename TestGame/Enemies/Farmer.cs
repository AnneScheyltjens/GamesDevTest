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
    internal class Farmer : IGameObject
    {
        public Texture2D Texture { get; set; }
        public Texture2D HitboxTexture { get; set; }

        public Animation Animation { get; set; }

        public Position Positie { get; set; }

        public float Scale { get; set; }

        public int Timinig { get; set; }

        public List<Bullet> Bullets { get; set; }

        public Vector2 HitboxBreedNr { get; set; }
        public Texture2D BulletTexture { get; set; }
        public GraphicsDevice Graphics { get; set; }

        public Farmer(Texture2D texture, GraphicsDevice graphics, Vector2 beginPositie, Richting richting, Texture2D bulletTexture)
        {
            BulletTexture = bulletTexture;
            Graphics = graphics;
            Texture = texture;
            Scale = 1.0f;
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.White });

            HitboxBreedNr = new Vector2(90, 130);
            Bullets = new List<Bullet>();
            Timinig = 190;

            Positie = new Position();
            Positie.Positie = new Vector2(beginPositie.X, beginPositie.Y);
            Positie.Richting = richting;
            Positie.HitboxPositie = new Vector2(Positie.Positie.X + 24, Positie.Positie.Y+2);
            Positie.HitboxRectangle = new Rectangle((int)Positie.HitboxPositie.X, (int)Positie.HitboxPositie.Y,
                (int)HitboxBreedNr.X, (int)HitboxBreedNr.Y);

            Animation = new Animation(0);

            Animation.AddFrame(new AnimationFrame(new Rectangle(0, 0, 142, 142)));


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(HitboxTexture, Positie.HitboxPositie, Positie.HitboxRectangle, Color.OrangeRed, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            //spriteBatch.Draw(Texture, CurrentPosition.Positie, Animation.CurrentFrame.SourceRectangle, Color.White);
            spriteBatch.Draw(Texture, Positie.Positie, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);

            foreach (Bullet bullet in Bullets)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            Timinig += 1;

            //enkel de bullets
            List<Bullet> bulletsToRemove = new List<Bullet>();
            foreach (Bullet bullet in Bullets)
            {
                bullet.Update(gameTime);
                if (bullet.CurrentPositie.Positie.X > 2000 || bullet.CurrentPositie.Positie.X < 0)
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            if (Timinig >= 200)
            {
                Vector2 newPositie = new Vector2(Positie.Positie.X + HitboxBreedNr.X + 24,
                    Positie.Positie.Y + (HitboxBreedNr.Y/2));
                Bullets.Add(new Bullet(BulletTexture, Graphics, newPositie));
                Timinig = 0;
            }

            foreach (Bullet bul in bulletsToRemove)
            {
                Bullets.Remove(bul);
            }

        }
    }
}
