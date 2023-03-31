using Hive.Drops.DropBehaviour;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

namespace Hive.Map
{
    public class NectarObject : HiveMapObject
    {
        protected Counter nectarCounter;
        public EventHandler onClaim;
        private bool IsClaimed;
        private IDropBehaviour dropBehaviour;
        Object locker = new object();
        public NectarObject(Texture2D texture, Vector2 position, Counter nectarCounter, IDropBehaviour dropBehaviour) : base(texture, position)
        {
            this.nectarCounter = nectarCounter;
            this.IsClaimed = false;
            this.dropBehaviour = dropBehaviour;
        }

        public void Claim()
        {
            lock (this.locker)
            {
                if (!this.IsClaimed)
                {
                    dropBehaviour.Claim(nectarCounter);
                    onClaim.Invoke(this, EventArgs.Empty);
                    this.IsClaimed = true;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}