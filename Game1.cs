using FluffyFighters.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace MonoGameMenu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private ScreenManager _screenManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

        protected override void Initialize()
        {
            // Create a new instance of the MainMenuScreen and add it to the ScreenManager
            var mainMenuScreen = new MainMenuScreen(this);
            _screenManager.LoadScreen(mainMenuScreen);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // Update the ScreenManager
            _screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Draw the ScreenManager
            _screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}


