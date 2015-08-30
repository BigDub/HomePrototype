﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Components
{
    class RenderComponent
    {
        GameEntity entity_;

        RenderComponent parent_;
        List<RenderComponent> children_;
        
        private int texture_id_;
        private int layer_;
        private Vector2 origin_;
        private Color color_;

        private RenderComponent(GameEntity entity)
        {
            entity_ = entity;
            children_ = new List<RenderComponent>();
        }

        public RenderComponent(GameEntity entity, int texture_id, int layer, Vector2 origin, Color color) : this(entity)
        {
            texture_id_ = texture_id;
            layer_ = layer;
            origin_ = origin;
            color_ = color;
        }

        public RenderComponent(GameEntity entity, RenderComponent parent) : this(entity)
        {
            parent_ = parent;
            parent.children_.Add(this);
            texture_id_ = parent_.texture_id_;
            layer_ = parent.layer_;
            origin_ = parent.origin_;
            color_ = parent.color_;
        }

        public void render(SpriteBatch spriteBatch)
        {
            if (texture_id_ >= -1)
            {
                SpatialComponent spatial = entity_.spatial;
                spriteBatch.Draw(Locator.getTextureManager().getTexture(texture_id_), new Vector2(spatial.w_translation.X, spatial.w_translation.Y), null, color_, spatial.w_rotation, origin_, spatial.w_scale, SpriteEffects.None, layer_);
            }
        }
    }
}
