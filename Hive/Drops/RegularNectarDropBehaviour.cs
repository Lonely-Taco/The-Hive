using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class RegularNectarDropBehaviour : IDropBehaviour
    {
        public async void Claim(Counter nectarCounter, DropManager dropManager, NectarDrop parentDrop)
        {
            await nectarCounter.AddCount(1);
            dropManager.RemoveDrop(parentDrop);
        }
    }
}