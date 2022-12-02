using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Levels
{
    internal abstract class Level : IGameObject
    {
        public abstract Texture2D Texture { get; set; }

        //public int[,] Gameboard { get; set; }

        public abstract void CreateBlocks();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
