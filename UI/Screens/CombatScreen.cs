using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components;
using FluffyFighters.UI.Components.Combat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Screens
{
    public class CombatScreen : GameScreen
    {
        // Constants
        private const int MONSTER_OFFSET_X = 160;
        private const int MONSTER_OFFSET_Y = 100;
        private const int BROADCAST_DELAY = 1500;

        // Properties
        private ScreenManager screenManager;
        private SkillsMenu skillsMenu;

        private CombatBroadcast combatBroadcast; 
        private Point combatBroadcastPosition => new(skillsMenu.rectangle.X + (skillsMenu.rectangle.Width / 2) - (combatBroadcast.texture.Width / 2),
        skillsMenu.rectangle.Y - combatBroadcast.texture.Height);

        private TeamMenu playerTeamMenu;
        private TeamMenu enemyTeamMenu;
        private Monster playerSelectedMonster => playerTeamMenu.team.GetSelectedMonster();
        private Monster enemySelectedMonster => enemyTeamMenu.team.GetSelectedMonster();

        // Constructors
        public CombatScreen(Game game, ScreenManager screenManager, Team playerTeam, Team enemyTeam) : base(game)
        {
            this.screenManager = screenManager;
            this.playerTeamMenu = new TeamMenu(game, playerTeam, CombatPosition.Left);
            this.enemyTeamMenu = new TeamMenu(game, enemyTeam, CombatPosition.Right);
        }


        // Methods
        public override void Initialize()
        {
            // Create components
            skillsMenu = new SkillsMenu(Game, playerTeamMenu.team, enemyTeamMenu.team);

            combatBroadcast = new CombatBroadcast(Game, $"A wild {enemySelectedMonster.name} appears!");
            combatBroadcast.SetPosition(combatBroadcastPosition);
            StartBroadcast();

            skillsMenu.SubscribeSkillClicked(NextTurn);
            skillsMenu.SubscribeSkillClicked(UpdateEnemyHealthBar);


            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            skillsMenu.Update(gameTime);
            playerTeamMenu.Update(gameTime);
            enemyTeamMenu.Update(gameTime);

            Mouse.SetCursor(skillsMenu.isHovering || playerTeamMenu.isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            combatBroadcast.Draw(gameTime);
            skillsMenu.Draw(gameTime);

            playerTeamMenu.Draw(gameTime);
            enemyTeamMenu.Draw(gameTime);
        }


        public void UpdateEnemyHealthBar(object sender, AttackEventArgs e)
        {
            enemyTeamMenu.statsMenu.SetHealth(enemySelectedMonster.currentHealth);
        }


        /*
            A wild ratata appears!
            What will player do?
            Pichu used attackName!
            The wild ratata used ...
            Pichu fainted!
            The wild ratata fainted
            nome gained 30xp
            name grew to level 11.
         */

        public async void NextTurn(object sender, AttackEventArgs e)
        {
            combatBroadcast.SetText($"{playerSelectedMonster.name} used {e.attack.name}");

            await Task.Delay(BROADCAST_DELAY);

            combatBroadcast.SetText($"{enemySelectedMonster.name} was paralized!");

            await Task.Delay(BROADCAST_DELAY);
            combatBroadcast.SetText($"What will {playerSelectedMonster.name} do?");

            skillsMenu.UnblockAllSkillButtons();
        }


        public async void StartBroadcast()
        {
            await Task.Delay(BROADCAST_DELAY);
            combatBroadcast.SetText($"What will {playerSelectedMonster.name} do?");

            skillsMenu.UnblockAllSkillButtons();
        }
    }
}
