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
    public class GameOverState : TextState
    {
        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        { 
            Button restartButton = new RestartButton(450, this);
            Button quitGameButton = new QuitButton(525, this);

            _gameObjects.Add(restartButton);
            _gameObjects.Add(quitGameButton);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.DrawString(FontLarge, "Game over", new Vector2(860, 200), Color.Red);
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(spritebatch);
            }
            spritebatch.End();
        }
    }
}
