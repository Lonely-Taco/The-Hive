using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class DropManager : IEntity
    {
        private double dropChance;
        private double goldenDropChance;
        private Counter nectarCounter;
        private int regularDropValue;
        private List<NectarDrop> dropList = new List<NectarDrop>();

        public DropManager(Counter nectarCounter)
        {
            this.nectarCounter = nectarCounter;
            this.regularDropValue = 1;
        }

        public NectarDrop SpawnDrop(float elapsedTime)
        {
            Random rnd = new Random();
            float chance = rnd.Next(0, 1);
            if(chance <= dropChance)
            {
                if(chance <= goldenDropChance)
                {
                    return new GoldenNectarDrop(nectarCounter, null, Vector2.One); //TODO: replace null and default position
                }
                else
                {
                    return new NectarDrop(nectarCounter, regularDropValue, null, Vector2.One); //TODO: replace null and default position
                }
            }
            return null;
        }

        public void SetRegularDropValue(int value)
        {
            this.regularDropValue = value;
        }

        public void Update(GameTime gameTime)
        {
            foreach(NectarDrop drop in dropList)
            {
                drop.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (NectarDrop drop in dropList)
            {
                drop.Draw(gameTime, spriteBatch);
            }
        }
    }
}
