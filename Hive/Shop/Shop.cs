using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Shop
{
    public abstract class Shop : IEntity
    {
        protected Texture2D icon;
        protected Counter nectarCounter;

        protected Shop(Texture2D icon, Counter nectarCounter)
        {
            this.icon = icon;
            this.nectarCounter = nectarCounter;
        }

        public abstract int CurrentCost(int currentAmount);

        public abstract void Buy();

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
