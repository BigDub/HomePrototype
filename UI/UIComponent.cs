using System;
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
        protected Vector4 margin_, padding_;

        public Vector2 size
        {
            get
            {
                return new Vector2(size.X + padding_.X + padding_.Z, size.Y + padding_.Y + padding_.W);
            }
        }
        public bool isWithin(Vector2 point)
        {
            if (point.X < loc_.X)
                return false;
            if (point.X >= loc_.X + size_.X)
                return false;
            if (point.Y < loc_.Y)
                return false;
            if (point.Y >= loc_.Y + size_.Y)
                return false;
            return true;
        }

        public virtual void pack() { }

        public virtual void click() { }

        public abstract void render(SpriteBatch spriteBatch);
    }
}
