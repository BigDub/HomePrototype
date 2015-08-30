using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShipPrototype.Services
{
    class InputHandler : Interfaces.IInputHandler
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

        public void update()
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

            bool shift = ks_.IsKeyDown(Keys.LeftShift) || ks_.IsKeyDown(Keys.RightShift);
            bool ctrl = ks_.IsKeyDown(Keys.LeftControl) || ks_.IsKeyDown(Keys.RightControl);
            #region W
            if (pks_.IsKeyUp(Keys.W) && ks_.IsKeyDown(Keys.W))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.W, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.W) && ks_.IsKeyUp(Keys.W))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.W, shift, ctrl));
            }
            #endregion
            #region A
            if (pks_.IsKeyUp(Keys.A) && ks_.IsKeyDown(Keys.A))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.A, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.A) && ks_.IsKeyUp(Keys.A))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.A, shift, ctrl));
            }
            #endregion
            #region S
            if (pks_.IsKeyUp(Keys.S) && ks_.IsKeyDown(Keys.S))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.S, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.S) && ks_.IsKeyUp(Keys.S))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.S, shift, ctrl));
            }
            #endregion
            #region D
            if (pks_.IsKeyUp(Keys.D) && ks_.IsKeyDown(Keys.D))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.D, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.D) && ks_.IsKeyUp(Keys.D))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.D, shift, ctrl));
            }
            #endregion
            #region SPACE
            if (pks_.IsKeyUp(Keys.Space) && ks_.IsKeyDown(Keys.Space))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.Space, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.Space) && ks_.IsKeyUp(Keys.Space))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.Space, shift, ctrl));
            }
            #endregion
            #region ENTER
            if (pks_.IsKeyUp(Keys.Enter) && ks_.IsKeyDown(Keys.Enter))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.Enter, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.Enter) && ks_.IsKeyUp(Keys.Enter))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.Enter, shift, ctrl));
            }
            #endregion
            #region ESCAPE
            if (pks_.IsKeyUp(Keys.Escape) && ks_.IsKeyDown(Keys.Escape))
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.Escape, shift, ctrl));
            }
            if (pks_.IsKeyDown(Keys.Escape) && ks_.IsKeyUp(Keys.Escape))
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.Escape, shift, ctrl));
            }
            #endregion
            #region SHIFT
            if ((pks_.IsKeyUp(Keys.LeftShift) && pks_.IsKeyUp(Keys.RightShift)) && shift)
            {
                if (keyPress != null)
                keyPress(this, new KeyboardEventArgs(Keys.LeftShift, shift, ctrl));
            }
            if ((pks_.IsKeyDown(Keys.LeftShift) || pks_.IsKeyDown(Keys.RightShift)) && !shift)
            {
                if (keyRelease != null)
                keyRelease(this, new KeyboardEventArgs(Keys.LeftShift, shift, ctrl));
            }
            #endregion
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
            mousePress += observer;
        }
        public void removeMouseReleaseObverser(EventHandler<MouseEventArgs> observer)
        {
            mousePress -= observer;
        }
    }
}
