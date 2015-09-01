using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ShipPrototype.UI;

namespace ShipPrototype
{
    enum GUIMessage
    {
        BUTTON_PRESS
    };
}

namespace ShipPrototype.Services
{
    class ControlManager
    {
        List<Window> windows;

        Window inventory;

        ControlStates.ControllerState state;

        public ControlManager()
        {
            windows = new List<UI.Window>();
        }

        public void forceState(ControlStates.ControllerState newState)
        {
            state = newState;
        }

        public void add(UI.Window window)
        {
            windows.Add(window);
        }

        public void update(float elapsed)
        {
            if (state != null)
            {
                state.update(elapsed);
            }
            foreach (UI.Window window in windows)
            {
                window.update(elapsed);
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            Vector2 m_pos = Locator.getInputHandler().getMousePosition();
            foreach (UI.Window window in windows)
            {
                if (window.isWithin(m_pos))
                {
                    switch (e.button_)
                    {
                        case MouseButton.LEFT:
                            window.click(m_pos);
                            break;
                        case MouseButton.RIGHT:
                            if (window == inventory)
                            {
                                inventory = null;
                            }
                            windows.Remove(window);
                            break;
                    }
                    return;
                }
            }
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
                    if (state != null)
                    {
                        state.mouseUp(sender, e);
                    }
                    break;
            }
        }

        public void keyRelease(object sender, KeyboardEventArgs e)
        {
            if (e.key_ == Keys.I)
            {
                if (inventory == null)
                {
                    inventory = WindowFactory.inventoryWindow();
                    windows.Insert(0, inventory);
                }
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
            foreach (UI.Window window in windows)
            {
                window.render(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
