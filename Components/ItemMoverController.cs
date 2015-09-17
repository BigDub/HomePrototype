using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    abstract class ItemMoverController : ControllerComponent
    {
        public InventoryComponent input_;
        public InventoryComponent output_;
        public float currentTime_, processTime_;

        protected ItemMoverController(GameEntity e)
            : base(e)
        {
        }

        public override void update(float elapsed)
        {
            if (
                entity_.info.state != ObjectState.OK ||
                input_ == null || input_.entity_.info.state != ObjectState.OK ||
                output_ == null || output_.entity_.info.state != ObjectState.OK
                )
                return;

            int itemIndex = input_.getFirstItem();
            GameEntity item = input_.getItem(itemIndex);
            if (item == null)
            {
                currentTime_ = 0;
                return;
            }

            if (!output_.canPlaceItem(item) || output_.getNumItem(item.item) >= output_.numberPerSlot_)
            {
                currentTime_ = 0;
                return;
            }

            currentTime_ += elapsed;

            if (currentTime_ >= processTime_)
            {
                currentTime_ = 0;
                output_.offerItem(Locator.getObjectFactory().createItem(item.item, 1));
                if (item.item.number_ > 1)
                {
                    item.item.number_--;
                    input_.onUpdate();
                }
                else
                {
                    input_.takeItem(itemIndex);
                }
            }
            if (notify_ != null)
                notify_();
        }
    }
}
