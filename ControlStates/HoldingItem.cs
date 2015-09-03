using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Services;

namespace ShipPrototype.ControlStates
{
    class HoldingItem : BaseState
    {
        GameEntity item_;
        GameEntity marker;

        GameEntity origContainer_;
        int origSlot_;

        bool itemPlaced_;

        public HoldingItem(GameEntity container, GameEntity item, int slot)
        {
            item_ = item;
            origContainer_ = container;
            origSlot_ = slot;
            marker = new GameEntity();
            marker.spatial = new Components.SpatialComponent(marker, Vector2.Zero, 0, Vector2.One);
            marker.render = new Components.RenderComponent(marker, item.info.itemTex, 0, Vector2.Zero, Color.White);

            itemPlaced_ = false;
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.button_ == MouseButton.RIGHT)
            {
                if (origContainer_ != null && origContainer_.inventory != null && (origContainer_.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() < interactRange)
                {
                    if (origContainer_.inventory.getItem(origSlot_) == null)
                    {
                        origContainer_.inventory.placeItem(item_, origSlot_);
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
            marker.spatial.translation_ = Locator.getInputHandler().getMousePosition();
        }

        public override void onPost(Services.Post post)
        {
            switch (post.category)
            {
                case PostCategory.PLACED_ITEM:
                    post.sourceEntity.inventory.placeItem(item_, post.slot);
                    itemPlaced_ = true;
                    changeState(new Selector());
                    return;
                case PostCategory.REQUEST_ITEM:
                    post.sourceEntity.inventory.takeItem(post.slot);
                    post.sourceEntity.inventory.placeItem(item_, post.slot);
                    item_ = post.targetEntity;
                    origContainer_ = post.sourceEntity;
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
        }
    }
}
