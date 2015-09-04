using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ShipPrototype.Components;

namespace ShipPrototype.Services
{
    class ObjectFactory
    {
        public InfoComponent
            gunInfo,
            debrisInfo,
            wreckInfo,
            tractorInfo,
            furnaceInfo,
            tankInfo,
            compressorInfo,
            pumpInfo,
            combustInfo,
            thrustInfo,
            reactorInfo,
            machineshopInfo;
        public ItemInfo
            orbItem,
            scrapItem,
            steelItem,
            pinionItem,
            motorItem;
        public void init()
        {
            initObjects();
            initItems();
        }
        private void initItems()
        {
            orbItem = new ItemInfo(Locator.getTextureManager().loadTexture("orb"), "Orb");
            scrapItem = new ItemInfo(Locator.getTextureManager().loadTexture("debris1"), "Scrap");
            steelItem = new ItemInfo(Locator.getTextureManager().loadTexture("steel"), "Steel");
            pinionItem = new ItemInfo(Locator.getTextureManager().loadTexture("pinion"), "Pinion");
            motorItem = new ItemInfo(Locator.getTextureManager().loadTexture("motor"), "Motor");
        }
        private void initObjects()
        {
            gunInfo = new InfoComponent();
            gunInfo.name = "Cannon";

            debrisInfo = new InfoComponent();
            debrisInfo.name = "Scrap";
            debrisInfo.type = ObjectType.LOOTABLE;

            wreckInfo = new InfoComponent();
            wreckInfo.name = "Wreck";
            wreckInfo.type = ObjectType.SHOOTABLE;

            tractorInfo = new InfoComponent();
            tractorInfo.name = "Tractor Beam";

            furnaceInfo = new InfoComponent();
            furnaceInfo.name = "Furnace";

            tankInfo = new InfoComponent();
            tankInfo.name = "Storage Tank";
            
            compressorInfo = new InfoComponent();
            compressorInfo.name = "Compressor";

            pumpInfo = new InfoComponent();
            pumpInfo.name = "Pump";

            combustInfo = new InfoComponent();
            combustInfo.name = "Combustion Chamber";

            thrustInfo = new InfoComponent();
            thrustInfo.name = "Thruster";

            reactorInfo = new InfoComponent();
            reactorInfo.name = "Reactor";

            machineshopInfo = new InfoComponent();
            machineshopInfo.name = "Machine Shop";
        }
        
        public GameEntity createGun(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("gun64"), 0, new Vector2(32), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            gun.info = new InfoComponent(gun, gunInfo);
            gun.inventory = new InventoryComponent(gun, 1);
            gun.controller = new TurretController(gun);
            return gun;
        }

        public GameEntity createTractor(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("mag128"), 0, new Vector2(64), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(4, 4), Locator.getShip().tiles);
            gun.info = new InfoComponent(gun, tractorInfo);
            gun.inventory = new InventoryComponent(gun, 4);
            gun.controller = new TractorController(gun);
            return gun;
        }

        public GameEntity createFurnace(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("furnace"), 0, new Vector2(32), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            //gun.inventory = new InventoryComponent(gun, 3);
            gun.info = new InfoComponent(gun, furnaceInfo);
            gun.production = new ProductionComponent(gun, scrapItem, steelItem, 1);
            return gun;
        }

        public GameEntity createTank(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("tank"), 0, new Vector2(32, 16), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(2, 1), Locator.getShip().tiles);
            gun.info = new InfoComponent(gun, tankInfo);
            return gun;
        }

        public GameEntity createCompressor(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("compressor"), 0, new Vector2(32, 16), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(2, 1), Locator.getShip().tiles);
            gun.inventory = new InventoryComponent(gun, 2);
            gun.info = new InfoComponent(gun, compressorInfo);
            return gun;
        }

        public GameEntity createPump(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("pump"), 0, new Vector2(32, 16), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(2, 1), Locator.getShip().tiles);
            gun.inventory = new InventoryComponent(gun, 2);
            gun.info = new InfoComponent(gun, pumpInfo);
            return gun;
        }

        public GameEntity createCombChamb(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("cchamber"), 0, new Vector2(48, 32), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(3, 2), Locator.getShip().tiles);
            gun.info = new InfoComponent(gun, combustInfo);
            return gun;
        }

        public GameEntity createThruster(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("thrust"), 0, new Vector2(32, 32), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            gun.info = new InfoComponent(gun, thrustInfo);
            return gun;
        }

        public GameEntity createReactor(int num)
        {
            GameEntity gun = new GameEntity();
            gun.render = new RenderComponent(gun, Locator.getTextureManager().loadTexture("reactor"), 0, new Vector2(64), Color.White);
            gun.spatial = new SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new TileCoord(gun, Point.Zero, new Point(4, 4), Locator.getShip().tiles);
            gun.inventory = new InventoryComponent(gun, 3);
            gun.info = new InfoComponent(gun, reactorInfo);
            return gun;
        }

        public GameEntity createMachineShop(int num)
        {
            GameEntity e = new GameEntity();
            e.render = new RenderComponent(e, Locator.getTextureManager().loadTexture("machineshop"), 0, new Vector2(32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            //e.inventory = new InventoryComponent(e, 3);
            e.production = new ProductionComponent(e, steelItem, pinionItem, 1);
            e.info = new InfoComponent(e, machineshopInfo);
            return e;
        }

        public GameEntity createEndGameFlame()
        {
            GameEntity e = new GameEntity();
            e.render = new RenderComponent(e, Locator.getTextureManager().loadTexture("flame"), 0, new Vector2(96, 40), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial, new Vector2(-16, 336), 0, Vector2.One);
            return e;
        }

        public GameEntity createLaser(Vector2 location, float rotation, float distance)
        {
            GameEntity e = new GameEntity();
            e.render = new RenderComponent(e, Locator.getTextureManager().loadTexture("laser64"), 1, new Vector2(0, 32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getWorld().spatial, Vector2.Zero, rotation, new Vector2(distance, 1));
            e.spatial.w_translation = location;
            e.controller = new ParticleController(e, 0.5f);
            return e;
        }

        public GameEntity createTractorEffect(Vector2 location, Vector2 destination)
        {
            GameEntity e = new GameEntity();
            e.render = new RenderComponent(e, Locator.getTextureManager().loadTexture("tractorEffect"), 1, new Vector2(32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getWorld().spatial, location, 0, Vector2.One);
            e.spatial.w_translation = location;
            e.controller = new ParticleController(e, 1.0f);

            e.physic = new PhysicsComponent(e, destination - location, 0, new Vector2(-1f));
            return e;
        }

        public GameEntity createDebris()
        {
            GameEntity deb = new GameEntity();
            int num = Locator.getRandom().Next(3);
            int tex = -1;
            switch (num)
            {
                case 0:
                    tex = Locator.getTextureManager().loadTexture("debris1");
                    break;
                case 1:
                    tex = Locator.getTextureManager().loadTexture("debris2");
                    break;
                case 2:
                    tex = Locator.getTextureManager().loadTexture("debris3");
                    break;
            }
            deb.render = new RenderComponent(deb, tex, 0, new Vector2(32), Color.White);
            deb.spatial = new SpatialComponent(deb, Locator.getWorld().spatial);
            deb.physic = new PhysicsComponent(deb, Vector2.Zero, (float) Locator.getRandom().NextDouble() * MathHelper.PiOver2, Vector2.Zero);
            deb.info = new InfoComponent(deb, debrisInfo);

            return deb;
        }

        public GameEntity createWreck()
        {
            GameEntity deb = new GameEntity();
            int num = Locator.getRandom().Next(3);
            int tex = -1;
            switch (num)
            {
                case 0:
                    tex = Locator.getTextureManager().loadTexture("debris1");
                    break;
                case 1:
                    tex = Locator.getTextureManager().loadTexture("debris2");
                    break;
                case 2:
                    tex = Locator.getTextureManager().loadTexture("debris3");
                    break;
            }
            deb.render = new RenderComponent(deb, tex, 0, new Vector2(32), Color.White);
            deb.spatial = new SpatialComponent(deb, Locator.getWorld().spatial, Vector2.Zero, 0, new Vector2(3));
            deb.physic = new PhysicsComponent(deb, Vector2.Zero, (float)Locator.getRandom().NextDouble() * 0.5f, Vector2.Zero);
            deb.info = new InfoComponent(deb, wreckInfo);
            deb.controller = new WreckController(deb);

            return deb;
        }

        public GameEntity createTitle()
        {
            GameEntity e = new GameEntity();
            e.spatial = new SpatialComponent(e, Vector2.Zero, 0, Vector2.One);
            e.render = new RenderComponent(e, Locator.getTextureManager().loadTexture("title"), 0, Vector2.Zero, new Color(1f, 1f, 1f, 0f));
            e.controller = new ParticleController(e, 0);
            ((ParticleController)e.controller).opacity = 0;
            ((ParticleController)e.controller).fadeSpeed = -0.2f;
            return e;
        }

        public GameEntity createOrb()
        {
            GameEntity e = new GameEntity();
            e.item = orbItem;
            return e;
        }
        public GameEntity createScrap()
        {
            GameEntity e = new GameEntity();
            e.item = scrapItem;
            return e;
        }
        public GameEntity createSteel()
        {
            GameEntity e = new GameEntity();
            e.item = steelItem;
            return e;
        }
        public GameEntity createPinion()
        {
            GameEntity e = new GameEntity();
            e.item = pinionItem;
            return e;
        }
        public GameEntity createMotor()
        {
            GameEntity e = new GameEntity();
            e.item = motorItem;
            return e;
        }
    }
}

