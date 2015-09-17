using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class InventoryComponent : Component
    {
        public GameEntity[] items_;
        private int capacity_;
        public int capacity
        {
            get
            {
                return capacity_;
            }
        }

        public int numberPerSlot_;

        public delegate void Notify();
        Notify notify_;

        public InventoryComponent(GameEntity entity, int cap, int perSlot = 5)
            : base(entity)
        {
            capacity_ = cap;
            items_ = new GameEntity[capacity_];
            numberPerSlot_ = perSlot;
        }
        public override Component deepCopy(GameEntity entity)
        {
            return new InventoryComponent(entity, capacity_, numberPerSlot_);
        }

        public void register(Notify notify)
        {
            notify_ += notify;
        }
        public void unregister(Notify notify)
        {
            notify_ -= notify;
        }

        public void onUpdate()
        {
            if (notify_ != null)
            {
                notify_();
            }
        }

        public int getFirstItem()
        {
            for (int i = 0; i < capacity_; i++)
            {
                if (items_[i] != null)
                    return i;
            }
            return -1;
        }

        public int getNumItem(ItemInfo item)
        {
            int response = 0;
            for (int i = 0; i < capacity_; i++)
            {
                if (items_[i] != null && items_[i].item.ID_ == item.ID_)
                    response += items_[i].item.number_;
            }
            return response;
        }

        public bool canPlaceItem(GameEntity item)
        {
            for (int i = 0; i < capacity_; i++)
            {
                if (items_[i] == null)
                {
                    return true;
                }
                else if (items_[i].item.ID_ == item.item.ID_ && items_[i].item.state_ == item.item.state_)
                {
                    return true;
                }
            }
            return false;
        }
        public bool offerItem(GameEntity item, int maxNum = 0)
        {
            for (int i = 0; i < capacity_; i++)
            {
                if (items_[i] != null)
                {
                    if (items_[i].item.ID_ == item.item.ID_ && items_[i].item.state_ == item.item.state_)
                    {
                        if (maxNum > 0)
                        {
                            items_[i].item.number_ += (int)MathHelper.Min(maxNum, item.item.number_);
                        }
                        else
                        {
                            items_[i].item.number_ += item.item.number_;
                        }
                        onUpdate();
                        return true;
                    }
                }
            }
            for (int i = 0; i < capacity_; i++)
            {
                if (items_[i] == null)
                {
                    items_[i] = item;
                    onUpdate();
                    return true;
                }
            }
            return false;
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

        public int numItems()
        {
            int count = 0;
            for (int index = 0; index < capacity; ++index)
            {
                if (items_[index] != null)
                    ++count;
            }
            return count;
        }
    }
}
