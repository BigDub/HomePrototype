using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Services
{
    class ScreenPrinter
    {
        static ScreenPrinter instance = null;
        SpriteFont fontLarge_;
        SpriteFont fontSmall_;

        public static ScreenPrinter init(SpriteFont large, SpriteFont small)
        {
            Debug.Assert(instance == null);

            instance = new ScreenPrinter(large, small);
            return instance;
        }

        private ScreenPrinter(SpriteFont large, SpriteFont small)
        {
            fontLarge_ = large;
            fontSmall_ = small;
        }

        public void printLg(SpriteBatch spriteBatch, Vector2 pos, String text, Color color)
        {
            spriteBatch.DrawString(fontLarge_, text, pos, color);
        }

        public void printSm(SpriteBatch spriteBatch, Vector2 pos, String text, Color color)
        {
            spriteBatch.DrawString(fontSmall_, text, pos, color);
        }

        public Vector2 measureStringLg(String str)
        {
            return fontLarge_.MeasureString(str);
        }

        public Vector2 measureStringSm(String str)
        {
            return fontSmall_.MeasureString(str);
        }
    }
}
