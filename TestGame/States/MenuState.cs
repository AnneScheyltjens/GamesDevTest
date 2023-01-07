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
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            Button level1Button = new LevelSelectButton(450, this, LevelSelectie.Level1);
            Button level2Button = new LevelSelectButton(525, this, LevelSelectie.Level2);
            Button quitGameButton = new QuitButton(600, this);

            _gameObjects.Add(level1Button);
            _gameObjects.Add(level2Button);
            _gameObjects.Add(quitGameButton);
        }
    }
}
