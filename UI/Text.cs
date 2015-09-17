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
        public String text_;
        public bool isLarge_;
        public Color color_;

        public Text(String text, bool isLarge)
        {
            color_ = Color.White;
            text_ = text;
            padding_ = Vector2.Zero;
            isLarge_ = isLarge;
            if (isLarge_)
            {
                size_ = Locator.getScreenPrinter().measureStringLg(text_);
            }
            else
            {
                size_ = Locator.getScreenPrinter().measureStringLg(text_);
            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
            if (isLarge_)
            {
                Locator.getScreenPrinter().printLg(spriteBatch, loc + padding_, text_, color_);
            }
            else
            {
                Locator.getScreenPrinter().printSm(spriteBatch, loc + padding_, text_, color_);
            }
        }

        public override void pack()
        {
            if (isLarge_)
            {
                size_ = Locator.getScreenPrinter().measureStringLg(text_);
            }
            else
            {
                size_ = Locator.getScreenPrinter().measureStringSm(text_);
            }
            base.pack();
        }
    }
}
