using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FluffyFighters.UI.Components
{
    public class PlayerStatsMenu : DrawableGameComponent
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/playerStatsBackground";
        private const int PADDING = 10;

        // Properties
        private Rectangle rectangle;
        private Texture2D backgroundTexture;

        private Point topCenterPosition => new(PADDING, PADDING);


        // Constructors
        public PlayerStatsMenu(Game game) : base(game)
        {
            backgroundTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            rectangle = new(topCenterPosition.X, topCenterPosition.Y, backgroundTexture.Width, backgroundTexture.Height);
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
            spriteBatch.Draw(backgroundTexture, rectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
