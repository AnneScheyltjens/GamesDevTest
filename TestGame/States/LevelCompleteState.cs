using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Controls;
using TestGame.Levels;

namespace TestGame.States
{
    internal class LevelCompleteState : State
    {
        public SpriteFont Font { get; set; }
        public SpriteFont FontLarge { get; set; }

        public List<IGameObject> Buttons { get; set; }


        public LevelCompleteState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            Font = content.Load<SpriteFont>("Fonts/FontNieuw");
            FontLarge = content.Load<SpriteFont>("Fonts/FontLargeNieuw");


            Buttons = new List<IGameObject>();

            Button replayLevel = new Button(buttonTexture, Font)
            {
                //helft van 1916 = 985
                Position = new Vector2(888, 450),
                Text = "Replay level",

            };

            replayLevel.Click += replayLevelButton_click;

            Buttons.Add(replayLevel);

            Button nextLevelButton = new Button(buttonTexture, Font)
            {
                //helft van 1916 = 985
                Position = new Vector2(888, 450),
                Text = "Next level",

            };

            nextLevelButton.Click += nextLevelButton_click;

            Buttons.Add(nextLevelButton);



            Button quitGameButton = new Button(buttonTexture, Font)
            {
                Position = new Vector2(888, 525),
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
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _levelSelect));
        }

        private void nextLevelButton_click(object sender, EventArgs e)
        {
            LevelSelectie nextLevel = (LevelSelectie)( (int)_levelSelect + 1);
            if (nextLevel > LevelSelectie.Level2)
            {
                _game.ChangeState(new GameFinishedState(_game, _graphicsDevice, _content, LevelSelectie.None));
            } else
            {
                _game.ChangeState(new GameState(_game, _graphicsDevice, _content, nextLevel));

            }
        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            spritebatch.DrawString(FontLarge, "Level complete", new Vector2(828, 200), Color.Gold);

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
