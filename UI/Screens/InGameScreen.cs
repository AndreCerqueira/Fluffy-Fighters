using FluffyFighters.Enums;
using FluffyFighters.Others;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework.Input;
using FluffyFighters.UI.Components.Menus;

namespace FluffyFighters.UI.Screens
{
    public class InGameScreen : GameScreen
    {
        // Constants
        private const string INVENTORY_BUTTON_ASSET_PATH = "sprites/ui/invetoryIcon";
        private const string SETTINGS_BUTTON_ASSET_PATH = "sprites/ui/settingsIcon";
        private const int BUTTON_PADDING = 10;
        private const int BUTTON_MARGIN_X = 10;
        private const int BUTTON_MARGIN_Y = 10;

        // Properties
        private Button inventoryButton;
        private Point inventoryButtonPosition => new(GraphicsDevice.Viewport.Width -
            settingsButton.texture.Width - inventoryButton.texture.Width - BUTTON_PADDING - BUTTON_MARGIN_X, BUTTON_MARGIN_Y);

        private Button settingsButton;
        private Point settingsButtonPosition => new(GraphicsDevice.Viewport.Width - settingsButton.texture.Width - BUTTON_MARGIN_X, BUTTON_MARGIN_Y);

        private InventoryMenu inventoryMenu;


        // Constructors
        public InGameScreen(Game game) : base(game)
        {
            inventoryButton = new Button(game, customAssetPath: INVENTORY_BUTTON_ASSET_PATH);
            inventoryButton.OnClicked += OnInventoryButtonClicked;

            settingsButton = new Button(game, customAssetPath: SETTINGS_BUTTON_ASSET_PATH);
            settingsButton.OnClicked += OnSettingsButtonClicked;

            inventoryMenu = new InventoryMenu(game);

            settingsButton.SetPosition(settingsButtonPosition);
            inventoryButton.SetPosition(inventoryButtonPosition);
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void LoadContent()
        {
            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            inventoryButton.Draw(gameTime);
            settingsButton.Draw(gameTime);
            inventoryMenu.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            Mouse.SetCursor(inventoryButton.isHovering || settingsButton.isHovering || inventoryMenu.isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        private void OnInventoryButtonClicked(object sender, EventArgs e)
        {
        }


        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
        }
    }
}
