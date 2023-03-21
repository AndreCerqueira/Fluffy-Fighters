﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Reflection.Metadata;

namespace FluffyFighters.UI.Components
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
        public Label label;
        private Vector2 labelPosition => new Vector2(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f) - label.font.MeasureString(label.text) / 2f;

        // Clicked event
        public event EventHandler Clicked;

        
        public bool isHovering
        {
            get
            {
                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                return mouseRectangle.Intersects(rectangle);
            }
        }


        public bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;


        // Constructors
        public Button(Game game, string text = null) : base(game)
        {
            this.texture = game.Content.Load<Texture2D>(ASSET_PATH);
            
            rectangle = new(0, 0, texture.Width, texture.Height);

            if (text != null)
                label = new Label(game, text);
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
            spriteBatch.End();
            
            label?.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void OnClicked() => Clicked?.Invoke(this, new EventArgs());


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
            
            label?.SetPosition(labelPosition);
        }
    }
}