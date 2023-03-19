using FluffyFighters.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace FluffyFighters.UI.Screens
{
    public class CombatScreen : GameScreen
    {
        // Constants
        private const string SKILLS_MENU_BACKGROUND_ASSET_PATH = "sprites/ui/skillsMenuBackground";
        private const string PLAYER_STATS_MENU_BACKGROUND_ASSET_PATH = "sprites/ui/playerStatsBackground";
        private const string SKILL_BUTTON_1_ASSET_PATH = "sprites/ui/skillButton1";
        private const string SKILL_BUTTON_2_ASSET_PATH = "sprites/ui/skillButton2";

        // Properties
        private ScreenManager screenManager;
        private Texture2D skillsMenuBackgroundTexture;
        private Texture2D playerStatsMenuBackgroundTexture;
        private Texture2D skillButton1Texture;
        private Texture2D skillButton2Texture;
        private SkillsMenu skillsMenu;
        private PlayerStatsMenu playerStatsMenu;
        private SkillButton skillButton1;
        private SkillButton skillButton2;
        private SkillButton skillButton3;
        private SkillButton skillButton4;
        

        // Constructors
        public CombatScreen(Game game, ScreenManager screenManager) : base(game)
        {
            this.screenManager = screenManager;
        }


        // Methods
        public override void LoadContent()
        {
            // Load textures
            skillsMenuBackgroundTexture = Content.Load<Texture2D>(SKILLS_MENU_BACKGROUND_ASSET_PATH);
            playerStatsMenuBackgroundTexture = Content.Load<Texture2D>(PLAYER_STATS_MENU_BACKGROUND_ASSET_PATH);
            skillButton1Texture = Content.Load<Texture2D>(SKILL_BUTTON_1_ASSET_PATH);
            skillButton2Texture = Content.Load<Texture2D>(SKILL_BUTTON_2_ASSET_PATH);

            /*
             
            Point PlayerStatsMenuPosition = new Point(
                GraphicsDevice.Viewport.Width / 2 - texture.Width / 2,
                GraphicsDevice.Viewport.Height - texture.Height
            );
             */

            // Get skill button positions from skills menu, position 1 is the top left corner of the skills menu
            // place buttons horizontally
            Point skillButton1Position = new Point(
                GraphicsDevice.Viewport.Width / 2 - skillsMenuBackgroundTexture.Width / 2 + 10,
                GraphicsDevice.Viewport.Height - skillsMenuBackgroundTexture.Height + 10
            );
            Point skillButton2Position = new Point(
                GraphicsDevice.Viewport.Width / 2 - skillsMenuBackgroundTexture.Width / 2 + 10 + skillButton1Texture.Width + 10,
                GraphicsDevice.Viewport.Height - skillsMenuBackgroundTexture.Height + 10
            );
            Point skillButton3Position = new Point(
                GraphicsDevice.Viewport.Width / 2 - skillsMenuBackgroundTexture.Width / 2 + 10 + skillButton1Texture.Width + 10 + skillButton2Texture.Width + 10,
                GraphicsDevice.Viewport.Height - skillsMenuBackgroundTexture.Height + 10
            );
            Point skillButton4Position = new Point(
                GraphicsDevice.Viewport.Width / 2 - skillsMenuBackgroundTexture.Width / 2 + 10 + skillButton1Texture.Width + 10 + skillButton2Texture.Width + 10 + skillButton1Texture.Width + 10,
                GraphicsDevice.Viewport.Height - skillsMenuBackgroundTexture.Height + 10
            );


            // Create components
            skillsMenu = new SkillsMenu(Game, skillsMenuBackgroundTexture);
            playerStatsMenu = new PlayerStatsMenu(Game, playerStatsMenuBackgroundTexture);
            skillButton1 = new SkillButton(Game, skillButton1Texture, skillButton1Position);
            skillButton2 = new SkillButton(Game, skillButton1Texture, skillButton2Position);
            skillButton3 = new SkillButton(Game, skillButton1Texture, skillButton3Position);
            skillButton4 = new SkillButton(Game, skillButton2Texture, skillButton4Position);

            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            skillsMenu.Update(gameTime);
            playerStatsMenu.Update(gameTime);
            skillButton1.Update(gameTime);
            skillButton2.Update(gameTime);
            skillButton3.Update(gameTime);
            skillButton4.Update(gameTime);

            Mouse.SetCursor(Button.defaultCursor); // TEMP
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            skillsMenu.Draw(gameTime);
            playerStatsMenu.Draw(gameTime);
            skillButton1.Draw(gameTime);
            skillButton2.Draw(gameTime);
            skillButton3.Draw(gameTime);
            skillButton4.Draw(gameTime);
        }
    }
}
