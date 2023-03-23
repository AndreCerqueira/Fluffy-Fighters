using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;
using FluffyFighters.Enums;
using FluffyFighters.Others;

namespace FluffyFighters.UI.Components.Menus
{
    public class StatsMenu : DrawableGameComponent
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/playerStatsBackground";
        private const int PADDING = 10;
        private const int LABEL_PADDING = 15;

        // Properties
        private CombatPosition combatPosition;
        private Rectangle rectangle;
        private Texture2D backgroundTexture;
        private SpriteEffects spriteEffect;

        // Positions
        private Point topLeftCenterPosition => new(PADDING, PADDING);
        private Point topRightCenterPosition => new(Game.Window.ClientBounds.Width - backgroundTexture.Width - PADDING, PADDING);
        private Point selectedPosition => combatPosition == CombatPosition.Left ? topLeftCenterPosition : topRightCenterPosition;
        private Point sliderPosition => new(selectedPosition.X + PADDING * 2, selectedPosition.Y + backgroundTexture.Height / 2);
        private Point sliderPositionInStatsMenu => combatPosition == CombatPosition.Left ? selectedPosition + sliderPosition : topLeftCenterPosition + sliderPosition;
        private Point nameLabelPosition => new(selectedPosition.X + LABEL_PADDING, selectedPosition.Y + LABEL_PADDING);
        private Point levelLabelPosition => new(selectedPosition.X + backgroundTexture.Width - LABEL_PADDING * 4, selectedPosition.Y + LABEL_PADDING);

        // Components
        private Monster monster;
        private Label nameLabel;
        private Label levelLabel;
        private Slider healthSlider;


        // Constructors
        public StatsMenu(Game game, CombatPosition combatPosition, Monster monster) : base(game)
        {
            this.monster = monster;
            this.combatPosition = combatPosition;
            backgroundTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            rectangle = new(selectedPosition.X, selectedPosition.Y, backgroundTexture.Width, backgroundTexture.Height);

            spriteEffect = combatPosition == CombatPosition.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            nameLabel = new(game, monster.name);
            nameLabel.SetPosition(nameLabelPosition);

            levelLabel = new(game, $"Lv. {monster.level}");
            levelLabel.SetPosition(levelLabelPosition);

            healthSlider = new(game, monster.maxHealth);
            healthSlider.SetPosition(sliderPositionInStatsMenu);
            healthSlider.SetValue(monster.currentHealth);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, rectangle, null, Color.White, 0f, Vector2.Zero, spriteEffect, 0f);
            spriteBatch.End();

            nameLabel.SetText($"{monster.name} ({monster.currentHealth}/{monster.maxHealth})");

            nameLabel.Draw(gameTime);
            levelLabel.Draw(gameTime);
            healthSlider.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void SetHealth(int value) => healthSlider.SetValue(value);


        public void UpdateMonster(Monster monster)
        {
            this.monster = monster;
            healthSlider.SetMaxValue(monster.maxHealth);
            healthSlider.SetValue(monster.currentHealth, false);
        }
    }
}
