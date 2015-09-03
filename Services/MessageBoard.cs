using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ShipPrototype.Services
{
    enum PostCategory
    {
        PLACED_OBJECT,
        REMOVED_OBJECT,
        TOOK_ITEM,
        PLACED_ITEM,
        END_GAME,
        SPAWN_JUNK,
        JUNK_SHOT,
        JUNK_SPAWN
    };

    class Post
    {
        public PostCategory category;
        public GameEntity sourceEntity;
        public GameEntity targetEntity;
        public int slot;

        public Post(PostCategory c, GameEntity s, GameEntity t, int sl)
        {
            category = c;
            sourceEntity = s;
            targetEntity = t;
            slot = sl;
        }
    }

    class MessageBoard
    {
        public delegate void notify(Post post);

        private notify
            placeObjectNotify,
            removeObjectNotify,
            takeItemNotify,
            placeItemNotify,
            special;

        private static MessageBoard instance_;
        public static MessageBoard init()
        {
            Debug.Assert(instance_ == null);

            instance_ = new MessageBoard();
            return instance_;
        }

        public void register(PostCategory category, notify func)
        {
            switch (category)
            {
                case PostCategory.PLACED_ITEM:
                    placeItemNotify += func;
                    break;
                case PostCategory.PLACED_OBJECT:
                    placeObjectNotify += func;
                    break;
                case PostCategory.REMOVED_OBJECT:
                    removeObjectNotify += func;
                    break;
                case PostCategory.TOOK_ITEM:
                    takeItemNotify += func;
                    break;
                default:
                    special += func;
                    break;
            }
        }

        public void postMessage(Post post)
        {
            Console.WriteLine("Recieved post: " + post.category);
            switch (post.category)
            {
                case PostCategory.PLACED_ITEM:
                    if (placeItemNotify != null)
                    {
                        placeItemNotify(post);
                    }
                    break;
                case PostCategory.PLACED_OBJECT:
                    if (placeObjectNotify != null)
                    {
                        placeObjectNotify(post);
                    }
                    break;
                case PostCategory.REMOVED_OBJECT:
                    if (removeObjectNotify != null)
                    {
                        removeObjectNotify(post);
                    }
                    break;
                case PostCategory.TOOK_ITEM:
                    if (takeItemNotify != null)
                    {
                        takeItemNotify(post);
                    }
                    break;
                default:
                    if (special != null)
                    {
                        special(post);
                    }
                    break;
            }
        }
    }
}
