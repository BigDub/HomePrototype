using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class TractorController : ControllerComponent
    {
        private static float speed = 50f;
        private static float accel_speed = 10f;
        public GameEntity target_;

        float effectTimer;

        public TractorController(GameEntity entity)
            : base(entity)
        {
        }

        public override Component deepCopy(GameEntity entity)
        {
            return new TractorController(entity);
        }

        public override void linkInput()
        {
            entity_.inventory.register(checkStatus);
        }
        public override void unlinkInput()
        {
            entity_.inventory.unregister(checkStatus);
        }

        private void checkStatus()
        {
            InventoryComponent ic = entity_.inventory;
            for (int index = 0; index < ic.capacity; ++index)
            {
                GameEntity item = ic.getItem(index);
                if (item == null)
                    continue;
                if (item.item.ID_ == Locator.getObjectFactory().orbItem.ID_)
                {
                    entity_.info.state = ObjectState.OK;
                    return;
                }
            }
            entity_.info.state = ObjectState.DISABLED;
        }

        private void findTarget()
        {
            foreach (InfoComponent info in Locator.getComponentManager().info_)
            {
                if (info.type == ObjectType.LOOTABLE)
                {
                    target_ = info.entity_;
                    Vector2 dir = entity_.spatial.w_translation - target_.spatial.w_translation;
                    dir.Normalize();
                    target_.physic.velocity_ = dir * speed;
                    target_.physic.accel_ = dir * accel_speed;
                    break;
                }
            }
        }

        public override void update(float elapsed)
        {
            if (entity_.info.state != ObjectState.OK)
                return;

            if (entity_.inventory.numItems() == entity_.inventory.capacity)
            {
                bool scrap = false;
                for (int i = 0; i < entity_.inventory.capacity; i++)
                {
                    if (entity_.inventory.getItem(i).item.ID_ == Locator.getObjectFactory().scrapItem.ID_)
                    {
                        scrap = true;
                        break;
                    }
                }
                if (!scrap)
                    return;
            }

            if (target_ == null)
                findTarget();

            if (target_ != null)
            {
                effectTimer += elapsed;
                if (effectTimer > 0.1f)
                {
                    Locator.getComponentManager().addEntity(Locator.getObjectFactory().createTractorEffect(target_.spatial.w_translation, entity_.spatial.w_translation));
                    effectTimer -= 0.1f;
                }

                if ((entity_.spatial.w_translation - target_.spatial.w_translation).Length() < 50f)
                {
                    Locator.getComponentManager().removeEntity(target_);
                    target_ = null;
                    for (int index = 0; index < entity_.inventory.capacity; ++index)
                    {
                        GameEntity item = entity_.inventory.getItem(index);
                        if (item == null)
                        {
                            entity_.inventory.placeItem(Locator.getObjectFactory().createScrap(), index);
                            break;
                        }
                        else if (item.item.ID_ == Locator.getObjectFactory().scrapItem.ID_)
                        {
                            item.item.number_++;
                            entity_.inventory.onUpdate();
                        }
                    }
                }
            }
        }
    }
}
