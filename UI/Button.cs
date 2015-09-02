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
        private float scale_;
        public GUIMessage message_;

        public Button(int texture_id, GUIMessage message)
        {
            texture_id_ = texture_id;
            Texture2D tex = Locator.getTextureManager().getTexture(texture_id);
            size_ = new Vector2(tex.Width, tex.Height);
            message_ = message;
            scale_ = 1;
        }

        public Button(int texture_id, GUIMessage message, float scale) : this(texture_id, message)
        {
            scale_ = scale;
            size_ *= scale_;
        }

        public override void click(Vector2 pos)
        {
            Locator.getControlManager().sendMessage(message_);
        }

        public override void render(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), loc, null, Color.White, 0, Vector2.Zero, scale_, SpriteEffects.None, 0);
        }
    }
}
