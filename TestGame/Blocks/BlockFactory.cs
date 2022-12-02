using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Blocks
{
    internal class BlockFactory
    {
        public static BlockLeerkracht CreateBlock(string type, int x, int y, GraphicsDevice graphics)
        {
            BlockLeerkracht newBlock = null;
            type = type.ToUpper();
            switch (type)
            {
                case "NORMAL":
                    newBlock = new BlockLeerkracht(x, y, graphics);
                    break;
                default:
                    break;
            }

            return newBlock;
        }
    }
}
