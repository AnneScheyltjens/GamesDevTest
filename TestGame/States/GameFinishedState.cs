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
    public class GameFinishedState : State
    {
        public SpriteFont FontLarge { get; set; }

        public List<IGameObject> Buttons { get; set; }

        public GameFinishedState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect) 
            : base(game, graphicsDevice, content, levelSelect)
        {
            game.IsMouseVisible = true;

            FontLarge = content.Load<SpriteFont>("Fonts/FontLargeNieuw");

            Buttons = new List<IGameObject>();

            Button quitGameButton = new QuitButton(525, this);
            Button menuButton = new MenuButton(450, this);

            Buttons.Add(quitGameButton);
            Buttons.Add(menuButton);

        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            spritebatch.DrawString(FontLarge, "Game finished", new Vector2(828, 200), Color.Gold);

            foreach (var gameObject in Buttons)
            {
                gameObject.Draw(spritebatch);
            }

            spritebatch.End();
        }

        public override void PostUpdate(GameTime gametime)
        {
            
        }

        public override void Update(GameTime gametime)
        {
            foreach (var gameObject in Buttons)
            {
                gameObject.Update(gametime);
            }
        }
    }
}
