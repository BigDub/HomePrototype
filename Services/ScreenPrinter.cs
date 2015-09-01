using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Services
{
    class ScreenPrinter : Interfaces.IScreenPrinter
    {
        static ScreenPrinter instance = null;
        SpriteFont spriteFont_;

        public static ScreenPrinter init(SpriteFont spriteFont)
        {
            Debug.Assert(instance == null);

            instance = new ScreenPrinter(spriteFont);
            return instance;
        }

        private ScreenPrinter(SpriteFont spriteFont)
        {
            spriteFont_ = spriteFont;
        }

        public void print(SpriteBatch spriteBatch, Vector2 pos, String text, Color color, float scale)
        {
            spriteBatch.DrawString(spriteFont_, text, pos, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public Vector2 measureString(String str)
        {
            return spriteFont_.MeasureString(str);
        }
    }
}
