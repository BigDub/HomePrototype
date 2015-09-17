using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.ControlStates
{
    class Removing : BaseState
    {
        float currentTime_;
        float processTime_;
        UI.ProgressBar bar_;
        GameEntity entity_;

        public Removing(GameEntity entity)
        {
            entity_ = entity;
            currentTime_ = 0;
            processTime_ = 0.5f;
            bar_ = new UI.ProgressBar(50);
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            base.mouseUp(sender, e);
            if (e.button_ == MouseButton.RIGHT)
            {
                changeState(new Selector());
            }
        }

        public override void update(float elapsed)
        {
            base.update(elapsed);
            bar_.loc_ = maus_;
            bar_.loc_.X -= bar_.size.X / 2;

            currentTime_ += elapsed;
            bar_.percent = currentTime_ / processTime_;

            if (
                (entity_.spatial.w_translation - Locator.getPlayer().spatial.w_translation).Length() >= interactRange ||
                Locator.getComponentManager().pick(Locator.getShip().tiles, mouseTile_) != entity_
                )
            {
                changeState(new Selector());
            }

            if (currentTime_ >= processTime_)
            {
                if (entity_.inventory != null)
                {
                    for (int i = 0; i < entity_.inventory.capacity; i++)
                    {
                        GameEntity it = entity_.inventory.getItem(i);
                        if (it != null)
                        {
                            if (Locator.getPlayer().inventory.canPlaceItem(it))
                            {
                                Locator.getPlayer().inventory.offerItem(it);
                            }
                        }
                    }
                }
                GameEntity item = Locator.getObjectFactory().createItem(entity_.item);
                item.item.state_ = entity_.info.state;
                Locator.getShip().tiles.open(entity_.tile.coord_, entity_.tile.size_);
                Locator.getComponentManager().removeEntity(entity_);
                if (Locator.getPlayer().inventory.canPlaceItem(item))
                {
                    Locator.getPlayer().inventory.offerItem(item);
                    changeState(new Selector());
                }
                else
                {
                    changeState(new HoldingItem(null, item, 0));
                }
            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            bar_.render(spriteBatch);
            spriteBatch.End();
            base.render(spriteBatch);
        }
    }
}
