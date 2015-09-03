using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class InventoryComponent
    {
        public GameEntity entity_;
        public GameEntity[] items_;
        private int capacity_;
        public int capacity
        {
            get
            {
                return capacity_;
            }
        }

        public delegate void Notify();
        Notify notify_;

        public InventoryComponent(GameEntity entity, int cap)
        {
            entity_ = entity;
            capacity_ = cap;
            items_ = new GameEntity[capacity_];
        }

        public void register(Notify notify)
        {
            notify_ += notify;
        }
        public void unregister(Notify notify)
        {
            notify_ -= notify;
        }

        protected void onUpdate()
        {
            if (notify_ != null)
            {
                notify_();
            }
        }

        public void placeItem(GameEntity item, int slot)
        {
            if (slot < 0 || slot >= capacity_)
                return;
            items_[slot] = item;
            onUpdate();
        }    
        public void takeItem(int slot)
        {
            if (slot < 0 || slot >= capacity_)
                return;
            items_[slot] = null;
            onUpdate();
        }

        public GameEntity getItem(int slot)
        {
            if (slot < 0 || slot >= capacity_)
                return null;
            return items_[slot];
        }
    }
}
