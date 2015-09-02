using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class TileCoord
    {
        private static float tileSize = 32f;

        public GameEntity entity_;

        private Point pcoord_;
        public Point coord_
        {
            get
            {
                return pcoord_;
            }
            set
            {
                pcoord_ = value;
                entity_.spatial.translation_ = getWorld();
            }
        }
        public Point size_;
        public TileSystemComponent tileSystem_;

        public TileCoord(GameEntity entity, Point coord, Point size, TileSystemComponent tileSystem)
        {
            entity_ = entity;
            tileSystem_ = tileSystem;
            coord_ = coord;
            size_ = size;
        }

        public Vector2 getWorld()
        {
            //Point mid = new Point(coord_.X + (size_.X / 2), coord_.Y + (size_.Y / 2));
            Vector2 topLeft = tileSystem_.getWorld(coord_);
            //return topLeft;
            return topLeft - new Vector2(16) + new Vector2(tileSize * size_.X / 2.0f, tileSize * size_.Y / 2.0f);
        }
        public bool isWithin(Point point)
        {
            return
                point.X < coord_.X + size_.X &&
                point.X >= coord_.X &&
                point.Y < coord_.Y + size_.Y &&
                point.Y >= coord_.Y;
        }
    }
}
