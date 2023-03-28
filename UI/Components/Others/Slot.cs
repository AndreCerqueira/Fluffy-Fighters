using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Emit;

namespace FluffyFighters.UI.Components.Others
{
    public class Slot : DrawableGameComponent
    {
        // Constants
        private const string DEFAULT_ASSET_PATH = "sprites/ui/monster-icons/default-icon";

        // Properties
        private SpriteBatch spriteBatch;
        private Rectangle rectangle;
        public Texture2D texture { get; private set; }
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        private bool isBlocked = false;


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


        // Constructors
        public Slot(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = game.Content.Load<Texture2D>(DEFAULT_ASSET_PATH);
            texture = Utils.ResizeTexture(texture, 0.5f);

            rectangle = new(0, 0, texture.Width, texture.Height);
        }


        public override void Draw(GameTime gameTime)
        {
            var color = GetColor();
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        private Color GetColor()
        {
            if (isBlocked)
                return blockedColor;

            return isHovering ? hoverColor : defaultColor;
        }


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
        }
    }
}
