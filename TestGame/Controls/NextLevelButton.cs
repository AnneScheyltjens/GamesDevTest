using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Levels;
using TestGame.States;

namespace TestGame.Controls
{
    internal class NextLevelButton : Button
    {
        public NextLevelButton(int y, State state) 
            : base(y, state)
        {
            Text = "Next level";
            Click += NextLevelButton_click;
        }

        private void NextLevelButton_click(object sender, EventArgs e)
        {
            LevelSelectie nextLevel = (LevelSelectie)((int)State._levelSelect + 1);
            if (nextLevel > LevelSelectie.Level2)
            {
                State._game.ChangeState(new GameFinishedState(State._game, State._graphicsDevice, State._content, LevelSelectie.None));
            } else
            {
                State._game.ChangeState(new GameState(State._game, State._graphicsDevice, State._content, nextLevel));
            }
        }
    }
}
