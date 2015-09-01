using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype
{
    class Ship
    {
        private const int width_ = 15;
        private const int height_ = 7;

        private static int tile_tex_id;
        public static void loadTextures()
        {
            tile_tex_id = Locator.getTextureManager().loadTexture("tile32");
        }

        public GameEntity entity_;
        public Components.TileSystemComponent tiles;

        private static Ship ship;
        public static Ship init(GameEntity world)
        {
            Debug.Assert(ship == null);

            ship = new Ship(world);
            return ship;
        }

        private Ship(GameEntity world)
        {
            entity_ = new GameEntity();
            entity_.spatial = new Components.SpatialComponent(entity_, world.spatial);

            Locator.getComponentManager().addEntity(entity_);

            tiles = new Components.TileSystemComponent();
            tiles.addArea(new Point(16, 8), Point.Zero);
            tiles.addArea(new Point(16, 8), new Point(0, 10));
            tiles.addArea(new Point(16, 8), new Point(0, 20));
            tiles.addArea(new Point(6, 28), new Point(16, 0));
            tiles.addArea(new Point(10, 16), new Point(22, 6));
            tiles.addArea(new Point(10, 8), new Point(32, 10));
            tiles.addArea(new Point(6, 16), new Point(42, 6));

            tiles.generateTileEntities(entity_);
        }
    }
}
