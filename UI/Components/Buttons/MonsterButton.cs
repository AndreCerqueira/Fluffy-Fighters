using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static FluffyFighters.Others.Monster;
using static FluffyFighters.UI.Components.Buttons.SkillButton;

namespace FluffyFighters.UI.Components.Buttons
{
    public class MonsterButton : DrawableGameComponent
    {
        // Constants
        private const string DEFEATED_ASSET_PATH = "sprites/ui/monster-icons/defeated-icon";
        private const float TEXTURE_SCALE = 0.3f;

        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D texture;
        private Texture2D defeatedTexture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        private bool isBlocked = false;
        public bool isInteractible = true;
        private Rectangle rectangle;
        private Monster monster;

        // Clicked event
        public event MonsterEventHandler OnClicked;


        public bool isHovering
        {
            get
            {
                if (!isInteractible)
                    return false;

                var mouseState = Mouse.GetState();
                var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                return mouseRectangle.Intersects(rectangle);
            }
        }


        public bool isClicked => Mouse.GetState().LeftButton == ButtonState.Pressed;


        // Constructors
        public MonsterButton(Game game, Monster monster, float scale = TEXTURE_SCALE) : base(game)
        {
            this.monster = monster;
            texture = game.Content.Load<Texture2D>(monster.iconAssetPath);
            texture = ResizeTexture(texture, scale);
            defeatedTexture = game.Content.Load<Texture2D>(DEFEATED_ASSET_PATH);

            rectangle = new(0, 0, texture.Width, texture.Height);

            monster.OnDeath += Block;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Block();
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            if (isClicked && isHovering && !isBlocked && !monster.IsDead())
                Clicked();

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, GetColor());

            if (monster.IsDead() && isInteractible)
                spriteBatch.Draw(defeatedTexture, rectangle, defaultColor);

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
            if (!isInteractible)
                return defaultColor;

            if (isBlocked || monster.IsDead())
                return blockedColor;

            return isHovering ? hoverColor : defaultColor;
        }


        public void Clicked() => OnClicked?.Invoke(this, new MonsterEventArgs(monster));


        public void Block(object sender, MonsterEventArgs e) => isBlocked = true;
        public void Block() => isBlocked = true;
        public void Unblock() => isBlocked = false;


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
