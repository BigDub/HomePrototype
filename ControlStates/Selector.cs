using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Services;

namespace ShipPrototype.ControlStates
{
    class Selector : BaseState
    {
        public Selector()
        {
        }

        public override void onPost(Post post)
        {
            switch (post.category)
            {
                case PostCategory.REQUEST_ITEM:
                    changeState(new HoldingItem(post.sourceEntity, post.targetEntity, post.slot));
                    post.sourceEntity.inventory.takeItem(post.slot);
                    break;
                default:
                    break;
            }
            base.onPost(post);
        }
    }
}
