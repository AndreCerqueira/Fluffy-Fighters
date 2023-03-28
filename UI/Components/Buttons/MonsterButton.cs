using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static FluffyFighters.Others.Monster;

namespace FluffyFighters.UI.Components.Buttons
{
    public class MonsterButton : DrawableGameComponent
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/monster-icons/default-icon";
        private const string DEFEATED_ASSET_PATH = "sprites/ui/monster-icons/defeated-icon";
        private const float TEXTURE_SCALE = 0.3f;

        // Properties
        private SpriteBatch spriteBatch;
        public Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;
        public Texture2D texture;
        private Texture2D defeatedTexture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        private bool isBlocked = false;
        public bool isInteractible = true;
        private Rectangle rectangle;
        public Monster monster;

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

            if (scale != 1)
                texture = Utils.ResizeTexture(texture, scale);

            defeatedTexture = game.Content.Load<Texture2D>(DEFEATED_ASSET_PATH);
            backgroundTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            if (scale != 1)
                backgroundTexture = Utils.ResizeTexture(backgroundTexture, scale);

            rectangle = new(0, 0, texture.Width, texture.Height);
            backgroundRectangle = new(0, 0, backgroundTexture.Width, backgroundTexture.Height);

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
            spriteBatch.Draw(backgroundTexture, rectangle, GetColor());
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
    }
}
