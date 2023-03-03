using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shops
{
    internal class AntShop : Shop
    {

        public AntShop(Texture2D icon, Counter antCounter, Counter nectarCounter, Vector2 position)
            : base(icon, nectarCounter, position, antCounter, 1f)
        {

        }

        public override int CurrentCost()
        {
            return 5 * counter.GetCount(); //TODO: change later
        }


    }
}
