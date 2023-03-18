using Hive.Drops;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Hive.Map
{
    public class NectarObject : HiveMapObject
    {
        protected Counter nectarCounter;
        public EventHandler onClaim;
        public NectarObject(Vector2 mapCoordinates, Texture2D texture, Vector2 position, Counter nectarCounter) : base(mapCoordinates, texture, position)
        {
            this.nectarCounter = nectarCounter;

        }

        public async void Claim()
        {
            int addedNectar = await nectarCounter.Add(1);
            onClaim.Invoke(this, EventArgs.Empty);
        }


        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}