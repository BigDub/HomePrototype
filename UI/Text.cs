using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    class Text : UIComponent
    {
        String text_;

        public override void render(SpriteBatch spriteBatch)
        {
            Locator.getScreenPrinter().print(spriteBatch, loc_, text_, Color.White, 1);
        }
    }
}
