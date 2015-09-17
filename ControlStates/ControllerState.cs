using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Services;


namespace ShipPrototype.ControlStates
{
    interface ControllerState
    {
        void changeState(ControllerState newState);

        void update(float elapsed);

        void mouseUp(object sender, MouseEventArgs e);
        void mouseDown(object sender, MouseEventArgs e);

        void onPost(Services.Post post);

        void render(SpriteBatch spriteBatch);

        void setMouseInWindow(bool b);
    }
}
