using Hive.Utility;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shop
{
    internal class ExpansionShop : Shop
    {
        private Counter expansionCounter;
        public ExpansionShop(Texture2D icon, Counter expansionCounter, Counter nectarCounter) : base(icon, nectarCounter)
        {
            this.expansionCounter = expansionCounter;
        }

        public async override void Buy()
        {
            int expansionLevel = await expansionCounter.AddCount(1);
            await nectarCounter.AddCount(-CurrentCost(expansionLevel));
        }

        public override int CurrentCost(int currentAmount)
        {
            return currentAmount * 5; //TODO: change later
        }
    }
}
