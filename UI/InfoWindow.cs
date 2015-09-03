using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.UI
{
    class InfoWindow : Window
    {
        Components.InfoComponent info_;

        public InfoWindow(Components.InfoComponent info, int rows, int columns)
            : base(rows, columns)
        {
            info_ = info;
        }
    }
}
