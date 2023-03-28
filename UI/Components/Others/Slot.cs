using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace FluffyFighters.UI.Components.Others
{
    public class Slot : DrawableGameComponent
    {
        // Constants
        private const string DEFAULT_ASSET_PATH = "sprites/ui/monster-icons/default-icon";
        private const string SELECTED_ASSET_PATH = "sprites/ui/monster-icons/highlighted-icon";

        // Properties
        private SpriteBatch spriteBatch;
        private Rectangle rectangle;
        public Texture2D texture { get; private set; }
        private Texture2D defaultTexture;
        private Texture2D selectedTexture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private bool isSelected = false;

        private Monster content;
        private Rectangle contentRectangle;
        private Texture2D contentTexture;


        // Clicked event
        public event EventHandler<Slot> OnClicked;


        public bool isHovering
        {
            get
            {
                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                return mouseRectangle.Intersects(rectangle);
            }
        }


        private MouseState previousMouseState;
        public bool isClicked
        {
            get
            {
                MouseState currentMouseState = Mouse.GetState();
                bool isLeftButtonDown = currentMouseState.LeftButton == ButtonState.Pressed;
                bool wasLeftButtonDown = previousMouseState.LeftButton == ButtonState.Pressed;

                previousMouseState = currentMouseState;

                return isLeftButtonDown && !wasLeftButtonDown;
            }
        }


        // Constructors
        public Slot(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            defaultTexture = game.Content.Load<Texture2D>(DEFAULT_ASSET_PATH);
            selectedTexture = game.Content.Load<Texture2D>(SELECTED_ASSET_PATH);
            defaultTexture = Utils.ResizeTexture(defaultTexture, 0.5f);
            selectedTexture = Utils.ResizeTexture(selectedTexture, 0.5f);
            texture = defaultTexture;

            rectangle = new Rectangle(0, 0, defaultTexture.Width, defaultTexture.Height);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            if (isClicked && isHovering)
                Clicked();

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var color = GetColor();
            spriteBatch.Begin();

            if (isSelected)
                spriteBatch.Draw(selectedTexture, rectangle, color);
            else
                spriteBatch.Draw(defaultTexture, rectangle, color);

            if (content != null)
                spriteBatch.Draw(contentTexture, contentRectangle, color);

            spriteBatch.End();

            base.Draw(gameTime);
        }


        private void Clicked() => OnClicked?.Invoke(this, this);


        private Color GetColor() => isHovering ? hoverColor : defaultColor;


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
        }


        public void Select() => isSelected = true;
        public void Deselect() => isSelected = false;


        public void SetContent(Monster monster)
        {
            content = monster;
            contentTexture = Game.Content.Load<Texture2D>(monster.iconAssetPath);
            contentTexture = Utils.ResizeTexture(contentTexture, 0.5f);
            contentRectangle = new Rectangle(rectangle.X, rectangle.Y, contentTexture.Width, contentTexture.Height);
        }


        public Monster GetContent() => content;
    }
}
