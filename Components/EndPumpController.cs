using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class EndPumpController : ControllerComponent
    {
        public EndPumpController(GameEntity entity)
            : base(entity)
        {
        }

        public override Component deepCopy(GameEntity entity)
        {
            return new EndPumpController(entity);
        }

        public override void update(float elapsed)
        {
            if (entity_.info.state == ObjectState.OK)
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
                Locator.getMessageBoard().postMessage(new Services.Post(Services.PostCategory.END_GAME, null, null, null, 0));
                Locator.getShip().end();
            }
        }
    }
}
