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

        public override Component deepCopy(GameEntity entity)
        {
            return new ProductionComponent(entity, input_, output_, productionTime_);
        }

        public void update(float elapsed)
        {
            if (entity_.info.state != ObjectState.OK)
                return;

            GameEntity left = getItem(0);
            GameEntity right = getItem(1);
            if (left == null)
            {
                if (currentTime > 0)
                {
                    currentTime = 0;
                    onUpdate();
                }
                return;
            }
            if (right != null && right.item.ID_ != output_.ID_)
                return;

            if (input_ != null && left.item.ID_ != input_.ID_)
                return;

            currentTime += elapsed;
            if (currentTime >= productionTime_)
            {
                currentTime = 0;
                Locator.getComponentManager().removeEntity(left);
                if (left.item.number_ > 1)
                {
                    left.item.number_ -= 1;
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
                    right.item.number_++;
                }
            }
            onUpdate();
        }
    }
}
