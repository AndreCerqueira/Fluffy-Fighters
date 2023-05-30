using FluffyFighters.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace FluffyFighters.Others
{
    public class MonsterSpawner
    {
        private const int MONSTER_QUANTITY = 20;

        private Game game;
        private Map map;
        private List<MapMonster> monsters;


        public MonsterSpawner(Game game, Map map)
        {
            this.game = game;
            this.map = map;
            monsters = new List<MapMonster>();

            for (int i = 0; i < MONSTER_QUANTITY; i++)
            {
                MapMonster monster = GetRandomMonster();
                monster.position = GetRandomSpawnPosition();
                monsters.Add(monster);
            }

        }


        public void Update(GameTime gameTime)
        {
            foreach (MapMonster monster in monsters)
            {
                monster.Update(gameTime);
            }
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (MapMonster monster in monsters)
            {
                monster.Draw(spriteBatch);
            }
        }


        // Get Random Spawn Position
        public Vector2 GetRandomSpawnPosition()
        {
            Random random = new Random();
            Vector2 spawnPosition = new Vector2();

            spawnPosition.X = random.Next(0, (int)Math.Round(map.map.Width * Map.FIXED_TILE_SIZE * Map.GAME_SCALE_FACTOR));
            spawnPosition.Y = random.Next(0, (int)Math.Round(map.map.Height * Map.FIXED_TILE_SIZE * Map.GAME_SCALE_FACTOR));

            return spawnPosition;
        }


        public MapMonster GetRandomMonster()
        {
            Random random = new Random();
            int monsterId = random.Next(0, 3);

            switch (monsterId)
            {
                case 0:
                    return new MapMonster(game, map, "sprites/monsters/fofi_spritesheet");
                case 1:
                    return new MapMonster(game, map, "sprites/monsters/bolhas_spritesheet");
                default:
                    return new MapMonster(game, map, "sprites/monsters/toco_spritesheet");
            }

        }
    }
}
