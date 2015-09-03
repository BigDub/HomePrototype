using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class WorldController : ControllerComponent
    {
        Point screen_;
        private bool sticky_ = true;

        public WorldController(GameEntity entity, Point screen)
            : base(entity)
        {
            screen_ = screen;

            Locator.getMessageBoard().register(Services.PostCategory.END_GAME, listen);
        }

        void listen(Services.Post post)
        {
            if (post.category == Services.PostCategory.END_GAME)
            {
                sticky_ = false;
            }
        }

        public override void update(float elapsed)
        {
            if (sticky_)
            {
                GameEntity player = Locator.getPlayer();

                entity_.spatial.translation_ = new Vector2((screen_.X / 2) - player.spatial.translation_.X, (screen_.Y / 2) - player.spatial.translation_.Y);
            }
        }
    }
}
