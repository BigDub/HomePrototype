using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Services
{
    class ControlManager
    {
        List<UI.Window> windows;

        public void mouseClick(object sender, MouseEventArgs e)
        {
            Vector2 m_pos = Locator.getInputHandler().getMousePosition();
            switch (e.button_)
            {
                case MouseButton.LEFT:
                    break;
                case MouseButton.RIGHT:
                    foreach (UI.Window window in windows)
                    {
                        if (window.isWithin(m_pos))
                        {
                            windows.Remove(window);
                            return;
                        }
                    }
                    //TODO: If no windows captured the event, send it to the game.
                    break;
            }
        }
    }
}
