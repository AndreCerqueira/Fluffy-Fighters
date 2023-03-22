using FluffyFighters.Enums;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FluffyFighters.Others;
using MonoGame.Extended.Content;

namespace FluffyFighters.UI.Components.Combat
{
    public class MonsterDisplayer : DrawableGameComponent
    {
        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture;
        private SpriteEffects spriteEffect;
        private Rectangle rectangle;


        // Constructors
        public MonsterDisplayer(Game game, CombatPosition combatPosition, Monster monster) : base(game)
        {
            texture = game.Content.Load<Texture2D>(monster.assetPath);
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
