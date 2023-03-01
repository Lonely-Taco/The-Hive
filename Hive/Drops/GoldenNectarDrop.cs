using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class GoldenNectarDrop : NectarDrop
    {
        public GoldenNectarDrop(Counter nectarCounter, Texture2D texture, Vector2 position) : base(nectarCounter, 0, texture, position)
        {

        }

        public new async Task Claim()
        {
            await nectarCounter.DoubleNectar();
        }
    }
}