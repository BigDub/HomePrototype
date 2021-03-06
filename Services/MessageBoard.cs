﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using ShipPrototype.Components;

namespace ShipPrototype.Services
{
    enum PostCategory
    {
        PLACING_OBJECT,
        PLACED_OBJECT,
        REMOVED_OBJECT,
        REQUEST_ITEM,
        PLACED_ITEM,
        INV_SLOT,
        REPAIRED_ENGINE,
        END_GAME,
        SPAWN_JUNK,
        JUNK_SHOT,
        JUNK_SPAWN,
        UNUSED
    };

    class Post
    {
        public PostCategory category;
        public GameEntity sourceEntity;
        public GameEntity targetEntity;
        public Component component;
        public int slot;

        public Post(PostCategory c, GameEntity s = null, GameEntity t = null, Component comp = null, int sl = 0)
        {
            category = c;
            sourceEntity = s;
            targetEntity = t;
            component = comp;
            slot = sl;
        }
    }

    class MessageBoard
    {
        public delegate void Notify(Post post);

        private Notify onPost_;

        private static MessageBoard instance_;
        public static MessageBoard init()
        {
            Debug.Assert(instance_ == null);

            instance_ = new MessageBoard();
            return instance_;
        }

        public void register(Notify func)
        {
            onPost_ += func;
        }

        public void unregister(Notify func)
        {
            onPost_ -= func;
        }

        public void postMessage(Post post)
        {
            Console.WriteLine("Recieved post: " + post.category);
            onPost_(post);
        }
    }
}
