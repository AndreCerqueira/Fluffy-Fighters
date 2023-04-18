﻿using FluffyFighters.Enums;
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
        private SettingsMenu settingsMenu;
        private bool isPaused = false;

        public event EventHandler OnClose;


        // Constructors
        public InGameScreen(Game game) : base(game)
        {
            inventoryButton = new Button(game, customAssetPath: INVENTORY_BUTTON_ASSET_PATH);
            inventoryButton.OnClicked += OnInventoryButtonClicked;

            settingsButton = new Button(game, customAssetPath: SETTINGS_BUTTON_ASSET_PATH);
            settingsButton.OnClicked += OnSettingsButtonClicked;

            inventoryMenu = new InventoryMenu(game);
            settingsMenu = new SettingsMenu(game);
            settingsMenu.continueButton.OnClicked += OnContinueButtonClicked;
            settingsMenu.exitButton.OnClicked += OnExitButtonClicked;

            settingsButton.SetPosition(settingsButtonPosition);
            inventoryButton.SetPosition(inventoryButtonPosition);
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            inventoryButton.Draw(gameTime);
            settingsButton.Draw(gameTime);
            inventoryMenu.Draw(gameTime);
            settingsMenu.Draw(gameTime);
        }


        public override void Update(GameTime gameTime)
        {
            settingsMenu.Update(gameTime);
            inventoryMenu.Update(gameTime);
            inventoryButton.Update(gameTime);
            settingsButton.Update(gameTime);

            Mouse.SetCursor(inventoryButton.isHovering || settingsButton.isHovering || inventoryMenu.isHovering || settingsMenu.isHovering ? Button.hoverCursor : Button.defaultCursor);
        }


        private void OnInventoryButtonClicked(object sender, EventArgs e)
        {
            if (isPaused) return;

            inventoryMenu.Show();
        }


        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            settingsMenu.Show();
            inventoryMenu.Hide();
            isPaused = true;
        }


        private void OnContinueButtonClicked(object sender, EventArgs e) => isPaused = false;


        private void OnExitButtonClicked(object sender, EventArgs e) => OnClose?.Invoke(this, EventArgs.Empty);
        
    }
}