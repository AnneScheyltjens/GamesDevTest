using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.States;

namespace TestGame.Controls
{
    internal class MenuButton : Button
    {
        public MenuButton(int y, State state) 
            : base(y, state)
        {
            Text = "Menu";
            Click += MenuButton_click;
        }

        private void MenuButton_click(object sender, EventArgs e)
        {
            State._game.ChangeState(new MenuState(State._game, State._graphicsDevice, State._content, Levels.LevelSelectie.None));
        }
    }
}
