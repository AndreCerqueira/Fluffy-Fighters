using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using FluffyFighters.UI.Components.Menus;
using FluffyFighters.UI.Components.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Screens
{
    public class CombatScreen : GameScreen
    {
        // Constants
        private const int BROADCAST_DELAY = 1500;

        // Properties
        private ScreenManager screenManager;
        private SkillsMenu skillsMenu;

        private CombatBroadcast combatBroadcast; 
        private Point combatBroadcastPosition => new(skillsMenu.rectangle.X + (skillsMenu.rectangle.Width / 2) - (combatBroadcast.texture.Width / 2),
        skillsMenu.rectangle.Y - combatBroadcast.texture.Height);

        private TeamMenu playerTeamMenu;
        private TeamMenu enemyTeamMenu;
        private Team playerTeam;
        private Team enemyTeam;
        private Monster playerSelectedMonster => playerTeamMenu.team.GetSelectedMonster();
        private Monster enemySelectedMonster => enemyTeamMenu.team.GetSelectedMonster();

        // Constructors
        public CombatScreen(Game game, ScreenManager screenManager, Team playerTeam, Team enemyTeam) : base(game)
        {
            this.screenManager = screenManager;
            this.playerTeam = playerTeam;
            this.enemyTeam = enemyTeam;
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

            skillsMenu.SubscribeSkillClicked(SelectAttackOrder);


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


        public void UpdateHealthBar(Team team)
        {
            TeamMenu teamMenu = (team == playerTeamMenu.team) ? playerTeamMenu : enemyTeamMenu;
            teamMenu.statsMenu.SetHealth(team.GetSelectedMonster().currentHealth);
        }


        public async void SelectAttackOrder(object sender, AttackEventArgs e)
        {
            Attack playerAttack = e.attack;
            Attack enemyAttack = SelectEnemyAttack();

            if (playerSelectedMonster.IsDead() || enemySelectedMonster.IsDead())
                return;

            // get whose is faster
            bool playerAttackedFirst = false;
            if (playerAttack.speed > enemyAttack.speed)
                playerAttackedFirst = true;
            else if (playerAttack.speed == enemyAttack.speed)
                playerAttackedFirst = (new Random().Next(0, 2) == 0);


            Monster firstAttacker = playerAttackedFirst ? playerSelectedMonster : enemySelectedMonster;
            Monster secondAttacker = playerAttackedFirst ? enemySelectedMonster : playerSelectedMonster;

            // Get teams
            Team firstTeam = playerAttackedFirst ? playerTeam : enemyTeam;
            Team secondTeam = playerAttackedFirst ? enemyTeam : playerTeam;

            // Get attacks
            Attack firstAttack = playerAttackedFirst ? playerAttack : enemyAttack;
            Attack secondAttack = playerAttackedFirst ? enemyAttack : playerAttack;

            // Perform attacks
            PerformAttack(firstAttack, firstAttacker, secondTeam);
            await Task.Delay(BROADCAST_DELAY);
            PerformAttack(secondAttack, secondAttacker, firstTeam);

            // End turn
            await Task.Delay(BROADCAST_DELAY);
            combatBroadcast.SetText($"What will {playerSelectedMonster.name} do?");
            skillsMenu.UnblockAllSkillButtons();
        }


        public Attack SelectEnemyAttack() => enemySelectedMonster.GetRandomAttack();


        public void PerformAttack(Attack attack, Monster attacker, Team target)
        {
            if (attacker.IsDead()) return;

            combatBroadcast.SetText($"{attacker.name} used {attack.name}!");
            target.GetSelectedMonster().TakeDamage(attack.damage);

            UpdateHealthBar(target);
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
            //await Task.Delay(BROADCAST_DELAY);
            //combatBroadcast.SetText($"What will {playerSelectedMonster.name} do?");

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
