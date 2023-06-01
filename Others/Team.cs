using FluffyFighters.Args;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluffyFighters.Others
{
    public class Team
    {
        // Delegates
        public delegate void LoseEventHandler(object sender, LoseEventArgs e);

        // Constants
        public const int MAX_MONSTERS = 3;

        // Properties
        private Monster[] monsters { get; set; }
        private int currentMonsterIndex { get; set; }

        // Events
        public event LoseEventHandler OnLose;


        // Constructors
        public Team()
        {
            this.monsters = new Monster[MAX_MONSTERS];
            currentMonsterIndex = 0;

        }


        // Methods
        public void AddMonster(Monster monster, int? position = null)
        {
            int pos = position ?? monsters.ToList().IndexOf(null);

            if (pos > MAX_MONSTERS || !HaveAvailableSpots())
                throw new ArgumentOutOfRangeException("position", "Position must be less than or equal to MAX_MONSTERS.");

            monster.OnDeath += OnMonsterDeath;
            monsters[pos] = monster;
        }


        public void RemoveMonster(Monster monster)
        {
            monsters.Where(m => m == monster).ToList().Remove(monster);
        }


        public Monster GetMonster(int position) => monsters[position];
        public Monster GetSelectedMonster() => monsters[currentMonsterIndex];
        public List<Monster> GetMonsters() => monsters.Where(m => m != null).ToList();


        public void HeallAllMonsters()
        {
            foreach (var monster in monsters)
                monster.HealAll();
        }


        public void SelectMonster(int position) => currentMonsterIndex = position;
        public void SelectMonster(Monster monster) => currentMonsterIndex = monsters.ToList().IndexOf(monster);


        public void SelectNextMonster()
        {
            currentMonsterIndex++;
            if (currentMonsterIndex > MAX_MONSTERS)
                currentMonsterIndex = 0;
        }


        public void SelectPreviousMonster()
        {
            currentMonsterIndex--;
            if (currentMonsterIndex < 0)
                currentMonsterIndex = 0;
        }


        public bool HaveAvailableSpots() => monsters.Where(m => m == null).ToList().Count > 0;


        private bool HaveMonstersAlive() => monsters.Where(m => m != null && m.currentHealth > 0).ToList().Count > 0;
        

        private void OnMonsterDeath(object sender, MonsterEventArgs e)
        {
            if (!HaveMonstersAlive())
                Lose();
        }


        private void Lose() => OnLose?.Invoke(this, new LoseEventArgs(this));

    }
}
