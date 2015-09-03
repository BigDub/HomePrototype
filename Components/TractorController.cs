using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class TractorController : ControllerComponent
    {
        private static float speed = 50f;
        private static float accel_speed = 10f;
        public GameEntity target_;

        float effectTimer;

        public TractorController(GameEntity entity)
            : base(entity)
        {
        }

        private void findTarget()
        {
            foreach (InfoComponent info in Locator.getComponentManager().info_)
            {
                if (info.type == ObjectType.LOOTABLE)
                {
                    target_ = info.entity_;
                    Vector2 dir = entity_.spatial.w_translation - target_.spatial.w_translation;
                    dir.Normalize();
                    target_.physic.velocity_ = dir * speed;
                    target_.physic.accel_ = dir * accel_speed;
                    break;
                }
            }
        }

        public override void update(float elapsed)
        {
            if (entity_.info.state != ObjectState.OK)
                return;

            if (target_ == null)
                findTarget();

            if (target_ != null)
            {
                effectTimer += elapsed;
                if (effectTimer > 0.1f)
                {
                    Locator.getComponentManager().addEntity(Locator.getObjectFactory().createTractorEffect(target_.spatial.w_translation, entity_.spatial.w_translation));
                    effectTimer -= 0.1f;
                }

                if ((entity_.spatial.w_translation - target_.spatial.w_translation).Length() < 50f)
                {
                    Locator.getComponentManager().removeEntity(target_);
                    target_ = null;
                }
            }
        }
    }
}
