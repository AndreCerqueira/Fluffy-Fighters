using FluffyFighters.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;

namespace FluffyFighters.UI.Screens
{
    public class CombatScreen : GameScreen
    {
        // Properties
        private ScreenManager screenManager;
        private SkillsMenu skillsMenu;
        private StatsMenu playerStatsMenu;
        

        // Constructors
        public CombatScreen(Game game, ScreenManager screenManager) : base(game)
        {
            this.screenManager = screenManager;
        }


        // Methods
        public override void LoadContent()
        {

            // Create components
            skillsMenu = new SkillsMenu(Game);
            playerStatsMenu = new StatsMenu(Game);

            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            skillsMenu.Update(gameTime);
            playerStatsMenu.Update(gameTime); 

            Mouse.SetCursor(skillsMenu.isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            skillsMenu.Draw(gameTime);
            playerStatsMenu.Draw(gameTime);
        }
    }
}
