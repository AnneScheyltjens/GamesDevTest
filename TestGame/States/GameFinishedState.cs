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
    public class GameFinishedState : TextState
    {
        public GameFinishedState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect) 
            : base(game, graphicsDevice, content, levelSelect)
        {
            Button quitGameButton = new QuitButton(525, this);
            Button menuButton = new MenuButton(450, this);

            _gameObjects.Add(quitGameButton);
            _gameObjects.Add(menuButton);
        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.DrawString(FontLarge, "Game finished", new Vector2(828, 200), Color.Gold);
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(spritebatch);
            }
            spritebatch.End();
        }
    }
}
