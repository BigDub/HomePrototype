using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class ProductionComponent : InventoryComponent
    {
        public ItemInfo input_, output_;
        public float productionTime_, currentTime;

        public ProductionComponent(GameEntity entity, ItemInfo input, ItemInfo output, float productionTime) : base(entity, 2)
        {
            input_ = input;
            output_ = output;
            productionTime_ = productionTime;
        }

        public void update(float elapsed)
        {
            if (entity_.info.state != ObjectState.OK)
                return;

            GameEntity left = getItem(0);
            GameEntity right = getItem(1);
            if (left == null || (right != null && right.item != output_))
                return;

            if (input_ != null && left.item != input_)
                return;

            currentTime += elapsed;
            if (currentTime >= productionTime_)
            {
                currentTime = 0;
                Locator.getComponentManager().removeEntity(left);
                if (left.info.number > 1)
                {
                    left.info.number -= 1;
                }
                else
                {
                    items_[0] = null;
                }

                if (right == null)
                {
                    right = Locator.getObjectFactory().createItem(output_);
                    items_[1] = right;
                }
                else
                {
                    right.info.number++;
                }
            }
            onUpdate();
        }
    }
}
