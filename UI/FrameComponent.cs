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

        public FrameComponent(int rows, int columns, float padding = 0)
        {
            rows_ = rows;
            columns_ = columns;
            children_ = new UIComponent[rows_, columns_];
            padding_ = new Vector2(padding);
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
            if (minimum_ != null && minimum_ != Vector2.Zero)
            {
                Vector2 remaining = minimum_ - 2 * padding_;
                for (int col = 0; col < columns_; col++)
                {
                    remaining.X -= columnWidths[col];
                }
                for (int row = 0; row < rows_; row++)
                {
                    remaining.Y -= rowHeights[row];
                }
                if (remaining.X > 0)
                {
                    float delta = remaining.X / columns_;
                    for (int col = 0; col < columns_; col++)
                    {
                        columnWidths[col] += delta;
                    }
                }
                if (remaining.Y > 0)
                {
                    float delta = remaining.Y / rows_;
                    for (int row = 0; row < rows_; row++)
                    {
                        rowHeights[row] += delta;
                    }
                }
            }
            Vector2 cursor = Vector2.Zero;
            for (int row = 0; row < rows_; ++row)
            {
                cursor.X = 0;
                for (int col = 0; col < columns_; ++col)
                {
                    if (children_[row, col] != null)
                    {
                        children_[row, col].loc_ = cursor;
                        bool centerX = children_[row, col].centerX;
                        bool centerY = children_[row, col].centerY;
                        bool fill = children_[row, col].fill;
                        Vector2 size = children_[row, col].size;
                        if (fill)
                        {
                            children_[row, col].minimum_ = new Vector2(columnWidths[col], rowHeights[row]);
                            children_[row, col].pack();
                        }
                        else
                        {
                            if (centerX && columnWidths[col] > size.X)
                            {
                                children_[row, col].loc_.X += (columnWidths[col] - size.X) / 2.0f;
                            }
                            if (centerY && rowHeights[row] > size.Y)
                            {
                                children_[row, col].loc_.Y += (rowHeights[row] - size.Y) / 2.0f;
                            }
                        }
                    }
                    cursor.X += columnWidths[col];
                }
                cursor.Y += rowHeights[row];
            }
            size_ = Vector2.Zero;
            for (int row = 0; row < rows_; ++row)
            {
                size_.Y += rowHeights[row];
            }
            for (int col = 0; col < columns_; ++col)
            {
                size_.X += columnWidths[col];
            }
            base.pack();
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
