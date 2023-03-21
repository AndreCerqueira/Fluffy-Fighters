using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FluffyFighters.UI.Components
{
    internal class CombatBroadcast : DrawableGameComponent
    {
        // Constants
        private const string ASSET_PATH = "sprites/ui/Round-Broadcast";
        private const int LABEL_PADDING = 5;


        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture { get; private set; }
        private Rectangle rectangle;
        private Label label;
        private Vector2 labelPosition => new Vector2(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f + LABEL_PADDING) - label.font.MeasureString(label.text) / 2f;


        // Constructors
        public CombatBroadcast(Game game, string text) : base(game)
        {
            texture = game.Content.Load<Texture2D>(ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            label = new(game, text);
            label.SetPosition(labelPosition);
            label.SetColor(Color.White);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            label.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
            label.SetPosition(labelPosition);
        }
    }
}
