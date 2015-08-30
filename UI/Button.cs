using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    class Button : UIComponent
    {
        int texture_id_;
        public bool isDown_;

        public override void render(SpriteBatch spriteBatch)
        {
            if (isDown_)
            {
                spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), loc_, Color.LightGray);
            }
            else
            {
                spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), loc_, Color.White);
            }
        }
    }
}
