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
            Window window = new Window(1, 1);
            window.padding_ = new Vector2(5, 0);
            window.set(0, 0, new InventoryFrame(Locator.getPlayer().inventory));

            window.pack();
            window.loc_ = new Vector2((screen_.X - window.size.X) / 2f, screen_.Y - window.size.Y);
            return window;
        }

        public Window cheaterWindow()
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

            button = new Button(Locator.getTextureManager().loadTexture("gun64"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createGun;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("mag128"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createTractor;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("furnace"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createFurnace;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("tank"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createTank;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("cchamber"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createCombChamb;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("compressor"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createCompressor;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("pump"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createPump;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("thrust"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createThruster;
            button.slot_ = num;
            buttonframe.set(0, num++, button);

            button = new Button(Locator.getTextureManager().loadTexture("reactor"), PostCategory.PLACING_OBJECT);
            button.getTarget = Locator.getObjectFactory().createReactor;
            buttonframe.set(0, num++, button);

            for (int i = num; i < 10; ++i)
            {
                button = new Button(Locator.getTextureManager().loadTexture("tile32"), PostCategory.PLACED_ITEM);
                button.slot_ = num;
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
            Window window = new InfoWindow(entity.info);

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
