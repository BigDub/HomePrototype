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

        public Text(String text, Vector2 padding)
        {
            text_ = text;
            padding_ = padding;
            size_ = Locator.getScreenPrinter().measureString(text);
        }

        public override void render(SpriteBatch spriteBatch)
        {
            Locator.getScreenPrinter().print(spriteBatch, loc, text_, Color.White, 1);
        }
    }
}
