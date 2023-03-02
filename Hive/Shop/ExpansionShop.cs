using Hive.Utility;
using Microsoft.Xna.Framework;
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
        public ExpansionShop(Texture2D icon, Counter expansionCounter, Counter nectarCounter, Texture2D buttonTexture, Texture2D backgroundTexture, Vector2 position, SpriteFont font, Texture2D costIcon) 
            : base(icon, nectarCounter, buttonTexture, backgroundTexture, position, font, costIcon, expansionCounter)
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
