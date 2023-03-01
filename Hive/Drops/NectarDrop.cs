using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace Hive.Drops
{
    public class NectarDrop : DrawnEntity
    {
        protected Counter nectarCounter;
        private int nectarValue;

        public NectarDrop(Counter nectarCounter, int nectarValue, Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.nectarCounter = nectarCounter;
            this.nectarValue = nectarValue;
        }

        public async Task Claim()
        {
            await nectarCounter.AddCount(nectarValue);
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}