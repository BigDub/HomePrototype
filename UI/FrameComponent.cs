using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.UI
{
    class FrameComponent : UIComponent
    {
        protected UIComponent[,] children_;
        protected int rows_;
        protected int columns_;

        public FrameComponent(int rows, int columns)
        {
            rows_ = rows;
            columns_ = columns;
            children_ = new UIComponent[rows_, columns_];
        }

        public override void click(Vector2 pos)
        {
            pos -= loc_;
            pos -= padding_;
            foreach (UIComponent child in children_)
            {
                if (child == null)
                    continue;
                if (child.isWithin(pos))
                {
                    child.click(pos);
                    return;
                }
            }
        }
        public override void pack()
        {
            float[] rowHeights = new float[rows_];
            float[] columnWidths = new float[columns_];
            Vector2 cursor = new Vector2(padding_.X, padding_.Y);
            for (int row = 0; row < rows_; ++row)
            {
                for (int col = 0; col < columns_; ++col)
                {
                    if (children_[row, col] != null)
                    {
                        children_[row, col].pack();
                        Vector2 size = children_[row, col].size;
                        if (size.X > columnWidths[col])
                            columnWidths[col] = size.X;
                        if (size.Y > rowHeights[row])
                            rowHeights[row] = size.Y;
                    }
                }
            }
            size_ = padding_ * 2;
            for (int row = 0; row < rows_; ++row)
            {
                cursor.X = padding_.X;
                for (int col = 0; col < columns_; ++col)
                {
                    if (children_[row, col] != null)
                    {
                        children_[row, col].loc_ = cursor;
                        bool center = children_[row, col].center;
                        Vector2 size = children_[row, col].size;
                        if (center && columnWidths[col] > size.X)
                        {
                            children_[row, col].loc_.X += (columnWidths[col] - size.X) / 2.0f;
                        }
                        if (center && rowHeights[row] > size.Y)
                        {
                            children_[row, col].loc_.Y += (rowHeights[row] - size.Y) / 2.0f;
                        }
                    }
                    cursor.X += columnWidths[col];
                }
                cursor.Y += rowHeights[row];
            }
            for (int row = 0; row < rows_; ++row)
            {
                size_.Y += rowHeights[row];
            }
            for (int col = 0; col < columns_; ++col)
            {
                size_.X += columnWidths[col];
            }
        }

        public override void update(float elapsed)
        {
            for (int row = 0; row < rows_; ++row)
            {
                for (int col = 0; col < columns_; ++col)
                {
                    if (children_[row, col] != null)
                    {
                        children_[row, col].update(elapsed);
                    }
                }
            }
        }

        public void set(int row, int col, UIComponent child)
        {
            children_[row, col] = child;
            child.parent_ = this;
        }

        public override void render(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < rows_; ++row)
            {
                for (int col = 0; col < columns_; ++col)
                {
                    if (children_[row, col] != null)
                    {
                        children_[row, col].render(spriteBatch);
                    }
                }
            }
 	    }

        public override void cleanup()
        {
            for (int row = 0; row < rows_; ++row)
            {
                for (int col = 0; col < columns_; ++col)
                {
                    if (children_[row, col] != null)
                    {
                        children_[row, col].cleanup();
                    }
                }
            }
        }
    }
}
