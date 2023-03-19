using FluffyFighters.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;

namespace FluffyFighters.UI.Screens
{
    public class MainMenuScreen : GameScreen
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/background";
        private const string BUTTON_ASSET_PATH = "sprites/ui/button";
        private const string LOGO_ASSET_PATH = "sprites/ui/title";
        private const int BUTTON_PADDING = 20;

        // Properties
        private ScreenManager screenManager;
        private SpriteBatch spriteBatch;
        private Texture2D buttonTexture;
        private Texture2D backgroundTexture;
        private Texture2D logoTexture;
        private Button playButton;
        private Button settingsButton;
        private Button exitButton;
        private SpriteFont font;

        // Positioning
        Point center => new(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
        Point playButtonPosition => new(center.X - (buttonTexture.Width / 2), center.Y + (buttonTexture.Height + BUTTON_PADDING) * -1);
        Point exitButtonPosition => new(center.X - (buttonTexture.Width / 2), center.Y + (buttonTexture.Height + BUTTON_PADDING) * 1);
        Point settingsButtonPosition => new(center.X - (buttonTexture.Width / 2), center.Y + 0);
        Vector2 logoPosition => new(center.X - (logoTexture.Width / 2), center.Y - (logoTexture.Height / 2) - 240);


        public MainMenuScreen(Game game, ScreenManager screenManager) : base(game)
        {
            this.screenManager = screenManager;
        }
        

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            buttonTexture = Content.Load<Texture2D>(BUTTON_ASSET_PATH);
            backgroundTexture = Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            logoTexture = Content.Load<Texture2D>(LOGO_ASSET_PATH);
            font = Content.Load<SpriteFont>("File");

            // Create buttons
            CreatePlayButton();
            CreateSettingsButton();
            CreateExitButton();

            base.LoadContent();
        }
        

        private void CreatePlayButton()
        {
            Label label = new(Game, font, "Play", playButtonPosition);

            playButton = new Button(Game, buttonTexture, playButtonPosition, label);
            playButton.Clicked += OnPlayButtonClicked;
        }


        private void CreateSettingsButton()
        {
            Label label = new(Game, font, "Settings", settingsButtonPosition);

            settingsButton = new Button(Game, buttonTexture, settingsButtonPosition, label);
            settingsButton.Clicked += OnSettingsButtonClicked;
        }

        
        private void CreateExitButton()
        {
            Label label = new(Game, font, "Exit", exitButtonPosition);

            exitButton = new Button(Game, buttonTexture, exitButtonPosition, label);
            exitButton.Clicked += OnExitButtonClicked;
        }

        
        private void OnPlayButtonClicked(object sender, EventArgs e)
        {
            screenManager.LoadScreen(new CombatScreen(Game, screenManager));
        }

        
        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            // Do something when the settings button is clicked
        }

        
        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }

        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(Point.Zero, new Point(1280, 720)), Color.White);
            spriteBatch.Draw(logoTexture, logoPosition, Color.White);
            spriteBatch.End();

            playButton.Draw(gameTime);
            settingsButton.Draw(gameTime);
            exitButton.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            playButton.Update(gameTime);
            settingsButton.Update(gameTime);
            exitButton.Update(gameTime);

            Mouse.SetCursor(isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        private bool isHovering => playButton.isHovering || settingsButton.isHovering || exitButton.isHovering;
    }
}