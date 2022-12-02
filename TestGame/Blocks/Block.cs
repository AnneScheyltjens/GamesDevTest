using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestGame.Blocks
{
    internal class Block : IGameObject
    {
        #region Properties
        public Texture2D Texture { get; set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 Position { get; set; }
        public float Scale { get; set; }

        public Rectangle Hitbox { get; set; }

        public Texture2D HitboxTexture { get; set; }

        #endregion

        #region Methodes

        public Block(Texture2D texture, Vector2 position, Rectangle rectangle, float scale, GraphicsDevice graphics)
        {
            Texture = texture;
            Position = position;
            Rectangle = rectangle;
            Scale = scale;
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.Red });

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(HitboxTexture, Position, Hitbox,  Color.Red);
            spriteBatch.Draw(Texture, Position, Rectangle, Color.White);
            //spriteBatch.Draw(Texture, Position, Rectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            //not needed
        }

        #endregion



    }
}
