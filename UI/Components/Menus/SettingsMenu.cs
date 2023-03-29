using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FluffyFighters.UI.Components.Menus
{
    public class SettingsMenu : DrawableGameComponent
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/settingsBackground";
        private const int BUTTON_MARGIN_Y = 40;

        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture { get; private set; }
        private Rectangle rectangle;
        private bool isVisible = false;

        public Button exitButton;
        private Point exitButtonPosition => new Point(rectangle.X + rectangle.Width / 2 - exitButton.texture.Width / 2,
                                              rectangle.Bottom - exitButton.texture.Height - BUTTON_MARGIN_Y);

        public Button continueButton;
        private Point continueButtonPosition => new Point(rectangle.X + rectangle.Width / 2 - continueButton.texture.Width / 2,
                                                  rectangle.Top + BUTTON_MARGIN_Y);

        private int screenWidth => GraphicsDevice.Viewport.Width;
        private int screenHeight => GraphicsDevice.Viewport.Height;

        public bool isHovering => isVisible && (exitButton.isHovering || continueButton.isHovering);

        // Constructors
        public SettingsMenu(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            // Get position at center of screen
            Point position = new(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
            rectangle = new(position.X, position.Y, texture.Width, texture.Height);

            // Create button
            exitButton = new Button(game, "Exit");
            exitButton.SetPosition(exitButtonPosition);
            exitButton.OnClicked += OnExitButtonClicked;

            continueButton = new Button(game, "Continue");
            continueButton.SetPosition(continueButtonPosition);
            continueButton.OnClicked += OnContinueButtonClicked;
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            if (!isVisible) return;

            exitButton.Update(gameTime);
            continueButton.Update(gameTime);

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if (!isVisible) return;

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            exitButton.Draw(gameTime);
            continueButton.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void Show() => isVisible = true;
        public void Hide() => isVisible = false;


        // Events
        private void OnExitButtonClicked(object sender, EventArgs e) => Hide();
        private void OnContinueButtonClicked(object sender, EventArgs e) => Hide();
    }
}
