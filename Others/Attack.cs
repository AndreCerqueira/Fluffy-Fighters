﻿using FluffyFighters.Enums;
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
        public string name { get; set; }
        public Element element { get; set; }
        public int damage { get; set; }
        public int speed { get; set; }
        public float missChance { get; set; }

        // Constructors
        public Attack(string name, Element element, int damage, int speed, float missChance)
        {
            this.name = name;
            this.element = element;
            this.damage = damage;
            this.speed = speed;
            this.missChance = missChance;
        }
    }
}
