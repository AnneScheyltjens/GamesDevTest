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

namespace TestGame.States
{
    public class GameState : State
    {
        private List<IGameObject> _gameObjects;
        private Hero hero;
        private bool updateHero = true;
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
            foreach (Block block in levelMap.Blocks)
            {
                //Debug.WriteLine(hero.NextPosition);
                /*if (block.Hitbox.Intersects(hero.NextRectagle))
                {
                    //do not move the hero
                    updateHero = false;
                }
                else
                {*/
                    //Debug.WriteLine(block.Hitbox);
                    //var trueHeroBox = new Rectangle((int)hero.HitboxPosition.X, (int)hero.HitboxPosition.Y, hero.Animation.CurrentFrame.HitboxRectangle.Width, hero.Animation.CurrentFrame.HitboxRectangle.Height);
                    if (block.Hitbox.Intersects(hero.HitboxRectangle))
                    {
                        //intersects with block from level
                        updateHero = false;
                    }
                //}
            }
            

            foreach (IGameObject gameObject in _gameObjects)
            {
                if (gameObject == hero && !updateHero)
                {
                    hero.UpdateToOldPostion(gametime);
                    //skip the hero update
                    //reset the collision detextor
                    updateHero = true;
                }
                else
                {
                    //no problems
                    gameObject.Update(gametime);
                }
                
            }
        }
    }
}
