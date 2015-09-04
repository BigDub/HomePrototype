using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class ProductionFrame : FrameComponent
    {
        ProductionComponent source_;
        Button[] buttons_;
        ProgressBar bar_;

        public ProductionFrame(ProductionComponent source) : base(1, 3)
        {
            padding_ = new Vector2(5);
            source_ = source;
            buttons_ = new Button[2];
            for (int index = 0; index < 2; ++index)
            {
                buttons_[index] = new Button(Locator.getTextureManager().loadTexture("tile32"), Services.PostCategory.PLACED_ITEM);
                buttons_[index].slot_ = index;
                buttons_[index].getSource = getSource;
            }
            bar_ = new ProgressBar();

            set(0, 0, buttons_[0]);
            set(0, 1, bar_);
            set(0, 2, buttons_[1]);

            refresh();
            source_.register(refresh);
        }

        public GameEntity getSource(int num)
        {
            return source_.entity_;
        }

        public void refresh()
        {
            for (int index = 0; index < 2; ++index)
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
                    buttons_[index].texture_id_ = e.item.tex_;
                    buttons_[index].category_ = Services.PostCategory.REQUEST_ITEM;
                    buttons_[index].getTarget = source_.getItem;
                }
            }
            bar_.percent = source_.currentTime / source_.productionTime_;
        }

        public override void cleanup()
        {
            if (source_ != null)
                source_.unregister(refresh);
            base.cleanup();
        }
    }
}
