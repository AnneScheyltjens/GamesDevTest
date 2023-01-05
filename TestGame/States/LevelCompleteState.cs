using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Controls;

namespace TestGame.States
{
    internal class LevelCompleteState : State
    {
        public SpriteFont Font { get; set; }
        public SpriteFont FontLarge { get; set; }

        public List<IGameObject> Buttons { get; set; }


        public LevelCompleteState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            Font = content.Load<SpriteFont>("Fonts/Font");
            FontLarge = content.Load<SpriteFont>("Fonts/FontLarge");


            Buttons = new List<IGameObject>();

            Button replayLevel = new Button(buttonTexture, Font)
            {
                //helft van 1916 = 985
                Position = new Vector2(888, 450),
                Text = "Replay level",

            };

            replayLevel.Click += replayLevelButton_click;

            Buttons.Add(replayLevel);

            Button quitGameButton = new Button(buttonTexture, Font)
            {
                Position = new Vector2(888, 500),
                Text = "Quit",

            };

            quitGameButton.Click += quitGameButton_click;

            Buttons.Add(quitGameButton);

        }

        private void quitGameButton_click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void replayLevelButton_click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void nextLevelButton_click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, false));
        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            spritebatch.DrawString(FontLarge, "Level complete", new Vector2(828, 100), Color.Gold);

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
