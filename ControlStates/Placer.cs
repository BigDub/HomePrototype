using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.ControlStates
{
    class Placer : BaseState
    {
        GameEntity marker;
        bool canPlace;
        float transTimer;

        public Placer(GameEntity thing)
        {
            marker = thing;
            Locator.getComponentManager().addEntity(marker);
            canPlace = false;
        }

        public override void changeState(ControllerState newState)
        {
            if (marker != null)
            {
                Locator.getComponentManager().removeEntity(marker);
            }
            base.changeState(newState);
        }

        public override void update(float elapsed)
        {
            Vector2 maus = Locator.getInputHandler().getMousePosition();
            Ship ship = Locator.getShip();
            Vector2 localMaus = ship.entity_.spatial.worldToLocal(maus);
            localMaus -= new Vector2(marker.tile.size_.X * 4f, marker.tile.size_.Y * 4f);
            mouseTile_ = new Point((int)(localMaus.X / 32f), (int)(localMaus.Y / 32f));

            transTimer += 2f * elapsed;
            canPlace = Locator.getShip().tiles.canBuild(mouseTile_, marker.tile.size_);
            marker.tile.coord_ = mouseTile_;
            if (canPlace)
            {
                marker.render.color_ = new Color(0.5f, 1, 0.5f, 0.5f + (float)(Math.Sin(transTimer) / 4f));
            }
            else
            {
                marker.render.color_ = new Color(1, 0.5f, 0.5f, 0.5f + (float)(Math.Sin(transTimer) / 4f));
            }
        }

        public override void mouseUp(object sender, MouseEventArgs e)
        {
            switch (e.button_)
            {
                case MouseButton.LEFT:
                    if (canPlace)
                    {
                        Console.WriteLine("Placing " + marker.info.name + " at " + mouseTile_.X + ", " + mouseTile_.Y);
                        marker.render.color_ = Color.White;
                        Locator.getShip().tiles.build(mouseTile_, marker.tile.size_);
                        marker = null;

                        changeState(new Selector());
                    }
                    break;
                case MouseButton.RIGHT:
                    changeState(new Selector());
                    break;
            }
        }
    }
}
