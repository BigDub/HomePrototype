using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class StatusFrame : FrameComponent
    {
        public InfoComponent source_;
        public Text statusText;

        public StatusFrame(InfoComponent source)
            : base(1, 2)
        {
            source_ = source;
            padding_ = new Vector2(5);
            Text status = new Text("STATUS...", false);
            status.center = false;
            set(0, 0, status);
            statusText = new Text("", false);
            set(0, 1, statusText);

            refresh();
            source_.register(refresh);
        }

        public void refresh()
        {
            switch (source_.state)
            {
                case ObjectState.OK:
                    statusText.text_ = "ONLINE";
                    statusText.color_ = Color.LightGreen;
                    break;
                case ObjectState.DISABLED:
                    statusText.text_ = "OFFLINE";
                    statusText.color_ = Color.Yellow;
                    break;
                case ObjectState.DAMAGED:
                    statusText.text_ = "DAMAGED";
                    statusText.color_ = Color.Red;
                    break;
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
