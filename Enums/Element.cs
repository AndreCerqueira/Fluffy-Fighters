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

    public static class ElementExtensions
    {
        public static string GetAssetPath(this Element element)
        {
            return $"sprites/elements/{element.ToString().ToLower()}";
        }
    }
}
