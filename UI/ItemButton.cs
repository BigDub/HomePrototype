using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class ItemButton : Button
    {
        public InventoryComponent component_;
        public GameEntity item_;
        public Text count_;

        public ItemButton(InventoryComponent component, int slot)
            : base(Locator.getTextureManager().loadTexture("tile32"), Services.PostCategory.INV_SLOT)
        {
            component_ = component;
            slot_ = slot;
            count_ = new Text("", false);
            count_.parent_ = this;
        }

        public ItemButton(GameEntity item)
            : base(Locator.getTextureManager().loadTexture("tile32"), Services.PostCategory.UNUSED)
        {
            item_ = item;
            count_ = new Text("" + item.item.number_, false);
            count_.parent_ = this;
            count_.pack();
            count_.loc_ = new Vector2(buttonSize) - count_.size;
        }

        public void refresh()
        {
            item_ = component_.getItem(slot_);
            if (item_ != null)
            {
                count_.text_ = "" + item_.item.number_;
                count_.pack();
                count_.loc_ = new Vector2(buttonSize) - count_.size;
            }
        }

        public void refresh(GameEntity item)
        {
            item_ = item;
            count_.text_ = "" + item.item.number_;
            count_.pack();
            count_.loc_ = new Vector2(buttonSize) - count_.size;
        }

        public override void render(SpriteBatch spriteBatch)
        {
            if (component_ != null)
            {
                base.render(spriteBatch);
            }
            if (item_ != null)
            {
                Color color = Color.White;
                if (item_.item.state_ == ObjectState.DAMAGED)
                    color = Color.Red;
                spriteBatch.Draw(Locator.getTextureManager().getTexture(item_.item.tex_), new Rectangle((int)loc.X, (int)loc.Y, buttonSize, buttonSize), color);
                count_.render(spriteBatch);
            }
        }
    }
}
