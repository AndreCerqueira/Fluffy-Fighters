using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FluffyFighters.UI.Components
{
    public class Label : DrawableGameComponent
    {
        // Constants
        private const string FONT_ASSET_PATH = "File";
        
        // Properties
        private SpriteBatch spriteBatch;
        public SpriteFont font;
        public string text;
        public Vector2 position;
        private Vector2 offset;
        private Color color;
        private float scale;


        // Constructors
        public Label(Game game, string text, SpriteFont font = null) : base(game)
        {
            this.font = font ?? game.Content.Load<SpriteFont>(FONT_ASSET_PATH);
            this.text = text;
            this.color = Color.Black;
            this.offset = Vector2.Zero;
        }


        // Methods
        public Vector2 GetPosition() => position + offset;
        public Vector2 SetPosition(Vector2 position) => this.position = position;
        public Vector2 SetPosition(Point position) => this.position = new Vector2(position.X, position.Y);
        public void SetColor(Color color) => this.color = color;
        public void SetOffset(Vector2 offset) => this.offset = offset;
        public void SetScale(float scale) => this.scale = scale;
        public void SetText(string text) => this.text = text;


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, GetPosition(), color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
