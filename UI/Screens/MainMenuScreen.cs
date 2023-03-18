using FluffyFighters.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Screens
{
    public class MainMenuScreen : GameScreen
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/background";
        private const string BUTTON_ASSET_PATH = "sprites/ui/button";
        private const int BUTTON_PADDING = 20;

        // Properties
        private SpriteBatch spriteBatch;
        private Texture2D buttonTexture;
        private Texture2D backgroundTexture;
        private Button playButton;
        private Button settingsButton;
        private Button exitButton;

        Point center => new(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);


        public MainMenuScreen(Game game) : base(game)
        {
        }
        

        public override void Initialize()
        {
            // Get center coordinates of the screen
            Point buttonSize = new Point(192, 64);
            Point playButtonPosition = center - new Point(buttonSize.X / 2, (buttonSize.Y + BUTTON_PADDING) * 1);
            Point settingsButtonPosition = center - new Point(buttonSize.X / 2, 0);
            Point exitButtonPosition = center - new Point(buttonSize.X / 2, (buttonSize.Y + BUTTON_PADDING) * -1);

            Label playLabel = new(Game, Content.Load<SpriteFont>("File"), "Play", playButtonPosition + new Point(0, 10));

            // Create buttons
            playButton = new Button(Game, buttonTexture, playButtonPosition, playLabel);
            playButton.Clicked += OnPlayButtonClicked;

            settingsButton = new Button(Game, buttonTexture, settingsButtonPosition);
            settingsButton.Clicked += OnSettingsButtonClicked;

            exitButton = new Button(Game, buttonTexture, exitButtonPosition);
            exitButton.Clicked += OnExitButtonClicked;

            base.Initialize();
        }


        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load textures
            buttonTexture = Content.Load<Texture2D>(BUTTON_ASSET_PATH);
            backgroundTexture = Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            base.LoadContent();
        }


        private void OnPlayButtonClicked(object sender, EventArgs e)
        {
            // Do something when the play button is clicked
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
            // spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
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
        }
    }
}