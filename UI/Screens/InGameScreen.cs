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
using System.IO;
using TiledCS;

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

        private SpriteBatch spriteBatch;
        private TiledMap map;
        private TiledTileset[] tilesets;
        private Texture2D[] tilesetTextures;


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


        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = new TiledMap(Content.RootDirectory + "\\sprites\\Mapa.tmx");

            int tilesetCount = map.Tilesets.Length;
            tilesets = new TiledTileset[tilesetCount];
            tilesetTextures = new Texture2D[tilesetCount];

            for (int i = 0; i < tilesetCount; i++)
            {
                tilesets[i] = new TiledTileset("Content\\sprites\\tilesets\\" + Path.GetFileName(map.Tilesets[i].source));
                string imagePath = "Content\\sprites\\tilesImages\\" + Path.GetFileName(tilesets[i].Image.source);
                tilesetTextures[i] = Texture2D.FromStream(GraphicsDevice, File.OpenRead(imagePath));
            }

        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            float scaleFactor = 0.75f;
            int fixedTileSize = 64;

            foreach (TiledLayer layer in map.Layers)
            {
                if (!layer.visible)
                    continue;

                for (int y = 0; y < layer.height; y++)
                {
                    for (int x = 0; x < layer.width; x++)
                    {
                        int index = y * layer.width + x;
                        int tileId = layer.data[index];
                        if (tileId == 0)
                            continue;

                        // get tileset index for the tile
                        int tilesetIndex = 0;
                        for (int i = 0; i < map.Tilesets.Length; i++)
                        {
                            if (tileId >= map.Tilesets[i].firstgid)
                                tilesetIndex = i;
                        }
                        if (tilesetIndex == 0)
                            continue;

                        int tilesetTileId = tileId - map.Tilesets[tilesetIndex].firstgid;
                        int tilesetTileX = tilesetTileId % tilesets[tilesetIndex].Columns;
                        int tilesetTileY = tilesetTileId / tilesets[tilesetIndex].Columns;
                        Rectangle sourceRectangle = new Rectangle(tilesetTileX * tilesets[tilesetIndex].TileWidth, tilesetTileY * tilesets[tilesetIndex].TileHeight, tilesets[tilesetIndex].TileWidth, tilesets[tilesetIndex].TileHeight);

                        int scaledTileWidth = (int)(tilesets[tilesetIndex].TileWidth * scaleFactor);
                        int scaledTileHeight = (int)(tilesets[tilesetIndex].TileHeight * scaleFactor);

                        // Calculate the starting position of the destination rectangle based on the object's size
                        int destinationX = (int)(x * fixedTileSize * scaleFactor) - (scaledTileWidth - (int)(fixedTileSize * scaleFactor));
                        int destinationY = (int)(y * fixedTileSize * scaleFactor) - (scaledTileHeight - (int)(fixedTileSize * scaleFactor));

                        Rectangle destinationRectangle = new Rectangle(destinationX, destinationY, scaledTileWidth, scaledTileHeight);

                        spriteBatch.Draw(tilesetTextures[tilesetIndex], destinationRectangle, sourceRectangle, Color.White);
                    }
                }
            }

            spriteBatch.End();

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
