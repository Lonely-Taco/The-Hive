using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class RegularNectarDropBehaviour : IDropBehaviour
    {
        public async Task<int> Claim(Counter nectarCounter, DropManager dropManager)
        {
            int addedNectar = await nectarCounter.Add(1);
            return addedNectar;
        }
    }
}