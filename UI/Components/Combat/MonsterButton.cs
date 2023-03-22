using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FluffyFighters.UI.Components.Combat.SkillButton;

namespace FluffyFighters.UI.Components.Combat
{
    public class MonsterButton : DrawableGameComponent
    {
        // Constants
        private const float TEXTURE_SCALE = 0.3f;

        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        private bool isBlocked = false;
        private Rectangle rectangle;

        // Clicked event
        public event AttackEventHandler Clicked;


        public bool isHovering
        {
            get
            {
                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                return mouseRectangle.Intersects(rectangle);
            }
        }


        public bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;


        // Constructors
        public MonsterButton(Game game, CombatPosition combatPosition, Monster monster) : base(game)
        {
            texture = game.Content.Load<Texture2D>(monster.iconAssetPath);
            texture = ResizeTexture(texture, TEXTURE_SCALE);
            rectangle = new(0, 0, texture.Width, texture.Height);

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            if (isClicked && isHovering && !isBlocked)
                OnClicked();

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, GetColor());
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void SetPosition(Point position)
        {
            rectangle.X = position.X;
            rectangle.Y = position.Y;
        }


        private Color GetColor()
        {
            if (isBlocked)
                return blockedColor;

            return isHovering ? hoverColor : defaultColor;
        }


        public void OnClicked() => Clicked?.Invoke(this, new AttackEventArgs(null));


        private static Texture2D ResizeTexture(Texture2D texture, float scaleFactor)
        {
            int newWidth = (int)(texture.Width * scaleFactor);
            int newHeight = (int)(texture.Height * scaleFactor);

            // Create a new texture with the desired dimensions
            Texture2D resizedTexture = new Texture2D(texture.GraphicsDevice, newWidth, newHeight);

            // Scale the original texture onto the new texture
            Color[] textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            Color[] resizedData = new Color[newWidth * newHeight];
            for (int y = 0; y < newHeight; y++)
            {
                for (int x = 0; x < newWidth; x++)
                {
                    int index = (int)(y / scaleFactor) * texture.Width + (int)(x / scaleFactor);
                    resizedData[y * newWidth + x] = textureData[index];
                }
            }

            resizedTexture.SetData(resizedData);

            return resizedTexture;
        }
    }
}
