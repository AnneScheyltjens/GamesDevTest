using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Characters;
using TestGame.Controls;
using TestGame.Input;
using TestGame.Levels;

namespace TestGame.States
{
    public class MenuState : State
    {
        private List<IGameObject> _gameObjects;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {

            game.IsMouseVisible = true;

            Button level1Button = new LevelSelectButton(450, this, LevelSelectie.Level1);
            Button level2Button = new LevelSelectButton(525, this, LevelSelectie.Level2);
            Button quitGameButton = new QuitButton(600, this);

            Hero schaap = new Hero(content.Load<Texture2D>("sheep"), new MouseReader(), graphicsDevice, new Vector2(0, 0));

            _gameObjects = new List<IGameObject>()
            {
                level1Button,
                level2Button,
                quitGameButton,
                schaap,
            };
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            foreach (var gameObject in _gameObjects)
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
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(gametime);
                if (gameObject is Hero)
                {
                    Hero schaap = gameObject as Hero;
                    schaap.CurrentPositie = schaap.NextPositie;
                }
            }

            
        }
    }
}
