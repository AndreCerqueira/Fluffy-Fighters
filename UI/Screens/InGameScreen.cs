using FluffyFighters.Enums;
using FluffyFighters.Others;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using FluffyFighters.UI.Components.Buttons;
using Microsoft.Xna.Framework.Input;

namespace FluffyFighters.UI.Screens
{
    public class InGameScreen : GameScreen
    {
        // Properties
        


        // Constructors
        public InGameScreen(Game game) : base(game)
        {
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void LoadContent()
        {
            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
        }


        public override void Update(GameTime gameTime)
        {
            Mouse.SetCursor(Button.defaultCursor);
        }
    }
}
