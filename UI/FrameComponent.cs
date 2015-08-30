using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.UI
{
    abstract class FrameComponent : UIComponent
    {
        protected List<UIComponent> children_;

        public override void pack()
        {
            Vector2 cursor = new Vector2(padding_.X, padding_.Y);
            foreach (UIComponent child in children_)
            {
                child.pack();
            }
            size_.X = padding_.X + padding_.Z;
            float max_height = 0;
            foreach (UIComponent child in children_)
            {
                child.loc_ = cursor;
                Vector2 size = child.size;
                if (size.Y > max_height)
                {
                    max_height = size.Y;
                }
                size_.X += size.X;
                cursor.X += size.X;
            }
            size_.Y = max_height + padding_.Y + padding_.W;
        }

        protected void add(UIComponent child)
        {
            children_.Add(child);
            child.parent_ = this;
        }
    }
}
