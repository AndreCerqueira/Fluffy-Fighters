using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using FluffyFighters.UI.Components.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;
using System.Linq;

namespace FluffyFighters.UI.Components.Menus
{
    public class InventoryMenu : DrawableGameComponent
    {
        // Constants
        private const string BACKGROUND_ASSET_PATH = "sprites/ui/inventoryBackground";
        private const string EXIT_BUTTON_ASSET_PATH = "sprites/ui/exitButton";
        private const int BUTTON_PADDING = 20;
        private const int SLOT_WIDTH_QUANTITY = 5;
        private const int SLOT_HEIGHT_QUANTITY = 2;
        private const int SLOT_PADDING_X = 60;
        private const int SLOT_PADDING_Y = 40;
        private const int SLOT_MARGIN_X = 60;
        private const int SLOT_MARGIN_Y = 30;
        private const int SELECTED_SLOT_MARGIN_Y = 60;

        // Properties
        private SpriteBatch spriteBatch;
        public Rectangle rectangle { get; private set; }
        public Texture2D texture { get; private set; }

        private Button exitButton;
        private Point exitButtonPosition => new(rectangle.X + rectangle.Width - exitButton.texture.Width + BUTTON_PADDING, rectangle.Y - BUTTON_PADDING);

        private Slot[,] slots;
        private Slot[] selectedSlots;

        private int screenWidth => GraphicsDevice.Viewport.Width;
        private int screenHeight => GraphicsDevice.Viewport.Height;


        public bool isHovering => exitButton.isHovering || slots.Cast<Slot>().Any(slot => slot.isHovering) || selectedSlots.Any(slot => slot.isHovering);         


        // Constructors
        public InventoryMenu(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            // Get position at center of screen
            Point position = new(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
            rectangle = new(position.X, position.Y, texture.Width, texture.Height);

            CreateSlots(game);

            exitButton = new Button(game, customAssetPath: EXIT_BUTTON_ASSET_PATH);
            exitButton.OnClicked += OnExitButtonClicked;
            exitButton.SetPosition(exitButtonPosition);
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();

            DrawSlots(gameTime);

            exitButton.Draw(gameTime);

            base.Draw(gameTime);
        }


        public void CreateSlots(Game game)
        {
            // Create inventory slots
            slots = new Slot[SLOT_WIDTH_QUANTITY, SLOT_HEIGHT_QUANTITY];
            for (int i = 0; i < SLOT_WIDTH_QUANTITY; i++)
            {
                for (int j = 0; j < SLOT_HEIGHT_QUANTITY; j++)
                {
                    slots[i, j] = new Slot(game);
                    int x = rectangle.X + SLOT_PADDING_X + i * (slots[i, j].texture.Width + SLOT_MARGIN_X);
                    int y = rectangle.Y + SLOT_PADDING_Y + j * (slots[i, j].texture.Height + SLOT_MARGIN_Y);
                    slots[i, j].SetPosition(new Point(x, y));
                }
            }

            // Create Selected Slots
            selectedSlots = new Slot[Team.MAX_MONSTERS];
            for (int i = 0; i < selectedSlots.Length; i++)
            {
                selectedSlots[i] = new Slot(game);
                int x = (screenWidth / 2) - ((selectedSlots.Length * selectedSlots[i].texture.Width + (selectedSlots.Length - 1) * SLOT_MARGIN_X) / 2) + i * (selectedSlots[i].texture.Width + SLOT_MARGIN_X);
                int y = texture.Height + SELECTED_SLOT_MARGIN_Y;
                selectedSlots[i].SetPosition(new Point(x, y));
            }
        }


        public void DrawSlots(GameTime gameTime)
        {
            // Draw inventory slots
            for (int i = 0; i < SLOT_WIDTH_QUANTITY; i++)
            {
                for (int j = 0; j < SLOT_HEIGHT_QUANTITY; j++)
                {
                    slots[i, j].Draw(gameTime);
                }
            }

            // Draw selected slots
            for (int i = 0;i < selectedSlots.Length; i++)
            {
                selectedSlots[i].Draw(gameTime);
            }
        }


        private void OnExitButtonClicked(object sender, EventArgs e)
        {
        }
    }
}
