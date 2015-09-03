using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Components;

namespace ShipPrototype.Services
{
    class ComponentManager
    {
        private static ComponentManager instance_;

        public static ComponentManager init()
        {
            Debug.Assert(instance_ == null);

            instance_ = new ComponentManager();
            return instance_;
        }

        private BlendState bs;

        private List<PhysicsComponent> physics_;
        private List<RenderComponent> renders_;
        private List<SpatialComponent> spatials_;
        private List<ControllerComponent> controllers_;
        private List<TileCoord> tiles_;
        public List<InfoComponent> info_;

        private List<GameEntity> removals_, additions_;

        private ComponentManager()
        {
            physics_ = new List<PhysicsComponent>();
            renders_ = new List<RenderComponent>();
            spatials_ = new List<SpatialComponent>();
            controllers_ = new List<ControllerComponent>();
            tiles_ = new List<TileCoord>();
            info_ = new List<InfoComponent>();

            additions_ = new List<GameEntity>();
            removals_ = new List<GameEntity>();
        }

        private void add(PhysicsComponent physic)
        {
            physics_.Add(physic);
        }
        private void add(RenderComponent render)
        {
            renders_.Add(render);
        }
        private void add(SpatialComponent spatial)
        {
            spatials_.Add(spatial);
        }
        private void add(ControllerComponent controller)
        {
            controllers_.Add(controller);
            controller.linkInput();
        }
        private void add(TileCoord tile)
        {
            tiles_.Add(tile);
        }
        private void add(InfoComponent info)
        {
            info_.Add(info);
        }

        public void addEntity_(GameEntity entity)
        {
            if (entity.physic != null)
                add(entity.physic);
            if (entity.render != null)
                add(entity.render);
            if (entity.spatial != null)
                add(entity.spatial);
            if (entity.controller != null)
                add(entity.controller);
            if (entity.tile != null)
                add(entity.tile);
            if (entity.info != null)
                add(entity.info);
        }

        public void addEntity(GameEntity entity)
        {
            additions_.Add(entity);
        }

        private void remove(InfoComponent info)
        {
            info_.Remove(info);
        }
        private void remove(TileCoord tile)
        {
            tiles_.Remove(tile);
        }
        private void remove(PhysicsComponent physic)
        {
            physics_.Remove(physic);
        }
        private void remove(RenderComponent render)
        {
            renders_.Remove(render);
        }
        private void remove(SpatialComponent spatial)
        {
            spatials_.Remove(spatial);
        }
        private void remove(ControllerComponent controller)
        {
            controllers_.Remove(controller);
            controller.unlinkInput();
        }

        public void removeEntity(GameEntity entity)
        {
            removals_.Add(entity);
        }

        private void removeEntity_(GameEntity entity)
        {
            if (entity.physic != null)
                remove(entity.physic);
            if (entity.render != null)
                remove(entity.render);
            if (entity.spatial != null)
                remove(entity.spatial);
            if (entity.controller != null)
                remove(entity.controller);
            if (entity.tile != null)
                remove(entity.tile);
            if (entity.info != null)
                remove(entity.info);
        }

        public GameEntity pick(TileSystemComponent tsc, Point point)
        {
            foreach (TileCoord tile in tiles_)
            {
                if (tile.tileSystem_ == tsc)
                {
                    if (tile.isWithin(point))
                    {
                        return tile.entity_;
                    }
                }
            }
            return null;
        }

        public void update(float elapsed)
        {
            foreach (ControllerComponent controller in controllers_)
            {
                controller.update(elapsed);
            }

            foreach (GameEntity entity in additions_)
            {
                addEntity_(entity);
            }
            additions_.Clear();
            foreach (GameEntity entity in removals_)
            {
                removeEntity_(entity);
            }
            removals_.Clear();

            foreach (PhysicsComponent physic in physics_)
            {
                physic.update(elapsed);
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            foreach (RenderComponent render in renders_)
            {
                render.render(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
