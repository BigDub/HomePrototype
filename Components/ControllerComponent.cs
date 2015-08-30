﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShipPrototype.Components
{
    class ControllerComponent
    {
        GameEntity entity_;

        ControllerComponent parent_;
        List<ControllerComponent> children_;

        bool[] keys;
        bool dir_dirty;
        Vector2 direction;

        public ControllerComponent(GameEntity entity)
        {
            entity_ = entity;
            children_ = new List<ControllerComponent>();
            keys = new bool[4];
            direction = Vector2.Zero;
            dir_dirty = true;
        }

        public void update()
        {
            if (dir_dirty)
            {
                direction = Vector2.Zero;
                if (keys[0])
                    direction += new Vector2(0, -1);
                if (keys[1])
                    direction += new Vector2(-1, 0);
                if (keys[2])
                    direction += new Vector2(0, 1);
                if (keys[3])
                    direction += new Vector2(1, 0);
                if (direction.LengthSquared() > 0)
                {
                    direction.Normalize();
                }
            }
            if (direction.LengthSquared() > 0)
            {
                entity_.spatial.translation_ += direction;
            }
        }

        public void KeyPressed(object sender, KeyboardEventArgs e)
        {
            switch (e.key_)
            {
                case Keys.W:
                    if (!keys[0])
                        dir_dirty = true;
                    keys[0] = true;
                    break;
                case Keys.A:
                    if (!keys[1])
                        dir_dirty = true;
                    keys[1] = true;
                    break;
                case Keys.S:
                    if (!keys[2])
                        dir_dirty = true;
                    keys[2] = true;
                    break;
                case Keys.D:
                    if (!keys[3])
                        dir_dirty = true;
                    keys[3] = true;
                    break;
            }
        }

        public void KeyReleased(object sender, KeyboardEventArgs e)
        {
            switch (e.key_)
            {
                case Keys.W:
                    if (keys[0])
                        dir_dirty = true;
                    keys[0] = false;
                    break;
                case Keys.A:
                    if (keys[0])
                        dir_dirty = true;
                    keys[1] = false;
                    break;
                case Keys.S:
                    if (keys[0])
                        dir_dirty = true;
                    keys[2] = false;
                    break;
                case Keys.D:
                    if (keys[0])
                        dir_dirty = true;
                    keys[3] = false;
                    break;
            }
        }
    }
}