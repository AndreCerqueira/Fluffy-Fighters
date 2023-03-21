using FluffyFighters.Enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Components
{
    internal class SkillDescriptor : DrawableGameComponent
    {
        // Constants
        private const string ASSET_PATH = "sprites/ui/Skill-Description";


        // Properties
        private SpriteBatch spriteBatch;
        private Point position;
        private Rectangle rectangle;
        public Texture2D texture { get; private set; }
        private float timeElapsed = 0f;
        private float yOffset = 0f;
        private const float Y_OFFSET_AMOUNT = 5f;


        // Constructors
        public SkillDescriptor(Game game) : base(game)
        {
            texture = game.Content.Load<Texture2D>(ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            base.Draw(gameTime);
        }


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y + (int)yOffset;
        }
    }
}
