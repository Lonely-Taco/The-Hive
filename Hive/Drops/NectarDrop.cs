using Hive.Counter;
using System.Threading.Tasks;

namespace Hive.Drops
{
    public class NectarDrop
    {
        protected NectarCounter nectarCounter;
        private int nectarValue;

        public NectarDrop(NectarCounter nectarCounter, int nectarValue)
        {
            this.nectarCounter = nectarCounter;
            this.nectarValue = nectarValue;
        }

        public async Task Claim()
        {
            await nectarCounter.AddCount(nectarValue);
        }
    }
}