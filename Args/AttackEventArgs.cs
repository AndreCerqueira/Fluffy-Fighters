using FluffyFighters.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Args
{
    public class AttackEventArgs : EventArgs
    {
        // Properties
        public Attack attack;

        // Constructors
        public AttackEventArgs(Attack attack)
        {
            this.attack = attack;
        }
    }
}
