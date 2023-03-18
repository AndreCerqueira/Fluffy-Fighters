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
        public Vector2 position;
        public Color color = Color.White;
        public Vector2 origin;
        public float scale = 1f;
        public SpriteEffects spriteEffects = SpriteEffects.None;
        public float rotation = 0f;
        public float layerDepth = 0f;


        // Constructors
        public Label(Game game, SpriteFont font, string text, Point position) : base(game)
        {
            this.font = font;
            this.text = text;
            this.position = new Vector2(position.X, position.Y);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, spriteEffects, layerDepth);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // public void SetPosition(int x, int y) => this.position = new Vector2(x, y);
    }
}
