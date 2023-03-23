using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using FluffyFighters.UI.Components.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System.Linq;

namespace FluffyFighters.UI.Components.Menus
{
    public class TeamMenu : GameScreen
    {
        // Constants
        private const int MONSTER_BUTTON_OFFSET_X = 10;
        private const int MONSTER_OFFSET_X = 160;
        private const int MONSTER_OFFSET_Y = 100;
        private const int MONSTER_BUTTON_PADDING_Y = 10;

        // Properties
        public Team team;
        private Monster selectedMonster => team.GetSelectedMonster();
        public StatsMenu statsMenu;
        private CombatPosition combatPosition;
        private MonsterDisplayer monsterDisplayer;
        private MonsterButton[] monsterButtons;
        private Point monsterLeftPosition => new(MONSTER_OFFSET_X, MONSTER_OFFSET_Y);
        private Point monsterRightPosition => new(GraphicsDevice.Viewport.Width - monsterDisplayer.texture.Width - MONSTER_OFFSET_X, MONSTER_OFFSET_Y);
        private Point monsterButtonPosition => new(combatPosition == CombatPosition.Left ? MONSTER_BUTTON_OFFSET_X :
            GraphicsDevice.Viewport.Width - monsterButtons[0].texture.Width - MONSTER_BUTTON_OFFSET_X, MONSTER_OFFSET_Y);


        // Constructors
        public TeamMenu(Game game, Team team, CombatPosition combatPosition) : base(game)
        {
            this.team = team;
            this.combatPosition = combatPosition;

            statsMenu = new StatsMenu(Game, combatPosition, selectedMonster);
            monsterDisplayer = new MonsterDisplayer(Game, combatPosition, selectedMonster);
            monsterDisplayer.SetPosition(combatPosition == CombatPosition.Left ? monsterLeftPosition : monsterRightPosition);

            Monster[] teamMonsters = team.GetMonsters().ToArray();
            monsterButtons = new MonsterButton[teamMonsters.Length];
            for (int i = 0; i < teamMonsters.Length; i++)
            {
                monsterButtons[i] = new MonsterButton(Game, teamMonsters[i]);
                monsterButtons[i].SetPosition(new Point(monsterButtonPosition.X, monsterButtonPosition.Y + (monsterButtons[i].texture.Height + MONSTER_BUTTON_PADDING_Y) * i));
                monsterButtons[i].OnClicked += SelectMonster;
            }
        }


        public override void Update(GameTime gameTime)
        {
            statsMenu.Update(gameTime);

            foreach (MonsterButton monsterButton in monsterButtons)
                monsterButton.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            statsMenu.Draw(gameTime);
            monsterDisplayer.Draw(gameTime);

            foreach (MonsterButton monsterButton in monsterButtons)
                monsterButton.Draw(gameTime);
        }


        public void SelectMonster(object sender, MonsterEventArgs e)
        {
            team.SelectMonster(e.monster);
            statsMenu.UpdateMonster(e.monster);
            monsterDisplayer.UpdateMonster(e.monster);
            monsterDisplayer.SetPosition(combatPosition == CombatPosition.Left ? monsterLeftPosition : monsterRightPosition);
        }


        public bool isHovering => monsterButtons.Any(mb => mb.isHovering);
    }
}
