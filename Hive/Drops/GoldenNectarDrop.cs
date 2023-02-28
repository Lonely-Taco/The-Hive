using Hive.Counter;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class GoldenNectarDrop : NectarDrop
    {
        public GoldenNectarDrop(NectarCounter nectarCounter) : base(nectarCounter, 0)
        {

        }

        public new async Task Claim()
        {
            await nectarCounter.DoubleNectar();
        }
    }
}