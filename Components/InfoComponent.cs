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

    class InfoComponent : Component
    {
        public delegate void Notify();
        Notify notify_;

        public InfoComponent parent_;

        public int typeID_;

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
        
        private bool cusFT_;
        private String flavorText_;
        public String flavorText
        {
            get
            {
                if (parent_ != null && !cusFT_)
                {
                    return parent_.flavorText;
                }
                return flavorText_;
            }

            set
            {
                flavorText_ = value;
                cusFT_ = true;
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

        public InfoComponent(int typeID)
            : base(null)
        {
            cusName_ = false;
            cusFT_ = false;
            flavorText_ = "";
            type_ = ObjectType.SHIPBOARD;
            typeID_ = typeID;
        }

        public InfoComponent(GameEntity entity, int typeID)
            : base(entity)
        {
            cusName_ = false;
            cusFT_ = false;
            flavorText_ = "";
            type_ = ObjectType.SHIPBOARD;
            typeID_ = typeID;
        }
        public InfoComponent(GameEntity entity, InfoComponent parent)
            : base(entity)
        {
            parent_ = parent;
            cusName_ = false;
            cusFT_ = false;
            flavorText_ = "";
            type_ = ObjectType.INHERIT;
            typeID_ = parent_.typeID_;
        }

        public override Component deepCopy(GameEntity entity)
        {
            InfoComponent c = new InfoComponent(entity, typeID_);
            c.parent_ = parent_;
            c.cusName_ = cusName_;
            c.name_ = name_;
            c.cusFT_ = cusFT_;
            c.flavorText_ = flavorText_;
            c.type_ = type_;
            c.state_ = state_;
            return c;
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
