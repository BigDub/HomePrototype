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
        ItemButton[] buttons_;
        int capacity_;

        public InventoryFrame(InventoryComponent source) : base(1, 1)
        {
            padding_ = new Vector2(5);
            source_ = source;
            capacity_ = source_.capacity;
            buttons_ = new ItemButton[capacity_];
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
                buttons_[index] = new ItemButton(source_, index);
                buttons_[index].slot_ = index;
                buttons_[index].getSource = getSource;
                buttons_[index].getComponent = getComponent;
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

        public Component getComponent()
        {
            return source_;
        }

        public void refresh()
        {
            for (int index = 0; index < source_.capacity; ++index)
            {
                buttons_[index].refresh();
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
