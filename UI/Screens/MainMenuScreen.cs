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
        // Properties
        private Texture2D _playButtonTexture;
        private Texture2D _settingsButtonTexture;
        private Texture2D _exitButtonTexture;
        private Button _playButton;
        private Button _settingsButton;
        private Button _exitButton;

        // background
        private Texture2D _backgroundTexture;


        public MainMenuScreen(Game game) : base(game)
        {
        }
        

        public override void Initialize()
        {
            _playButton = new Button(Game, _playButtonTexture, new Rectangle(200, 200, 200, 50));
            _playButton.Clicked += OnPlayButtonClicked;

            _settingsButton = new Button(Game, _settingsButtonTexture, new Rectangle(200, 300, 200, 50));
            _settingsButton.Clicked += OnSettingsButtonClicked;

            _exitButton = new Button(Game, _exitButtonTexture, new Rectangle(200, 400, 200, 50));
            _exitButton.Clicked += OnExitButtonClicked;

            base.Initialize();
        }

        //protected override void LoadContent()
        //{
        //    _playButtonTexture = Game.Content.Load<Texture2D>("UI/Buttons/playButton");
        //    _settingsButtonTexture = Game.Content.Load<Texture2D>("UI/Buttons/settingsButton");
        //    _exitButtonTexture = Game.Content.Load<Texture2D>("UI/Buttons/exitButton");

        //    _backgroundTexture = Game.Content.Load<Texture2D>("UI/Backgrounds/mainMenuBackground");

        //    base.LoadContent();
        //}


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

            _playButton.Draw(gameTime);
            _settingsButton.Draw(gameTime);
            _exitButton.Draw(gameTime);

            // Draw background

            var spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            // spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
            spriteBatch.End();
        }


        public override void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime);
            _settingsButton.Update(gameTime);
            _exitButton.Update(gameTime);
        }
    }
}