﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FluffyFighters.Characters
{
    public class AnimatedSprite
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private int rows;
        private int columns;
        protected int currentRow;
        private int currentColumn;
        private int width;
        private int height;
        private float timer;
        private float animationSpeed;

        public Vector2 position { get; set; }

        public AnimatedSprite(Game game, Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            currentRow = 0;
            currentColumn = 0;
            width = texture.Width / columns;
            height = texture.Height / rows;
            timer = 0;
            animationSpeed = 0.2f;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= animationSpeed)
            {
                timer = 0f;
                currentColumn++;
                if (currentColumn >= columns)
                    currentColumn = 0;
            }
        }

        public virtual void Draw()
        {
            spriteBatch.Begin();

            Rectangle sourceRectangle = new Rectangle(width * currentColumn, height * currentRow, width, height);
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
        }
    }
}
