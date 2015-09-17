using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class ItemInfo : Component
    {
        private static int idCounter = 0;
        public int ID_;
        public int tex_;
        public int number_;
        public int structureID_;
        public ObjectState state_;

        private ItemInfo(GameEntity e)
            : base(e)
        {
        }
        public ItemInfo(int tex)
            : base(null)
        {
            ID_ = idCounter++;
            tex_ = tex;
            structureID_ = -1;
            state_ = ObjectState.OK;
        }
        public ItemInfo(GameEntity e, ItemInfo parent, int number = 1)
            : base(e)
        {
            ID_ = parent.ID_;
            tex_ = parent.tex_;
            structureID_ = parent.structureID_;
            number_ = number;
            state_ = parent.state_;
        }

        public override Component deepCopy(GameEntity entity)
        {
            ItemInfo c = new ItemInfo(entity);
            c.ID_ = ID_;
            c.tex_ = tex_;
            c.number_ = number_;
            c.structureID_ = structureID_;
            c.state_ = state_;
            return c;
        }
    }
}
