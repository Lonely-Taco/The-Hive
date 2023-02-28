using Hive.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hive.Drops
{
    internal class DropManager
    {
        private double dropChance;
        private double goldenDropChance;
        private NectarCounter nectarCounter;
        private int regularDropValue;

        public DropManager(NectarCounter nectarCounter)
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
                    return new GoldenNectarDrop();
                }
                else
                {
                    return new NectarDrop(nectarCounter, regularDropValue);
                }
            }
            return null;
        }

        public void SetRegularDropValue(int value)
        {
            this.regularDropValue = value;
        }
    }
}
