using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGame.Input;

namespace TestGame.Blocks
{
    internal class BlockZelf : IGameObject
    {
        #region Properties
        public Rectangle Rectangle { get; set; }
        public Vector2 Positie { get; set; }
        public Texture2D Texture { get; set; }

        #endregion


        public BlockZelf(Vector2 positie, Rectangle rectangle, Texture2D texture)
        {
            Positie = positie;
            Rectangle = rectangle;
            Texture = texture;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            //not needing an update, is statisch 
        }

    }
}
