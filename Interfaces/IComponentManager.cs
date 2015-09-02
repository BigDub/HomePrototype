using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Components;

namespace ShipPrototype.Interfaces
{
    interface IComponentManager
    {
        void addEntity(GameEntity entity);

        void removeEntity(GameEntity entity);

        void update(float elapsed);

        void render(SpriteBatch spriteBatch);

        GameEntity pick(TileSystemComponent tsc, Point point);
    }
}
