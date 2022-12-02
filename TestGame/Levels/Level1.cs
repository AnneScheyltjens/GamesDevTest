using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Blocks;

namespace TestGame.Levels
{
    internal class Level1
    {
        int[,] gameboard = new int[,]
        {
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {1,1,1,1,1,1,1,1 }
        };

        private int nrRows = 8;
        private int nrColumns = 8;

        public Dictionary<int, Texture2D> blockTextures { get; set; }

        public List<BlockZelf> Blocks { get; set; }

        private int windowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private int windowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

        public Level1()
        {
            Blocks = new List<BlockZelf>();
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < gameboard.GetLength(0); l++)
            {
                for (int c = 0; c < gameboard.GetLength(1); c++)
                {
                    if (gameboard[l, c] != 0)
                    {
                        Vector2 position = new Vector2((windowWidth / nrColumns)*l, (windowHeight/ nrRows)*c);
                        Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, (windowWidth / nrColumns), (windowHeight / nrRows));
                        Blocks.Add(new BlockZelf(position, rectangle, blockTextures.GetValueOrDefault(gameboard[l, c])));
                    }
                }
            }
        }
    }
}
