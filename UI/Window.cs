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
        private static Color windowColor = new Color(100, 149, 237, 200);
        public Window()
        {
            children_ = new List<UIComponent>();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.getTextureManager().getTexture(-1), new Rectangle((int)loc_.X, (int)loc_.Y, (int)size_.X, (int)size_.Y), windowColor);
            foreach (UIComponent child in children_)
            {
                child.render(spriteBatch);
            }
        }
    }
}
