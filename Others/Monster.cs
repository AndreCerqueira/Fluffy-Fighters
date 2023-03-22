using FluffyFighters.Args;
using FluffyFighters.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Others
{
    public class Monster
    {
        // Constants
        public const string DEFAULT_ICON_ASSET_PATH = "sprites/ui/monster-icons/default-icon";

        // Properties
        public string assetPath { get; private set; }
        public string iconAssetPath { get; private set; }
        public string name { get; private set; }
        public int currentHealth { get; private set; }
        public int maxHealth { get; private set; }
        public int level { get; private set; }
        public int xp { get; private set; }
        public Element element { get; }
        public Attack[] attacks { get; private set; }


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
        public void TakeDamage(object sender, AttackEventArgs e)
        {
            currentHealth -= e.attack.damage;
        }
    }
}
