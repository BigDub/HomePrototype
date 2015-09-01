﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    abstract class UIComponent
    {
        public UIComponent parent_;

        public Vector2 loc_;
        protected Vector2 size_;
        //Left, top, right, bottom
        public Vector2 padding_;
        public bool center = true;

        public Vector2 loc
        {
            get
            {
                if (parent_ != null)
                {
                    return parent_.loc + loc_ + padding_;
                }
                else
                {
                    return loc_ + padding_;
                }
            }
        }
        public Vector2 size
        {
            get
            {
                return new Vector2(size_.X + 2 * padding_.X, size_.Y + 2 * padding_.Y);
            }
        }
        public bool isWithin(Vector2 point)
        {
            if (point.X < loc_.X)
                return false;
            if (point.X >= loc_.X + size.X)
                return false;
            if (point.Y < loc_.Y)
                return false;
            if (point.Y >= loc_.Y + size.Y)
                return false;
            return true;
        }

        public virtual void pack() { }

        public virtual void click(Vector2 pos) { }

        public virtual void update(float elapsed) { }

        public abstract void render(SpriteBatch spriteBatch);
    }
}
