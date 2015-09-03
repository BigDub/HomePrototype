using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ShipPrototype.Components
{
    class SpatialComponent
    {
        GameEntity entity_;

        SpatialComponent parent_;

        private SpatialComponent(GameEntity entity)
        {
            entity_ = entity;
        }

        public SpatialComponent(GameEntity entity, Vector2 translation, float rotation, Vector2 scale) : this(entity)
        {
            translation_ = translation;
            rotation_ = rotation;
            scale_ = scale;
        }

        public SpatialComponent(GameEntity entity, SpatialComponent parent, Vector2 translation, float rotation, Vector2 scale) : this(entity)
        {
            parent_ = parent;
            translation_ = translation;
            rotation_ = rotation;
            scale_ = scale;
        }

        public SpatialComponent(GameEntity entity, SpatialComponent parent) : this(entity)
        {
            parent_ = parent;
            translation_ = Vector2.Zero;
            rotation_ = 0f;
            scale_ = Vector2.One;
        }

        private Matrix getMatrix()
        {
            Matrix srt = Matrix.CreateScale(scale_.X, scale_.Y, 1)
                * Matrix.CreateRotationZ(rotation_)
                * Matrix.CreateTranslation(translation_.X, translation_.Y, 0);
            if (parent_ != null)
            {
                return srt * parent_.getMatrix();
            }
            return srt;
        }
        public Vector2 worldToLocal(Vector2 world)
        {
            Matrix inv = Matrix.Invert(getMatrix());
            Vector3 loc = Vector3.Transform(new Vector3(world, 0), inv);
            return new Vector2(loc.X, loc.Y);
        }

        public Vector2 translation_ { get; set; }
        private float rotation_;
        public float rotation
        {
            get
            {
                return rotation_;
            }
            set
            {
                rotation_ = value % MathHelper.TwoPi;
            }
        }
        public Vector2 scale_ { get; set; }

        public Vector2 w_scale
        {
            get
            {
                if (parent_ != null)
                {
                    return parent_.w_scale * scale_;
                }
                else
                {
                    return scale_;
                }
            }
        }

        public float w_rotation
        {
            get
            {
                if (parent_ != null)
                {
                    return parent_.w_rotation + rotation_;
                }
                else
                {
                    return rotation_;
                }
            }
            set
            {
                if (parent_ != null)
                {
                    rotation_ = value - parent_.w_rotation;
                }
                else
                {
                    rotation_ = value;
                }
            }
        }

        public Vector2 w_translation
        {
            get
            {
                if (parent_ != null)
                {
                    Vector2 sca = parent_.w_scale;
                    Vector2 p_trans = parent_.w_translation;
                    float rot = parent_.w_rotation;
                    float sin = (float) Math.Sin(rot);
                    float cos = (float) Math.Cos(rot);
                    return new Vector2(
                        p_trans.X + sca.X * (cos * translation_.X - sin * translation_.Y),
                        p_trans.Y + sca.Y * (sin * translation_.X + cos * translation_.Y)
                    );
                }
                else
                {
                    return translation_;
                }
            }

            set
            {
                if (parent_ != null)
                {
                    translation_ = value - parent_.w_translation;
                }
                else
                {
                    translation_ = value;
                }
            }
        }
    }
}
