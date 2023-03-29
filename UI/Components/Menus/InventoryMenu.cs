using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Buttons;
using FluffyFighters.UI.Components.Others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
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
        private bool isVisible = false;

        private Button exitButton;
        private Point exitButtonPosition => new(rectangle.X + rectangle.Width - exitButton.texture.Width + BUTTON_PADDING, rectangle.Y - BUTTON_PADDING);

        private Slot[,] slots;
        private Slot[] teamSlots;
        private List<Slot> allSlots => slots.Cast<Slot>().Concat(teamSlots).ToList();
        private Slot previousSelected;

        private int screenWidth => GraphicsDevice.Viewport.Width;
        private int screenHeight => GraphicsDevice.Viewport.Height;


        public bool isHovering => isVisible && (exitButton.isHovering || slots.Cast<Slot>().Any(slot => slot.isHovering) || teamSlots.Any(slot => slot.isHovering));         


        // Constructors
        public InventoryMenu(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = game.Content.Load<Texture2D>(BACKGROUND_ASSET_PATH);

            // Get position at center of screen
            Point position = new(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);
            rectangle = new(position.X, position.Y, texture.Width, texture.Height);

            CreateSlots(game);

            Monster monster4 = new Monster("Bolhas", 1, Element.Water, new Attack[] { }, "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon");
            Monster monster5 = new Monster("Fofi", 1, Element.Fire, new Attack[] { }, "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon");
            Monster monster6 = new Monster("Tonco", 1, Element.Grass, new Attack[] { }, "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon");

            slots[0, 0].SetContent(monster4);
            slots[0, 1].SetContent(monster5);
            teamSlots[0].SetContent(monster6);

            exitButton = new Button(game, customAssetPath: EXIT_BUTTON_ASSET_PATH);
            exitButton.OnClicked += OnExitButtonClicked;
            exitButton.SetPosition(exitButtonPosition);
        }


        // Methods
        public override void Update(GameTime gameTime)
        {
            if (!isVisible) return;

            allSlots.ForEach(slot => slot.Update(gameTime));
            exitButton.Update(gameTime);

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            if (!isVisible) return;

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
                    slots[i, j].OnClicked += SelectSlot;
                }
            }

            // Create Selected Slots
            teamSlots = new Slot[Team.MAX_MONSTERS];
            for (int i = 0; i < teamSlots.Length; i++)
            {
                teamSlots[i] = new Slot(game);
                int x = (screenWidth / 2) - ((teamSlots.Length * teamSlots[i].texture.Width + (teamSlots.Length - 1) * SLOT_MARGIN_X) / 2) + i * (teamSlots[i].texture.Width + SLOT_MARGIN_X);
                int y = texture.Height + SELECTED_SLOT_MARGIN_Y;
                teamSlots[i].SetPosition(new Point(x, y));
                teamSlots[i].OnClicked += SelectSlot;
            }
        }


        public void DrawSlots(GameTime gameTime)
        {
            allSlots.ForEach(slot => slot.Draw(gameTime));
        }


        public void DeselectAllSlots()
        {
            allSlots.ForEach(slot => slot.Deselect());
        }


        public void SelectSlot(object sender, Slot e)
        {
            if (previousSelected == null)
            {
                e.Select();
                previousSelected = e;
            }
            else
            {
                // change content between slots
                Monster previousContent = previousSelected.GetContent();
                Monster newContent = e.GetContent();
                previousSelected.SetContent(newContent);
                e.SetContent(previousContent);

                previousSelected.Deselect();
                previousSelected = null;
            }
        }


        private void OnExitButtonClicked(object sender, EventArgs e) => Hide();


        public void Show() => isVisible = true;
        public void Hide() => isVisible = false;

    }
}
