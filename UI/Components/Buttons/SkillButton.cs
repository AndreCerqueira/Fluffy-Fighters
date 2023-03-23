using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FluffyFighters.Others;
using FluffyFighters.Args;
using FluffyFighters.UI.Components.Others;

namespace FluffyFighters.UI.Components.Buttons
{
    public class SkillButton : DrawableGameComponent
    {
        // Delegates
        public delegate void AttackEventHandler(object sender, AttackEventArgs e);

        // Constants
        private const string ASSET_PATH = "sprites/ui/skillButton1";

        // Properties
        private SpriteBatch spriteBatch;
        public Rectangle rectangle;
        public Texture2D texture;
        private Color defaultColor = Color.White;
        private Color hoverColor = Color.LightGray;
        private Color blockedColor = Color.DarkGray;
        private bool isBlocked = false;
        public Attack attack;
        private Vector2 labelPosition => new Vector2(rectangle.X + rectangle.Width / 2f, rectangle.Y + rectangle.Height / 2f) - label.font.MeasureString(label.text) / 2f;

        // Components
        private Label label;
        private ElementIcon elementIcon;

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
        public SkillButton(Game game) : base(game)
        {
            texture = game.Content.Load<Texture2D>(ASSET_PATH);
            rectangle = new(0, 0, texture.Width, texture.Height);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Block();
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
            var color = GetColor();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, color);
            spriteBatch.End();

            label?.Draw(gameTime);
            elementIcon?.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void Block() => isBlocked = true;
        public void Unblock() => isBlocked = false;


        private Color GetColor()
        {
            if (isBlocked)
                return blockedColor;

            return isHovering ? hoverColor : defaultColor;
        }


        public void OnClicked() => Clicked?.Invoke(this, new AttackEventArgs(attack));


        public void SetAttack(Attack attack)
        {
            this.attack = attack;
            label = new Label(Game, attack.name);
            elementIcon = new ElementIcon(Game, attack.element);
            label?.SetPosition(labelPosition);
            elementIcon?.SetPosition(new Point(rectangle.X + 12, rectangle.Y + 16));
        }


        public void SetPosition(int x, int y)
        {
            rectangle.X = x;
            rectangle.Y = y;
            label?.SetPosition(labelPosition);
            elementIcon?.SetPosition(new Point(x + 12, y + 16));
        }


        public Point GetPosition() => new Point(rectangle.X, rectangle.Y);
    }
}