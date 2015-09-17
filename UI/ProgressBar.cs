using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    class ProgressBar : UIComponent
    {
        public Color bg, fg;
        public float percent;

        public ProgressBar()
        {
            bg = Color.Gray;
            fg = Color.Green;
            size_ = new Vector2(150, 5);
        }

        public ProgressBar(int width)
        {
            bg = Color.Gray;
            fg = Color.Green;
            size_ = new Vector2(width, 5);
        }

        public override void render(SpriteBatch spriteBatch)
        {
            Rectangle front = new Rectangle((int)loc.X, (int)loc.Y, (int)(size_.X * percent), (int)(size_.Y));
            Rectangle back = new Rectangle((int)loc.X, (int)loc.Y, (int)(size_.X), (int)(size_.Y));
            spriteBatch.Draw(Locator.getTextureManager().getTexture(-1), back, bg);
            spriteBatch.Draw(Locator.getTextureManager().getTexture(-1), front, fg);
        }
    }
}
