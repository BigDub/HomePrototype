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
        GameEntity ground;
        bool canPlace;
        float transTimer;

        public Placer(GameEntity thing)
        {
            marker = thing;
            marker.info.state = Components.ObjectState.DISABLED;
            ground = new GameEntity();
            ground.spatial = new Components.SpatialComponent(ground, marker.spatial);
            ground.spatial.scale_ = new Vector2(marker.tile.size_.X, marker.tile.size_.Y) * 32f;
            ground.render = new Components.RenderComponent(ground, -1, 0, new Vector2(0.5f), Color.White);
            Locator.getComponentManager().addEntity(ground);
            Locator.getComponentManager().addEntity(marker);
            canPlace = false;
        }

        public override void changeState(ControllerState newState)
        {
            if (ground != null)
            {
                Locator.getComponentManager().removeEntity(ground);
            }
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
            //localMaus += new Vector2(16) - new Vector2(marker.tile.size_.X * 16f, marker.tile.size_.Y * 16f);
            mouseTile_ = new Point((int)(localMaus.X / 32f), (int)(localMaus.Y / 32f));

            transTimer += 4f * elapsed;
            canPlace = Locator.getShip().tiles.canBuild(mouseTile_, marker.tile.size_);
            marker.tile.coord_ = mouseTile_;
            if (canPlace)
            {
                float alpha = 0.5f + (float)(Math.Sin(transTimer) * 0.25f);
                marker.render.color_ = new Color(0.5f, 1, 0.5f, alpha);
                ground.render.color_ = new Color(0.5f, 1, 0.5f, 0.5f);
            }
            else
            {
                float alpha = 0.5f + (float)(Math.Sin(transTimer) * 0.25f);
                marker.render.color_ = new Color(1, 0.5f, 0.5f, alpha);
                ground.render.color_ = new Color(1, 0.5f, 0.5f, 0.5f);
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
                        marker.info.state = Components.ObjectState.OK;
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
