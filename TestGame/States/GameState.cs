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
        private Hero hero;
        private Level levelMap;

        public bool IsDead { get; set; }
        public bool LevelDone { get; set; }

        public Game1 Game { get; set; }
        public GraphicsDevice Graphics { get; set; }
        public ContentManager Content { get; set; }

        private List<IGameObject> dingenInGame { get; set; }
        private List<Bullet> ActiveBullets { get; set; }

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect) 
            : base(game, graphicsDevice, content, levelSelect)
        {
            game.IsMouseVisible = false;

            Level level = new Level(1, graphicsDevice, new KeyboardReader(), content, levelSelect);
            levelMap = level;
            hero = level.Hero;
            
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
            levelMap.Draw(spritebatch);
            spritebatch.End();
        }
        
        public override void PostUpdate(GameTime gametime)
        {
            if (LevelDone)
            {

                Game.ChangeState(new LevelCompleteState(Game, Graphics, Content, _levelSelect));
                return;
            }

            if (levelMap.Hero.NrOfLivesLeft <= 0)
            {
                IsDead = true;
            }

            if (IsDead)
            {
                Game.ChangeState(new GameOverState(Game, Graphics, Content, _levelSelect));
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

        public void OnGround(IMovingObject col, int groundLevel, GameTime gametime)
        {
            //adjust to walk on ground
            if (col is Hero)
            {
                Hero hero = col as Hero;
                hero.NextPositie.Positie = new Vector2(hero.NextPositie.Positie.X, 
                    groundLevel - 3 - (2 * hero.NextPositie.HitboxRectangle.Height));
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

                                if (!hero.HasBeenHit)
                                {
                                    hero.NrOfLivesLeft -= 1;
                                    hero.NotTakingDamagaTime = 100;
                                    hero.HasBeenHit = true;
                                   
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
                        }
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
