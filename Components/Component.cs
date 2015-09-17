using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    abstract class Component
    {
        public GameEntity entity_;

        public Component(GameEntity entity)
        {
            entity_ = entity;
        }

        public abstract Component deepCopy(GameEntity entity);
    }
}
