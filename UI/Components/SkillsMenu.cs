using FluffyFighters.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace FluffyFighters.UI.Components
{
    public class SkillsMenu : DrawableGameComponent
    {
        // Constants 
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/skillsMenuBackground";
        private const int PADDING = 10;
        private const int OFFSET_X = 45;
        private const int OFFSET_Y = 20;

        // Properties
        private Rectangle rectangle;
        private Texture2D menuTexture;
        private SkillButton[] skillButtons;
        private int screenWidth => GraphicsDevice.Viewport.Width;
        private int screenHeight => GraphicsDevice.Viewport.Height;


        // Constructors
        public SkillsMenu(Game game) : base(game)
        {
            this.menuTexture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            // Get position at bottom center of screen
            Point position = new(screenWidth / 2 - menuTexture.Width / 2, screenHeight - menuTexture.Height );
            rectangle = new(position.X, position.Y, menuTexture.Width, menuTexture.Height);

            CreateSkillButtons();
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            foreach (var bt in skillButtons)
                bt.Update(gameTime);

            base.Update(gameTime);
        }
        

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();
            spriteBatch.Draw(menuTexture, rectangle, Color.White);
            spriteBatch.End();

            foreach (var bt in skillButtons)
                bt.Draw(gameTime);

            base.Draw(gameTime);
        }


        string[] attacks = new string[] { "Tackle", "Water Pulse", "Ember", "Magical Leaf" };
        Element[] elements = new Element[] { Element.Neutral, Element.Water, Element.Fire, Element.Grass };
        private void CreateSkillButtons()
        {
            skillButtons = new SkillButton[4];
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i] = new SkillButton(Game);
                int x = screenWidth / 2 - menuTexture.Width / 2 + 10 + (skillButtons[i].texture.Width + PADDING) * i + OFFSET_X;
                int y = screenHeight - menuTexture.Height + 10 + OFFSET_Y;
                skillButtons[i].SetAttack(elements[i], attacks[i]);
                skillButtons[i].SetPosition(x, y);
            }
        }


        public bool isHovering => skillButtons.Any(bt => bt.isHovering);
    }
}
