using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShipPrototype.Services
{
    class NullInputHandler : Interfaces.IInputHandler
    {
        public NullInputHandler()
        {
            //TODO: Log that the null service has been used.
        }

        public void update(float elapsed) { }

        public bool isKeyDown(Keys key)
        {
            return false;
        }

        public Vector2 getMousePosition()
        {
            return Vector2.Zero;
        }

        public Vector2 getMouseDown()
        {
            return Vector2.Zero;
        }

        public void addKeyPressObserver(EventHandler<KeyboardEventArgs> observer) { }
        public void removeKeyPressObserver(EventHandler<KeyboardEventArgs> observer) { }

        public void addKeyReleaseObserver(EventHandler<KeyboardEventArgs> observer) { }
        public void removeKeyReleaseObserver(EventHandler<KeyboardEventArgs> observer) { }

        public void addMousePressObserver(EventHandler<MouseEventArgs> observer) { }
        public void removeMousePressObverser(EventHandler<MouseEventArgs> observer) { }

        public void addMouseReleaseObserver(EventHandler<MouseEventArgs> observer) { }
        public void removeMouseReleaseObverser(EventHandler<MouseEventArgs> observer) { }
    }
}
