using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Enums
{
    public enum Element
    {
        Neutral,
        Fire,
        Water,
        Grass
    }

    public enum ElementEffectiveness
    {
        Neutral,
        Effective,
        NotEffective
    }

    public static class ElementExtensions
    {
        public static string GetAssetPath(this Element element)
        {
            return $"sprites/elements/{element.ToString().ToLower()}";
        }


        public static ElementEffectiveness GetElementEffectiveness(this Element attacker, Element target)
        {
            if ((attacker == Element.Water && target == Element.Fire) ||
                (attacker == Element.Fire && target == Element.Grass) ||
                (attacker == Element.Grass && target == Element.Water))
            {
                return ElementEffectiveness.Effective;
            }
            else if ((attacker == Element.Fire && target == Element.Water) ||
                     (attacker == Element.Water && target == Element.Grass) ||
                     (attacker == Element.Grass && target == Element.Fire))
            {
                return ElementEffectiveness.NotEffective;
            }

            return ElementEffectiveness.Neutral;
        }
    }
}
