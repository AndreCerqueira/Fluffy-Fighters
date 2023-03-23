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
        private const float ANIMATION_DURATION = 1f;

        // Properties
        private Texture2D backgroundTexture;
        private Texture2D foregroundTexture;
        private Rectangle backgroundRectangle;
        private Rectangle foregroundRectangle;
        private float value;
        private float maxValue;
        private float percentage => value / maxValue;

        private Color? fixedColor;
        private Color foregroundColor => (value / maxValue) switch
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
            spriteBatch.Draw(foregroundTexture, foregroundRectangle, fixedColor ?? foregroundColor);
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void SetMaxValue(int value) => maxValue = value;
        public async void SetValue(int value, bool isAnimated = true)
        {
            if (isAnimated)
                await AnimateValue(value);
            else
            {
                this.value = value;
                foregroundRectangle.Width = (int)(foregroundTexture.Width * percentage);
            }
        }


        public void SetPosition(Point position)
        {
            backgroundRectangle = new(position.X, position.Y, backgroundTexture.Width, backgroundTexture.Height);
            foregroundRectangle = new(position.X + FOREGROUND_OFFSET, position.Y + FOREGROUND_OFFSET, foregroundTexture.Width, foregroundTexture.Height);
        }


        public void SetColor(Color color) => fixedColor = color;


        // AnimateValue is a coroutine that animates the value of the slider
        private async Task AnimateValue(int value)
        {
            float startValue = this.value;
            float endValue = value;
            float duration = ANIMATION_DURATION;
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += (float)Game.TargetElapsedTime.TotalSeconds;
                float t = elapsed / duration;
                this.value = MathHelper.Lerp(startValue, endValue, t);
                foregroundRectangle.Width = (int)(foregroundTexture.Width * percentage);
                await Task.Delay(1);
            }
        }

    }
}
