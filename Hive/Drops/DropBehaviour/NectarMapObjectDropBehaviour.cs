using Hive.Map;
using Hive.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Drops.DropBehaviour
{
    internal class NectarMapObjectDropBehaviour : IDropBehaviour
    {
        private Counter expansionCounter;
        public NectarMapObjectDropBehaviour(Counter expansionCounter)
        {
            this.expansionCounter = expansionCounter;
        }

        public async Task<int> Claim(Counter nectarCounter)
        {
            int addedNectar = await nectarCounter.Add(CalculateAddedNectar());
            return addedNectar;
        }

        private int CalculateAddedNectar()
        {
            return 4 + expansionCounter.GetCount();
        }
    }
}
