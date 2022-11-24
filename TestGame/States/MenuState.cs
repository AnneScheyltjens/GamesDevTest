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

namespace TestGame.States
{
    public class MenuState : State
    {
        private List<IGameObject> _gameObjects;


        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            Button newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "New game",
                
            };

            newGameButton.Click += newGameButton_click;

            Button loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Load game",

            };

            loadGameButton.Click += loadGameButton_click;

            Button quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit",

            };

            quitGameButton.Click += quitGameButton_click;

            _gameObjects = new List<IGameObject>()
            {
                newGameButton,
                loadGameButton,
                quitGameButton,
            };
        }

        private void quitGameButton_click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void loadGameButton_click(object sender, EventArgs e)
        {
            Debug.WriteLine("Load game");
        }

        private void newGameButton_click(object sender, EventArgs e)
        {
            //load new state
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
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
