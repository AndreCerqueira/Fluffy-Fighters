using FluffyFighters.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace MonoGameMenu
{
    public class Game1 : Game
    {
        // Properties
        private GraphicsDeviceManager graphics;
        private ScreenManager screenManager;

        
        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            screenManager = new ScreenManager();
            Components.Add(screenManager);
        }

        
        protected override void Initialize()
        {
            // Create a new instance of the MainMenuScreen and add it to the ScreenManager
            var mainMenuScreen = new MainMenuScreen(this);
            screenManager.LoadScreen(mainMenuScreen);

            base.Initialize();
        }
        
        
        protected override void Update(GameTime gameTime)
        {
            // Update the ScreenManager
            screenManager.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            // Draw the ScreenManager
            screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}


