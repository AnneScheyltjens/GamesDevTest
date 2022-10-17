using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using TestGame.Characters;

namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Random rand = new Random();

       
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

           
            base.Initialize();

            _sheep = new Hero(_sheepTexture);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _sheepTexture = Content.Load<Texture2D>("Sheep");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _sheep.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            List<Color> kleurtjes = new List<Color>();
            kleurtjes.Add(Color.AliceBlue);
            kleurtjes.Add(Color.HotPink);
            kleurtjes.Add(Color.DarkOliveGreen);
            kleurtjes.Add(Color.Yellow);

            //int randomGetal = rand.Next(kleurtjes.Count);

            GraphicsDevice.Clear(kleurtjes[0]);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _sheep.Draw(_spriteBatch);
            _spriteBatch.End();



            base.Draw(gameTime);
        }

    }
}