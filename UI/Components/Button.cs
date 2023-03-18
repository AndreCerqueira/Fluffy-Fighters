using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FluffyFighters.UI.Components
{
    public class Button : DrawableGameComponent
    {
        // Properties
        private Rectangle rectangle => new(position.X, position.Y, texture.Width, texture.Height);
        private Texture2D texture;
        private Point position;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Label label;

        // Clicked event
        public event EventHandler Clicked;

        
        private bool isHovering
        {
            get
            {
                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                return mouseRectangle.Intersects(rectangle);
            }
        }


        private bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;


        // Constructors
        public Button(Game game, Texture2D texture, Point position, Label label = null) : base(game)
        {
            this.texture = texture;
            this.position = position;
            this.label = label;
        }
        

        // Methods
        public override void Update(GameTime gameTime)
        {
            if (isClicked && isHovering)
                OnClicked();

            base.Update(gameTime);
        }
        

        public override void Draw(GameTime gameTime)
        {
            var color = isHovering ? hoverColor : defaultColor;
            var spriteBatch = new SpriteBatch(GraphicsDevice);
            
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, color);

            if (label != null) { 
            label.position = new Vector2(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f) - label.font.MeasureString(label.text) / 2f;
            label.Draw(gameTime);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void OnClicked() => Clicked?.Invoke(this, new EventArgs());
    }
}