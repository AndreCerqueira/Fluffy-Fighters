using FluffyFighters.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FluffyFighters.UI.Components.Others
{
    public class ElementIcon : DrawableGameComponent
    {
        // Properties
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Rectangle rectangle;


        // Constructors
        public ElementIcon(Game game, Element element) : base(game)
        {
            texture = game.Content.Load<Texture2D>(element.GetAssetPath());
            rectangle = new(0, 0, texture.Width, texture.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        // Methods
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
            rectangle.Y = position.Y;
        }
    }
}
