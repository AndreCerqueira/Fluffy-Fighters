using FluffyFighters.Others;
using System;

namespace FluffyFighters.Args
{
    public class LoseEventArgs : EventArgs
    {
        // Properties
        public Team team;

        // Constructors
        public LoseEventArgs(Team team)
        {
            this.team = team;
        }
    }
}
