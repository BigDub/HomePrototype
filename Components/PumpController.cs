using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Services;

namespace ShipPrototype.Components
{
    class PumpController : ItemMoverController
    {
        bool initialSearch;
        public PumpController(GameEntity e)
            : base(e)
        {
            processTime_ = 1;
            isDrawn = true;
            initialSearch = true;
        }

        public override Component deepCopy(GameEntity entity)
        {
            return new PumpController(entity);
        }

        public override void linkInput()
        {
            entity_.inventory.register(checkStatus);
            Locator.getMessageBoard().register(onPost);
        }
        public override void unlinkInput()
        {
            entity_.inventory.unregister(checkStatus);
            Locator.getMessageBoard().unregister(onPost);
        }

        public override UI.FrameComponent getFrame()
        {
            return new UI.ItemMoverFrame(this);
        }

        public void onPost(Post post)
        {
            switch(post.category)
            {
                case PostCategory.PLACED_OBJECT:
                case PostCategory.REMOVED_OBJECT:
                    checkIO();
                    break;
                default:
                    break;
            }
        }

        public override void update(float elapsed)
        {
            if (initialSearch)
            {
                checkIO();
                initialSearch = false;
            }
            base.update(elapsed);
        }

        public void checkIO()
        {
            Point point;
            GameEntity entity;

            point = new Point(entity_.tile.coord_.X - 1, entity_.tile.coord_.Y);
            entity = entity_.tile.tileSystem_.getEntityAt(point);
            if (entity == null || entity.inventory == null)
            {
                output_ = null;
            }
            else
            {
                output_ = entity.inventory;
            }
            
            point = new Point(entity_.tile.coord_.X + entity_.tile.size_.X, entity_.tile.coord_.Y);
            entity = entity_.tile.tileSystem_.getEntityAt(point);
            if (entity == null || entity.inventory == null)
            {
                input_ = null;
            }
            else
            {
                input_ = entity.inventory;
            }
        }

        public void checkStatus()
        {
            if (entity_.info.state == ObjectState.DAMAGED)
                return;

            bool motor = false;
            bool pinion = false;
            for (int index = 0; index < entity_.inventory.capacity; ++index)
            {
                GameEntity e = entity_.inventory.getItem(index);
                if (e != null)
                {
                    if (e.item.ID_ == Locator.getObjectFactory().pinionItem.ID_)
                        pinion = true;
                    if (e.item.ID_ == Locator.getObjectFactory().motorItem.ID_)
                        motor = true;
                }
            }

            if (motor && pinion)
            {
                entity_.info.state = ObjectState.OK;
            }
            else
            {
                entity_.info.state = ObjectState.DISABLED;
            }
        }
    }
}
