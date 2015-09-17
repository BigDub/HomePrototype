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
        public List<GameEntity> entities;
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
            machineshopInfo,
            grinderInfo;
        public ItemInfo
            orbItem,
            scrapItem,
            steelItem,
            pinionItem,
            motorItem,
            oxygenItem,
            hydrogenItem,
            gunItemInfo,
            tractorItemInfo,
            furnaceItemInfo,
            tankItemInfo,
            compressorItemInfo,
            pumpItemInfo,
            combustItemInfo,
            thrustItemInfo,
            reactorItemInfo,
            machineshopItemInfo,
            grinderItemInfo;
        public void init()
        {
            entities = new List<GameEntity>();
            initObjects();
            initItems();
        }
        private void initItems()
        {
            orbItem = new ItemInfo(Locator.getTextureManager().loadTexture("orb"));
            scrapItem = new ItemInfo(Locator.getTextureManager().loadTexture("debris1"));
            steelItem = new ItemInfo(Locator.getTextureManager().loadTexture("steel"));
            pinionItem = new ItemInfo(Locator.getTextureManager().loadTexture("pinion"));
            motorItem = new ItemInfo(Locator.getTextureManager().loadTexture("motor"));
            oxygenItem = new ItemInfo(Locator.getTextureManager().loadTexture("o2"));
            hydrogenItem = new ItemInfo(Locator.getTextureManager().loadTexture("h2"));
        }
        private void initObjects()
        {
            int typeId = 0;
            gunInfo = new InfoComponent(typeId++);
            gunInfo.name = "Cannon";
            gunInfo.flavorText = "Dual purpose weaponry can defend or mine.\nRequires blue orb to function.";
            gunItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("gun64"));
            gunItemInfo.structureID_ = gunInfo.typeID_;
            entities.Add(createGun());

            debrisInfo = new InfoComponent(typeId++);
            debrisInfo.name = "Scrap";
            debrisInfo.type = ObjectType.LOOTABLE;
            entities.Add(createDebris());

            wreckInfo = new InfoComponent(typeId++);
            wreckInfo.name = "Wreck";
            wreckInfo.type = ObjectType.SHOOTABLE;
            entities.Add(createWreck());

            tractorInfo = new InfoComponent(typeId++);
            tractorInfo.name = "Tractor Beam";
            tractorInfo.flavorText = "Pulls objects into the ship.\nRequires blue orb to function.";
            tractorItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("mag128"));
            tractorItemInfo.structureID_ = tractorInfo.typeID_;
            entities.Add(createTractor());

            furnaceInfo = new InfoComponent(typeId++);
            furnaceInfo.name = "Furnace";
            furnaceInfo.flavorText = "Reforms scrap into usable steel.";
            furnaceItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("furnace"));
            furnaceItemInfo.structureID_ = furnaceInfo.typeID_;
            entities.Add(createFurnace());

            tankInfo = new InfoComponent(typeId++);
            tankInfo.name = "Storage Tank";
            tankInfo.flavorText = "Stores liquid or gaseous substances.";
            tankItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("tank"));
            tankItemInfo.structureID_ = tankInfo.typeID_;
            entities.Add(createTank());
            
            compressorInfo = new InfoComponent(typeId++);
            compressorInfo.name = "Compressor";
            compressorInfo.flavorText = "Pressurizes gas, moving it from one entity to another.";
            compressorItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("compressor"));
            compressorItemInfo.structureID_ = compressorInfo.typeID_;
            entities.Add(createCompressor());

            pumpInfo = new InfoComponent(typeId++);
            pumpInfo.name = "Pump";
            pumpInfo.flavorText = "Pumps liquids from one entity to another.\nRequires a motor and pinion to function.";
            pumpItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("pump"));
            pumpItemInfo.structureID_ = pumpInfo.typeID_;
            entities.Add(createPump());

            combustInfo = new InfoComponent(typeId++);
            combustInfo.name = "Combustion Chamber";
            combustInfo.flavorText = "Burns oxygen and hydrogen.";
            combustItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("cchamber"));
            combustItemInfo.structureID_ = combustInfo.typeID_;
            entities.Add(createCombChamb());

            thrustInfo = new InfoComponent(typeId++);
            thrustInfo.name = "Thruster";
            thrustInfo.flavorText = "Directs exploding materials to provide thrust.";
            thrustItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("thrust"));
            thrustItemInfo.structureID_ = thrustInfo.typeID_;
            entities.Add(createThruster());

            reactorInfo = new InfoComponent(typeId++);
            reactorInfo.name = "Reactor";
            reactorInfo.flavorText = "Provides power.";
            reactorItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("reactor"));
            reactorItemInfo.structureID_ = reactorInfo.typeID_;
            entities.Add(createReactor());

            machineshopInfo = new InfoComponent(typeId++);
            machineshopInfo.name = "Machine Shop";
            machineshopInfo.flavorText = "Creates various components from steel.";
            machineshopItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("machineshop"));
            machineshopItemInfo.structureID_ = machineshopInfo.typeID_;
            entities.Add(createMachineShop());

            grinderInfo = new InfoComponent(typeId++);
            grinderInfo.name = "Grinder";
            grinderInfo.flavorText = "Turns things into scrap.";
            grinderItemInfo = new ItemInfo(Locator.getTextureManager().loadTexture("grinder"));
            grinderItemInfo.structureID_ = grinderInfo.typeID_;
            entities.Add(createGrinder());
        }

        public GameEntity createEntity(int typeID)
        {
            foreach (GameEntity entity in entities)
            {
                if (entity.info.typeID_ == typeID)
                    return entity.deepCopy();
            }
            return null;
        }

        public GameEntity createGun(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("gun64");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            e.info = new InfoComponent(e, gunInfo);
            e.inventory = new InventoryComponent(e, 1);
            e.controller = new TurretController(e);
            e.item = new ItemInfo(e, gunItemInfo);
            return e;
        }

        public GameEntity createTractor(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("mag128");
            e.render = new RenderComponent(e, tex, 0, new Vector2(64), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(4, 4), Locator.getShip().tiles);
            e.info = new InfoComponent(e, tractorInfo);
            e.inventory = new InventoryComponent(e, 2);
            e.controller = new TractorController(e);
            e.item = new ItemInfo(e, tractorItemInfo);
            return e;
        }

        public GameEntity createFurnace(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("furnace");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            //e.inventory = new InventoryComponent(e, 3);
            e.info = new InfoComponent(e, furnaceInfo);
            e.production = new ProductionComponent(e, scrapItem, steelItem, 1);
            e.item = new ItemInfo(e, furnaceItemInfo);
            return e;
        }

        public GameEntity createTank(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("tank");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32, 16), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 1), Locator.getShip().tiles);
            e.info = new InfoComponent(e, tankInfo);
            e.inventory = new InventoryComponent(e, 1, 100);
            e.item = new ItemInfo(e, tankItemInfo);
            return e;
        }

        public GameEntity createCompressor(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("compressor");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32, 16), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 1), Locator.getShip().tiles);
            e.inventory = new InventoryComponent(e, 2);
            e.info = new InfoComponent(e, compressorInfo);
            e.item = new ItemInfo(e, compressorItemInfo);
            return e;
        }

        public GameEntity createPump(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("pump");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32, 16), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 1), Locator.getShip().tiles);
            e.inventory = new InventoryComponent(e, 2);
            e.info = new InfoComponent(e, pumpInfo);
            e.controller = new PumpController(e);
            e.item = new ItemInfo(e, pumpItemInfo);
            return e;
        }

        public GameEntity createCombChamb(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("cchamber");
            e.render = new RenderComponent(e, tex, 0, new Vector2(48, 32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(3, 2), Locator.getShip().tiles);
            e.info = new InfoComponent(e, combustInfo);
            e.inventory = new InventoryComponent(e, 2);
            e.item = new ItemInfo(e, combustItemInfo);
            return e;
        }

        public GameEntity createThruster(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("thrust");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32, 32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            e.info = new InfoComponent(e, thrustInfo);
            e.item = new ItemInfo(e, thrustItemInfo);
            return e;
        }

        public GameEntity createReactor(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("reactor");
            e.render = new RenderComponent(e, tex, 0, new Vector2(64), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(4, 4), Locator.getShip().tiles);
            e.inventory = new InventoryComponent(e, 3);
            e.info = new InfoComponent(e, reactorInfo);
            e.item = new ItemInfo(e, reactorItemInfo);
            return e;
        }

        public GameEntity createMachineShop(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("machineshop");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            //e.inventory = new InventoryComponent(e, 3);
            e.production = new ProductionComponent(e, steelItem, pinionItem, 1);
            e.info = new InfoComponent(e, machineshopInfo);
            e.item = new ItemInfo(e, machineshopItemInfo);
            return e;
        }

        public GameEntity createGrinder(int num = 0)
        {
            GameEntity e = new GameEntity();
            int tex = Locator.getTextureManager().loadTexture("grinder");
            e.render = new RenderComponent(e, tex, 0, new Vector2(32), Color.White);
            e.spatial = new SpatialComponent(e, Locator.getShip().entity_.spatial);
            e.tile = new TileCoord(e, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            //e.inventory = new InventoryComponent(e, 3);
            e.production = new ProductionComponent(e, null, scrapItem, 1);
            e.info = new InfoComponent(e, grinderInfo);
            e.item = new ItemInfo(e, grinderItemInfo);
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

        public GameEntity createItem(ItemInfo item, int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, item, count);
            return e;
        }
        public GameEntity createOrb(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, orbItem, count);
            return e;
        }
        public GameEntity createScrap(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, scrapItem, count);
            return e;
        }
        public GameEntity createSteel(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, steelItem, count);
            return e;
        }
        public GameEntity createPinion(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, pinionItem, count);
            return e;
        }
        public GameEntity createMotor(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, motorItem, count);
            return e;
        }
        public GameEntity createOxygen(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, oxygenItem, count);
            return e;
        }
        public GameEntity createHydrogen(int count = 1)
        {
            GameEntity e = new GameEntity();
            e.item = new ItemInfo(e, hydrogenItem, count);
            return e;
        }
    }
}

