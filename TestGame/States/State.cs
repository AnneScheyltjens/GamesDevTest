using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGame.Levels;

namespace TestGame.States
{
    // de button classe en de baics van de states heb ik overgenomen van deze youtube video's
    // https://www.youtube.com/watch?v=lcrgj26G5Hg
    // https://www.youtube.com/watch?v=76Mz7ClJLoE
    public abstract class State
    {
        #region Fields

        public ContentManager _content;

        public GraphicsDevice _graphicsDevice;

        public Game1 _game;

        public LevelSelectie _levelSelect;

        #endregion

        #region Methods

        public abstract void Draw(SpriteBatch spritebatch);

        public abstract void PostUpdate(GameTime gametime);
        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, LevelSelectie levelSelect)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _levelSelect = levelSelect;
        }

        public abstract void Update(GameTime gametime);


        #endregion

    }
}
