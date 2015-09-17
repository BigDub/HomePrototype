using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class PhysicsComponent : Component
    {
        PhysicsComponent parent_;
        List<PhysicsComponent> children_;

        public float spin_;
        public Vector2 velocity_, accel_;
        public Vector2 growth_;

        private PhysicsComponent(GameEntity entity)
            : base(entity)
        {
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

        public override Component deepCopy(GameEntity entity)
        {
            PhysicsComponent c = new PhysicsComponent(entity);
            c.spin_ = spin_;
            c.velocity_ = velocity_;
            c.accel_ = accel_;
            c.growth_ = growth_;
            return c;
        }

        public virtual void update(float elapsed)
        {
            entity_.spatial.rotation += spin_ * elapsed;
            entity_.spatial.translation_ += velocity_ * elapsed;
            entity_.spatial.scale_ += growth_ * elapsed;

            velocity_ += accel_ * elapsed;
        }
    }
}
