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
            Window window = new Window(2, 1);
            window.padding_ = new Vector2(5, 0);
            Text text = new Text("Inventory", true);
            text.center = false;
            window.set(0, 0, text);
            FrameComponent buttonframe = new FrameComponent(2, 10);
            window.set(1, 0, buttonframe);

            int num = 0;
            Button button;

            button = new Button(Locator.getTextureManager().loadTexture("gun64"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createGun);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("mag128"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createTractor);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("furnace"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createFurnace);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("tank"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createTank);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("cchamber"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createCombChamb);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("compressor"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createCompressor);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("pump"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createPump);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("thrust"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createThruster);
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("reactor"), GUIMessage.START_PLACEMENT);
            button.setSpawn(Locator.getObjectFactory().createReactor);
            buttonframe.set(0, num++, button);

            for (int i = num; i < 10; ++i)
            {
                button = new Button(Locator.getTextureManager().loadTexture("tile32"), GUIMessage.BUTTON_PRESS);
                //button.padding_ = new Vector2(5);
                buttonframe.set(0, i, button);
            }

            for (int i = 0; i < 10; ++i)
            {
                Text label = new Text("" + ((i + 1) % 10), false);
                buttonframe.set(1, i, label);
            }

            buttonframe.padding_ = new Vector2(5, 0);
            window.pack();
            window.loc_ = new Vector2((screen_.X - window.size.X) / 2f, screen_.Y - window.size.Y);
            return window;
        }

        public Window infoWindow(GameEntity entity, Vector2 point)
        {
            Window window = new Window(2, 1);
            window.padding_ = new Vector2(5);
            window.minimum_ = new Vector2(200, 50);
            Text name = new Text(entity.info.name, true);
            name.center = false;
            window.set(0, 0, name);

            Window frame = new Window(1, 2);
            window.padding_ = new Vector2(5);
            frame.color_ = new Color(0, 0, 0, 255);
            Text status = new Text("STATUS...", false);
            status.center = false;
            frame.set(0, 0, status);
            switch (entity.info.state)
            {
                case Components.ObjectState.OK:
                    status = new Text("ONLINE", false);
                    status.color_ = Color.LightGreen;
                    break;
                case Components.ObjectState.DISABLED:
                    status = new Text("OFFLINE", false);
                    status.color_ = Color.Yellow;
                    break;
                case Components.ObjectState.DAMAGED:
                    status = new Text("DAMAGED", false);
                    status.color_ = Color.Red;
                    break;
            }
            status.center = false;
            frame.set(0, 1, status);
            window.set(1, 0, frame);

            window.pack();
            window.loc_ = point;
            if (window.loc_.X + window.size.X > screen_.X)
            {
                window.loc_.X = screen_.X - window.size.X;
            }
            if (window.loc_.Y + window.size.Y > screen_.Y)
            {
                window.loc_.Y = screen_.Y - window.size.Y;
            }
            if (window.loc_.X < 0)
            {
                window.loc_.X = 0;
            }
            if (window.loc_.Y < 0)
            {
                window.loc_.Y = 0;
            }
            return window;
        }
    }
}
