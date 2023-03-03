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
        private Color color;

        protected DrawnEntity icon;
        protected Vector2 iconOffset = new Vector2(15, 15);
        protected Vector2 textOffset = new Vector2(110, 15);

        private static Texture2D backgroundTexture;
        private static SpriteFont font;

        public Counter(Vector2 position, Texture2D iconTexture, float scale) : base(Counter.backgroundTexture, position, scale * 0.3f)
        {
            count = 0;
            semaphore = new SemaphoreSlim(1);
            this.color = Color.White;
            this.icon = new DrawnEntity(iconTexture, position + iconOffset, scale);
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
            base.Draw(gameTime, spriteBatch);
            icon.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, count.ToString(), position + textOffset, color, 0f, Vector2.Zero, scale * 6, SpriteEffects.None, 1);
        }

        public static void Initialize(Texture2D backgroundTexture, SpriteFont font)
        {
            Counter.backgroundTexture = backgroundTexture;
            Counter.font = font;
        }
    }
}
