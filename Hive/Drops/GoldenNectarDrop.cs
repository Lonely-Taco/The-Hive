using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class GoldenNectarDrop : NectarDrop
    {
        public GoldenNectarDrop(Counter nectarCounter, Texture2D texture, Vector2 position, DropManager dropManager) : base(nectarCounter, 0, texture, position, dropManager)
        {

        }

        public new async Task Claim()
        {
            await nectarCounter.DoubleNectar();
        }
    }
}