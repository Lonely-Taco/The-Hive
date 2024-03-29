﻿using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace Hive.Drops.DropBehaviour
{
    internal class RegularNectarDropBehaviour : IDropBehaviour
    {
        public async Task<int> Claim(Counter nectarCounter)
        {
            int addedNectar = await nectarCounter.Add(1);
            return addedNectar;
        }
    }
}