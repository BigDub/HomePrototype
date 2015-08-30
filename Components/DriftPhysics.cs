using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class DriftPhysics : PhysicsComponent
    {
        Vector2 max_drift;
        Vector2 max_speed;
        float drift_speed;

        public DriftPhysics(GameEntity entity, Vector2 drift, Vector2 speed, float d_speed) : base(entity, Vector2.Zero, 0, Vector2.Zero)
        {
            max_drift = drift;
            max_speed = speed;
            drift_speed = d_speed;
        }

        public override void update()
        {
            Vector2 pos = entity_.spatial.translation_;

            velocity_.X = MathHelper.Clamp(velocity_.X + drift_speed * (float)(Locator.getRandom().NextDouble() * 2.0 - 1.0), -max_speed.X, max_speed.X);
            velocity_.Y = MathHelper.Clamp(velocity_.Y + drift_speed * (float)(Locator.getRandom().NextDouble() * 2.0 - 1.0), -max_speed.Y, max_speed.Y);

            if (pos.X > max_drift.X)
            {
                entity_.spatial.translation_ = new Vector2(max_drift.X, entity_.spatial.translation_.Y);
                velocity_.X = MathHelper.Min(velocity_.X, 0);
            }
            else if (pos.X < -max_drift.X)
            {
                entity_.spatial.translation_ = new Vector2(-max_drift.X, entity_.spatial.translation_.Y);
                velocity_.X = MathHelper.Max(velocity_.X, 0);
            }
            if (pos.Y > max_drift.Y)
            {
                entity_.spatial.translation_ = new Vector2(entity_.spatial.translation_.X, max_drift.Y);
                velocity_.Y = MathHelper.Min(velocity_.Y, 0);
            }
            else if (pos.Y < -max_drift.Y)
            {
                entity_.spatial.translation_ = new Vector2(entity_.spatial.translation_.X, -max_drift.Y);
                velocity_.Y = MathHelper.Max(velocity_.Y, 0);
            }

            base.update();
        }
    }
}
