using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Blocks;
using TestGame.Characters;
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
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Hero sheep = new Hero(content.Load<Texture2D>("sheep"), new KeyboardReader(), graphicsDevice);
            hero = sheep;
            LevelTest level = new LevelTest(content.Load<Texture2D>("tileset"), 1, graphicsDevice);
            levelMap = level;


            _gameObjects = new List<IGameObject>()
            {
                level,
                sheep,

            };
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            foreach (IGameObject gameObject in _gameObjects)
            {
                gameObject.Draw(spritebatch);
            }

            spritebatch.End();
        }

        public override void PostUpdate(GameTime gametime)
        {
        }

        public override void Update(GameTime gametime)
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
                            if (hero.NextPositie.HitboxRectangle.Bottom < block.Hitbox.Top)
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

                    //if valid
                    if (!intersects)
                    {
                        //check if walks on ground
                        if (walkOnGround)
                        {
                            //adjust to walk on ground
                            hero.NextPositie.Positie = new Vector2(hero.NextPositie.Positie.X, groundLevel);
                        }

                        //  move to next position
                        hero.CurrentPositie = hero.NextPositie;
                    }
                    else
                    {
                        //if not valid
                        //  no changes to position
                        hero.CurrentPositie = hero.CurrentPositie;
                    }
                } 

                
                
            }
        }
    }
}
