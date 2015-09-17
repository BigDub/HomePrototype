using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShipPrototype.Components;

namespace ShipPrototype
{
    class GameEntity
    {
        public RenderComponent render { get; set; }
        public SpatialComponent spatial { get; set; }
        public PhysicsComponent physic { get; set; }
        public ControllerComponent controller { get; set; }
        public TileCoord tile { get; set; }
        public InfoComponent info { get; set; }
        public InventoryComponent inventory { get; set; }
        public ProductionComponent production { get; set; }
        public ItemInfo item { get; set; }

        public GameEntity deepCopy()
        {
            GameEntity entity = new GameEntity();

            if (render != null)
            {
                entity.render = (RenderComponent)render.deepCopy(entity);
            }
            if (spatial != null)
            {
                entity.spatial = (SpatialComponent)spatial.deepCopy(entity);
            }
            if (physic != null)
            {
                entity.physic = (PhysicsComponent)physic.deepCopy(entity);
            }
            if (controller != null)
            {
                entity.controller = (ControllerComponent)controller.deepCopy(entity);
            }
            if (tile != null)
            {
                entity.tile = (TileCoord)tile.deepCopy(entity);
            }
            if (info != null)
            {
                entity.info = (InfoComponent)info.deepCopy(entity);
            }
            if (inventory != null)
            {
                entity.inventory = (InventoryComponent)inventory.deepCopy(entity);
            }
            if (production != null)
            {
                entity.production = (ProductionComponent)production.deepCopy(entity);
            }
            if (item != null)
            {
                entity.item = (ItemInfo)item.deepCopy(entity);
            }

            return entity;
        }
    }
}
