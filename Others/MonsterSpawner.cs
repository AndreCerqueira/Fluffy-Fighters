using FluffyFighters.Characters;
using FluffyFighters.Enums;
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
        public List<MapMonster> monsters;

        private EventHandler<Monster> onMonsterClicked;
        public EventHandler<Monster> OnMonsterClicked
        {
            get { return onMonsterClicked; }
            set
            {
                onMonsterClicked = value;
                foreach (MapMonster monster in monsters)
                {
                    monster.OnClicked += onMonsterClicked;
                }
            }
        }


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
            int level = GetRandomLevel();
            Monster monster;
            MapMonster mapMonster;

            switch (monsterId)
            {
                case 0:
                    monster = new Monster("Fofi", GetRandomHealthByLevel(level), Element.Fire, GetRandomAttacksByElement(Element.Fire, level), "sprites/monsters/Fofi", "sprites/ui/monster-icons/fofi-icon", level);
                    mapMonster = new MapMonster(game, map, "sprites/monsters/fofi_spritesheet", monster);
                    break;
                case 1:
                    monster = new Monster("Bolhas", GetRandomHealthByLevel(level), Element.Water, GetRandomAttacksByElement(Element.Water, level), "sprites/monsters/Bolhas", "sprites/ui/monster-icons/bolhas-icon", level);
                    mapMonster = new MapMonster(game, map, "sprites/monsters/bolhas_spritesheet", monster);
                    break;
                default:
                    monster = new Monster("Tonco", GetRandomHealthByLevel(level), Element.Grass, GetRandomAttacksByElement(Element.Grass, level), "sprites/monsters/Tonco", "sprites/ui/monster-icons/tonco-icon", level);
                    mapMonster = new MapMonster(game, map, "sprites/monsters/toco_spritesheet", monster);
                    break;
            }

            return mapMonster;
        }


        // get random level 1-10
        private int GetRandomLevel()
        {
            Random random = new Random();
            return random.Next(1, 6);
        }


        // get random attacks by element
        private Attack[] GetRandomAttacksByElement(Element element, int level)
        {
            Attack[] attacks = new Attack[4];
            for (int i = 0; i < 4; i++)
                attacks[i] = Attack.GetRandomAttack(element, level);
            return attacks;
        }


        // get random health by level
        private int GetRandomHealthByLevel(int level)
        {
            Random random = new Random();
            return random.Next(10, 100) * level;
        }

    }
}
