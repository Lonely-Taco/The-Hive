using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Utility
{
    public class Counter : DrawnEntity
    {
        private SemaphoreSlim semaphore;
        private int count;
        public Counter(Texture2D texture, Vector2 position) : base(texture, position)
        {
            count = 0;
            semaphore = new SemaphoreSlim(1);
        }

        public async Task<int> AddCount(int addedInteger)
        {
            await semaphore.WaitAsync();
            count = count + addedInteger;
            semaphore.Release();
            return count;
        }
        public async Task<int> DoubleNectar()
        {
            await semaphore.WaitAsync();
            count = count * 2;
            semaphore.Release();
            return count;
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

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}