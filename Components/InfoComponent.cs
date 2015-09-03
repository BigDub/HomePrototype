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
        private static int inv_size = 5;
        public GameEntity entity_;

        public InfoComponent()
        {
            inventory = new GameEntity[inv_size];
            entity_ = null;
            cusName_ = false;
            type_ = ObjectType.SHIPBOARD;
        }

        public InfoComponent(GameEntity entity)
        {
            entity_ = entity;
            cusName_ = false;
            type_ = ObjectType.SHIPBOARD;
        }
        public InfoComponent(GameEntity entity, InfoComponent parent)
        {
            entity_ = entity;
            parent_ = parent;
            cusName_ = false;
            type_ = ObjectType.INHERIT;
        }

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
            }
        }

        public GameEntity[] inventory;
    }
}
