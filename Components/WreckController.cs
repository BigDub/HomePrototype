using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class WreckController : ControllerComponent
    {
        public WreckController(GameEntity entity) : base(entity)
        {
            Locator.getMessageBoard().register(Services.PostCategory.JUNK_SHOT, notify);
        }

        public void notify(Services.Post post)
        {
            if (post.category == Services.PostCategory.JUNK_SHOT)
            {
                if (post.targetEntity == entity_)
                {
                    float speed = entity_.physic.velocity_.Length() / 6f;
                    for (int i = 0; i < 3; ++i)
                    {
                        GameEntity e = Locator.getObjectFactory().createDebris();
                        e.spatial.w_translation = entity_.spatial.w_translation;
                        float angle = 2f * ((float)Locator.getRandom().NextDouble() - 0.5f) * MathHelper.PiOver4;

                        //hardcoded to go up
                        e.physic.velocity_ = new Vector2((float)Math.Sin(angle) * speed, -(float)Math.Cos(angle) * speed);
                        Locator.getComponentManager().addEntity(e);
                    }
                    Locator.getComponentManager().removeEntity(entity_);
                }
            }
        }
    }
}
