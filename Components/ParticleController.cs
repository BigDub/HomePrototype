using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipPrototype.Components
{
    class ParticleController : ControllerComponent
    {
        float life_;
        bool timed;
        public float fadeSpeed = 0;
        public float opacity = 1;
        public ParticleController(GameEntity entity, float life) : base(entity)
        {
            if (life == 0)
            {
                timed = false;
            }
            else
            {
                timed = true;
            }
            life_ = life;
        }

        public override Component deepCopy(GameEntity entity)
        {
            ParticleController c = new ParticleController(entity, life_);
            c.timed = timed;
            c.fadeSpeed = fadeSpeed;
            c.opacity = opacity;
            return c;
        }

        public override void update(float elapsed)
        {
            if (fadeSpeed != 0)
            {
                opacity -= fadeSpeed * elapsed;
                entity_.render.color_ = new Microsoft.Xna.Framework.Color(1f, 1f, 1f, opacity);
            }
            if (timed)
            {
                life_ -= elapsed;
                if (life_ <= 0 || opacity <= 0)
                {
                    Locator.getComponentManager().removeEntity(entity_);
                }
            }
        }
    }
}
