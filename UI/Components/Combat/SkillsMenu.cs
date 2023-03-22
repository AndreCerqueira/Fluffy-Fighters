using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using static FluffyFighters.UI.Components.Combat.SkillButton;

namespace FluffyFighters.UI.Components.Combat
{
    public class SkillsMenu : DrawableGameComponent
    {
        // Constants 
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/skillsMenuBackground";
        private const int PADDING = 10;
        private const int OFFSET_X = 45;
        private const int OFFSET_Y = 20;

        // Properties
        public Rectangle rectangle { get; private set; }
        public Texture2D texture { get; private set; }
        private SkillButton[] skillButtons;
        private SkillDescriptor skillDescriptor;
        private Team playerTeam, enemyTeam;
        private Monster playerMonster => playerTeam.GetSelectedMonster();
        private Monster enemyMonster => enemyTeam.GetSelectedMonster();

        private int screenWidth => GraphicsDevice.Viewport.Width;
        private int screenHeight => GraphicsDevice.Viewport.Height;


        // Constructors
        public SkillsMenu(Game game, Team playerTeam, Team enemyTeam) : base(game)
        {
            texture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            this.playerTeam = playerTeam;
            this.enemyTeam = enemyTeam;

            // Get position at bottom center of screen
            Point position = new(screenWidth / 2 - texture.Width / 2, screenHeight - texture.Height);
            rectangle = new(position.X, position.Y, texture.Width, texture.Height);

            skillDescriptor = new SkillDescriptor(game);

            CreateSkillButtons();
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            foreach (var bt in skillButtons)
                bt.Update(gameTime);

            skillDescriptor.Update(gameTime);

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            foreach (var bt in skillButtons)
                bt.Draw(gameTime);

            SkillButton hoveredButton = GetHoveredButton();
            if (hoveredButton != null)
            {
                skillDescriptor.Draw(gameTime);
                skillDescriptor.SetPosition(hoveredButton);
            }

            base.Draw(gameTime);
        }


        private void CreateSkillButtons()
        {
            skillButtons = new SkillButton[playerMonster.attacks.Length];
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i] = new SkillButton(Game);

                int x = screenWidth / 2 - texture.Width / 2 + 10 + (skillButtons[i].texture.Width + PADDING) * i + OFFSET_X;
                int y = screenHeight - texture.Height + 10 + OFFSET_Y;

                skillButtons[i].SetAttack(playerMonster.attacks[i]);
                skillButtons[i].SetPosition(x, y);

                skillButtons[i].Clicked += enemyMonster.TakeDamage;
                skillButtons[i].Clicked += BlockAllSkillButtons;
            }
        }


        public void SubscribeSkillClicked(AttackEventHandler attackEvent)
        {
            foreach (var bt in skillButtons)
                bt.Clicked += attackEvent;
        }


        public bool isHovering => skillButtons.Any(bt => bt.isHovering);


        public SkillButton GetHoveredButton()
        {
            foreach (var bt in skillButtons)
                if (bt.isHovering)
                    return bt;

            return null;
        }


        public void BlockAllSkillButtons(object sender, AttackEventArgs e)
        {
            foreach (var item in skillButtons)
                item.Block(sender, e);
        }


        public void UnblockAllSkillButtons()
        {
            foreach (var item in skillButtons)
                item.Unblock();
        }
    }
}
