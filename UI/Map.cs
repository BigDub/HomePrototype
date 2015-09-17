using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    class Map : UIComponent
    {
        int texture_id_;
        public Map()
        {
            texture_id_ = Locator.getTextureManager().loadTexture("map");
            Texture2D tex = Locator.getTextureManager().getTexture(texture_id_);
            size_ = new Vector2(tex.Width, tex.Height);
        }

        public override void click(Vector2 pos)
        {
            Locator.getMessageBoard().postMessage(new Services.Post(Services.PostCategory.END_GAME));
            Locator.getShip().end();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), loc + padding_, Color.White);
        }
    }
}
