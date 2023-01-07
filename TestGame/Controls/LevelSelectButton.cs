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
    internal class LevelSelectButton : Button
    {
        public LevelSelectie Level { get; set; }

        public LevelSelectButton(int y, State state, LevelSelectie level) 
            : base(y, state)
        {
            Level = level;
            Text = level.ToString();
            Click += LevelButton_click;
        }

        private void LevelButton_click(object sender, EventArgs e)
        {
            State._game.ChangeState(new GameState(State._game, State._graphicsDevice, State._content, Level));
        }
    }
}
