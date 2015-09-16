using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class Button : UIComponent
    {
        public static int buttonSize = 32;
        public int texture_id_;
        public Services.PostCategory category_;
        public delegate GameEntity EntityDelegate(int num);
        public delegate Component ComponentDelegate();
        public EntityDelegate getSource, getTarget;
        public ComponentDelegate getComponent;
        public int slot_;

        public Button(int texture_id, Services.PostCategory category)
        {
            texture_id_ = texture_id;
            Texture2D tex = Locator.getTextureManager().getTexture(texture_id);
            size_ = new Vector2(buttonSize);
            category_ = category;
            slot_ = -1;
            getSource = null;
            getTarget = null;
        }

        public override void click(Vector2 pos)
        {
            GameEntity source = null;
            GameEntity target = null;
            Component component = null;

            if (getSource != null)
                source = getSource(slot_);
            if (getTarget != null)
                target = getTarget(slot_);
            if (getComponent != null)
                component = getComponent();

            Locator.getMessageBoard().postMessage(
                new Services.Post(category_, source, target, component, slot_)
            );
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), new Rectangle((int)loc.X, (int)loc.Y, buttonSize, buttonSize), Color.White);
        }
    }
}
