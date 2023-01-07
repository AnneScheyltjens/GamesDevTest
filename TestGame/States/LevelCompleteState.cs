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
    public class LevelCompleteState : TextState
    {
        public LevelCompleteState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            Button nextLevelButton = new NextLevelButton(450, this);
            Button replayLevel = new RestartButton(525, this);
            Button quitGameButton = new QuitButton(600, this);

            _gameObjects.Add(replayLevel);
            _gameObjects.Add(nextLevelButton);
            _gameObjects.Add(quitGameButton);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            spritebatch.DrawString(FontLarge, "Level complete", new Vector2(828, 200), Color.Gold);
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(spritebatch);
            }
            spritebatch.End();
        }
    }
}
