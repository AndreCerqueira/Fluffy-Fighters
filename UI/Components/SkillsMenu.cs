using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FluffyFighters.UI.Components
{
    public class SkillsMenu : DrawableGameComponent
    {
        // Properties
        private Rectangle rectangle;
        private Texture2D texture;


        // Constructors
        public SkillsMenu(Game game, Texture2D texture) : base(game)
        {
            this.texture = texture;

            // Get position at bottom center of screen
            Point position = new Point(
                GraphicsDevice.Viewport.Width / 2 - texture.Width / 2,
                GraphicsDevice.Viewport.Height - texture.Height
            );

            rectangle = new(position.X, position.Y, texture.Width, texture.Height);
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
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
