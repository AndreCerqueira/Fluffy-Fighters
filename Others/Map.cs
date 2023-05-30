using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledCS;
using FluffyFighters.Characters;
using MonoGame.Extended;

namespace FluffyFighters.Others
{
    public class Map
    {
        // Constants
        public const float GAME_SCALE_FACTOR = 0.75f;
        public const int FIXED_TILE_SIZE = 64;
        private readonly Vector2 PLAYER_START_POSITION = new(12, 6);

        // Properties
        private SpriteBatch spriteBatch;
        public TiledMap map;
        public TiledTileset[] tilesets;
        private Texture2D[] tilesetTextures;
        private string[] layersOverPlayer; 
        public Vector2 Offset { get; set; }

        private Player player;
        private MonsterSpawner spawner;


        public Map(Game game, Player player, string mapPath)
        {
            layersOverPlayer = new string[] { "trees", "pilares", "pedras", "bushes", "water" };

            this.player = player;
            player.position = PLAYER_START_POSITION * FIXED_TILE_SIZE * GAME_SCALE_FACTOR;

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            map = new TiledMap(game.Content.RootDirectory + mapPath);
            spawner = new MonsterSpawner(game, this);

            int tilesetCount = map.Tilesets.Length;
            tilesets = new TiledTileset[tilesetCount];
            tilesetTextures = new Texture2D[tilesetCount];

            for (int i = 0; i < tilesetCount; i++)
            {
                tilesets[i] = new TiledTileset("Content\\sprites\\tilesets\\" + Path.GetFileName(map.Tilesets[i].source));
                string imagePath = "Content\\sprites\\tilesImages\\" + Path.GetFileName(tilesets[i].Image.source);
                tilesetTextures[i] = Texture2D.FromStream(game.GraphicsDevice, File.OpenRead(imagePath));
            }
        }


        public void Update(Vector2 screenPosition, GameTime gameTime)
        {
            Vector2 previousOffset = Offset;

            player.Update(gameTime);

            spawner.Update(gameTime);

            if (CheckCollision(screenPosition, player.GetCollider()))
            {
                Offset = previousOffset;
            }
        }


        public void Draw(Vector2 screenPosition, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            // Draw layers below player
            foreach (TiledLayer layer in map.Layers)
            {
                if (!layer.visible || layersOverPlayer.Contains(layer.name))
                    continue;

                DrawLayer(screenPosition, layer, true);
            }

            // DrawRectangles(spriteBatch, GetCollisionRectangles(screenPosition), Color.Red);
            // player.DrawCollider(spriteBatch);

            player.Draw(spriteBatch);

            spawner.Draw(spriteBatch, gameTime);

            // Draw layers over player
            foreach (TiledLayer layer in map.Layers)
            {
                if (!layer.visible || !layersOverPlayer.Contains(layer.name))
                    continue;

                DrawLayer(screenPosition, layer, false);
            }

            spriteBatch.End();
        }


        private void DrawLayer(Vector2 screenPosition, TiledLayer layer, bool isBackground)
        {
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

                    int scaledTileWidth = (int)(tilesets[tilesetIndex].TileWidth * GAME_SCALE_FACTOR);
                    int scaledTileHeight = (int)(tilesets[tilesetIndex].TileHeight * GAME_SCALE_FACTOR);

                    // Calculate the starting position of the destination rectangle based on the object's size
                    int destinationX = (int)(x * FIXED_TILE_SIZE * GAME_SCALE_FACTOR) - (scaledTileWidth - (int)(FIXED_TILE_SIZE * GAME_SCALE_FACTOR)) - (int)Offset.X;
                    int destinationY = (int)(y * FIXED_TILE_SIZE * GAME_SCALE_FACTOR) - (scaledTileHeight - (int)(FIXED_TILE_SIZE * GAME_SCALE_FACTOR)) - (int)Offset.Y;

                    // Adjust destination based on the screen position
                    destinationX -= (int)screenPosition.X;
                    destinationY -= (int)screenPosition.Y;

                    Rectangle destinationRectangle = new Rectangle(destinationX, destinationY, scaledTileWidth, scaledTileHeight);

                    spriteBatch.Draw(tilesetTextures[tilesetIndex], destinationRectangle, sourceRectangle, Color.White);

                    float layerDepth = isBackground ? 0f : GetLayerDepth(destinationY + tilesetTextures[tilesetIndex].Height);
                    spriteBatch.Draw(tilesetTextures[tilesetIndex], destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
                }
            }
        }


        private List<Rectangle> GetCollisionRectangles(Vector2 screenPosition)
        {
            List<Rectangle> collisionRectangles = new List<Rectangle>();

            TiledLayer collisionLayer = map.Layers.First(l => l.name == "Colisions");

            if (collisionLayer != null)
            {
                foreach (TiledObject obj in collisionLayer.objects)
                {
                    Rectangle rect = new Rectangle(
                        (int)(obj.x * GAME_SCALE_FACTOR - screenPosition.X - (int)Offset.X),
                        (int)(obj.y * GAME_SCALE_FACTOR - screenPosition.Y - (int)Offset.Y),
                        (int)(obj.width * GAME_SCALE_FACTOR),
                        (int)(obj.height * GAME_SCALE_FACTOR)
                    );

                    collisionRectangles.Add(rect);
                }
            }

            return collisionRectangles;
        }


        public bool CheckCollision(Vector2 screenPosition, Rectangle rectangle)
        {
            List<Rectangle> collisionRectangles = GetCollisionRectangles(screenPosition);

            foreach (Rectangle collisionRectangle in collisionRectangles)
            {
                if (collisionRectangle.Intersects(rectangle))
                {
                    return true;
                }
            }

            return false;
        }


        public void DrawRectangles(SpriteBatch spriteBatch, List<Rectangle> rectangles, Color color)
        {
            foreach (Rectangle rectangle in rectangles)
            {
                Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                texture.SetData(new Color[] { color });
                spriteBatch.Draw(texture, rectangle, color);
            }
        }


        public float GetLayerDepth(float y)
        {
            var result = MathHelper.Clamp(y / (float)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height, 0.01f, 1f);
            return result;
        }
    }
}
