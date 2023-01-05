using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Blocks;
using TestGame.Characters;
using TestGame.Collision;
using TestGame.Enemies;
using TestGame.Input;
using TestGame.Levels;
using static System.Net.Mime.MediaTypeNames;

namespace TestGame.States
{

    
    public class GameState : State
    {
        private List<IGameObject> _gameObjects;
        private Hero hero;
        //private bool updateHero = true;
        //private bool heroOnGround = false;
        //private int groundLevel = 0;
        private LevelTest levelMap;

        public bool IsDead { get; set; }
        public bool LevelDone { get; set; }

        public Game1 Game { get; set; }
        public GraphicsDevice Graphics { get; set; }
        public ContentManager Content { get; set; }

        private List<IGameObject> dingenInGame { get; set; }

        private List<Bullet> ActiveBullets { get; set; }

        public bool LevelOneToPlay { get; set; }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, bool levelOne = true) : base(game, graphicsDevice, content)
        {
            LevelOneToPlay = levelOne;
            if (LevelOneToPlay)
            {
                //hero = sheep;
                LevelTest level = new LevelTest(content.Load<Texture2D>("tileset"), 1, graphicsDevice, content.Load<Texture2D>("sheep"), new KeyboardReader(), content);
                levelMap = level;
                hero = level.Hero;
            } else
            {
                //other level
                //ook nog aanpassen
            }
            //Hero sheep = new Hero(content.Load<Texture2D>("sheep"), new KeyboardReader(), graphicsDevice);
            

            

            /*_gameObjects = new List<IGameObject>()
            {
                level,
                hero,

            };*/

            dingenInGame = new List<IGameObject>();
            addGameObjectsToList();
         

            Game = game;
            Graphics = graphicsDevice;
            Content = content;

            IsDead = false;
            LevelDone = false;
        }

        private void addGameObjectsToList()
        {
            foreach (Block blok in levelMap.Blocks)
            {
                dingenInGame.Add(blok);
            }
            foreach (Prikkeldraad prik in levelMap.Prikkeldraden)
            {
                dingenInGame.Add(prik);
            }
            foreach (Wolf wolf in levelMap.Wolven)
            {
                dingenInGame.Add(wolf);
            }
            foreach (Farmer farmer in levelMap.Farmers)
            {
                dingenInGame.Add(farmer);
            }

            checkBullets();
            foreach (Bullet bullet in ActiveBullets)
            {
                dingenInGame.Add(bullet);
            }

            dingenInGame.Add(levelMap.Hero);

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            /*foreach (IGameObject gameObject in dingenInGame)
            {
                gameObject.Draw(spritebatch);
            }*/
            levelMap.Draw(spritebatch);

            spritebatch.End();
        }
        
        public override void PostUpdate(GameTime gametime)
        {
            if (LevelDone)
            {

                Game.ChangeState(new LevelCompleteState(Game, Graphics, Content));
                return;
            }

            if (levelMap.Hero.NrOfLivesLeft <= 0)
            {
                IsDead = true;
            }

            if (IsDead)
            {
                Game.ChangeState(new GameOverState(Game, Graphics, Content));
                return;
            }

            if (levelMap.Hero.HasBeenHit)
            { 
                if (levelMap.Hero.NotTakingDamagaTime == 100)
                {
                    levelMap.Hero.Lives.ElementAt(levelMap.Hero.NrOfLivesLeft-1).Update(gametime);
                }
                levelMap.Hero.NotTakingDamagaTime -= 1;

            }

            if (levelMap.Hero.NotTakingDamagaTime <= 0)
            {
                levelMap.Hero.HasBeenHit = false;
            }

            

            
        }

        public void UpdateOld(GameTime gametime)
        {
            foreach (IGameObject gameObject in _gameObjects)
            {
                gameObject.Update(gametime);


                if (gameObject == hero)
                {
                    bool walkOnGround = false;
                    bool intersects = false;
                    int groundLevel = 0;

                    //check if next position is valid

                    foreach (Block block in levelMap.Blocks)
                    {
                        if (block.Hitbox.Intersects(hero.NextPositie.HitboxRectangle))
                        {
                            //check of je op de vloer staat
                            if (hero.NextPositie.HitboxRectangle.Bottom == block.Hitbox.Top ||
                                hero.NextPositie.HitboxRectangle.Bottom == block.Hitbox.Top + 1)
                            {
                                //wandel op de vloer
                                walkOnGround = true;
                                groundLevel = block.Hitbox.Top;
                            }
                            else
                            {
                                intersects = true;
                            }
                        }
                    }

                    foreach (Prikkeldraad prik in levelMap.Prikkeldraden)
                    {
                        if (prik.Positie.HitboxRectangle.Intersects(hero.NextPositie.HitboxRectangle)) {
                            //check of het langs de zijkant is
                            if (prik.Positie.HitboxRectangle.Left < hero.NextPositie.HitboxRectangle.Right)
                            {
                                //prikkeldraad wordt langs links benaderd
                                IsDead = true;

                            } else if (prik.Positie.HitboxRectangle.Right > hero.NextPositie.HitboxRectangle.Left)
                            {
                                //prikkeldraad wordt langs rechts benaderd
                                IsDead = true;
                            } else
                            {
                                //langs boven of onder
                            }
                           
                        }
                    }

                    //if valid
                    if (!intersects)
                    {
                        //check if walks on ground
                        if (walkOnGround)
                        {
                            //adjust to walk on ground
                            /*hero.NextPositie.Positie = new Vector2(hero.NextPositie.Positie.X,
                                 groundLevel - 3 - (2*hero.NextPositie.HitboxRectangle.Height)); // - hero.NextPositie.Positie.Y);
                            hero.UpdateWithoutPositionRetrieve(gametime);*/


                            //heroOnGround(groundLevel, gametime);
                            OnGround(hero, groundLevel, gametime);
                        }

                        //  move to next position
                        hero.CurrentPositie = hero.NextPositie;
                    }
                    else
                    {
                        //if not valid
                        //  no changes to position
                        //hero.CurrentPositie = hero.CurrentPositie;
                        
                    }
                } 

                
                
            }
        }

        public void OnGround(IMovingObject col, int groundLevel, GameTime gametime)
        {
            //adjust to walk on ground
            if (col is Hero)
            {
                Hero hero = col as Hero;
                hero.NextPositie.Positie = new Vector2(hero.NextPositie.Positie.X, 
                    groundLevel - 3 - (2 * hero.NextPositie.HitboxRectangle.Height)); // - hero.NextPositie.Positie.Y);
                hero.UpdateWithoutPositionRetrieve(gametime);
                hero.OnGround = true;
            } else if (col is Wolf)
            {
                Wolf wolf = col as Wolf;
                wolf.NextPositie.Positie = new Vector2(wolf.NextPositie.Positie.X + wolf.GoesRight,
                    groundLevel - (1.5f * col.NextPositie.HitboxRectangle.Height));
                hero.UpdateWithoutPositionRetrieve(gametime);
            }
            
        }

        private void checkBullets()
        {
            List<Bullet> activeBullets = new List<Bullet>();
            foreach (Farmer farmer in levelMap.Farmers)
            {
                foreach (Bullet bullet in farmer.Bullets)
                {
                    activeBullets.Add(bullet);
                }
            }

            ActiveBullets = activeBullets;
        }

        private void CheckLives()
        {

        }

        public override void Update(GameTime gametime)
        {
            
            checkBullets();


            foreach (IGameObject gameObject in dingenInGame)
            {
                gameObject.Update(gametime);

                List<IGameObject> collidesWith;

                if (gameObject is Hero)
                {
                    foreach (Bullet bullet in ActiveBullets)
                    {
                        if (bullet.CurrentPositie.HitboxRectangle.Intersects(hero.NextPositie.HitboxRectangle))
                        {
                            Hero hero = gameObject as Hero;
                            if (!hero.HasBeenHit)
                            {
                                hero.NrOfLivesLeft -= 1;
                                hero.HasBeenHit = true;
                                hero.NotTakingDamagaTime = 100;
                                //IsDead = true;
                                Debug.WriteLine("Bullet raakt hero!");
                                return;
                            }
                            
                        }
                    }
                }

                if (gameObject is IMovingObject)
                {
                    IMovingObject col = gameObject as IMovingObject;

                    collidesWith = Collidable.IntersectsWith(col, dingenInGame);

                    if (collidesWith.Count != 0)
                    {
                        List<Block> bloks = new List<Block>();
                        int groundLevel = 0;
                        //it intersects with something
                        foreach (IGameObject ding in collidesWith)
                        {
                            if (ding is Prikkeldraad && col is Hero)
                            {
                                Hero hero = col as Hero;
                                //Debug.WriteLine(hero.NotTakingDamagaTime);

                                if (!hero.HasBeenHit)
                                {
                                    hero.NrOfLivesLeft -= 1;
                                    hero.NotTakingDamagaTime = 100;
                                    hero.HasBeenHit = true;
                                    //IsDead = true;
                                    //Debug.WriteLine(hero.NotTakingDamagaTime);

                                    Debug.WriteLine("Prikkeldraad raakt hero!");

                                    return;
                                }
                                
                            }
                            else if (ding is Prikkeldraad && col is Wolf)
                            {
                                Wolf wolf = col as Wolf;
                                wolf.GoesRight *= -1;
                            } else if (col is Hero && ding is Wolf)
                            {
                                //IsDead = true;
                                Hero hero = col as Hero;
                                if (!hero.HasBeenHit)
                                {
                                    hero.HasBeenHit = true;
                                    hero.NrOfLivesLeft -= 1;
                                    hero.NotTakingDamagaTime = 100;
                                    Debug.WriteLine("Wolf raakt hero!");

                                    return;
                                }
                                
                            }
                            else if (ding is Block)
                            {
                                Block blok = ding as Block;

                                if (col is Hero && blok.IsGrass)
                                {
                                    LevelDone = true;
                                    return;
                                }
                                bloks.Add(ding as Block);
                            } 
                        }

                        if (bloks.Count != 0)
                        {
                            groundLevel = Collidable.WalksOnGround(col, bloks);
                        }

                        if (groundLevel != 0)
                        {
                            OnGround(col, groundLevel, gametime);
                            col.CurrentPositie = col.NextPositie;
                            if (col is Hero)
                            {
                                Hero hero = col as Hero;
                            }

                        }/* else if (groundLevel != 0)
                        {
                            IGameObject ding = col as IGameObject;
                            ding.Update(gametime);
                            IMovingObject colli = ding as IMovingObject;
                            colli.CurrentPositie = colli.NextPositie;
                        }*/
                    } else
                    {
                        col.CurrentPositie = col.NextPositie;
                        if (col is Hero)
                        {
                            Hero hero = col as Hero;
                            hero.OnGround = false;
                        }
                    }
                }


            }
        }
    }
}
