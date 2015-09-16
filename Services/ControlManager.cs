﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ShipPrototype.UI;
using ShipPrototype.ControlStates;

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

namespace ShipPrototype.Services
{
    class ControlManager
    {
        public List<Window> windows;

        Window inventory;
        Window info;

        GameEntity observing;

        ControllerState state;

        public ControlManager()
        {
            windows = new List<Window>();
            Locator.getMessageBoard().register(onPost);
        }

        public void onPost(Services.Post post)
        {
            state.onPost(post);
        }

        public void forceState(ControllerState newState)
        {
            state = newState;
        }

        public void setInventory(Window window)
        {
            if (inventory != null)
            {
                windows.Remove(inventory);
            }
            inventory = window;
            add(window);
        }

        public void setInfo(Window window, GameEntity e)
        {
            observing = e;
            if (info != null)
            {
                windows.Remove(info);
            }
            info = window;
            add(window);
        }

        public void add(Window window)
        {
            windows.Add(window);
        }

        public void update(float elapsed)
        {
            if (state != null)
            {
                state.update(elapsed);
            }
            foreach (Window window in windows)
            {
                window.update(elapsed);
            }
            if (info != null && observing != null)
            {
                if ((observing.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() > BaseState.interactRange)
                {
                    windows.Remove(info);
                    info = null;
                    observing = null;
                }
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            Vector2 m_pos = Locator.getInputHandler().getMousePosition();
            foreach (Window window in windows)
            {
                if (window.isWithin(m_pos))
                {
                    switch (e.button_)
                    {
                        case MouseButton.LEFT:
                            window.click(m_pos);
                            break;
                        case MouseButton.RIGHT:
                            if (window != inventory)
                            {
                                windows.Remove(window);
                            }
                            break;
                    }
                    return;
                }
            }
            if (state != null)
            {
                state.mouseUp(sender, e);
            }
        }

        public void keyRelease(object sender, KeyboardEventArgs e)
        {
            if (e.key_ == Keys.F1)
            {
                GameEntity entity = Locator.getObjectFactory().createWreck();
                entity.spatial.translation_ = new Vector2(1800, 1000);
                entity.physic.velocity_ = new Vector2(0, -100);
                Locator.getComponentManager().addEntity(entity);
                Locator.getMessageBoard().postMessage(new Post(PostCategory.JUNK_SPAWN, entity, null, null, 0));
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
            state.render(spriteBatch);
            spriteBatch.End();
        }
    }
}
