using FluffyFighters.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Components
{
    public class ElementIcon : DrawableGameComponent
    {
        // Properties
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Rectangle rectangle;
        private Element element;


        // Constructors
        public ElementIcon(Game game, Element element) : base(game)
        {
            this.element = element;
            texture = game.Content.Load<Texture2D>(element.GetAssetPath());
            rectangle = new(0, 0, texture.Width, texture.Height);
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

        
        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
        }
    }
}
