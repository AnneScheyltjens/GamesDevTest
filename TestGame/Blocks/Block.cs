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

        public Vector2 HitboxPosition { get; set; }

        public Texture2D HitboxTexture { get; set; }

        public bool IsGrass { get; set; }

        #endregion

        #region Methodes

        public Block(Texture2D texture, Vector2 position, Rectangle rectangle, float scale, GraphicsDevice graphics, bool isGrass = false)
        {
            Texture = texture;
            Position = position;
            Rectangle = rectangle;
            Scale = scale;
            IsGrass = isGrass;

            if (IsGrass)
            {
                HitboxPosition = new Vector2((int)Position.X + 30, (int)Position.Y+30);
                Hitbox = new Rectangle((int)HitboxPosition.X, (int)HitboxPosition.Y, Rectangle.Width-60, Rectangle.Height-30);

            }
            else
            {
                HitboxPosition = Position;
                Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);

            }

            //Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height);
            HitboxTexture = new Texture2D(graphics, 1, 1);
            HitboxTexture.SetData(new[] { Color.Red });


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            /*if (IsGrass)
            {
                spriteBatch.Draw(HitboxTexture, Position, Hitbox, Color.Red, 0, new Vector2(0,0), Scale, SpriteEffects.None, 0);
                spriteBatch.Draw(Texture, Position, Rectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            }
            else
            {*/

                //spriteBatch.Draw(HitboxTexture, HitboxPosition, Hitbox, Color.Red);
                spriteBatch.Draw(Texture, Position, Rectangle, Color.White);
                //spriteBatch.Draw(Texture, Position, Rectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            //}
        }

        public void Update(GameTime gameTime)
        {
            //not needed
        }

        #endregion



    }
}
