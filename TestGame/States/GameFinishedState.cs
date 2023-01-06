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
    internal class GameFinishedState : State
    {

        public SpriteFont Font { get; set; }
        public SpriteFont FontLarge { get; set; }

        public List<IGameObject> Buttons { get; set; }

        public GameFinishedState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect) 
            : base(game, graphicsDevice, content, levelSelect)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            Font = content.Load<SpriteFont>("Fonts/FontNieuw");
            FontLarge = content.Load<SpriteFont>("Fonts/FontLargeNieuw");


            Buttons = new List<IGameObject>();

            Button quitGameButton = new Button(buttonTexture, Font)
            {
                Position = new Vector2(888, 525),
                Text = "Quit",

            };

            quitGameButton.Click += quitGameButton_click;

            Buttons.Add(quitGameButton);

            Button menuButton = new Button(buttonTexture, Font)
            {
                Position = new Vector2(888, 450),
                Text = "Menu",

            };

            menuButton.Click += menuButton_click;

            Buttons.Add(menuButton);


        }
        private void quitGameButton_click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void menuButton_click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, LevelSelectie.None));
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            spritebatch.DrawString(FontLarge, "Game finished", new Vector2(828, 200), Color.Gold);

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
