using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using TestGame.Characters;
using TestGame.Controls;
using TestGame.Input;
using TestGame.States;

namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private State _currentState;
        private State _nextState;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;

            //dit is van op het internet overgenomen maar ik heb de bron niet meer
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.HardwareModeSwitch = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //beginstates, used when testing

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content, Levels.LevelSelectie.None);
            //_currentState = new GameState(this, _graphics.GraphicsDevice, Content, Levels.LevelSelectie.Level1);
            //_currentState = new GameOverState(this, _graphics.GraphicsDevice, Content, Levels.LevelSelectie.Level1);
            //_currentState = new LevelCompleteState(this, _graphics.GraphicsDevice, Content, Levels.LevelSelectie.Level2);
            //_currentState = new GameFinishedState(this, _graphics.GraphicsDevice, Content, Levels.LevelSelectie.None);

            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            // TODO: Add your update logic here
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AliceBlue);

            // TODO: Add your drawing code here

            _currentState.Draw(_spriteBatch);

            base.Draw(gameTime);
        }



        

    }
}