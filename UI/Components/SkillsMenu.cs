using FluffyFighters.Enums;
using FluffyFighters.Others;
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
        public Rectangle rectangle { get; private set; }
        public Texture2D texture { get; private set; }
        private SkillButton[] skillButtons;
        private SkillDescriptor skillDescriptor;
        private int screenWidth => GraphicsDevice.Viewport.Width;
        private int screenHeight => GraphicsDevice.Viewport.Height;


        // Constructors
        public SkillsMenu(Game game, Monster monster) : base(game)
        {
            this.texture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            // Get position at bottom center of screen
            Point position = new(screenWidth / 2 - texture.Width / 2, screenHeight - texture.Height );
            rectangle = new(position.X, position.Y, texture.Width, texture.Height);

            skillDescriptor = new SkillDescriptor(game);

            CreateSkillButtons(monster);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            foreach (var bt in skillButtons)
                bt.Update(gameTime);

            skillDescriptor.Update(gameTime);

            base.Update(gameTime);
        }
        

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            foreach (var bt in skillButtons)
                bt.Draw(gameTime);

            SkillButton hoveredButton = GetHoveredButton();
            if (hoveredButton != null)
            {
                skillDescriptor.Draw(gameTime);
                skillDescriptor.SetPosition(hoveredButton);
            }

            base.Draw(gameTime);
        }


        private void CreateSkillButtons(Monster monster)
        {
            skillButtons = new SkillButton[monster.attacks.Length];
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i] = new SkillButton(Game);
                int x = screenWidth / 2 - texture.Width / 2 + 10 + (skillButtons[i].texture.Width + PADDING) * i + OFFSET_X;
                int y = screenHeight - texture.Height + 10 + OFFSET_Y;
                skillButtons[i].SetAttack(monster.attacks[i]);
                skillButtons[i].SetPosition(x, y);
            }
        }


        public bool isHovering => skillButtons.Any(bt => bt.isHovering);


        public SkillButton GetHoveredButton()
        {
            foreach (var bt in skillButtons)
                if (bt.isHovering)
                    return bt;

            return null;
        }
    }
}
