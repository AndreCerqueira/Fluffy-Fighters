using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Reflection.Metadata;

namespace FluffyFighters.UI.Components.Buttons
{
    public class Button : DrawableGameComponent
    {
        // Cursors
        static public readonly MouseCursor defaultCursor = MouseCursor.Arrow;
        static public readonly MouseCursor hoverCursor = MouseCursor.Hand;

        // Constants
        private const string ASSET_PATH = "sprites/ui/button";
        public const int PADDING = 20;

        // Properties
        private Rectangle rectangle;
        public Texture2D texture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        public Label label;
        private bool isBlocked = false;
        private Vector2 labelPosition => new Vector2(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f) - label.font.MeasureString(label.text) / 2f;

        // Clicked event
        public event EventHandler OnClicked;


        public bool isHovering
        {
            get
            {
                if (isBlocked)
                    return false;

                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                return mouseRectangle.Intersects(rectangle);
            }
        }


        public bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;


        // Constructors
        public Button(Game game, string text = null, string customAssetPath = null) : base(game)
        {
            texture = game.Content.Load<Texture2D>(customAssetPath ?? ASSET_PATH);

            rectangle = new(0, 0, texture.Width, texture.Height);

            if (text != null)
                label = new Label(game, text);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            if (isClicked && isHovering && !isBlocked)
                Clicked();

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var color = GetColor();
            var spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, color);
            spriteBatch.End();

            label?.Draw(gameTime);

            base.Draw(gameTime);
        }

        private Color GetColor()
        {
            if (isBlocked)
                return blockedColor;

            return isHovering ? hoverColor : defaultColor;
        }


        private void Clicked() => OnClicked?.Invoke(this, new EventArgs());
        public void Block() => isBlocked = true;
        public void Unblock() => isBlocked = false;


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;

            label?.SetPosition(labelPosition);
        }
    }
}