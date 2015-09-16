using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Components;

namespace ShipPrototype.UI
{
    class InfoWindow : Window
    {
        public InfoWindow(InfoComponent info)
            : base(2, 1)
        {
            padding_ = new Vector2(5);
            minimum_ = new Vector2(100, 50);
            Text name = new Text(info.name, true);
            name.center = false;
            set(0, 0, name);
            
            int subsections = 1;
            if (info.entity_.inventory != null)
            {
                ++subsections;
            }
            if (info.entity_.production != null)
            {
                ++subsections;
            }
            Window subFrames = new Window(subsections, 1);
            subFrames.color_ = new Color(0, 0, 0, 255);
            int subIndex = 0;

            subFrames.set(subIndex++, 0, new StatusFrame(info));

            if (info.entity_.production != null)
            {
                subFrames.set(subIndex++, 0, new ProductionFrame(info.entity_.production));
            }
            if (info.entity_.inventory != null)
            {
                subFrames.set(subIndex++, 0, new InventoryFrame(info.entity_.inventory));
            }

            set(1, 0, subFrames);
        }
    }
}
