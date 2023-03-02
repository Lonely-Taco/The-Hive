﻿using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shop
{
    internal class AntShop : Shop
    {
        private Counter antCounter;

        public AntShop(Texture2D icon, Counter antCounter, Counter nectarCounter, Texture2D buttonTexture, Texture2D backgroundTexture, Vector2 position, SpriteFont font)
            : base(icon, nectarCounter, buttonTexture, backgroundTexture, position, font)
        {
            this.antCounter = antCounter;
        }

        public async override void Buy()
        {
            int antAmount = await antCounter.AddCount(1);
            await nectarCounter.AddCount(-CurrentCost(antAmount));
        }

        public override int CurrentCost(int currentAmount)
        {
            return 5 * currentAmount; //TODO: change later
        }


    }
}
