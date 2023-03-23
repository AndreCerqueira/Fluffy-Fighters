using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
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
        private const string LOGO_ASSET_PATH = "sprites/ui/title";

        // Properties
        private ScreenManager screenManager;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private Texture2D logoTexture;
        private Button playButton;
        private Button settingsButton;
        private Button exitButton;

        // Positioning
        Point center => new(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
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
            backgroundTexture = Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            logoTexture = Content.Load<Texture2D>(LOGO_ASSET_PATH);

            // Create buttons
            CreateButtons();

            base.LoadContent();
        }
        

        private void CreateButtons()
        {
            playButton = new Button(Game, "Play");
            Point position = new(center.X - (playButton.texture.Width / 2), center.Y + (playButton.texture.Height + Button.PADDING) * -1);
            playButton.SetPosition(position);
            playButton.Clicked += OnPlayButtonClicked;

            settingsButton = new Button(Game, "Settings");
            position = new(center.X - (settingsButton.texture.Width / 2), center.Y + 0);
            settingsButton.SetPosition(position);
            settingsButton.Clicked += OnSettingsButtonClicked;

            exitButton = new Button(Game, "Exit");
            position = new(center.X - (exitButton.texture.Width / 2), center.Y + (exitButton.texture.Height + Button.PADDING) * 1);
            exitButton.SetPosition(position);
            exitButton.Clicked += OnExitButtonClicked;
        }

        
        private void OnPlayButtonClicked(object sender, EventArgs e)
        {
            Attack tacle = new Attack("Tackle", Element.Neutral, 10, 80, 100);
            Attack waterPulse = new Attack("Water Pulse", Element.Water, 20, 70, 100);
            Attack ember = new Attack("Ember", Element.Fire, 30, 60, 100);
            Attack magicalLeaf = new Attack("Magical Leaf", Element.Grass, 40, 50, 100);
            Monster monster1 = new Monster("Bolhas", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
            Monster monster2 = new Monster("Fofi", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
            Monster monster3 = new Monster("Tonco", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");
            Team team1 = new Team();
            team1.AddMonster(monster3);
            team1.AddMonster(monster1);
            team1.AddMonster(monster2);

            Monster monster4 = new Monster("Bolhas", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
            Monster monster5 = new Monster("Fofi", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
            Monster monster6 = new Monster("Tonco", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");
            Team team2 = new Team();
            int random = new Random().Next(4, 7);
            if (random == 4)
                team2.AddMonster(monster4);
            else if (random == 5)
                team2.AddMonster(monster5);
            else if (random == 6)
                team2.AddMonster(monster6);

            screenManager.LoadScreen(new CombatScreen(Game, screenManager, team1, team2));
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
            spriteBatch.Draw(backgroundTexture, new Rectangle(Point.Zero, new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)), Color.White);
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