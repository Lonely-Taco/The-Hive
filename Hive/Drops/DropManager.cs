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
    public class DropManager : IEntity
    {
        private double dropChance = 0.5;
        private double goldenDropChance = 0.1;
        private Counter nectarCounter;
        private int regularDropValue;
        private List<NectarDrop> dropList = new List<NectarDrop>();

        private List<NectarDrop> dropsToBeRemoved = new List<NectarDrop>();
        private float elapsedDropSpawnTime = 0;
        private float _dropSpawnTimeInterval = 5;

        private Texture2D dropTexture;


        public DropManager(Counter nectarCounter, Texture2D dropTexture)
        {
            this.nectarCounter = nectarCounter;
            this.regularDropValue = 1;
            this.dropTexture = dropTexture;
        }

        private float DropSpawnTimeInterval
        {
            get
            {
                return _dropSpawnTimeInterval;
            }
            set
            {
                _dropSpawnTimeInterval = value;
                elapsedDropSpawnTime = 0;
            }
        }

        public NectarDrop SpawnRandomDrop()
        {
            Random rnd = new Random();
            float chance = rnd.Next(0, 1);
            if(chance <= dropChance)
            {
                if(chance <= goldenDropChance)
                {
                    return new GoldenNectarDrop(nectarCounter, dropTexture, new Vector2(rnd.Next(0,200), rnd.Next(0, 200)), this); //TODO: replace null and default position
                }
                else
                {
                    return new NectarDrop(nectarCounter, regularDropValue, dropTexture, new Vector2(rnd.Next(0, 200), rnd.Next(0, 200)), this); //TODO: replace null and default position
                }
            }
            return null;
        }

        public void SetRegularDropValue(int value)
        {
            this.regularDropValue = value;
        }

        public void RemoveDrop(NectarDrop drop)
        {
            dropsToBeRemoved.Add(drop);
        }

        public void Update(GameTime gameTime)
        {
            if(elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                NectarDrop newDrop = SpawnRandomDrop();
                if(newDrop != null)
                {
                    dropList.Add(newDrop);
                }
                elapsedDropSpawnTime = 0;
            }
            elapsedDropSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (NectarDrop drop in dropList)
            {
                drop.Update(gameTime);
            }
            
            foreach(NectarDrop drop in dropsToBeRemoved)
            {
                dropList.Remove(drop);
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
