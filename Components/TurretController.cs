using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class TurretController : ControllerComponent
    {
        private static float shootRange_ = 400f;
        private static float idleTurnSpeed_ = 0.4f;
        private static float turnSpeed_ = 0.8f;

        private GameEntity target_;

        public TurretController(GameEntity entity) : base(entity)
        {
        }


        private void findTarget()
        {
            foreach (InfoComponent info in Locator.getComponentManager().info_)
            {
                if (info.type == ObjectType.SHOOTABLE)
                {
                    target_ = info.entity_;
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

            if (target_ == null)
            {
                entity_.spatial.rotation += idleTurnSpeed_ * elapsed;
            }
            else
            {
                Vector2 diff = target_.spatial.w_translation - entity_.spatial.w_translation;
                float distance = diff.Length();
                float targetAngle = (float) Math.Atan2(diff.Y, diff.X);
                float dA = targetAngle - entity_.spatial.w_rotation;
                if (Math.Abs(dA + MathHelper.TwoPi) < Math.Abs(dA))
                {
                    dA += MathHelper.TwoPi;
                }
                else if (Math.Abs(dA - MathHelper.TwoPi) < Math.Abs(dA))
                {
                    dA -= MathHelper.TwoPi;
                }
                float change = turnSpeed_ * elapsed;
                if (Math.Abs(change) > Math.Abs(dA))
                {
                    entity_.spatial.w_rotation = targetAngle;

                    if (distance < shootRange_)
                    {
                        Locator.getComponentManager().addEntity(Locator.getObjectFactory().createLaser(entity_.spatial.w_translation, entity_.spatial.w_rotation, distance));
                        Locator.getMessageBoard().postMessage(new Services.Post(Services.PostCategory.JUNK_SHOT, entity_, target_, 0));
                        target_ = null;
                    }
                }
                else
                {
                    if (dA < 0)
                    {
                        entity_.spatial.rotation -= change;
                    }
                    else
                    {
                        entity_.spatial.rotation += change;
                    }
                }
            }
        }
    }
}
