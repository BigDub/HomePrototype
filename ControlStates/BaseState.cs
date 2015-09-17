using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Services;
using ShipPrototype.UI;

namespace ShipPrototype.ControlStates
{
    abstract class BaseState : ControllerState
    {
        public static float interactRange = 150f;
        protected static Services.ControlManager manager_;
        protected Point mouseTile_;
        protected Vector2 maus_;
        protected bool mouseInWindow_;

        public static void setParent(ControlManager manager)
        {
            manager_ = manager;
        }

        public virtual void changeState(ControllerState newState)
        {
            manager_.forceState(newState);
        }

        public virtual void update(float elapsed)
        {
            maus_ = Locator.getInputHandler().getMousePosition();
            Ship ship = Locator.getShip();
            Vector2 localMaus = ship.entity_.spatial.worldToLocal(maus_);
            mouseTile_ = new Point((int)((localMaus.X + 16) / 32f), (int)((localMaus.Y + 16) / 32f));
        }

        public virtual void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.button_ == MouseButton.LEFT)
            {
                GameEntity en = Locator.getComponentManager().pick(Locator.getShip().tiles, mouseTile_);
                if (en != null && en.info != null && (en.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() < interactRange)
                {
                    manager_.setInfo(Locator.getWindowFactory().infoWindow(en, maus_), en);
                }
            }
        }

        public virtual void mouseDown(object sender, MouseEventArgs e)
        {
        }

        public virtual void onPost(Services.Post post)
        {
            switch (post.category)
            {
                case PostCategory.PLACING_OBJECT:
                    changeState(new ControlStates.Placer(post.targetEntity));
                    break;
                default:
                    break;
            }
        }

        public void setMouseInWindow(bool b)
        {
            mouseInWindow_ = b;
        }

        public virtual void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
            foreach (Window window in manager_.windows)
            {
                window.render(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
