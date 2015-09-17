using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class ItemMoverFrame : FrameComponent
    {
        ItemMoverController source_;
        ProgressBar bar_;

        public ItemMoverFrame(ItemMoverController source)
            : base(1, 1)
        {
            source_ = source;
            source_.register(update);

            bar_ = new ProgressBar();
            set(0, 0, bar_);
        }
        ~ItemMoverFrame()
        {
            source_.unregister(update);
        }

        public void update()
        {
            bar_.percent = source_.currentTime_ / source_.processTime_;
        }
    }
}
