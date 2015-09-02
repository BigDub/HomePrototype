using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.ControlStates
{
    abstract class BaseState : ControllerState
    {
        protected static Services.ControlManager manager_;
        protected Point mouseTile_;
        protected Vector2 maus_;

        public static void setParent(Services.ControlManager manager)
        {
            manager_ = manager;
        }

        public virtual void changeState(ControllerState newState)
        {
            manager_.forceState(newState);
        }

        public virtual void update(float elapsed)
        {
            maus_ = Locator.getInputHandler().getMousePosition();
            Ship ship = Locator.getShip();
            Vector2 localMaus = ship.entity_.spatial.worldToLocal(maus_);
            mouseTile_ = new Point((int)((localMaus.X + 16) / 32f), (int)((localMaus.Y + 16) / 32f));
        }

        public virtual void mouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
