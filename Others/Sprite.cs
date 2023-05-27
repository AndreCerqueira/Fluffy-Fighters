using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Others
{
    public class Sprite
    {
        // Properties
        public Texture2D texture { get; set; }
        public Rectangle destination { get; set; }
        public Rectangle source { get; set; }
        public Color color { get; set; }
        public float rotation { get; set; }
        public Rectangle origin { get; set; }
        public SpriteEffects spriteEffects { get; set; }
        public float layerDepth { get; set; }


        public Sprite(Texture2D texture, Rectangle destination, Rectangle source, Color color, float rotation, Rectangle origin, SpriteEffects spriteEffects, float layerDepth)
        {
            this.texture = texture;
            this.destination = destination;
            this.source = source;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.spriteEffects = spriteEffects;
            this.layerDepth = layerDepth;
        }
    }
}
