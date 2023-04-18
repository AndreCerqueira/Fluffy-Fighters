using FluffyFighters.Others;
using FluffyFighters.UI.Components.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.UI.Managers
{
    public class BroadcastManager
    {
        // Constants
        private const int BROADCAST_DELAY = 1500;

        // Properties
        private CombatBroadcast combatBroadcast;


        // Constructors
        public BroadcastManager(CombatBroadcast combatBroadcast)
        {
            this.combatBroadcast = combatBroadcast;
        }


        public async Task Display(string text)
        {
            combatBroadcast.SetText(text);
            await Task.Delay(BROADCAST_DELAY);
        }


        public async Task Display(string text, int delay)
        {
            combatBroadcast.SetText(text);
            await Task.Delay(delay);
        }
    }
}
