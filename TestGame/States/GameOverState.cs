using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Controls;
using TestGame.Levels;

namespace TestGame.States
{
    internal class GameOverState : State
    {
        public SpriteFont Font { get; set; }
        public SpriteFont FontLarge { get; set; }

        public List<IGameObject> Buttons { get; set; }
        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            Font = content.Load<SpriteFont>("Fonts/FontNieuw");
            FontLarge = content.Load<SpriteFont>("Fonts/FontLargeNieuw");


            /*Button restartButton = new Button(buttonTexture, Font)
            {
                Position = new Vector2(888, 450),
                Text = "Restart level"
            };

            restartButton.Click += restartButton_click;*/

            Button restartButton = new RestartButton(450, this);

            Buttons = new List<IGameObject>();

            Buttons.Add(restartButton);

            /*Button quitGameButton = new Button(buttonTexture, Font)
            {
                Position = new Vector2(888, 525),
                Text = "Quit",

            };

            quitGameButton.Click += quitGameButton_click;*/

            Button quitGameButton = new QuitButton(525, this);

            Buttons.Add(quitGameButton);

        }

        private void restartButton_click(object sender, EventArgs e)
        {
            //load new state
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _levelSelect));
        }

        private void quitGameButton_click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            spritebatch.DrawString(FontLarge, "Game over", new Vector2(860, 200), Color.Red);

            foreach (var gameObject in Buttons)
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
            foreach (var gameObject in Buttons)
            {
                gameObject.Update(gametime);
            }
        }
    }
}
