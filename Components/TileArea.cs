using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class TileArea
    {
        TileSystemComponent parent_;
        Point size_;
        Point offset_;
        TileStatus[,] tiles_;

        public Point size
        {
            get
            {
                return size_;
            }
        }
        public Point offset
        {
            get
            {
                return offset_;
            }
        }

        public TileArea(TileSystemComponent parent, Point size, Point offset)
        {
            parent_ = parent;
            size_ = size;
            offset_ = offset;

            tiles_ = new TileStatus[size_.X, size_.Y];
        }

        //parentspace
        public bool isContained(Point point)
        {
            Point p = new Point(point.X - offset_.X, point.Y - offset_.Y);
            if (p.X < 0 || p.Y < 0)
                return false;
            return p.X < size_.X && p.Y < size_.Y;
        }

        //parentspace
        public TileStatus getTile(Point point)
        {
            Point p = new Point(point.X - offset_.X, point.Y - offset_.Y);
            return tiles_[p.X, p.Y];
        }

        //localspace
        public TileStatus getLocalTile(Point p)
        {
            return tiles_[p.X, p.Y];
        }

        public void setTile(Point point, TileStatus ts)
        {
            Point p = new Point(point.X - offset_.X, point.Y - offset_.Y);
            tiles_[p.X, p.Y] = ts;
        }

        public void setLocalTile(Point p, TileStatus ts)
        {
            tiles_[p.X, p.Y] = ts;
        }

        public void generateTileEntities(GameEntity parent)
        {
            int tile_tex_id = Locator.getTextureManager().loadTexture("tile32");
            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    GameEntity tile = new GameEntity();
                    tile.spatial = new Components.SpatialComponent(tile, parent.spatial, new Vector2((x + offset_.X) * 32, (y + offset_.Y) * 32), 0, Vector2.One);
                    tile.render = new Components.RenderComponent(tile, tile_tex_id, 1, new Vector2(16), Color.White);
                    Locator.getComponentManager().addEntity(tile);
                }
            }
        }
    }
}
