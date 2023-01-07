﻿using Microsoft.Xna.Framework;
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
    public class LevelCompleteState : State
    {
        public SpriteFont FontLarge { get; set; }

        public List<IGameObject> Buttons { get; set; }


        public LevelCompleteState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
            : base(game, graphicsDevice, content, levelSelect)
        {
            game.IsMouseVisible = true;

            FontLarge = content.Load<SpriteFont>("Fonts/FontLargeNieuw");

            Buttons = new List<IGameObject>();
            
            Button nextLevelButton = new NextLevelButton(450, this);
            Button replayLevel = new RestartButton(525, this);
            Button quitGameButton = new QuitButton(600, this);

            Buttons.Add(replayLevel);
            Buttons.Add(nextLevelButton);
            Buttons.Add(quitGameButton);

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            spritebatch.DrawString(FontLarge, "Level complete", new Vector2(828, 200), Color.Gold);

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
