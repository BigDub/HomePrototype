using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShipPrototype.Services;

namespace ShipPrototype
{
    class Locator
    {
        private static ScreenPrinter printer_;
        private static InputHandler input_;
        private static TextureManager texture_;
        private static ComponentManager component_;
        private static Random random_;
        private static ControlManager control_;
        private static Ship ship_;
        private static ObjectFactory factory_;
        private static WindowFactory window_;
        private static GameEntity world_;
        private static GameEntity player_;
        private static MessageBoard mboard_;

        public static void provide(ScreenPrinter printer)
        {
            printer_ = printer;
        }
        public static void provide(InputHandler input)
        {
            input_ = input;
        }
        public static void provide(TextureManager texture)
        {
            texture_ = texture;
        }
        public static void provide(ComponentManager component)
        {
            component_ = component;
        }
        public static void provide(Random random)
        {
            random_ = random;
        }
        public static void provide(ControlManager control)
        {
            control_ = control;
        }
        public static void provide(Ship ship)
        {
            ship_ = ship;
        }
        public static void provide(ObjectFactory factory)
        {
            factory_ = factory;
        }
        public static void provide(WindowFactory window)
        {
            window_ = window;
        }
        public static void provideWorld(GameEntity world)
        {
            world_ = world;
        }
        public static void providePlayer(GameEntity player)
        {
            player_ = player;
        }
        public static void provide(MessageBoard mb)
        {
            mboard_ = mb;
        }

        public static ScreenPrinter getScreenPrinter()
        {
            return printer_;
        }
        public static InputHandler getInputHandler()
        {
            return input_;
        }
        public static TextureManager getTextureManager()
        {
            return texture_;
        }
        public static ComponentManager getComponentManager()
        {
            return component_;
        }
        public static Random getRandom()
        {
            return random_;
        }
        public static ControlManager getControlManager()
        {
            return control_;
        }
        public static Ship getShip()
        {
            return ship_;
        }
        public static ObjectFactory getObjectFactory()
        {
            return factory_;
        }
        public static WindowFactory getWindowFactory()
        {
            return window_;
        }
        public static GameEntity getWorld()
        {
            return world_;
        }
        public static GameEntity getPlayer()
        {
            return player_;
        }
        public static MessageBoard getMessageBoard()
        {
            return mboard_;
        }
    }
}
