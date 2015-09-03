using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    class Window : FrameComponent
    {
        public Color color_;

        public Window(int rows, int columns) : base(rows, columns)
        {
            color_ = new Color(100, 100, 100, 200);
            padding_ = Vector2.Zero;
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.getTextureManager().getTexture(-1), new Rectangle((int)loc.X, (int)loc.Y, (int)size.X, (int)size.Y), null, color_, 0, Vector2.Zero, SpriteEffects.None, 1);
            base.render(spriteBatch);
        }
    }
}
