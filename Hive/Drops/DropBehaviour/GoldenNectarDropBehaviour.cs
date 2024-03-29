﻿using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hive.Drops.DropBehaviour
{
    internal class GoldenNectarDropBehaviour : IDropBehaviour
    {
        public async Task<int> Claim(Counter nectarCounter)
        {
            int addedNectar = await nectarCounter.DoubleOrAdd20Nectar();
            return addedNectar;
        }
    }
}