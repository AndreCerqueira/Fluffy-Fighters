using FluffyFighters.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Args
{
    public class AttackPerformedEventArgs : EventArgs
    {
        // Properties
        public Attack attack;
        public Monster attacker;
        public Team target;

        // Constructors
        public AttackPerformedEventArgs(Attack attack, Monster attacker, Team target)
        {
            this.attack = attack;
            this.attacker = attacker;
            this.target = target;
        }
    }
}
