using FluffyFighters.Args;
using FluffyFighters.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Others
{
    public class Attack
    {
        // Properties
        public string name { get; private set; }
        public Element element { get; private set; }
        public int damage { get; private set; }
        public int speed { get; private set; }
        public float successChance { get; private set; }

        // Constructors
        public Attack(string name, Element element, int damage, int speed, float successChance)
        {
            this.name = name;
            this.element = element;
            this.damage = damage;
            this.speed = speed;
            this.successChance = successChance;
        }


        // Methods
        // GetRandomAttack
        public static Attack GetRandomAttack(Element element, int level)
        {
            string name = GetRandomName(element);
            int damage = GetRandomDamage(level);
            int speed = GetRandomSpeed(damage);
            float successChance = GetRandomSuccessChance(damage);

            return new Attack(name, element, damage, speed, successChance);
        }


        private static string GetRandomName(Element element)
        {
            Random rnd = new Random();
            string[] nameArray;

            switch (element)
            {
                case Element.Water:
                    nameArray = new string[] { "Water Splash", "Aqua Beam", "Hydro Blast" };
                    break;
                case Element.Fire:
                    nameArray = new string[] { "Fire Punch", "Inferno Strike", "Flame Burst" };
                    break;
                case Element.Grass:
                    nameArray = new string[] { "Leaf Blade", "Vine Whip", "Nature's Fury" };
                    break;
                case Element.Neutral:
                    nameArray = new string[] { "Neutral Strike", "Elemental Blast", "Mystic Wave" };
                    break;
                default:
                    nameArray = new string[] { "Unknown Element" };
                    break;
            }

            return nameArray[rnd.Next(0, nameArray.Length)];
        }


        private static int GetRandomDamage(int level)
        {
            Random rnd = new Random();

            if (level < 5) 
                return rnd.Next(10, 40);
            else
                return rnd.Next(20, 70);
        }


        private static int GetRandomSpeed(int damage)
        {
            Random rnd = new Random();
            float speedMultiplier = (float)damage / 100;

            int minSpeed = (int)(100 - (speedMultiplier * 100));
            int maxSpeed = 100;

            return rnd.Next(minSpeed, maxSpeed + 1);
        }


        private static float GetRandomSuccessChance(int damage)
        {
            Random rnd = new Random();
            float successMultiplier = (float)damage / 100; 

            float minSuccessChance = successMultiplier * 0.5f;  
            float maxSuccessChance = 1.0f;

            return ((float)rnd.NextDouble() * (maxSuccessChance - minSuccessChance) + minSuccessChance) * 100f;
        }
    }
}
