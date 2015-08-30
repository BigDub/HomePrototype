using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private bool[,] tiles_;

        public GameEntity entity_;

        public Ship(GameEntity world)
        {
            tiles_ = new bool[width_, height_];
            entity_ = new GameEntity();
            entity_.spatial = new Components.SpatialComponent(entity_, world.spatial);
            //entity_.spatial = new Components.SpatialComponent(entity_, new Vector2(-width_ / 2, -height_ / 2), 0, Vector2.One);
            //entity_.physic = new Components.PhysicsComponent(entity_, Vector2.Zero, 0f, Vector2.Zero);
            entity_.physic = new Components.DriftPhysics(entity_, new Vector2(100), new Vector2(1), 0.1f);
            //entity_.physic = new Components.PhysicsComponent(entity_, new Vector2(1, 1), 0.1f, new Vector2(-0.01f));

            Locator.getComponentManager().addEntity(entity_);
            for (int x = -width_ / 2; x < width_ / 2; ++x)
            {
                for (int y = -height_ / 2; y < height_; ++y)
                {
                    GameEntity tile = new GameEntity();
                    tile.spatial = new Components.SpatialComponent(tile, entity_.spatial, new Vector2(x * 32, y * 32), 0, Vector2.One);
                    tile.render = new Components.RenderComponent(tile, tile_tex_id, 1, new Vector2(16), Color.White);
                    Locator.getComponentManager().addEntity(tile);
                }
            }
        }

        public bool isTileOpen(int x, int y)
        {
            if (x >= 0 && x < width_ && y >= 0 && y < height_)
            {
                return tiles_[x, y];
            }
            return false;
        }
    }
}
