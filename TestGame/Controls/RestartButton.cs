using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.States;

namespace TestGame.Controls
{
    internal class RestartButton : Button
    {
        public RestartButton(int y, State state) 
            : base(y, state)
        {
            Text = "Restart level";
            Click += RestartButton_click;
        }

        private void RestartButton_click(object sender, EventArgs e)
        {
            State._game.ChangeState(new GameState(State._game, State._graphicsDevice, State._content, State._levelSelect));
        }
    }
}
