using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShipPrototype
{
    enum MouseButton
    {
        LEFT = 0,
        RIGHT = 1
    };

    class KeyboardEventArgs : EventArgs
    {
        public Keys key_;
        public bool shift_;
        public bool ctrl_;

        public KeyboardEventArgs(Keys key, bool shift, bool ctrl)
        {
            key_ = key;
            shift_ = shift;
            ctrl_ = ctrl;
        }
    }

    class MouseEventArgs : EventArgs
    {
        public MouseButton button_;

        public MouseEventArgs(MouseButton button)
        {
            button_ = button;
        }
    }
}
namespace ShipPrototype.Interfaces
{
    interface IInputHandler
    {
        void update(float elapsed);

        bool isKeyDown(Keys key);

        Vector2 getMousePosition();
        Vector2 getMouseDown();

        void addKeyPressObserver(EventHandler<KeyboardEventArgs> observer);
        void removeKeyPressObserver(EventHandler<KeyboardEventArgs> observer);

        void addKeyReleaseObserver(EventHandler<KeyboardEventArgs> observer);
        void removeKeyReleaseObserver(EventHandler<KeyboardEventArgs> observer);

        void addMousePressObserver(EventHandler<MouseEventArgs> observer);
        void removeMousePressObverser(EventHandler<MouseEventArgs> observer);

        void addMouseReleaseObserver(EventHandler<MouseEventArgs> observer);
        void removeMouseReleaseObverser(EventHandler<MouseEventArgs> observer);
    }
}
