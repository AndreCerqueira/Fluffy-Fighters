using FluffyFighters.Args;
using FluffyFighters.Enums;
using System;

namespace FluffyFighters.Others
{
    public class Monster
    {
        // Delegates
        public delegate void MonsterEventHandler(object sender, MonsterEventArgs e);

        // Constants
        public const string DEFAULT_ICON_ASSET_PATH = "sprites/ui/monster-icons/default-icon";

        // Properties
        public string assetPath { get; private set; }
        public string iconAssetPath { get; private set; }
        public string name { get; private set; }
        public int currentHealth { get; private set; }
        public int maxHealth { get; private set; }
        public int level { get; private set; }
        public int maxXp { get; private set; }
        public int xp { get; private set; }
        public Element element { get; }
        public Attack[] attacks { get; private set; }

        // Events
        public event MonsterEventHandler OnDeath;


        // Constructors
        public Monster(string name, int maxHealth, Element element, Attack[] attacks, string assetPath, string iconAssetPath = null, int level = 1)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.element = element;
            this.attacks = attacks;
            this.assetPath = assetPath;
            this.level = level;
            this.iconAssetPath = iconAssetPath ?? DEFAULT_ICON_ASSET_PATH;
        }


        // Methods
        public void TakeDamage(float damage)
        {
            currentHealth -= (int)Math.Round(damage);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
        }


        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }


        public void GainXp(int amount)
        {
            xp += amount;
            if (xp >= 100)
            {
                level++;
                xp -= 100;
            }
        }


        public Attack GetRandomAttack()
        {
            return attacks[new Random().Next(attacks.Length)];
        }


        public void Death() => OnDeath?.Invoke(this, new MonsterEventArgs(this));


        public bool IsDead() => currentHealth <= 0;
    }
}
