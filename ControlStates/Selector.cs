using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ShipPrototype.Services;
using ShipPrototype.Components;

namespace ShipPrototype.ControlStates
{
    class Selector : BaseState
    {
        public Selector()
        {
        }

        public override void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.button_ == MouseButton.RIGHT)
            {
                GameEntity en = Locator.getComponentManager().pick(Locator.getShip().tiles, mouseTile_);
                if (en != null && en.tile != null && en.item != null && (en.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() < interactRange)
                {
                    changeState(new Removing(en));
                }
            }
        }

        public override void onPost(Post post)
        {
            switch (post.category)
            {
                case PostCategory.INV_SLOT:
                    InventoryComponent inv = (InventoryComponent)post.component;
                    GameEntity item = inv.getItem(post.slot);
                    if (item != null)
                    {
                        if (item.item.number_ > 1 && Locator.getInputHandler().isKeyDown(Keys.LeftShift))
                        {
                            item.item.number_--;
                            item = Locator.getObjectFactory().createItem(item.item);
                            inv.onUpdate();
                            changeState(new HoldingItem(inv, item, post.slot));
                        }
                        else
                        {
                            inv.takeItem(post.slot);
                            changeState(new HoldingItem(inv, item, post.slot));
                        }
                    }
                    break;
                default:
                    break;
            }
            base.onPost(post);
        }
    }
}
