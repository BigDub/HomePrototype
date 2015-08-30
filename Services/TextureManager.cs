﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Services
{
    class TextureManager : Interfaces.ITextureManager
    {
        private static Texture2D blank_;
        private static TextureManager instance_;

        public static TextureManager init(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Debug.Assert(instance_ == null);

            instance_ = new TextureManager(graphicsDevice, content);
            return instance_;
        }

        ContentManager content_;
        List<Texture2D> textures_;
        private TextureManager(GraphicsDevice graphicsDevice, ContentManager content)
        {
            content_ = content;
            textures_ = new List<Texture2D>();
            blank_ = new Texture2D(graphicsDevice, 1, 1);
            Color[] color = { Color.White };
            blank_.SetData<Color>(color);
        }

        public int loadTexture(String name)
        {
            textures_.Add(content_.Load<Texture2D>(name));
            return textures_.Count - 1;
        }

        public Texture2D getTexture(int id)
        {
            Debug.Assert(id >= -1 && id < textures_.Count);

            if (id == -1)
            {
                return blank_;
            }

            return textures_[id];
        }
    }
}
