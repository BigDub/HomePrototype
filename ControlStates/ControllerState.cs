using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShipPrototype.Services;


namespace ShipPrototype.ControlStates
{
    interface ControllerState
    {
        void setParent(ControlManager manager);

        void reset();

        void changeState(ControllerState newState);

        void update(float elapsed);

        void mouseUp(object sender, MouseEventArgs e);
    }
}
