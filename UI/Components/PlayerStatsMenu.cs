using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Components
{
    public class PlayerStatsMenu : DrawableGameComponent
    {
        // Properties
        private Rectangle rectangle;
        private Texture2D texture;


        // Constructors
        public PlayerStatsMenu(Game game, Texture2D texture) : base(game)
        {
            this.texture = texture;

            // Get position at left top of screen
            int padding = 10;
            Point position = new(padding, padding);

            rectangle = new(position.X, position.Y, texture.Width, texture.Height);
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
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
