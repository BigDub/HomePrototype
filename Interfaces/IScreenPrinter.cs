using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Interfaces
{
    interface IScreenPrinter
    {
        void print(SpriteBatch spriteBatch, Vector2 pos, String text, Color color, float scale);
    }
}
