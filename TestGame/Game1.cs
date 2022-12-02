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
        //private Random rand = new Random();

        private List<IGameObject> _gameObjects;

        private State _currentState;
        private State _nextState;


        public void ChangeState(State state)
        {
            _nextState = state;

        }

       
        Hero _sheep;
        private Texture2D _sheepTexture;

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

            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.HardwareModeSwitch = true;
            //_graphics.ApplyChanges();


           
            base.Initialize();

            _sheep = new Hero(_sheepTexture, new KeyboardReader(), GraphicsDevice);
           
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //_currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
            _currentState = new GameState(this, _graphics.GraphicsDevice, Content);

            /*var randomButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 200),
                Text = "Random",
            };

            randomButton.Click += RandomButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 250),
                Text = "Quit",
            };

            quitButton.Click += QuitButton_Click;

            _gameObjects = new List<IGameObject>()
            {
                randomButton, 
                quitButton
            };*/

        // TODO: use this.Content to load your game content here

        _sheepTexture = Content.Load<Texture2D>("Sheep");



        }
        /*
        private void QuitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void RandomButton_Click(object sender, EventArgs e)
        {
                Exit();
        }
        */
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*foreach (var component in _gameComponents)
            {
                component.Update(gameTime);
            }*/
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            // TODO: Add your update logic here

            _sheep.Update(gameTime);
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            //int randomGetal = rand.Next(kleurtjes.Count);

            GraphicsDevice.Clear(Color.AliceBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            /*foreach (var comonent in _gameComponents)
            {
                comonent.Draw(gameTime, _spriteBatch);
            }*/
            //_sheep.Draw(_spriteBatch);
            _spriteBatch.End();

            _currentState.Draw(_spriteBatch);

            base.Draw(gameTime);
        }



        

    }
}