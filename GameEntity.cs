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
    }
}
