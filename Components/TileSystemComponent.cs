using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype
{
    enum TileStatus
    {
        INVALID = 0,
        OPEN = 1,
        FILLED = 2
    };
}
namespace ShipPrototype.Components
{
    class TileSystemComponent : Component
    {
        private static float tileSize = 32;
        List<TileArea> areas_;

        public TileSystemComponent(GameEntity entity)
            : base(entity)
        {
            areas_ = new List<TileArea>();
        }

        public override Component deepCopy(GameEntity entity)
        {
            TileSystemComponent c = new TileSystemComponent(entity);
            foreach (TileArea area in areas_)
            {
                c.areas_.Add(area.deepCopy(c));
            }
            return c;
        }

        public GameEntity getEntityAt(Point point)
        {
            foreach (TileCoord tile in Locator.getComponentManager().tiles_)
            {
                if (tile.tileSystem_ != this)
                    continue;
                if (tile.coord_.X > point.X || tile.coord_.Y > point.Y)
                    continue;
                if (tile.coord_.X + tile.size_.X - 1 < point.X || tile.coord_.Y + tile.size_.Y - 1 < point.Y)
                    continue;
                return tile.entity_;
            }
            return null;
        }

        public void addArea(Point size, Point offset)
        {
            TileArea area = new TileArea(this, size, offset);

            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    Point temp = new Point(offset.X + x, offset.Y + y);
                    TileStatus status = getTile(temp);
                    if (status == TileStatus.INVALID)
                    {
                        area.setTile(temp, TileStatus.OPEN);
                    }
                    else
                    {
                        area.setTile(temp, status);
                    }
                }
            }

            areas_.Add(area);
        }

        public TileStatus getTile(Point point)
        {
            foreach (TileArea area in areas_)
            {
                if (area.isContained(point))
                    return area.getTile(point);
            }
            return TileStatus.INVALID;
        }
        public void setTile(Point point, TileStatus status)
        {
            foreach (TileArea area in areas_)
            {
                if (area.isContained(point))
                    area.setTile(point, status);
            }
        }

        public bool canBuild(Point point, Point size)
        {
            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    Point temp = new Point(point.X + x, point.Y + y);
                    if (getTile(temp) != TileStatus.OPEN)
                        return false;
                }
            }
            return true;
        }
        public void build(Point point, Point size)
        {
            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    Point temp = new Point(point.X + x, point.Y + y);
                    setTile(temp, TileStatus.FILLED);
                }
            }
        }
        public void open(Point point, Point size)
        {
            for (int x = 0; x < size.X; ++x)
            {
                for (int y = 0; y < size.Y; ++y)
                {
                    Point temp = new Point(point.X + x, point.Y + y);
                    setTile(temp, TileStatus.OPEN);
                }
            }
        }

        public Vector2 getWorld(Point point)
        {
            return new Vector2(point.X * tileSize, point.Y * tileSize);
        }

        public void generateTileEntities(GameEntity parent)
        {
            foreach (TileArea area in areas_)
            {
                area.generateTileEntities(parent);
            }
        }
    }
}
