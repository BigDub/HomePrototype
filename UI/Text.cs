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
        bool isLarge_;
        public Color color_;

        public Text(String text, bool isLarge)
        {
            color_ = Color.White;
            text_ = text;
            padding_ = Vector2.Zero;
            isLarge_ = isLarge;
            if (isLarge_)
            {
                size_ = Locator.getScreenPrinter().measureStringLg(text);
            }
            else
            {
                size_ = Locator.getScreenPrinter().measureStringLg(text);
            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
            if (isLarge_)
            {
                Locator.getScreenPrinter().printLg(spriteBatch, loc, text_, color_);
            }
            else
            {
                Locator.getScreenPrinter().printSm(spriteBatch, loc, text_, color_);
            }
        }
    }
}
