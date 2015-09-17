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
        GameEntity marker_;
        GameEntity ground_;
        bool isPlaceable_;
        bool canPlace;
        float transTimer;
        UI.ItemButton itemButton_;

        InventoryComponent origContainer_;
        int origSlot_;

        public HoldingItem(InventoryComponent container, GameEntity item, int slot)
        {
            Console.WriteLine("new HoldingItem structureID_: " + item.item.structureID_);
            item_ = item;
            origContainer_ = container;
            origSlot_ = slot;

            isPlaceable_ = item.item.structureID_ >= 0;
            if (isPlaceable_)
            {
                marker_ = Locator.getObjectFactory().createEntity(item.item.structureID_);
                marker_.info.state = ObjectState.DISABLED;
                ground_ = new GameEntity();
                ground_.spatial = new SpatialComponent(ground_, marker_.spatial);
                ground_.spatial.scale_ = new Vector2(marker_.tile.size_.X, marker_.tile.size_.Y) * 32f;
                ground_.render = new RenderComponent(ground_, -1, 0, new Vector2(0.5f), Color.White);
            }

            itemButton_ = new UI.ItemButton(item_);
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            switch (e.button_)
            {
                case MouseButton.LEFT:
                    if (isPlaceable_ && !mouseInWindow_ && canPlace)
                    {
                        Console.WriteLine("Placing " + marker_.info.name + " at " + mouseTile_.X + ", " + mouseTile_.Y);
                        marker_.info.state = item_.item.state_;
                        Locator.getShip().tiles.build(marker_.tile.coord_, marker_.tile.size_);
                        Locator.getMessageBoard().postMessage(
                            new Post(PostCategory.PLACED_OBJECT, Locator.getPlayer(), marker_, null, 0)
                        );
                        Locator.getComponentManager().addEntity(marker_);
                        marker_ = null;
                        if (item_.item.number_ > 1)
                        {
                            item_.item.number_--;
                            changeState(new HoldingItem(origContainer_, item_, origSlot_));
                        }
                        else
                        {
                            changeState(new Selector());
                        }
                    }
                    break;
                case MouseButton.RIGHT:
                    if (origContainer_ != null && (origContainer_.entity_.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() < interactRange)
                    {
                        if (origContainer_.getItem(origSlot_) == null)
                        {
                            origContainer_.placeItem(item_, origSlot_);
                            changeState(new Selector());
                        }
                    }
                    break;
            }
            base.mouseUp(sender, e);
        }

        public override void update(float elapsed)
        {
            base.update(elapsed);
            Vector2 maus = Locator.getInputHandler().getMousePosition();
            itemButton_.loc_ = maus;

            if (isPlaceable_)
            {
                transTimer += 4f * elapsed;
                canPlace = Locator.getShip().tiles.canBuild(mouseTile_, marker_.tile.size_);
                marker_.tile.coord_ = mouseTile_;

                if (canPlace)
                {
                    float alpha = 0.5f + (float)(Math.Sin(transTimer) * 0.25f);
                    marker_.render.color_ = new Color(0.5f, 1, 0.5f, alpha);
                    ground_.render.color_ = new Color(0.5f, 1, 0.5f, 0.5f);
                }
                else
                {
                    float alpha = 0.5f + (float)(Math.Sin(transTimer) * 0.25f);
                    marker_.render.color_ = new Color(1, 0.5f, 0.5f, alpha);
                    ground_.render.color_ = new Color(1, 0.5f, 0.5f, 0.5f);
                }
            }
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
                            changeState(new Selector());
                        }
                        else if (slot.item.ID_ == item_.item.ID_)
                        {
                            if (item_.item.number_ > 1 && Locator.getInputHandler().isKeyDown(Keys.LeftShift))
                            {
                                slot.item.number_++;
                                item_.item.number_--;
                                itemButton_.refresh(item_);
                                inv.onUpdate();
                            }
                            else
                            {
                                slot.item.number_ += item_.item.number_;
                                item_ = null;
                                inv.onUpdate();
                                changeState(new Selector());
                            }
                        }
                        else
                        {
                            inv.takeItem(post.slot);
                            inv.placeItem(item_, post.slot);
                            changeState(new HoldingItem(inv, post.targetEntity, post.slot));
                        }
                    }
                    return;
                default:
                    break;
            }
            base.onPost(post);
        }

        public override void render(SpriteBatch spriteBatch)
        {
            if (isPlaceable_ && !mouseInWindow_)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

                ground_.render.render(spriteBatch);
                marker_.render.render(spriteBatch);

                spriteBatch.End();
            }

            base.render(spriteBatch);

            //if (!isPlaceable_ || mouseInWindow_)
            {
                spriteBatch.Begin();
                itemButton_.render(spriteBatch);
                spriteBatch.End();
            }
        }
    }
}
