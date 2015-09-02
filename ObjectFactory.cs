using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype
{
    class ObjectFactory
    {
        public Components.InfoComponent gunInfo;
        public Components.InfoComponent debrisInfo;
        public Components.InfoComponent tractorInfo;
        public Components.InfoComponent furnaceInfo;
        public void init()
        {
            gunInfo = new Components.InfoComponent();
            gunInfo.name = "Cannon";
            gunInfo.type = Components.ObjectType.SHIPBOARD;

            debrisInfo = new Components.InfoComponent();
            debrisInfo.name = "Debris";
            debrisInfo.type = Components.ObjectType.LOOTABLE;

            tractorInfo = new Components.InfoComponent();
            tractorInfo.name = "Tractor Beam";
            tractorInfo.type = Components.ObjectType.SHIPBOARD;

            furnaceInfo = new Components.InfoComponent();
            furnaceInfo.name = "Furnace";
            furnaceInfo.type = Components.ObjectType.SHIPBOARD;
        }
        
        public GameEntity createGun()
        {
            GameEntity gun = new GameEntity();
            gun.render = new Components.RenderComponent(gun, Locator.getTextureManager().loadTexture("gun64"), 0, new Vector2(32), Color.White);
            gun.spatial = new Components.SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new Components.TileCoord(gun, Point.Zero, new Point(2, 2), Locator.getShip().tiles);
            gun.info = new Components.InfoComponent(gun, gunInfo);
            gun.physic = new Components.PhysicsComponent(gun, Vector2.Zero, 0.2f, Vector2.Zero);
            return gun;
        }

        public GameEntity createTractor()
        {
            GameEntity gun = new GameEntity();
            gun.render = new Components.RenderComponent(gun, Locator.getTextureManager().loadTexture("mag128"), 0, new Vector2(64), Color.White);
            gun.spatial = new Components.SpatialComponent(gun, Locator.getShip().entity_.spatial);
            gun.tile = new Components.TileCoord(gun, Point.Zero, new Point(4, 4), Locator.getShip().tiles);
            gun.info = new Components.InfoComponent(gun, tractorInfo);
            return gun;
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
            deb.render = new Components.RenderComponent(deb, tex, 0, new Vector2(32), Color.White);
            deb.spatial = new Components.SpatialComponent(deb, Locator.getWorld().spatial);
            deb.physic = new Components.PhysicsComponent(deb, Vector2.Zero, (float) Locator.getRandom().NextDouble() * 3.14f, Vector2.One);

            return deb;
        }
    }
}
