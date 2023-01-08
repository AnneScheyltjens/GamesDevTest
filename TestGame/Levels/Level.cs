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
using TestGame.Animatie;
using TestGame.Blocks;
using TestGame.Characters;
using TestGame.Enemies;
using TestGame.Input;
using TestGame.States;

namespace TestGame.Levels
{
    internal class Level : IGameObject
    {
        public int[,] gameboard { get; set; }

        public List<Block> Blocks { get; set; }
        public List<Prikkeldraad> Prikkeldraden { get; set; }
        public List<Wolf> Wolven { get; set; }
        public List<Farmer> Farmers { get; set; }
        public Hero Hero { get; set; }

        public Texture2D PrikkeldraadTexture { get; set; }
        public float Scale { get; set; }
        public Texture2D Texture { get; set; }
        private Dictionary<int, Rectangle> blockTextureRectangles = new Dictionary<int, Rectangle>();
        public GraphicsDevice Graphics { get; set; }


        public Level(float scale, GraphicsDevice graphics, IInputReader inputReader, ContentManager content, LevelSelectie level)
        {

            GameboardSelectie gameboards = GameboardSelectie.getInstance();
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

            Rectangle normalBlock = new Rectangle(64 * 5, 0, 64, 64);
            blockTextureRectangles.Add(1, normalBlock); 

            Rectangle wallRightBlock = new Rectangle(64 * 0, 64 * 1, 64, 64);
            blockTextureRectangles.Add(2, wallRightBlock);

            Rectangle wallLeftBlock = new Rectangle(64 * 2, 64 * 1, 64, 64);
            blockTextureRectangles.Add(3, wallLeftBlock);

            Rectangle groundEdgeLeft = new Rectangle(64 * 4, 64 * 0, 64, 64);
            blockTextureRectangles.Add(4, groundEdgeLeft);

            Rectangle groundEdgeRight = new Rectangle(64 * 6, 64 * 0, 64, 64);
            blockTextureRectangles.Add(5, groundEdgeRight);

            Rectangle singleBlok = new Rectangle(64 * 4, 64 * 1, 64, 64);
            blockTextureRectangles.Add(8, singleBlok);

            Rectangle leftEdgeGrass = new Rectangle(64 * 3, 64 * 3, 64, 64);
            blockTextureRectangles.Add(7, leftEdgeGrass);

            Rectangle noGrassMiddle = new Rectangle(64 * 1, 64 * 1, 64, 64);
            blockTextureRectangles.Add(15, noGrassMiddle);
            
            Rectangle grassLeftEdgeDown = new Rectangle(64 * 2, 64 * 0, 64, 64);
            blockTextureRectangles.Add(16, grassLeftEdgeDown);
            
            Rectangle noGrassBottomEdge = new Rectangle(64 * 0, 64 * 3, 64, 64);
            blockTextureRectangles.Add(17, noGrassBottomEdge);
            
            Rectangle rightBottomEdge = new Rectangle(64 * 2, 64 * 3, 64, 64);
            blockTextureRectangles.Add(18, rightBottomEdge);
            
            Rectangle rightBottom = new Rectangle(64 * 4, 64 * 3, 64, 64);
            blockTextureRectangles.Add(19, rightBottom);
            
            Rectangle topLeftCorner = new Rectangle(64 * 3, 64 * 3, 64, 64);
            blockTextureRectangles.Add(20, topLeftCorner);
            
            Rectangle topRightCorner = new Rectangle(64 * 4, 64 * 3, 64, 64);
            blockTextureRectangles.Add(21, topRightCorner);
            
            Rectangle bottomConnect = new Rectangle(64 * 3, 64 * 0, 64, 64);
            blockTextureRectangles.Add(22, bottomConnect);
            
            Rectangle bottomConnector = new Rectangle(64 * 1, 64 * 3, 64, 64);
            blockTextureRectangles.Add(23, bottomConnector);


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
                            // 6 is schaap/hero
                            Vector2 beginPositie = new Vector2(c * 64, r * 64);
                            Hero = new Hero(content.Load<Texture2D>("sheep"), inputReader, Graphics, beginPositie);
                        }
                        else if (gameboard[r,c] == 12) {
                            //12 is prikkeldraad
                            Vector2 positie = new Vector2((c * 64)+20 , (r * 64)+20);
                            Rectangle rectangle = new Rectangle(0, 0, 27, 48);
                            Prikkeldraad prik = new Prikkeldraad(PrikkeldraadTexture, positie, rectangle, Scale, Graphics);
                            Prikkeldraden.Add(prik);
                        } 
                        else if (gameboard[r,c] == 10)
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
                        } 
                        else if (gameboard[r,c] == 111)
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
