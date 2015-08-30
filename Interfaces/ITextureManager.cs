using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Interfaces
{
    interface ITextureManager
    {
        int loadTexture(String name);
        Texture2D getTexture(int id);
    }
}
