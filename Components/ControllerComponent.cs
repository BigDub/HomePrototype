using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    abstract class ControllerComponent : Component
    {
        public bool isDrawn;
        
        public delegate void Notify();
        public Notify notify_;

        protected ControllerComponent(GameEntity entity)
            : base(entity)
        {
            isDrawn = false;
        }

        public void register(Notify notify)
        {
            notify_ += notify;
        }
        public void unregister(Notify notify)
        {
            notify_ -= notify;
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

        public virtual UI.FrameComponent getFrame()
        {
            return null;
        }
    }
}
