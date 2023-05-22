using FluffyFighters.Characters;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyFighters.Others
{
    public class Camera
    {
        public Vector2 Position { get; private set; }
        public float Speed { get; set; }

        public Camera(float Speed)
        {
            Position = Vector2.Zero;
            this.Speed = Speed;
        }
        

        public void Follow(Vector2 target, GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position = Vector2.Lerp(Position, target, Speed * elapsed);
        }
    }
}
