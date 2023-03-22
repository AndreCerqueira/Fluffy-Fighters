using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Components
{
    internal class Slider : DrawableGameComponent
    {
        // Constants 
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/progressBar";
        private const string FOREGROUND_ASSET_PATH = "sprites/ui/progressBarContent";
        private const int FOREGROUND_OFFSET = 4;

        // Properties
        private Texture2D backgroundTexture;
        private Texture2D foregroundTexture;
        private Rectangle backgroundRectangle;
        private Rectangle foregroundRectangle;
        private int value;
        private int maxValue;
        private float percentage => (float)value / maxValue;

        private Color foregroundColor => (value / (float)maxValue) switch
        {
            float r when r < 0.33f => Color.Red,
            float y when y < 0.66f => Color.Yellow,
            _ => Color.LightGreen
        };


        // Constructors
        public Slider(Game game, int maxValue) : base(game)
        {
            this.maxValue = maxValue;
            this.value = maxValue;

            backgroundTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            foregroundTexture = game.Content.Load<Texture2D>(FOREGROUND_ASSET_PATH);

            SetPosition(new Point(0, 0));
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            spriteBatch.Draw(foregroundTexture, foregroundRectangle, foregroundColor);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void SetValue(int value)
        {
            this.value = value;
            foregroundRectangle.Width = (int)Math.Round(percentage * (backgroundTexture.Width - FOREGROUND_OFFSET * 2));
        }


        public void SetPosition(Point position)
        {
            backgroundRectangle = new(position.X, position.Y, backgroundTexture.Width, backgroundTexture.Height);
            foregroundRectangle = new(position.X + FOREGROUND_OFFSET, position.Y + FOREGROUND_OFFSET, foregroundTexture.Width, foregroundTexture.Height);
        }


    }
}
