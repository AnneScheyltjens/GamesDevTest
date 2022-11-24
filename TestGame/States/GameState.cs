using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Characters;
using TestGame.Input;

namespace TestGame.States
{
    public class GameState : State
    {
        private List<IGameObject> _gameObjects;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            IGameObject sheep = new Hero(content.Load<Texture2D>("sheep"), new KeyboardReader(), graphicsDevice);


            _gameObjects = new List<IGameObject>()
            {
                sheep,

            };
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();

            foreach (IGameObject gameObject in _gameObjects)
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

            foreach (IGameObject gameObject in _gameObjects)
            {
                gameObject.Update(gametime);
            }
        }
    }
}
