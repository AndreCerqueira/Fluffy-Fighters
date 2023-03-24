using FluffyFighters.Args;
using FluffyFighters.Enums;
using FluffyFighters.Others;
using FluffyFighters.UI.Components.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FluffyFighters.Others.Monster;

namespace FluffyFighters.UI.Managers
{
    public class CombatManager
    {
        // Delegates
        public delegate void AttackPerformedEventHandler(object sender, AttackPerformedEventArgs e);

        // Constants
        private const int ATTACK_DELAY = 1000;

        // Properties
        public Team playerTeam;
        public Team enemyTeam;
        public Monster playerSelectedMonster => playerTeam.GetSelectedMonster();
        public Monster enemySelectedMonster => enemyTeam.GetSelectedMonster();


        // Constructors
        public CombatManager(Team playerTeam, Team enemyTeam)
        {
            this.playerTeam = playerTeam;
            this.enemyTeam = enemyTeam;
        }

        // Events
        public event MonsterEventHandler onMonsterSelected;
        public event EventHandler onMonsterDied;
        public event EventHandler onTurnEnd;
        public event EventHandler onTurnStart;
        public event AttackPerformedEventHandler onAttackPerformed;
        public event EventHandler onAttackFailed;
        public event EventHandler onBattleEnd;


        // Methods


        public void SelectMonster(object sender, MonsterEventArgs e)
        {
            // BlockAllButtons();

            // combatBroadcast.SetText($"{e.monster.name} joined the battle!");
            // await Task.Delay(BROADCAST_DELAY);

            Attack enemyAttack = SelectEnemyAttack();
            PerformAttack(enemyAttack, enemySelectedMonster, playerTeam);

            onMonsterSelected?.Invoke(sender, e);

        }


        public Attack SelectEnemyAttack() => enemySelectedMonster.GetRandomAttack();


        public Queue<Action> GetAttackOrder(Attack playerAttack)
        {
            Queue<Action> actions = new Queue<Action>();
            Attack enemyAttack = SelectEnemyAttack();

            if (playerSelectedMonster.IsDead() || enemySelectedMonster.IsDead())
                return null;

            // get whose is faster
            bool playerAttackedFirst = false;
            if (playerAttack.speed > enemyAttack.speed)
                playerAttackedFirst = true;
            else if (playerAttack.speed == enemyAttack.speed)
                playerAttackedFirst = (new Random().Next(0, 2) == 0);

            Monster firstAttacker = playerAttackedFirst ? playerSelectedMonster : enemySelectedMonster;
            Monster secondAttacker = playerAttackedFirst ? enemySelectedMonster : playerSelectedMonster;

            // Get teams
            Team firstTeam = playerAttackedFirst ? playerTeam : enemyTeam;
            Team secondTeam = playerAttackedFirst ? enemyTeam : playerTeam;

            // Get attacks
            Attack firstAttack = playerAttackedFirst ? playerAttack : enemyAttack;
            Attack secondAttack = playerAttackedFirst ? enemyAttack : playerAttack;

            // Perform attacks
            actions.Enqueue(() => PerformAttack(firstAttack, firstAttacker, secondTeam));
            actions.Enqueue(() => PerformAttack(secondAttack, secondAttacker, firstTeam));

            return actions;
        }


        public async void DoTurn(object sender, AttackEventArgs e)
        {
            onTurnStart?.Invoke(this, EventArgs.Empty);

            Queue<Action> actions = GetAttackOrder(e.attack);
            if (actions == null) return;

            while (actions.Count > 0) { 
                Action action = actions.Dequeue();
                action();
                await Task.Delay(1000);
            }

            onTurnEnd?.Invoke(this, EventArgs.Empty);
        }


        public void PerformAttack(Attack attack, Monster attacker, Team target)
        {
            if (attacker.IsDead() || !IsAttackSuccessful(attack))
            {
                onAttackFailed?.Invoke(this, EventArgs.Empty);
                return;
            }

            float damage = GetDamage(attack, attacker, target.GetSelectedMonster());
            target.GetSelectedMonster().TakeDamage(damage);

            onAttackPerformed?.Invoke(this, new AttackPerformedEventArgs(attack, attacker, target));
        }


        private bool IsAttackSuccessful(Attack attack)
        {
            Random random = new Random();
            int chance = random.Next(0, 100);
            return attack.successChance >= chance;
        }


        private float GetAttackMultiplier(Attack attack, Monster target)
        {
            ElementEffectiveness effectiveness = attack.element.GetElementEffectiveness(target.element);
            return effectiveness switch
            {
                ElementEffectiveness.Effective => 1.5f,
                ElementEffectiveness.NotEffective => 0.5f,
                _ => 1f
            };
        }


        private float GetDamage(Attack attack, Monster attacker, Monster target)
        {
            float multiplier = GetAttackMultiplier(attack, target);
            return attack.damage * multiplier;
        }

    }
}
