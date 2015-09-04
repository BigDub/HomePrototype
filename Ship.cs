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
        }

        public void populate()
        {
            Services.ObjectFactory o = Locator.getObjectFactory();
            GameEntity e;
            e = o.createGun(0);
            make(e, 45, 7);

            GameEntity orb = o.createOrb();
            e.inventory.placeItem(orb, 0);

            make(o.createGun(0), 45, 19, ObjectState.DISABLED);

            make(o.createTractor(0), 44, 12, ObjectState.DISABLED);

            make(o.createFurnace(0), 40, 10);
            make(o.createMachineShop(0), 38, 10);
            make(o.createFurnace(0), 40, 16, ObjectState.DAMAGED);

            make(o.createTank(0), 7, 0, ObjectState.DAMAGED);
            make(o.createTank(0), 9, 0, ObjectState.DAMAGED);
            make(o.createPump(0), 5, 0, ObjectState.DAMAGED);
            make(o.createTank(0), 7, 1, ObjectState.DAMAGED);
            make(o.createTank(0), 9, 1, ObjectState.DAMAGED);
            make(o.createCompressor(0), 5, 1, ObjectState.DAMAGED);
            make(o.createCombChamb(0), 2, 0, ObjectState.DAMAGED);
            make(o.createThruster(0), 0, 0, ObjectState.DAMAGED);

            make(o.createTank(0), 7, 6);
            make(o.createTank(0), 9, 6, ObjectState.DAMAGED);
            make(o.createCompressor(0), 5, 6, ObjectState.DISABLED);
            make(o.createTank(0), 7, 7);
            make(o.createTank(0), 9, 7);
            make(o.createPump(0), 5, 7, ObjectState.DAMAGED);
            make(o.createCombChamb(0), 2, 6, ObjectState.DAMAGED);
            make(o.createThruster(0), 0, 6, ObjectState.DAMAGED);

            make(o.createTank(0), 7, 10);
            make(o.createTank(0), 9, 10);
            make(o.createCompressor(0), 5, 10);
            make(o.createTank(0), 7, 11);
            make(o.createTank(0), 9, 11);
            e = o.createPump(0);
            e.controller = new EndPumpController(e);
            make(e, 5, 11, ObjectState.DISABLED);
            make(o.createCombChamb(0), 2, 10);
            make(o.createThruster(0), 0, 10);

            make(o.createTank(0), 7, 16);
            make(o.createTank(0), 9, 16, ObjectState.DAMAGED);
            make(o.createPump(0), 5, 16, ObjectState.DAMAGED);
            make(o.createTank(0), 7, 17);
            make(o.createTank(0), 9, 17, ObjectState.DAMAGED);
            make(o.createCompressor(0), 5, 17, ObjectState.DAMAGED);
            make(o.createCombChamb(0), 2, 16, ObjectState.DAMAGED);
            make(o.createThruster(0), 0, 16, ObjectState.DAMAGED);

            make(o.createTank(0), 7, 20, ObjectState.DAMAGED);
            make(o.createTank(0), 9, 20);
            make(o.createPump(0), 5, 20, ObjectState.DAMAGED);
            make(o.createTank(0), 7, 21);
            make(o.createTank(0), 9, 21);
            make(o.createCompressor(0), 5, 21);
            make(o.createCombChamb(0), 2, 20, ObjectState.DAMAGED);
            make(o.createThruster(0), 0, 20);

            make(o.createTank(0), 7, 26, ObjectState.DAMAGED);
            make(o.createTank(0), 9, 26);
            make(o.createCompressor(0), 5, 26);
            make(o.createTank(0), 7, 27, ObjectState.DAMAGED);
            make(o.createTank(0), 9, 27, ObjectState.DAMAGED);
            e = o.createPump(0);
            e.inventory.placeItem(o.createMotor(), 0);
            make(e, 5, 27, ObjectState.DISABLED);
            make(o.createCombChamb(0), 2, 26, ObjectState.DAMAGED);
            make(o.createThruster(0), 0, 26, ObjectState.DAMAGED);

            make(o.createReactor(0), 22, 12);
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
