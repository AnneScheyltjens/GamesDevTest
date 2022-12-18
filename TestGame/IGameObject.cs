using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame
{
    public interface IGameObject
    {
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
        public Texture2D Texture { get; set; }
    }
}
