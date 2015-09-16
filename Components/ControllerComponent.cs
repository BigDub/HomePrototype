using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    abstract class ControllerComponent : Component
    {
        public ControllerComponent(GameEntity entity)
            : base(entity)
        {
        }

        public virtual void update(float elapsed)
        {
        }

        public virtual void linkInput()
        {
        }

        public virtual void unlinkInput()
        {
        }
    }
}
