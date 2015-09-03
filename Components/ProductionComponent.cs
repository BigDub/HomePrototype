using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class ProductionComponent : InventoryComponent
    {
        public GameEntity entity_;

        InfoComponent input_, output_;
        float productionTime_, currentTime;

        public ProductionComponent(GameEntity entity, InfoComponent input, InfoComponent output, float productionTime) : base(entity, 2)
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
            if (left == null || right != null || left.info.itemTex != input_.itemTex)
                return;

            currentTime += elapsed;
            if (currentTime >= productionTime_)
            {
                currentTime = 0;
                Locator.getComponentManager().removeEntity(left);
                items_[0] = null;
                right = new GameEntity();
                right.info = new InfoComponent(right, output_);
                items_[1] = right;
            }
            onUpdate();
        }
    }
}
