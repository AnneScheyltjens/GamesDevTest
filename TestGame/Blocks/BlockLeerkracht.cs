using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Blocks
{
    internal class BlockLeerkracht : IGameObject
    {
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        //public CollideWithEvent CollideWithEvent { get; set; }


        public BlockLeerkracht(int x, int y, GraphicsDevice graphics)
        {
            BoundingBox = new Rectangle(x, y, 10, 10);
            Passable = false;
            Color = Color.White;
            Texture = new Texture2D(graphics, 1, 1);
            //CollideWithEvent = new NoEvent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color);
        }

        public void Update(GameTime gameTime)
        {
            //beweegt niet
            //moet niet geupdate worden
        }

        /*public virtual void IsColliderWithEvent(Character collider)
        {
            IsColliderWithEvent().Execute();
        }*/
    }
}
