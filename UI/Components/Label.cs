using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Components
{
    public class Label : DrawableGameComponent
    {
        // Properties
        private SpriteBatch spriteBatch;
        public SpriteFont font;
        public string text;
        private Vector2 position;
        private Vector2 offset;
        private Color color;
        private float scale;


        // Constructors
        public Label(Game game, SpriteFont font, string text, Point position, Color color) : base(game)
        {
            this.font = font;
            this.text = text;
            this.color = color;
            this.position = new Vector2(position.X, position.Y);
            this.offset = Vector2.Zero;
        }


        public Label(Game game, SpriteFont font, string text, Point position) : base(game)
        {
            this.font = font;
            this.text = text;
            this.color = Color.Black;
            this.position = new Vector2(position.X, position.Y);
            this.offset = Vector2.Zero;
        }


        // Methods
        public Vector2 GetPosition() => position + offset;
        public Vector2 SetPosition(Vector2 position) => this.position = position;
        public void SetOffset(Vector2 offset) => this.offset = offset;
        public void SetScale(float scale) => this.scale = scale;


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
