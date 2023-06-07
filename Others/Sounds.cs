using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FluffyFighters.Others
{
    public static class Sounds
    {
        public static SoundEffect main;
        public static SoundEffect cut;
        public static SoundEffect battle;
        public static SoundEffect prepareBattle;
        public static SoundEffect levelUp;

        public static void LoadSounds(ContentManager Content)
        {
            main = Content.Load<SoundEffect>("audio/AndTheJourneyBegins_");
            cut = Content.Load<SoundEffect>("Cut");
            battle = Content.Load<SoundEffect>("DecisiveBattle");
            prepareBattle = Content.Load<SoundEffect>("PrepareForBattle");
            levelUp = Content.Load<SoundEffect>("levelup");
        }
    }
}
