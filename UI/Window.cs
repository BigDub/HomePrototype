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
        private static Color windowColor = new Color(100, 100, 100, 200);

        public Window(Vector2 padding, int rows, int columns) : base(rows, columns)
        {
            padding_ = padding;
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.getTextureManager().getTexture(-1), new Rectangle((int)loc_.X, (int)loc_.Y, (int)size.X, (int)size.Y), null, windowColor, 0, Vector2.Zero, SpriteEffects.None, 1);
            base.render(spriteBatch);
        }
    }
}
