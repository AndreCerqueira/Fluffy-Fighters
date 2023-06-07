using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using FluffyFighters.UI.Components.Menus;
using FluffyFighters.UI.Components.Others;
using FluffyFighters.UI.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Screens
{
    public class CombatScreen : GameScreen
    {
        // Properties
        private SkillsMenu skillsMenu;

        private CaptureButton captureButton;
        private Point captureButtonPosition => new(skillsMenu.rectangle.X + skillsMenu.rectangle.Width + CaptureButton.PADDING, 
                       skillsMenu.rectangle.Y + (skillsMenu.rectangle.Height / 2));

        private LevelUpMenu levelUpMenu;
        private Point levelUpMenuPosition => new(skillsMenu.rectangle.X + (skillsMenu.rectangle.Width / 2) - (levelUpMenu.texture.Width / 2), 
            GraphicsDevice.Viewport.Height / 3 - levelUpMenu.texture.Height);

        private CombatBroadcast combatBroadcast;
        private Point combatBroadcastPosition => new(skillsMenu.rectangle.X + (skillsMenu.rectangle.Width / 2) - (combatBroadcast.texture.Width / 2),
        skillsMenu.rectangle.Y - combatBroadcast.texture.Height);

        private Action<Team> onClose;
        private TeamMenu playerTeamMenu;
        private TeamMenu enemyTeamMenu;
        private CombatManager cm;
        private BroadcastManager bm;

        // Constructors
        public CombatScreen(Game game, Team playerTeam, Team enemyTeam, Action<Team> onClose) : base(game)
        {
            playerTeamMenu = new TeamMenu(game, playerTeam, CombatPosition.Left);
            enemyTeamMenu = new TeamMenu(game, enemyTeam, CombatPosition.Right);

            cm = new CombatManager(playerTeam, enemyTeam);

            skillsMenu = new SkillsMenu(Game, playerTeamMenu.team, enemyTeamMenu.team);

            captureButton = new CaptureButton(Game);
            captureButton.SetPosition(captureButtonPosition);

            levelUpMenu = new LevelUpMenu(Game, cm.playerTeam, cm.enemyTeam);
            levelUpMenu.SetPosition(levelUpMenuPosition);

            combatBroadcast = new CombatBroadcast(Game, $"A wild {cm.enemySelectedMonster.name} appears!");
            combatBroadcast.SetPosition(combatBroadcastPosition);
            bm = new BroadcastManager(combatBroadcast);

            this.onClose = onClose;
        }


        // Methods
        public override void Initialize()
        {
            UnblockAllButtons();

            skillsMenu.SubscribeSkillClicked(cm.DoTurn);

            playerTeamMenu.SubscribeSelectMonster(cm.SelectMonster);
            playerTeamMenu.SubscribeSelectMonster(BlockAllButtons);

            cm.onMonsterSelected += BroadcastMonsterJoined;
            cm.onMonsterSelected += UnblockAllButtons;

            cm.enemyTeam.OnLose += levelUpMenu.Show;
            cm.enemyTeam.OnLose += BlockAllButtons;

            cm.onTurnStart += BlockAllButtons;
            cm.onTurnStart += BroadcastStartTurn;
            cm.onTurnEnd += UnblockAllButtons;

            cm.onAttackPerformed += UpdateHealthBar;
            cm.onAttackPerformed += BroadcastMonsterAttacked;
            cm.onAttackFailed += BroadcastMonsterMissed;

            levelUpMenu.onMonstersLeveledUp += Close;

            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            skillsMenu.Update(gameTime);
            playerTeamMenu.Update(gameTime);
            enemyTeamMenu.Update(gameTime);
            levelUpMenu.Update(gameTime);

            Mouse.SetCursor(skillsMenu.isHovering || playerTeamMenu.isHovering || levelUpMenu.nextButton.isHovering || captureButton.isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSeaGreen);

            combatBroadcast.Draw(gameTime);
            skillsMenu.Draw(gameTime);
            playerTeamMenu.Draw(gameTime);
            enemyTeamMenu.Draw(gameTime);
            levelUpMenu.Draw(gameTime);
            captureButton.Draw(gameTime);
        }


        public void UpdateHealthBar(object sender, AttackPerformedEventArgs e)
        {
            TeamMenu teamMenu = (e.target == playerTeamMenu.team) ? playerTeamMenu : enemyTeamMenu;
            teamMenu.statsMenu.SetHealth(e.target.GetSelectedMonster().currentHealth);
        }


        #region Broadcasts

        public async void BroadcastStartTurn(object sender, EventArgs e)
        {
            await bm.Display($"What will {playerTeamMenu.team.GetSelectedMonster().name} do?");
            UnblockAllButtons();
        }


        public async void BroadcastMonsterJoined(object sender, MonsterEventArgs e)
        {
            await bm.Display($"{e.monster.name} joined the battle!");
            await Task.Delay(1000);
            await bm.Display($"What will {e.monster.name} do?");
        }


        public async void BroadcastMonsterAttacked(object sender, AttackPerformedEventArgs e) => await bm.Display($"{e.attacker.name} used {e.attack.name}");


        public async void BroadcastMonsterMissed(object sender, EventArgs e) => await bm.Display($"{enemyTeamMenu.team.GetSelectedMonster().name}'s attack missed!");

        #endregion


        public void BlockAllButtons(object sender, EventArgs e) => BlockAllButtons();
        public async void UnblockAllButtons(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            UnblockAllButtons();
        }


        public void BlockAllButtons()
        {
            skillsMenu.BlockAllSkillButtons();
            playerTeamMenu.BlockAllMonsterButtons();
        }


        public void UnblockAllButtons()
        {
            skillsMenu.UnblockAllSkillButtons();
            playerTeamMenu.UnblockAllMonsterButtons();
        }


        private async void Close(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            onClose?.Invoke(cm.playerTeam);
        }
    }
}
