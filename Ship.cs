using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Components;

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

        public GameEntity pump;
        public GameEntity gun;

        private static Ship ship;
        public static Ship init(GameEntity world)
        {
            Debug.Assert(ship == null);

            ship = new Ship(world);

            return ship;
        }

        private static void make(GameEntity e, int x, int y, Components.ObjectState state = Components.ObjectState.OK)
        {
            e.tile.coord_ = new Point(x, y);
            e.info.state = state;
            ship.tiles.build(e.tile.coord_, e.tile.size_);
            Locator.getComponentManager().addEntity(e);
        }

        public void end()
        {
            Locator.getComponentManager().addEntity(Locator.getObjectFactory().createEndGameFlame());
            entity_.physic.accel_ = new Vector2(50, 0);
            pump.info.state = ObjectState.OK;
        }

        public void populate()
        {
            Services.ObjectFactory o = Locator.getObjectFactory();
            gun = o.createGun();
            make(gun, 45, 7);
            make(o.createGun(), 45, 19, ObjectState.DAMAGED);

            make(o.createTractor(), 44, 12);

            make(o.createFurnace(), 40, 10);
            make(o.createFurnace(), 40, 16, ObjectState.DAMAGED);

            make(o.createTank(), 7, 0, ObjectState.DAMAGED);
            make(o.createTank(), 9, 0, ObjectState.DAMAGED);
            make(o.createPump(), 5, 0, ObjectState.DAMAGED);
            make(o.createTank(), 7, 1, ObjectState.DAMAGED);
            make(o.createTank(), 9, 1, ObjectState.DAMAGED);
            make(o.createCompressor(), 5, 1, ObjectState.DAMAGED);
            make(o.createCombChamb(), 2, 0, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 0, ObjectState.DAMAGED);

            make(o.createTank(), 7, 6);
            make(o.createTank(), 9, 6, ObjectState.DAMAGED);
            make(o.createCompressor(), 5, 6, ObjectState.DISABLED);
            make(o.createTank(), 7, 7);
            make(o.createTank(), 9, 7);
            make(o.createPump(), 5, 7, ObjectState.DAMAGED);
            make(o.createCombChamb(), 2, 6, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 6, ObjectState.DAMAGED);

            make(o.createTank(), 7, 10);
            make(o.createTank(), 9, 10);
            make(o.createCompressor(), 5, 10);
            make(o.createTank(), 7, 11);
            make(o.createTank(), 9, 11);
            pump = o.createPump();
            make(pump, 5, 11, ObjectState.DISABLED);
            make(o.createCombChamb(), 2, 10);
            make(o.createThruster(), 0, 10);

            make(o.createTank(), 7, 16);
            make(o.createTank(), 9, 16, ObjectState.DAMAGED);
            make(o.createPump(), 5, 16, ObjectState.DISABLED);
            make(o.createTank(), 7, 17);
            make(o.createTank(), 9, 17, ObjectState.DAMAGED);
            make(o.createCompressor(), 5, 17, ObjectState.DAMAGED);
            make(o.createCombChamb(), 2, 16, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 16, ObjectState.DAMAGED);

            make(o.createTank(), 7, 20, ObjectState.DAMAGED);
            make(o.createTank(), 9, 20);
            make(o.createPump(), 5, 20, ObjectState.DAMAGED);
            make(o.createTank(), 7, 21);
            make(o.createTank(), 9, 21);
            make(o.createCompressor(), 5, 21);
            make(o.createCombChamb(), 2, 20, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 20);

            make(o.createTank(), 7, 26, ObjectState.DAMAGED);
            make(o.createTank(), 9, 26);
            make(o.createCompressor(), 5, 26);
            make(o.createTank(), 7, 27, ObjectState.DAMAGED);
            make(o.createTank(), 9, 27, ObjectState.DAMAGED);
            make(o.createPump(), 5, 27);
            make(o.createCombChamb(), 2, 26, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 26, ObjectState.DAMAGED);

            make(o.createReactor(), 22, 12);
        }

        private Ship(GameEntity world)
        {
            entity_ = new GameEntity();
            entity_.spatial = new Components.SpatialComponent(entity_, world.spatial);
            entity_.physic = new PhysicsComponent(entity_, Vector2.Zero, 0, Vector2.Zero);

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
