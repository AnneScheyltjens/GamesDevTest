using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestGame.Blocks;
using TestGame.Characters;
using TestGame.Input;

namespace TestGame.Levels
{
    internal class LevelTest : IGameObject
    {
        int[,] gameboard = new int[,]
        {
            {1,1,1,1,1,1,1,1 /*,1,1,1,1,1,1,1,1 ,1,1,1,1,1,1*/ /*,1,1,1,1*/,1,1,1,1},
            {3,0,0,0,0,0,0,0 /*,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0*/ /*,0,0,0,0*/,0,0,0,2},
            {3,0,0,0,0,0,0,0 /*,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0*/ /*,0,0,0,0*/,0,0,0,2},
            {3,0,0,0,0,0,0,0 /*,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0*/ /*,0,0,0,0*/,0,0,0,2},
            /*{1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0 ,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0 ,0,0,0,0,0,0,0,1},*/
            {3,0,0,6,0,0,0,0 /*,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0*/ /*,0,0,0,0*/,0,0,0,2},
            {3,0,0,0,0,0,0,0 /*,0,0,0,0,0,0,0,0 ,0,0,0,0,0,0*/ /*,0,0,0,0*/,0,0,0,2},
            {1,1,1,1,1,1,1,1 /*,1,1,1,1,1,1,1,1 ,1,1,1,1,1,1*/ /*,1,1,1,1*/,1,1,1,1}
        };

        //private List<Block> blocks = new List<Block>();

        public List<Block> Blocks { get; set; }

        public Hero Hero { get; set; }

        public float Scale { get; set; }
        public Texture2D Texture { get; set; }

        private int blockWidth = 64;
        private int blockHeight = 64;

        private int screenWidthPerBlock = 5; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeightPerBlock = 5; // GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private Dictionary<int, Rectangle> blockTextureRectangles = new Dictionary<int, Rectangle>();

        public GraphicsDevice Graphics { get; set; }

        public LevelTest(Texture2D texture, float scale, GraphicsDevice graphics, Texture2D heroTexture, IInputReader inputReader)
        {
            Blocks = new List<Block>();
            Texture = texture;
            Scale = scale;
            Graphics = graphics;

            //map de blok textures op een getal
            //beginent vanaf 0

            Rectangle normalBlock = new Rectangle(64 * 1, 0, 64, 64);
            blockTextureRectangles.Add(1, normalBlock); 

            Rectangle wallRightBlock = new Rectangle(64 * 0, 64 * 1, 64, 64);
            blockTextureRectangles.Add(2, wallRightBlock);

            Rectangle wallLeftBlock = new Rectangle(64 * 2, 64 * 1, 64, 64);
            blockTextureRectangles.Add(3, wallLeftBlock);
            //maak de nodige blokjes aan
            ReadMap(heroTexture, inputReader);
        }

        public void ReadMap(Texture2D heroTexture, IInputReader inputReader)
        {
            for (int r = 0; r < gameboard.GetLength(0); r++)
            {
                for (int c = 0; c < gameboard.GetLength(1); c++)
                {
                    //voor elke vakje op het bord
                    //kijk of er een blokje moet staan
                    if (gameboard[r,c] != 0)
                    {
                        //6 is hero plaats
                        if (gameboard[r,c] == 6)
                        {
                            Vector2 beginPositie = new Vector2(c * 64, r * 64);
                            Hero = new Hero(heroTexture, inputReader, Graphics, beginPositie);
                        }

                        //er moet een blokje staan

                        Texture2D textureBlock = Texture;
                        Vector2 position = new Vector2(c * 64 , r * 64 );
                        Rectangle rectangle = blockTextureRectangles.GetValueOrDefault(gameboard[r, c]);
                        Blocks.Add(new Block(textureBlock, position, rectangle, Scale, Graphics));
                    }
                    
                }
            }

            /*Rectangle rectangleN = new Rectangle(360, 260, 64, 64);
            Texture2D textureBlock2 = Texture;

            Block blokje = new Block(textureBlock2, new Vector2(360, 260), rectangleN, Scale, Graphics);
            Blocks.Add(blokje);*/
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Block block in Blocks)
            {
                block.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            //not needed
        }
    }
}
