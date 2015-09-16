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

        public override void onPost(Post post)
        {
            switch (post.category)
            {
                case PostCategory.INV_SLOT:
                    Console.WriteLine("test");
                    InventoryComponent inv = (InventoryComponent)post.component;
                    GameEntity item = inv.getItem(post.slot);
                    Console.WriteLine("slot " + post.slot);
                    if (item != null)
                    {
                        Console.WriteLine("item not null");
                        if (item.info.number > 1 && Locator.getInputHandler().isKeyDown(Keys.LeftShift))
                        {
                            Console.WriteLine("shift");
                            item.info.number--;
                            item = Locator.getObjectFactory().createItem(item.item);
                            changeState(new HoldingItem(inv, item, post.slot));
                        }
                        else
                        {
                            Console.WriteLine("normal");
                            inv.takeItem(post.slot);
                            changeState(new HoldingItem(inv, item, post.slot));
                        }
                    }
                    break;
                case PostCategory.REQUEST_ITEM:
                    changeState(new HoldingItem((InventoryComponent)post.component, post.targetEntity, post.slot));
                    if (post.sourceEntity.inventory != null)
                    {
                        post.sourceEntity.inventory.takeItem(post.slot);
                    }
                    else
                    {
                        post.sourceEntity.production.takeItem(post.slot);
                    }
                    break;
                default:
                    break;
            }
            base.onPost(post);
        }
    }
}
