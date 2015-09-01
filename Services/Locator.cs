using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShipPrototype.Interfaces;
using ShipPrototype.Services;

namespace ShipPrototype
{
    class Locator
    {
        private static IScreenPrinter printer_;
        private static IInputHandler input_;
        private static ITextureManager texture_;
        private static IComponentManager component_;
        private static Random random_;
        private static ControlManager control_;
        private static Ship ship_;

        public static void provide(IScreenPrinter printer)
        {
            printer_ = printer;
        }
        public static void provide(IInputHandler input)
        {
            input_ = input;
        }
        public static void provide(ITextureManager texture)
        {
            texture_ = texture;
        }
        public static void provide(IComponentManager component)
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

        public static IScreenPrinter getScreenPrinter()
        {
            return printer_;
        }
        public static IInputHandler getInputHandler()
        {
            return input_;
        }
        public static ITextureManager getTextureManager()
        {
            return texture_;
        }
        public static IComponentManager getComponentManager()
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
    }
}
