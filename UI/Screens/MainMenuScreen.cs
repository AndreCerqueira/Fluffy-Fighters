using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        private bool isHovering => playButton.isHovering || exitButton.isHovering; //  || settingsButton.isHovering

        // Player starting monsters
        private Attack tacle;
        private Attack waterPulse;
        private Attack ember;
        private Attack magicalLeaf;
        private Monster monster1;
        private Monster monster2;
        private Monster monster3;
        private Team playerTeam;


        public MainMenuScreen(Game game, ScreenManager screenManager) : base(game)
        {
            this.screenManager = screenManager;

            tacle = new Attack("Tackle", Element.Neutral, 10, 80, 100);
            waterPulse = new Attack("Water Pulse", Element.Water, 20, 70, 100);
            ember = new Attack("Ember", Element.Fire, 30, 60, 100);
            magicalLeaf = new Attack("Magical Leaf", Element.Grass, 40, 50, 100);
            monster1 = new Monster("Bolhas", 100, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
            monster2 = new Monster("Fofi", 100, Element.Fire, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
            monster3 = new Monster("Tonco", 100, Element.Grass, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");
            playerTeam = new Team();
            playerTeam.AddMonster(monster3);
            playerTeam.AddMonster(monster1);
            playerTeam.AddMonster(monster2);
            playerTeam.OnLose += OnPlayerTeamLose;
        }
        

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Sounds.LoadSounds(Content);
            SoundEffectInstance instance = Sounds.main.CreateInstance();
            instance.IsLooped = true;
            instance.Play();
            instance.Volume = 0.002f;

            // Load textures
            backgroundTexture = Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            logoTexture = Content.Load<Texture2D>(LOGO_ASSET_PATH);

            // Create buttons
            CreateButtons();

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(Point.Zero, new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)), Color.White);
            spriteBatch.Draw(logoTexture, logoPosition, Color.White);
            spriteBatch.End();

            playButton.Draw(gameTime);
            // settingsButton.Draw(gameTime);
            exitButton.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            playButton.Update(gameTime);
            // settingsButton.Update(gameTime);
            exitButton.Update(gameTime);

            Mouse.SetCursor(isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        private void CreateButtons()
        {
            playButton = new Button(Game, "Play");
            Point position = new(center.X - (playButton.texture.Width / 2), center.Y + (playButton.texture.Height + Button.PADDING) * -1);
            playButton.SetPosition(position);
            playButton.OnClicked += OnPlayButtonClicked;

            // settingsButton = new Button(Game, "Settings");
            // position = new(center.X - (settingsButton.texture.Width / 2), center.Y + 0);
            // settingsButton.SetPosition(position);
            // settingsButton.OnClicked += OnSettingsButtonClicked;

            exitButton = new Button(Game, "Exit");
            position = new(center.X - (exitButton.texture.Width / 2), center.Y + 0);
            // position = new(center.X - (exitButton.texture.Width / 2), center.Y + (exitButton.texture.Height + Button.PADDING) * 1);
            exitButton.SetPosition(position);
            exitButton.OnClicked += OnExitButtonClicked;
        }

        
        private void OnPlayButtonClicked(object sender, EventArgs e)
        {
            InGameScreen inGameScreen = new(Game);
            screenManager.LoadScreen(inGameScreen);
            inGameScreen.map.spawner.OnMonsterClicked += OnMonsterClicked;
            inGameScreen.OnClose += OnInGameScreenClosed;
        }

        
        private void OnPlayerTeamLose(object sender, LoseEventArgs e)
        {
            e.team.HeallAllMonsters();
            screenManager.LoadScreen(this);
        }

        
        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Game.Exit();
        }


        private void OnInGameScreenClosed(object sender, EventArgs e)
        {
            screenManager.LoadScreen(this);
        }

        
        private void OnMonsterClicked(object sender, Monster m)
        {
            Monster monster4 = new Monster("Bolhas", 1, Element.Water, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
            Monster monster5 = new Monster("Fofi", 1, Element.Fire, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
            Monster monster6 = new Monster("Tonco", 1, Element.Grass, new Attack[] { tacle, waterPulse, ember, magicalLeaf }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");
            
            Team enemyTeam = new Team();
            enemyTeam.AddMonster(m);

            screenManager.LoadScreen(new CombatScreen(Game, playerTeam, enemyTeam, (newTeam1) =>
            {
                playerTeam = newTeam1;
                playerTeam.HeallAllMonsters();
                InGameScreen inGameScreen = new(Game);
                screenManager.LoadScreen(inGameScreen);
                inGameScreen.map.spawner.OnMonsterClicked += OnMonsterClicked;
                inGameScreen.OnClose += OnInGameScreenClosed;
            }));
        }
    }
}
