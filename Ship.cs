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
            e.item.state_ = state;
            ship.tiles.build(e.tile.coord_, e.tile.size_);
            Locator.getComponentManager().addEntity_(e);
        }
        private static void makeOtank(int x, int y)
        {
            GameEntity e = Locator.getObjectFactory().createTank();
            e.inventory.offerItem(Locator.getObjectFactory().createOxygen(100));
            make(e, x, y);
        }
        private static void makeHtank(int x, int y)
        {
            GameEntity e = Locator.getObjectFactory().createTank();
            e.inventory.offerItem(Locator.getObjectFactory().createHydrogen(100));
            make(e, x, y);
        }
        private static void makeGpump(int x, int y)
        {
            GameEntity e = Locator.getObjectFactory().createPump();
            e.inventory.offerItem(Locator.getObjectFactory().createPinion());
            e.inventory.offerItem(Locator.getObjectFactory().createMotor());
            make(e, x, y);
        }
        private static void makeMpump(int x, int y)
        {
            GameEntity e = Locator.getObjectFactory().createPump();
            e.inventory.offerItem(Locator.getObjectFactory().createMotor());
            make(e, x, y, ObjectState.DISABLED);
        }
        private static void makePpump(int x, int y)
        {
            GameEntity e = Locator.getObjectFactory().createPump();
            e.inventory.offerItem(Locator.getObjectFactory().createPinion());
            make(e, x, y, ObjectState.DISABLED);
        }

        public void end()
        {
            Locator.getComponentManager().addEntity(Locator.getObjectFactory().createEndGameFlame());
            entity_.physic.accel_ = new Vector2(50, 0);
            Locator.getComponentManager().addEntity(Locator.getObjectFactory().createTitle());
        }

        public void populate()
        {
            Services.ObjectFactory o = Locator.getObjectFactory();
            GameEntity e;
            e = o.createGun();
            make(e, 45, 7);

            GameEntity orb = o.createOrb();
            e.inventory.placeItem(orb, 0);

            make(o.createGun(), 45, 19, ObjectState.DISABLED);

            make(o.createTractor(), 44, 12, ObjectState.DISABLED);

            make(o.createFurnace(), 40, 10);
            make(o.createMachineShop(), 38, 10);
            make(o.createGrinder(), 40, 16);

            make(o.createTank(), 7, 0, ObjectState.DAMAGED);
            make(o.createPump(), 9, 0, ObjectState.DAMAGED);
            make(o.createTank(), 11, 0, ObjectState.DAMAGED);
            make(o.createPump(), 5, 0, ObjectState.DAMAGED);
            make(o.createTank(), 7, 1, ObjectState.DAMAGED);
            make(o.createPump(), 9, 1, ObjectState.DAMAGED);
            make(o.createTank(), 11, 1, ObjectState.DAMAGED);
            make(o.createPump(), 5, 1, ObjectState.DAMAGED);
            make(o.createCombChamb(), 2, 0, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 0, ObjectState.DAMAGED);

            makeHtank(7, 6);
            make(o.createPump(), 9, 6, ObjectState.DAMAGED);
            make(o.createTank(), 11, 6, ObjectState.DAMAGED);
            make(o.createPump(), 5, 6, ObjectState.DISABLED);
            makeOtank(7, 7);
            make(o.createPump(), 9, 7, ObjectState.DAMAGED);
            makeOtank(11, 7);
            make(o.createPump(), 5, 7, ObjectState.DAMAGED);
            make(o.createCombChamb(), 2, 6, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 6, ObjectState.DAMAGED);


            make(o.createTank(), 7, 10, ObjectState.DAMAGED);
            makePpump(9, 10);
            makeHtank(11, 10);
            make(o.createPump(), 5, 10, ObjectState.DAMAGED);
            makeOtank(7, 11);
            make(o.createPump(), 9, 11, ObjectState.DAMAGED);
            make(o.createTank(), 11, 11, ObjectState.DAMAGED);
            //e = o.createPump();
            //e.controller = new EndPumpController(e);
            //make(e, 5, 11, ObjectState.DISABLED);
            make(o.createPump(), 5, 11, ObjectState.DISABLED);
            make(o.createCombChamb(), 2, 10);
            make(o.createThruster(), 0, 10, ObjectState.DAMAGED);

            make(o.createTank(), 7, 16);
            make(o.createPump(), 9, 16, ObjectState.DAMAGED);
            make(o.createTank(), 11, 16, ObjectState.DAMAGED);
            makeMpump(5, 16);
            make(o.createTank(), 7, 17);
            makeMpump(9, 17);
            make(o.createTank(), 11, 17, ObjectState.DAMAGED);
            make(o.createPump(), 5, 17, ObjectState.DAMAGED);
            make(o.createCombChamb(), 2, 16, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 16);

            make(o.createTank(), 7, 20, ObjectState.DAMAGED);
            make(o.createPump(), 9, 20, ObjectState.DAMAGED);
            makeOtank(11, 20);
            make(o.createPump(), 5, 20, ObjectState.DAMAGED);
            makeHtank(7, 21);
            make(o.createPump(), 9, 21, ObjectState.DAMAGED);
            makeHtank(11, 21);
            make(o.createPump(), 5, 21, ObjectState.DISABLED);
            make(o.createCombChamb(), 2, 20, ObjectState.DAMAGED);
            make(o.createThruster(), 0, 20);

            make(o.createTank(), 7, 26, ObjectState.DAMAGED);
            make(o.createPump(), 9, 26, ObjectState.DAMAGED);
            makeHtank(11, 26);
            make(o.createPump(), 5, 26, ObjectState.DISABLED);
            make(o.createTank(), 7, 27, ObjectState.DAMAGED);
            make(o.createPump(), 9, 27, ObjectState.DAMAGED);
            make(o.createTank(), 11, 27, ObjectState.DAMAGED);
            make(o.createPump(), 5, 27, ObjectState.DISABLED);
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

            tiles = new Components.TileSystemComponent(entity_);
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
