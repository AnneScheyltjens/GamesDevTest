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
    internal class Prikkeldraad : IGameObject
    {
        public Texture2D Texture { get; set; }

        public Position Positie { get; set; }

        public Texture2D HitboxTexture { get; set; }

        public float Scale { get; set; }

        public Rectangle Rectangle { get; set; }




        public Prikkeldraad(Texture2D texture, Vector2 position, Rectangle rectangle, float scale, GraphicsDevice graphics)
        {
            Texture = texture;
            Rectangle = rectangle;
            Positie = new Position(position, new Rectangle((int)position.X, (int)position.Y, 27, 48));
            Scale = scale;
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.Red });



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(HitboxTexture, Positie.Positie, Positie.HitboxRectangle, Color.Orange, 0, new Vector2(0,0), Scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Texture, Positie.Positie, Rectangle, Color.White, 0, new Vector2(0,0), Scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            //not needed
            
        }
    }
}
