using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Counter
{
    public class NectarCounter : Counter
    {
        public async Task<int> DoubleNectar()
        {
            await semaphore.WaitAsync();
            count = count * 2;
            semaphore.Release();
            return count;
        }
    }
}
