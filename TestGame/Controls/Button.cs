﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGame.Controls
{
    // de button classe en de baics van de states heb ik overgenomen van deze youtube video's
    // https://www.youtube.com/watch?v=lcrgj26G5Hg
    // https://www.youtube.com/watch?v=76Mz7ClJLoE
    public class Button : IGameObject
    {
        #region Fields

        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        //private Texture2D _texture;

        #endregion

        #region Properties

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle { get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width+10, Texture.Height+10);
            } 
        }
        public string Text { get; set; }

        public Texture2D Texture { get; set; }



        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            Texture = texture;
            _font = font;
            PenColour = Color.Black;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if (_isHovering)
            {
                colour = Color.Gray;
            }

            spriteBatch.Draw(Texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2) - (_font.MeasureString(Text).X / 2));
                var y = (Rectangle.Y + (Rectangle.Height / 2) - (_font.MeasureString(Text).Y / 2));

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);


            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed) {
                    //Click?.Invoke(this, new EventArgs()); //is hetzelfde als dit hieronder

                    if (Click != null)
                    {
                        Click(this, new EventArgs());
                    }
                }
            }


        }

        #endregion
    }
}
