using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class EngineController : ControllerComponent
    {
        bool init;
        public EngineController(GameEntity e)
            : base(e)
        {
            init = false;
        }

        public override Component deepCopy(GameEntity entity)
        {
            return new EngineController(entity);
        }

        public override void update(float elapsed)
        {
            if (!init)
            {
                checkStatus();
                init = true;
            }
        }

        void checkStatus()
        {
            if (entity_.info.state == ObjectState.DAMAGED)
                return;
            bool oxygen = false;
            bool hydrogen = false;
            for (int i = 0; i < entity_.inventory.capacity; i++)
            {
                GameEntity e = entity_.inventory.getItem(i);
                if (e == null)
                    continue;
                if (e.item.ID_ == Locator.getObjectFactory().oxygenItem.ID_)
                {
                    oxygen = true;
                }
                else if (e.item.ID_ == Locator.getObjectFactory().hydrogenItem.ID_)
                {
                    hydrogen = true;
                }
            }

            if (oxygen && hydrogen)
            {
                if (entity_.info.state != ObjectState.OK)
                {
                    entity_.info.state = ObjectState.OK;
                    Locator.getMessageBoard().postMessage(new Services.Post(Services.PostCategory.REPAIRED_ENGINE));
                }
            }
            else
            {
                if (entity_.info.state != ObjectState.DISABLED)
                {
                    entity_.info.state = ObjectState.DISABLED;
                }
            }
        }

        public override void linkInput()
        {
            entity_.inventory.register(checkStatus);
        }

        public override void unlinkInput()
        {
            entity_.inventory.unregister(checkStatus);
        }
    }
}
