using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Characters
{
    internal class Live : IGameObject
    {
        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }
        public Rectangle PositionRect { get; set; }
        public Animation Animation { get; set; }

        public bool IsActive { get; set; }
        public float Scale { get; set; }

        public Live(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            PositionRect = new Rectangle(0, 128, 128, 128);
            IsActive = true;

            Animation = new Animation(0);
            Animation.AddFrame(new AnimationFrame(PositionRect));
            Scale = 0.6f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive)
            {
                spriteBatch.Draw(Texture, Position, Animation.CurrentFrame.SourceRectangle, Color.Transparent, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            } else
            {
                spriteBatch.Draw(Texture, Position, Animation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), Scale, SpriteEffects.None, 0);

            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                IsActive = false;
            }
            else
            {
                IsActive = true;    // wordt momenteel nog niet gebruikt
            }
        }
    }
}
