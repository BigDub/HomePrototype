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
        public WindowFactory(Point screen)
        {
            screen_ = screen;
        }

        Point screen_;

        public Window inventoryWindow()
        {
            Window window = new Window(Vector2.Zero, 2, 1);
            window.minimum_ = new Vector2(300, 200);
            Text text = new Text("Inventory", new Vector2(5, 0));
            text.center = false;
            window.set(0, 0, text);
            FrameComponent frame = new FrameComponent(1, 5);
            window.set(1, 0, frame);
            Button guns = new Button(Locator.getTextureManager().loadTexture("gun64"), GUIMessage.NEW_CANNON, 0.5f);
            //guns.padding_ = new Vector2(5);
            frame.set(0, 0, guns);

            Button tractor = new Button(Locator.getTextureManager().loadTexture("mag128"), GUIMessage.NEW_TRACTOR, 0.25f);
            frame.set(0, 1, tractor);

            /*
            for (int i = 2; i < 5; ++i)
            {
                Button button = new Button(Locator.getTextureManager().loadTexture("tile32"), GUIMessage.BUTTON_PRESS);
                //button.padding_ = new Vector2(5);
                frame.set(0, i, button);
            }
             */
            frame.padding_ = new Vector2(5);
            window.pack();
            window.loc_ = new Vector2(screen_.X - window.size.X, screen_.Y - window.size.Y) / 2f;
            return window;
        }

        public Window infoWindow(GameEntity entity)
        {
            Window window = new Window(Vector2.Zero, 2, 1);
            window.minimum_ = new Vector2(300, 200);
            Text name = new Text(entity.info.name, new Vector2(5, 0));
            name.center = false;
            window.set(0, 0, name);

            window.pack();
            window.loc_ = new Vector2(screen_.X - window.size.X, screen_.Y - window.size.Y) / 2f;
            return window;
        }
    }
}
