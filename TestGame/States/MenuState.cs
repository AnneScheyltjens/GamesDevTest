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
    public class MenuState : TextState
    {
        public SpriteFont Font { get; set; }
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            Font = content.Load<SpriteFont>("Fonts/FontNieuw");

            Button level1Button = new LevelSelectButton(450, this, LevelSelectie.Level1);
            Button level2Button = new LevelSelectButton(525, this, LevelSelectie.Level2);
            Button level3Button = new LevelSelectButton(600, this, LevelSelectie.Level3);
            Button quitGameButton = new QuitButton(675, this);

            _gameObjects.Add(level1Button);
            _gameObjects.Add(level2Button);
            _gameObjects.Add(level3Button);
            _gameObjects.Add(quitGameButton);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.DrawString(FontLarge, "Hungry sheep", new Vector2(828, 200), Color.Green);
            spritebatch.DrawString(Font, "Het schaapje heeft honger en wilt naar het gras.", new Vector2(700, 275), Color.Green);
            spritebatch.DrawString(Font, "Maar opgepast voor de boer die het op de wolven heeft gemunt.", new Vector2(700, 300), Color.Green);
            
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(spritebatch);
            }
            spritebatch.End();
        }
    }
}
