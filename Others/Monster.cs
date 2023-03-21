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
        // Properties
        public string assetPath { get; set; }
        public string name { get; set; }
        public int currentHealth { get; set; }
        public int maxHealth { get; set; }
        public int level { get; set; }
        public int xp { get; set; }
        public Element element { get; set; }
        public Attack[] attacks { get; set; }

        // Constructors
        public Monster(string name, int maxHealth, Element element, Attack[] attacks, string assetPath, int level = 1)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.element = element;
            this.attacks = attacks;
            this.assetPath = assetPath;
            this.level = level;
        }
    }
}
