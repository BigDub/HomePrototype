using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class InventoryFrame : FrameComponent
    {
        private static int maxPerLine = 10;
        InventoryComponent source_;
        FrameComponent inner_;
        Button[] buttons_;
        int capacity_;

        public InventoryFrame(InventoryComponent source) : base(1, 1)
        {
            padding_ = new Vector2(5);
            source_ = source;
            capacity_ = source_.capacity;
            buttons_ = new Button[capacity_];
            int rows = 1;
            int cols = capacity_;
            if (capacity_ > maxPerLine)
            {
                rows = capacity_ / maxPerLine;
                cols = maxPerLine;
            }
            inner_ = new FrameComponent(rows, cols);
            set(0, 0, inner_);
            int row = 0;
            int col = 0;
            for (int index = 0; index < capacity_; ++index)
            {
                buttons_[index] = new Button(Locator.getTextureManager().loadTexture("tile32"), Services.PostCategory.PLACED_ITEM);
                buttons_[index].slot_ = index;
                buttons_[index].getSource = getSource;
                inner_.set(row, col++, buttons_[index]);
                if (col >= maxPerLine)
                {
                    ++row;
                    col = 0;
                }
            }

            refresh();
            source_.register(refresh);
        }

        public GameEntity getSource(int num)
        {
            return source_.entity_;
        }

        public void refresh()
        {
            for (int index = 0; index < source_.capacity; ++index)
            {
                GameEntity e = source_.getItem(index);
                if (e == null)
                {
                    buttons_[index].texture_id_ = Locator.getTextureManager().loadTexture("tile32");
                    buttons_[index].category_ = Services.PostCategory.PLACED_ITEM;
                    buttons_[index].getTarget = null;
                }
                else
                {
                    buttons_[index].texture_id_ = e.info.itemTex;
                    buttons_[index].category_ = Services.PostCategory.REQUEST_ITEM;
                    buttons_[index].getTarget = source_.getItem;
                }
            }
        }

        public override void cleanup()
        {
            if (source_ != null)
                source_.unregister(refresh);
            base.cleanup();
        }
    }
}
