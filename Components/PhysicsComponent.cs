using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class PhysicsComponent
    {
        protected GameEntity entity_;

        PhysicsComponent parent_;
        List<PhysicsComponent> children_;

        private PhysicsComponent(GameEntity entity)
        {
            entity_ = entity;
            children_ = new List<PhysicsComponent>();
        }

        public PhysicsComponent(GameEntity entity, Vector2 velocity, float spin, Vector2 growth) : this(entity)
        {
            velocity_ = velocity;
            spin_ = spin;
            growth_ = growth;
        }
        
        public PhysicsComponent(GameEntity entity, PhysicsComponent parent, Vector2 velocity, float spin, Vector2 growth) : this(entity)
        {
            parent_ = parent;
            parent_.children_.Add(this);
            velocity_ = velocity;
            spin_ = spin;
            growth_ = growth;
        }

        public PhysicsComponent(GameEntity entity, PhysicsComponent parent) : this(entity)
        {
            parent_ = parent;
            parent_.children_.Add(this);
            velocity_ = Vector2.Zero;
            spin_ = 0f;
            growth_ = Vector2.Zero;
        }

        public virtual void update(float elapsed)
        {
            entity_.spatial.rotation_ += spin_ * elapsed;
            entity_.spatial.translation_ += velocity_ * elapsed;
            entity_.spatial.scale_ += growth_ * elapsed;
        }

        protected float spin_;
        protected Vector2 velocity_;
        protected Vector2 growth_;
    }
}
