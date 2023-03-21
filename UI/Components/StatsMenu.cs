using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;

namespace FluffyFighters.UI.Components
{
    public class StatsMenu : DrawableGameComponent
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/playerStatsBackground";
        private const int PADDING = 10;
        private const int LABEL_PADDING = 15;

        // Properties
        private Rectangle rectangle;
        private Texture2D backgroundTexture;
        private Point topCenterPosition => new(PADDING, PADDING);
        private Point sliderPosition => new(topCenterPosition.X + PADDING * 2, topCenterPosition.Y + (backgroundTexture.Height/2));
        private Point sliderPositionInStatsMenu => topCenterPosition + sliderPosition;
        private Point nameLabelPosition => new(topCenterPosition.X + LABEL_PADDING, topCenterPosition.Y + LABEL_PADDING);
        private Point levelLabelPosition => new(topCenterPosition.X + backgroundTexture.Width - LABEL_PADDING * 4, topCenterPosition.Y + LABEL_PADDING);

        // Components
        private Label nameLabel;
        private Label levelLabel;
        private Slider healthSlider;


        // Constructors
        public StatsMenu(Game game) : base(game)
        {
            backgroundTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            rectangle = new(topCenterPosition.X, topCenterPosition.Y, backgroundTexture.Width, backgroundTexture.Height);

            nameLabel = new(game, "Fighter");
            nameLabel.SetPosition(nameLabelPosition);

            levelLabel = new(game, "Lv. 1");
            levelLabel.SetPosition(levelLabelPosition);

            healthSlider = new(game, 100);
            healthSlider.SetPosition(sliderPositionInStatsMenu);
            healthSlider.SetValue(70);
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

            nameLabel.Draw(gameTime);
            levelLabel.Draw(gameTime);
            healthSlider.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
