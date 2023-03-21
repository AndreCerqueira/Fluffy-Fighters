using FluffyFighters.Enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluffyFighters.Others;

namespace FluffyFighters.UI.Components
{
    internal class SkillDescriptor : DrawableGameComponent
    {
        // Constants
        private const string ASSET_PATH = "sprites/ui/Skill-Description";
        private const int DESCRIPTOR_OFFSET_Y = -10;
        private const int LABEL_OFFSET_Y = -10;


        // Properties
        private SpriteBatch spriteBatch;
        private Point position;
        private Rectangle rectangle;
        public Texture2D texture { get; private set; }
        private Label label;
        private Vector2 labelPosition => new Vector2(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f + LABEL_OFFSET_Y) - label.font.MeasureString(label.text) / 2f;
        private float timeElapsed = 0f;
        private float yOffset = 0f;
        private const float Y_OFFSET_AMOUNT = 5f;


        // Constructors
        public SkillDescriptor(Game game) : base(game)
        {
            texture = game.Content.Load<Texture2D>(ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            label = new Label(game, "Skill Description");
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > 1f)
            {
                yOffset = yOffset == 0 ? Y_OFFSET_AMOUNT : 0;
                timeElapsed = 0f;
            }

            rectangle.X = position.X;
            rectangle.Y = position.Y + (int)yOffset;


            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            label?.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void SetPosition(SkillButton skillButton)
        {
            position = new Point(skillButton.rectangle.X + (skillButton.rectangle.Width / 2) - (texture.Width / 2),
                skillButton.rectangle.Y - texture.Height + DESCRIPTOR_OFFSET_Y);

            label.text = $"Damage: {skillButton.attack.damage} \n Speed: {skillButton.attack.speed} \n Chance: {skillButton.attack.missChance}%";
            rectangle.X = position.X;
            rectangle.Y = position.Y + (int)yOffset;
            label?.SetPosition(labelPosition);
        }
    }
}
