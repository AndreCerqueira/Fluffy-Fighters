using FluffyFighters.Enums;
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
        private const int MONSTER_OFFSET_X = 160;
        private const int MONSTER_OFFSET_Y = 200;

        // Properties
        private ScreenManager screenManager;
        private SkillsMenu skillsMenu;

        private CombatBroadcast combatBroadcast; 
        private Point combatBroadcastPosition => new(skillsMenu.rectangle.X + (skillsMenu.rectangle.Width / 2) - (combatBroadcast.texture.Width / 2),
        skillsMenu.rectangle.Y - combatBroadcast.texture.Height);


        private StatsMenu monsterLeftStatsMenu;
        private CombatMonster combatMonsterLeft;
        private Point monsterLeftPosition => new(MONSTER_OFFSET_X, MONSTER_OFFSET_Y);

        private StatsMenu monsterRightStatsMenu;
        private CombatMonster combatMonsterRight;
        private Point monsterRightPosition => new(GraphicsDevice.Viewport.Width - combatMonsterRight.texture.Width - MONSTER_OFFSET_X, MONSTER_OFFSET_Y);

        // Constructors
        public CombatScreen(Game game, ScreenManager screenManager) : base(game)
        {
            this.screenManager = screenManager;
        }


        // Methods
        public override void Initialize()
        {
            // Create components
            skillsMenu = new SkillsMenu(Game);

            combatBroadcast = new CombatBroadcast(Game, "The game has started!");
            combatBroadcast.SetPosition(combatBroadcastPosition);

            monsterLeftStatsMenu = new StatsMenu(Game, CombatPosition.Left);
            combatMonsterLeft = new CombatMonster(Game, CombatPosition.Left);
            combatMonsterLeft.SetPosition(monsterLeftPosition);

            monsterRightStatsMenu = new StatsMenu(Game, CombatPosition.Right);
            combatMonsterRight = new CombatMonster(Game, CombatPosition.Right);
            combatMonsterRight.SetPosition(monsterRightPosition);
            monsterRightStatsMenu.SetHealth(50);

            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            skillsMenu.Update(gameTime);

            monsterLeftStatsMenu.Update(gameTime); 
            monsterRightStatsMenu.Update(gameTime);

            Mouse.SetCursor(skillsMenu.isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            combatBroadcast.Draw(gameTime);
            skillsMenu.Draw(gameTime);

            monsterLeftStatsMenu.Draw(gameTime);
            monsterRightStatsMenu.Draw(gameTime);

            combatMonsterLeft.Draw(gameTime);
            combatMonsterRight.Draw(gameTime);
        }
    }
}
