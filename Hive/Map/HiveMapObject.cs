using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Map
{
    public abstract class HiveMapObject : DrawnEntity
    {

        protected HiveMapObject(Texture2D texture, Vector2 position) : base(texture, position, 1f)
        {
        }

    }
}
