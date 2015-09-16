using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ShipPrototype.Services;
using ShipPrototype.Components;

namespace ShipPrototype.ControlStates
{
    class HoldingItem : BaseState
    {
        GameEntity item_;
        GameEntity marker;
        UI.Text counter;

        InventoryComponent origContainer_;
        int origSlot_;

        bool itemPlaced_;

        public HoldingItem(InventoryComponent container, GameEntity item, int slot)
        {
            item_ = item;
            origContainer_ = container;
            origSlot_ = slot;
            marker = new GameEntity();
            marker.spatial = new Components.SpatialComponent(marker, Vector2.Zero, 0, Vector2.One);
            marker.render = new Components.RenderComponent(marker, item.item.tex_, 0, Vector2.Zero, Color.White);
            counter = new UI.Text("" + item.info.number, false);
            counter.pack();

            itemPlaced_ = false;
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.button_ == MouseButton.RIGHT)
            {
                if (origContainer_ != null && (origContainer_.entity_.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() < interactRange)
                {
                    if (origContainer_.getItem(origSlot_) == null)
                    {
                        origContainer_.placeItem(item_, origSlot_);
                        itemPlaced_ = true;
                        changeState(new Selector());
                    }
                }
            }
            base.mouseUp(sender, e);
        }

        public override void changeState(ControllerState newState)
        {
            Debug.Assert(itemPlaced_);
            if (marker != null)
            {
                Locator.getComponentManager().removeEntity(marker);
            }
            base.changeState(newState);
        }

        public override void update(float elapsed)
        {
            base.update(elapsed);
            Vector2 maus = Locator.getInputHandler().getMousePosition();
            marker.spatial.translation_ = maus;
            counter.loc_ = maus + new Vector2(32) - counter.size;
        }

        public override void onPost(Services.Post post)
        {
            switch (post.category)
            {
                case PostCategory.INV_SLOT:
                    if (post.component != null)
                    {
                        InventoryComponent inv = (InventoryComponent)post.component;
                        GameEntity slot = inv.getItem(post.slot);
                        if (slot == null)
                        {
                            inv.placeItem(item_, post.slot);
                            itemPlaced_ = true;
                            changeState(new Selector());
                        }
                        else if (slot.item == item_.item)
                        {
                            if (item_.info.number > 1 && Locator.getInputHandler().isKeyDown(Keys.LeftShift))
                            {
                                slot.info.number++;
                                item_.info.number--;
                                counter.text_ = "" + item_.info.number;
                                counter.pack();
                            }
                            else
                            {
                                slot.info.number += item_.info.number;
                                itemPlaced_ = true;
                                item_ = null;
                                changeState(new Selector());
                            }
                        }
                        else
                        {
                            inv.takeItem(post.slot);
                            inv.placeItem(item_, post.slot);
                            item_ = post.targetEntity;
                            marker.render = new RenderComponent(marker, item_.item.tex_, 0, Vector2.Zero, Color.White);
                            counter.text_ = "" + item_.info.number;
                            counter.pack();
                            origContainer_ = inv;
                            origSlot_ = post.slot;
                        }
                    }
                    return;
                case PostCategory.PLACED_ITEM:
                    if (post.sourceEntity.inventory != null)
                    {
                        post.sourceEntity.inventory.placeItem(item_, post.slot);
                    }
                    else
                    {
                        post.sourceEntity.production.placeItem(item_, post.slot);
                    }
                    itemPlaced_ = true;
                    changeState(new Selector());
                    return;
                case PostCategory.REQUEST_ITEM:
                    if (post.sourceEntity.inventory != null)
                    {
                        post.sourceEntity.inventory.takeItem(post.slot);
                        post.sourceEntity.inventory.placeItem(item_, post.slot);
                    }
                    else
                    {
                        post.sourceEntity.production.takeItem(post.slot);
                        post.sourceEntity.production.placeItem(item_, post.slot);
                    }
                    item_ = post.targetEntity;
                    marker.render = new Components.RenderComponent(marker, item_.item.tex_, 0, Vector2.Zero, Color.White);
                    origContainer_ = (InventoryComponent)post.component;
                    origSlot_ = post.slot;
                    return;
                default:
                    break;
            }
            base.onPost(post);
        }

        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
            marker.render.render(spriteBatch);
            counter.render(spriteBatch);
        }
    }
}
