using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FluffyFighters.UI.Screens;

namespace FluffyFighters.Characters
{
    public class AnimatedSprite
    {
        private SpriteBatch spriteBatch;
        protected Texture2D texture;
        protected int rows;
        protected int columns;
        protected int currentRow;
        protected int currentColumn;
        public int width;
        public int height;
        private float timer;
        protected float animationSpeed;
        private Rectangle rectangle;

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


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(width * currentColumn, height * currentRow, width, height);
            float layerDepth = 0.4f;
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, InGameScreen.GAME_SCALE_FACTOR, SpriteEffects.None, layerDepth);
        }
    }
}
