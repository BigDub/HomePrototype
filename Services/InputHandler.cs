using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShipPrototype.Services
{
    class InputHandler
    {
        static InputHandler instance = null;
        public static InputHandler init()
        {
            Debug.Assert(instance == null);

            instance = new InputHandler();
            return instance;
        }

        MouseState pms_, ms_;
        Vector2 mouseDown = new Vector2(0, 0), mousePosition = new Vector2(0, 0);
        KeyboardState pks_, ks_;

        public event EventHandler<KeyboardEventArgs> keyPress, keyRelease;
        public event EventHandler<MouseEventArgs> mousePress, mouseRelease;

        public void update(float elapsed)
        {
            #region MOUSE
            pms_ = ms_;
            ms_ = Mouse.GetState();
            mousePosition.X = ms_.X;
            mousePosition.Y = ms_.Y;
            #region LEFT
            if (pms_.LeftButton == ButtonState.Released && ms_.LeftButton == ButtonState.Pressed)
            {
                if (mousePress != null)
                {
                    mousePress(this, new MouseEventArgs(MouseButton.LEFT));
                }
                mouseDown.X = ms_.X;
                mouseDown.Y = ms_.Y;
            }
            if (pms_.LeftButton == ButtonState.Pressed && ms_.LeftButton == ButtonState.Released)
            {
                if (mouseRelease != null)
                {
                    mouseRelease(this, new MouseEventArgs(MouseButton.LEFT));
                }
            }
            #endregion
            #region RIGHT
            if (pms_.RightButton == ButtonState.Released && ms_.RightButton == ButtonState.Pressed)
            {
                if (mousePress != null)
                {
                    mousePress(this, new MouseEventArgs(MouseButton.RIGHT));
                }
            }
            if (pms_.RightButton == ButtonState.Pressed && ms_.RightButton == ButtonState.Released)
            {
                if (mouseRelease != null)
                {
                    mouseRelease(this, new MouseEventArgs(MouseButton.RIGHT));
                }
            }
            #endregion
            #endregion
            #region KEYBOARD
            pks_ = ks_;
            ks_ = Keyboard.GetState();

            var values = Enum.GetValues(typeof(Keys));

            bool shift = ks_.IsKeyDown(Keys.LeftShift) || ks_.IsKeyDown(Keys.RightShift);
            bool ctrl = ks_.IsKeyDown(Keys.LeftControl) || ks_.IsKeyDown(Keys.RightControl);
            foreach (var value in values)
            {
                Keys key = (Keys)Enum.ToObject(typeof(Keys), value);
                if (pks_.IsKeyUp(key) && ks_.IsKeyDown(key))
                {
                    if (keyPress != null)
                    {
                        keyPress(this, new KeyboardEventArgs(key, shift, ctrl));
                    }
                }
                if (pks_.IsKeyDown(key) && ks_.IsKeyUp(key))
                {
                    if (keyRelease != null)
                    {
                        keyRelease(this, new KeyboardEventArgs(key, shift, ctrl));
                    }
                }
            }
            #endregion
        }

        public bool isKeyDown(Keys key)
        {
            return ks_.IsKeyDown(key);
        }
        
        public Vector2 getMousePosition()
        {
            return mousePosition;
        }
        public Vector2 getMouseDown()
        {
            return mouseDown;
        }

        public void addKeyPressObserver(EventHandler<KeyboardEventArgs> observer)
        {
            keyPress += observer;
        }
        public void removeKeyPressObserver(EventHandler<KeyboardEventArgs> observer)
        {
            keyPress -= observer;
        }
       
        public void addKeyReleaseObserver(EventHandler<KeyboardEventArgs> observer)
        {
            keyRelease += observer;
        }
        public void removeKeyReleaseObserver(EventHandler<KeyboardEventArgs> observer)
        {
            keyRelease -= observer;
        }
        
        public void addMousePressObserver(EventHandler<MouseEventArgs> observer)
        {
            mousePress += observer;
        }
        public void removeMousePressObverser(EventHandler<MouseEventArgs> observer)
        {
            mousePress -= observer;
        }
        
        public void addMouseReleaseObserver(EventHandler<MouseEventArgs> observer)
        {
            mouseRelease += observer;
        }
        public void removeMouseReleaseObverser(EventHandler<MouseEventArgs> observer)
        {
            mouseRelease -= observer;
        }
    }
}
