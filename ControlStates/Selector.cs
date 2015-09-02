using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.ControlStates
{
    class Selector : BaseState
    {
        public Selector()
        {
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.button_ == MouseButton.LEFT)
            {
                GameEntity en = Locator.getComponentManager().pick(Locator.getShip().tiles, mouseTile_);
                if (en != null && en.info != null)
                {
                    manager_.add(Locator.getWindowFactory().infoWindow(en));
                }
            }
        }
    }
}
