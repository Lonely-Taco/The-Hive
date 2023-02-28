using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Counter
{
    public abstract class Counter
    {
        protected SemaphoreSlim semaphore;
        protected int count;
        public Counter() 
        { 
            count = 0;
            semaphore = new SemaphoreSlim(1);
        }

        public async Task<int> AddCount(int addedInteger)
        {
            await semaphore.WaitAsync();
            count = count + addedInteger;
            semaphore.Release();
            return this.count;
        }

        public async Task ResetCount()
        {
            await semaphore.WaitAsync();
            count = 0;
            semaphore.Release();
        }

        public int GetCount()
        {
            return count;
        }

    }
}
