using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

using ShipPrototype.Components;

namespace ShipPrototype.Services
{
    class ComponentManager : Interfaces.IComponentManager
    {
        private static ComponentManager instance_;

        public static ComponentManager init()
        {
            Debug.Assert(instance_ == null);

            instance_ = new ComponentManager();
            return instance_;
        }

        private List<PhysicsComponent> physics_;
        private List<RenderComponent> renders_;
        private List<SpatialComponent> spatials_;
        private List<ControllerComponent> controllers_;
        private ComponentManager()
        {
            physics_ = new List<PhysicsComponent>();
            renders_ = new List<RenderComponent>();
            spatials_ = new List<SpatialComponent>();
            controllers_ = new List<ControllerComponent>();
        }

        private void addPhysic(PhysicsComponent physic)
        {
            physics_.Add(physic);
        }
        private void addRender(RenderComponent render)
        {
            renders_.Add(render);
        }
        private void addSpatial(SpatialComponent spatial)
        {
            spatials_.Add(spatial);
        }
        private void addController(ControllerComponent controller)
        {
            controllers_.Add(controller);
            Locator.getInputHandler().addKeyPressObserver(controller.KeyPressed);
            Locator.getInputHandler().addKeyReleaseObserver(controller.KeyReleased);
        }

        public void addEntity(GameEntity entity)
        {
            if (entity.physic != null)
            {
                addPhysic(entity.physic);
            }
            if (entity.render != null)
            {
                addRender(entity.render);
            }
            if (entity.spatial != null)
            {
                addSpatial(entity.spatial);
            }
            if (entity.controller != null)
            {
                addController(entity.controller);
            }
        }

        private void removePhysic(PhysicsComponent physic)
        {
            physics_.Remove(physic);
        }
        private void removeRender(RenderComponent render)
        {
            renders_.Remove(render);
        }
        private void removeSpatial(SpatialComponent spatial)
        {
            spatials_.Remove(spatial);
        }
        private void removeController(ControllerComponent controller)
        {
            controllers_.Remove(controller);
            Locator.getInputHandler().removeKeyPressObserver(controller.KeyPressed);
            Locator.getInputHandler().removeKeyReleaseObserver(controller.KeyReleased);
        }

        public void removeEntity(GameEntity entity)
        {
            if (entity.physic != null)
            {
                removePhysic(entity.physic);
            }
            if (entity.render != null)
            {
                removeRender(entity.render);
            }
            if (entity.spatial != null)
            {
                removeSpatial(entity.spatial);
            }
            if (entity.controller != null)
            {
                removeController(entity.controller);
            }
        }

        public void update()
        {
            foreach (ControllerComponent controller in controllers_)
            {
                controller.update();
            }
            foreach (PhysicsComponent physic in physics_)
            {
                physic.update();
            }
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
            foreach (RenderComponent render in renders_)
            {
                render.render(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
