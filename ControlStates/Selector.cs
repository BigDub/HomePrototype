using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.ControlStates
{
    class Selector : BaseState
    {
        public static float pickRange = 150f;
        public Selector()
        {
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.button_ == MouseButton.LEFT)
            {
                GameEntity en = Locator.getComponentManager().pick(Locator.getShip().tiles, mouseTile_);
                if (en != null && en.info != null && (en.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() < pickRange)
                {
                    manager_.setInfo(Locator.getWindowFactory().infoWindow(en, maus_), en);
                }
            }
        }
    }
}
