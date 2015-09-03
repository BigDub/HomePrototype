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
        public static int buttonSize = 32;
        int texture_id_;
        public GUIMessage message_;
        public delegate GameEntity SpawnFunction();
        SpawnFunction method;

        public Button(int texture_id, GUIMessage message)
        {
            texture_id_ = texture_id;
            Texture2D tex = Locator.getTextureManager().getTexture(texture_id);
            size_ = new Vector2(buttonSize);
            message_ = message;
        }

        public void setSpawn(SpawnFunction f)
        {
            method = f;
        }

        public override void click(Vector2 pos)
        {
            if (message_ == GUIMessage.START_PLACEMENT && method != null)
            {
                Locator.getControlManager().sendEvent(new GUIEvent(message_, method()));
            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), new Rectangle((int)loc.X, (int)loc.Y, buttonSize, buttonSize), Color.White);
        }
    }
}
