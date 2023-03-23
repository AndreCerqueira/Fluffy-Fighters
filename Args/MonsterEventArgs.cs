using FluffyFighters.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Args
{
    public class MonsterEventArgs : EventArgs
    {
        // Properties
        public Monster monster;

        // Constructors
        public MonsterEventArgs(Monster monster)
        {
            this.monster = monster;
        }
    }
}
