using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    enum ObjectType
    {
        SHOOTABLE,
        LOOTABLE,
        SHIPBOARD,
        ITEM,
        OTHER,
        INHERIT
    };

    enum ObjectState
    {
        OK,
        DISABLED,
        DAMAGED
    };

    class InfoComponent
    {
        public GameEntity entity_;

        public delegate void Notify();
        Notify notify_;

        public InfoComponent parent_;

        private bool cusName_;
        private String name_;
        public String name
        {
            get
            {
                if (!cusName_)
                {
                    return parent_.name;
                }
                return name_;
            }

            set
            {
                name_ = value;
                cusName_ = true;
                onUpdate();
            }
        }

        private bool cusItemTex_;
        private int itemTex_ = -1;
        public int itemTex
        {
            get
            {
                if (cusItemTex_)
                    return itemTex_;
                return parent_.itemTex;
            }
            set
            {
                itemTex_ = value;
                cusItemTex_ = true;
                onUpdate();
            }
        }

        private ObjectType type_;
        public ObjectType type
        {
            get
            {
                if (type_ == ObjectType.INHERIT)
                {
                    return parent_.type;
                }
                return type_;
            }
            set
            {
                type_ = value;
                onUpdate();
            }
        }

        private ObjectState state_;
        public ObjectState state
        {
            get
            {
                return state_;
            }
            set
            {
                state_ = value;
                entity_.render.setState(state_);
                onUpdate();
            }
        }

        public InfoComponent()
        {
            entity_ = null;
            cusName_ = false;
            cusItemTex_ = false;
            type_ = ObjectType.SHIPBOARD;
        }

        public InfoComponent(GameEntity entity)
        {
            entity_ = entity;
            cusName_ = false;
            cusItemTex_ = false;
            type_ = ObjectType.SHIPBOARD;
        }
        public InfoComponent(GameEntity entity, InfoComponent parent)
        {
            entity_ = entity;
            parent_ = parent;
            cusName_ = false;
            cusItemTex_ = false;
            type_ = ObjectType.INHERIT;
        }

        private int number_;
        public int number
        {
            get
            {
                return number_;
            }
            set
            {
                number_ = value;
                onUpdate();
            }
        }

        private void onUpdate()
        {
            if (notify_ != null)
                notify_();
        }

        public void register(Notify notify)
        {
            notify_ += notify;
        }
        public void unregister(Notify notify)
        {
            notify_ -= notify;
        }
    }
}
