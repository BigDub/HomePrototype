using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.UI;

namespace ShipPrototype.Services
{
    class WindowFactory
    {
        public static Window inventoryWindow()
        {
            Window window = new Window(Vector2.Zero, 2, 1);
            Text text = new Text("Inventory", Vector2.Zero);
            text.center = false;
            window.set(0, 0, text);
            FrameComponent frame = new FrameComponent(1, 5);
            window.set(1, 0, frame);
            Button guns = new Button(Locator.getTextureManager().loadTexture("gun64"), GUIMessage.BUTTON_PRESS, 0.5f);
            guns.padding_ = new Vector2(5);
            frame.set(0, 0, guns);
            for (int i = 1; i < 5; ++i)
            {
                Button button = new Button(1, GUIMessage.BUTTON_PRESS);
                button.padding_ = new Vector2(5);
                frame.set(0, i, button);
            }
            frame.padding_ = new Vector2(5);
            window.pack();
            return window;
        }
    }
}
