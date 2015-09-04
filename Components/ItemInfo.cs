using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class ItemInfo
    {
        private static int idCounter = 0;
        public int ID_;
        public int tex_;
        public String name_;

        public ItemInfo(int tex, String name)
        {
            ID_ = idCounter++;
            tex_ = tex;
            name_ = name;
        }
    }
}
