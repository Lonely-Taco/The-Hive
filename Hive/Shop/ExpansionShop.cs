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
    internal class ExpansionShop : Shop
    {
        public ExpansionShop(Texture2D icon, Counter expansionCounter, Counter nectarCounter, Vector2 position) 
            : base(icon, nectarCounter, position, expansionCounter)
        {

        }

        public async override void Buy()
        {
            await nectarCounter.AddCount(-CurrentCost());
            int expansionLevel = await counter.AddCount(1);
        }

        public override int CurrentCost()
        {
            return counter.GetCount() * 5; //TODO: change later
        }
    }
}
