using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.States;

namespace TestGame.Controls
{
    public class QuitButton : Button
    {
        public QuitButton(int y, State state) 
            : base(y, state)
        {
            Text = "Quit";
            Click += QuitButton_Click;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            State._game.Exit();
        }

    }
}
