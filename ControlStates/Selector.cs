using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.ControlStates
{
    class Selector : ControllerState
    {
        Point mousePoint;
        GameEntity marker;
        bool markerActive;

        public Selector()
        {
            marker = new GameEntity();
            marker.spatial = new Components.SpatialComponent(marker, Locator.getShip().entity_.spatial);
            marker.render = new Components.RenderComponent(marker, Locator.getTextureManager().loadTexture("tile32"), 0, new Vector2(16), Color.Green);
            markerActive = false;
        }

        public void changeState(ControllerState newState)
        {
        }

        public void update(float elapsed)
        {
            Vector2 maus = Locator.getInputHandler().getMousePosition();
            Ship ship = Locator.getShip();
            Vector2 localMaus = ship.entity_.spatial.worldToLocal(maus);
            Point point = new Point((int)((localMaus.X + 16) / 32f), (int)((localMaus.Y + 16) / 32f));
            if (mousePoint.X != point.X || mousePoint.Y != point.Y)
            {
                mousePoint = point;
                switch (ship.tiles.getTile(mousePoint))
                {
                    case TileStatus.INVALID:
                        if (markerActive)
                        {
                            Locator.getComponentManager().removeEntity(marker);
                            markerActive = false;
                        }
                        break;
                    case TileStatus.OPEN:
                    case TileStatus.FILLED:
                        if (!markerActive)
                        {
                            Locator.getComponentManager().addEntity(marker);
                            markerActive = true;
                        }
                        marker.spatial.translation_ = new Vector2(mousePoint.X * 32, mousePoint.Y * 32);
                        break;
                }
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
        }

        public void setParent(Services.ControlManager manager)
        {
        }

        public void reset()
        {
        }
    }
}
