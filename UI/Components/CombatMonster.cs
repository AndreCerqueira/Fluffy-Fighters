using FluffyFighters.Enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FluffyFighters.UI.Components
{
    public class CombatMonster : DrawableGameComponent
    {
        // Constants
        private const string ASSET_PATH = "sprites/monsters/Bolhinha";

        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture;
        private SpriteEffects spriteEffect;
        private Rectangle rectangle;


        // Constructors
        public CombatMonster(Game game, CombatPosition combatPosition) : base(game)
        {
            texture = game.Content.Load<Texture2D>(ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);
            spriteEffect = combatPosition == CombatPosition.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, Vector2.Zero, spriteEffect, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
        }
    }
}
