﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml;
using FluffyFighters.Enums;

namespace FluffyFighters.UI.Components
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
        private Point selectedPosition => (combatPosition == CombatPosition.Left) ? topLeftCenterPosition : topRightCenterPosition;
        private Point sliderPosition => new(selectedPosition.X + PADDING * 2, selectedPosition.Y + (backgroundTexture.Height/2));
        private Point sliderPositionInStatsMenu => (combatPosition == CombatPosition.Left) ? selectedPosition + sliderPosition : topLeftCenterPosition + sliderPosition;
        private Point nameLabelPosition => new(selectedPosition.X + LABEL_PADDING, selectedPosition.Y + LABEL_PADDING);
        private Point levelLabelPosition => new(selectedPosition.X + backgroundTexture.Width - LABEL_PADDING * 4, selectedPosition.Y + LABEL_PADDING);

        // Components
        private Label nameLabel;
        private Label levelLabel;
        private Slider healthSlider;


        // Constructors
        public StatsMenu(Game game, CombatPosition combatPosition) : base(game)
        {
            this.combatPosition = combatPosition;
            backgroundTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);
            rectangle = new(selectedPosition.X, selectedPosition.Y, backgroundTexture.Width, backgroundTexture.Height);

            spriteEffect = combatPosition == CombatPosition.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            nameLabel = new(game, "Fighter");
            nameLabel.SetPosition(nameLabelPosition);

            levelLabel = new(game, "Lv. 1");
            levelLabel.SetPosition(levelLabelPosition);

            healthSlider = new(game, 100);
            healthSlider.SetPosition(sliderPositionInStatsMenu);
            healthSlider.SetValue(70);
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

            nameLabel.Draw(gameTime);
            levelLabel.Draw(gameTime);
            healthSlider.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void SetHealth(int value) => healthSlider.SetValue(value);
    }
}