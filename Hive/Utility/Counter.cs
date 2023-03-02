using Hive.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hive.Utility
{
    public class Counter : DrawnEntity
    {
        private SemaphoreSlim semaphore;
        private int count;
        private Color color;

        protected static Texture2D backgroundTexture;
        protected static SpriteFont font;

        public Counter(Vector2 position) : base(Counter.backgroundTexture, position, 1f)
        {
            count = 0;
            semaphore = new SemaphoreSlim(1);
            this.color = Color.Black;
        }

        public async Task<bool> AddCount(int value)
        {
            await semaphore.WaitAsync();
            if (count + value < 0)
            {
                semaphore.Release();
                return false;
            }
            else
            {
                count += value;
                semaphore.Release();
                return true;
            }
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
            //throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, count.ToString(), position, color);
            base.Draw(gameTime, spriteBatch);
        }

        public static void Initialize(Texture2D backgroundTexture, SpriteFont font)
        {
            Counter.backgroundTexture = backgroundTexture;
            Counter.font = font;
        }
    }
}
