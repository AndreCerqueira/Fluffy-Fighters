using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace FluffyFighters.UI.Components.Buttons
{
    public class CaptureButton : DrawableGameComponent
    {
        // Cursors
        static public readonly MouseCursor defaultCursor = MouseCursor.Arrow;
        static public readonly MouseCursor hoverCursor = MouseCursor.Hand;

        // Constants
        private const string ASSET_PATH = "sprites/ui/captureButton";
        private const string ICON_ASSET_PATH = "sprites/ui/ball";
        public const int PADDING = 20;

        // Properties
        private Rectangle rectangle;
        public Texture2D texture;
        private Rectangle iconRectangle;
        public Texture2D iconTexture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        private bool isBlocked = false;

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
        public CaptureButton(Game game) : base(game)
        {
            texture = game.Content.Load<Texture2D>(ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);

            iconTexture = game.Content.Load<Texture2D>(ICON_ASSET_PATH);
            iconRectangle = new(0, 0, iconTexture.Width, iconTexture.Height);
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
            spriteBatch.Draw(texture, rectangle, defaultColor);
            spriteBatch.Draw(iconTexture, iconRectangle, color);
            spriteBatch.End();

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

            // Center icon
            iconRectangle.X = position.X + (rectangle.Width - iconRectangle.Width) / 2;
            iconRectangle.Y = position.Y + (rectangle.Height - iconRectangle.Height) / 2;
        }
    }
}
