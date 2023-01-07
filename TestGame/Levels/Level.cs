using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestGame.Blocks;
using TestGame.Characters;
using TestGame.Enemies;
using TestGame.Input;
using TestGame.States;

namespace TestGame.Levels
{
    internal class Level : IGameObject
    {
        /*int[,] gameboardSmall = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1},
            {3,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,2},
            {3,11,0,6,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,1,0,12,0,10,12,2},
            {1,1,1,1,1,1,1,1,1,1,1,1}
        };*/

        /*int[,] gameboard = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {3,0,0,0,00,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,1,0,1,0,1,1,1,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,1,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,2},
            {3,11,0,6,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,2},
            {3,0,0,0,0,0,0,12,0,0,0,0,0,0,0,1,1,0,0,0,12,0,0,0,0,0,0,10,12,2},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };*/
        //private List<Block> blocks = new List<Block>();


        public int[,] gameboard { get; set; }
        public List<Block> Blocks { get; set; }
        public List<Prikkeldraad> Prikkeldraden { get; set; }

        public List<Wolf> Wolven { get; set; }
        public List<Farmer> Farmers { get; set; }

        public Hero Hero { get; set; }
        public Texture2D PrikkeldraadTexture { get; set; }

        public float Scale { get; set; }
        public Texture2D Texture { get; set; }

        private int blockWidth = 64;
        private int blockHeight = 64;

        private int screenWidthPerBlock = 5; //GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        private int screenHeightPerBlock = 5; // GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        private Dictionary<int, Rectangle> blockTextureRectangles = new Dictionary<int, Rectangle>();

        public GraphicsDevice Graphics { get; set; }

        public Level(float scale, GraphicsDevice graphics, IInputReader inputReader, ContentManager content, LevelSelectie level)
        {

            GameboardSelectie gameboards = new GameboardSelectie();
            gameboard = gameboards.GameBoards.GetValueOrDefault(level);


            Blocks = new List<Block>();
            Texture = content.Load<Texture2D>("tileset");
            Scale = scale;
            Graphics = graphics;
            PrikkeldraadTexture = content.Load<Texture2D>("PrikkeldraadEchtScaled2");
            Prikkeldraden = new List<Prikkeldraad>();
            Wolven = new List<Wolf>();
            Farmers = new List<Farmer>();
            //map de blok textures op een getal
            //beginent vanaf 0

            Rectangle normalBlock = new Rectangle(64 * 1, 0, 64, 64);
            blockTextureRectangles.Add(1, normalBlock); 

            Rectangle wallRightBlock = new Rectangle(64 * 0, 64 * 1, 64, 64);
            blockTextureRectangles.Add(2, wallRightBlock);

            Rectangle wallLeftBlock = new Rectangle(64 * 2, 64 * 1, 64, 64);
            blockTextureRectangles.Add(3, wallLeftBlock);
            //maak de nodige blokjes aan
            ReadMap(content, inputReader);
        }

        public void ReadMap(ContentManager content, IInputReader inputReader)
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
                        if (gameboard[r, c] == 6)
                        {
                            Vector2 beginPositie = new Vector2(c * 64, r * 64);
                            Hero = new Hero(content.Load<Texture2D>("sheep"), inputReader, Graphics, beginPositie);
                        }
                        else if (gameboard[r,c] == 12) {
                            //12 is prikkeldraad
                            Vector2 positie = new Vector2((c * 64)+20 , (r * 64)+20);
                            Rectangle rectangle = new Rectangle(0, 0, 27, 48);
                            Prikkeldraad prik = new Prikkeldraad(PrikkeldraadTexture, positie, rectangle, Scale, Graphics);
                            Prikkeldraden.Add(prik);
                        } else if (gameboard[r,c] == 10)
                        {
                            //10 is wolf
                            Vector2 positie = new Vector2(c * 64, r * 64);
                            Wolf wolf = new Wolf(content.Load<Texture2D>("wolvenEcht"), Graphics, positie);
                            Wolven.Add(wolf);
                        }
                        else if (gameboard[r,c] == 11)
                        {
                            //11 is farmer
                            Vector2 positie = new Vector2(c * 64, (r * 64)-4);
                            Farmer farmer = new Farmer(content.Load<Texture2D>("farmer6"), Graphics, positie, Richting.Right, content.Load<Texture2D>("bullet"));
                            Farmers.Add(farmer);
                        } else if (gameboard[r,c] == 111)
                        {
                            //111 is farmer die naar links schiet
                            Vector2 positie = new Vector2(c * 64, (r * 64) - 4);
                            Farmer farmer = new Farmer(content.Load<Texture2D>("farmer6"), Graphics, positie, Richting.Left, content.Load<Texture2D>("bullet"));
                            Farmers.Add(farmer);
                        }
                        else if (gameboard[r,c] == 9)
                        {
                            //9 is grass
                            Vector2 positie = new Vector2(c * 64, r * 64);
                            Rectangle rectangle = new Rectangle(0, 0, 137, 77);
                            //Block grass = new Block(content.Load<Texture2D>("grass5"), positie, rectangle, 0.1f, Graphics, true);
                            Block grass = new Block(content.Load<Texture2D>("grass3"), positie, rectangle, 1, Graphics, true);
                            Blocks.Add(grass);
                        } 
                        else
                        {
                            //er moet een blokje staan

                            Texture2D textureBlock = Texture;
                            Vector2 position = new Vector2(c * 64, r * 64);
                            Rectangle rectangle = blockTextureRectangles.GetValueOrDefault(gameboard[r, c]);
                            Blocks.Add(new Block(textureBlock, position, rectangle, Scale, Graphics));

                        }

                    }
                    
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            foreach (Block block in Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach(Prikkeldraad prik in Prikkeldraden)
            {
                prik.Draw(spriteBatch);
            }
            foreach (Wolf wolf in Wolven)
            {
                wolf.Draw(spriteBatch);
            }
            foreach (Farmer farmer in Farmers)
            {
                farmer.Draw(spriteBatch);
            }

            Hero.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
