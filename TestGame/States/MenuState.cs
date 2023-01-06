using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Controls;
using TestGame.Levels;

namespace TestGame.States
{
    // de button classe en de baics van de states heb ik overgenomen van deze youtube video's
    // https://www.youtube.com/watch?v=lcrgj26G5Hg
    // https://www.youtube.com/watch?v=76Mz7ClJLoE
    public class MenuState : State
    {
        private List<IGameObject> _gameObjects;


        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/FontNieuw");

            Button level1Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(888, 450),
                Text = "Level 1",
                
            };

            level1Button.Click += level1Button_click;

            Button level2Button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(888, 525),
                Text = "Level 2",

            };

            level2Button.Click += level2Button_click;

            Button quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(888, 600),
                Text = "Quit",

            };

            quitGameButton.Click += quitGameButton_click;

            _gameObjects = new List<IGameObject>()
            {
                level1Button,
                level2Button,
                quitGameButton,
            };
        }

        private void quitGameButton_click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void level1Button_click(object sender, EventArgs e)
        {
            //load new state
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, Levels.LevelSelectie.Level1));
        }

        private void level2Button_click(object sender, EventArgs e)
        {
            //Debug.WriteLine("Load game");
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, Levels.LevelSelectie.Level2));
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
            //remove sprites if they're not needed
        }

        public override void Update(GameTime gametime)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(gametime);
            }
        }
    }
}
