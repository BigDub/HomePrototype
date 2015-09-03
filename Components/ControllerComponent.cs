using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    abstract class ControllerComponent
    {
        public GameEntity entity_;

        public ControllerComponent(GameEntity entity)
        {
            entity_ = entity;
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
