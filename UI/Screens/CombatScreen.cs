using FluffyFighters.Enums;
using FluffyFighters.Others;
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
        private const int MONSTER_OFFSET_Y = 100;

        // Properties
        private ScreenManager screenManager;
        private SkillsMenu skillsMenu;

        private CombatBroadcast combatBroadcast; 
        private Point combatBroadcastPosition => new(skillsMenu.rectangle.X + (skillsMenu.rectangle.Width / 2) - (combatBroadcast.texture.Width / 2),
        skillsMenu.rectangle.Y - combatBroadcast.texture.Height);

        private Monster leftMonster;
        private StatsMenu monsterLeftStatsMenu;
        private MonsterDisplayer combatMonsterLeft;
        private Point monsterLeftPosition => new(MONSTER_OFFSET_X, MONSTER_OFFSET_Y);

        private Monster rightMonster;
        private StatsMenu monsterRightStatsMenu;
        private MonsterDisplayer combatMonsterRight;
        private Point monsterRightPosition => new(GraphicsDevice.Viewport.Width - combatMonsterRight.texture.Width - MONSTER_OFFSET_X, MONSTER_OFFSET_Y);

        // Constructors
        public CombatScreen(Game game, ScreenManager screenManager, Monster leftMonster, Monster rightMonster) : base(game)
        {
            this.screenManager = screenManager;
            this.leftMonster = leftMonster;
            this.rightMonster = rightMonster;
        }


        // Methods
        public override void Initialize()
        {
            // Create components
            skillsMenu = new SkillsMenu(Game, leftMonster);

            combatBroadcast = new CombatBroadcast(Game, "The game has started!");
            combatBroadcast.SetPosition(combatBroadcastPosition);

            monsterLeftStatsMenu = new StatsMenu(Game, CombatPosition.Left, leftMonster);
            combatMonsterLeft = new MonsterDisplayer(Game, CombatPosition.Left, leftMonster);
            combatMonsterLeft.SetPosition(monsterLeftPosition);

            monsterRightStatsMenu = new StatsMenu(Game, CombatPosition.Right, rightMonster);
            combatMonsterRight = new MonsterDisplayer(Game, CombatPosition.Right, rightMonster);
            combatMonsterRight.SetPosition(monsterRightPosition);

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
