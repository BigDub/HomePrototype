using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Services
{
    class NullScreenPrinter : Interfaces.IScreenPrinter
    {
        public NullScreenPrinter()
        {
            //TODO: Log that the null service has been used.
        }

        public void print(SpriteBatch spriteBatch, Vector2 pos, string text, Color color, float scale)
        {
        }
    }
}
