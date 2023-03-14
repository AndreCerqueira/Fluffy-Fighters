using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Components
{
    public class Button : DrawableGameComponent
    {
        // Properties
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Color _defaultColor = Color.White;
        private Color _hoverColor = Color.Gray;
        private bool _isHovering;
        // Clicked event
        public event EventHandler Clicked;




        // Constructors
        public Button(Game game, Texture2D texture, Rectangle rectangle) : base(game)
        {
            _texture = texture;
            _rectangle = rectangle;
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(_rectangle))
            {
                _isHovering = true;

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Clicked?.Invoke(this, EventArgs.Empty);
                }
            }

            base.Update(gameTime);
        }
        

        public override void Draw(GameTime gameTime)
        {
            var color = _defaultColor;

            if (_isHovering)
            {
                color = _hoverColor;
            }

            var spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            // spriteBatch.Draw(_texture, _rectangle, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}