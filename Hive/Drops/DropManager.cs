using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Hive.Drops
{
    public class DropManager : IEntity
    {
        private float dropChance = 0.5f;
        private float goldenDropChance = 0.1f;
        private Counter nectarCounter;
        private int regularDropValue;
        private List<NectarDrop> dropList = new List<NectarDrop>();
        private List<NectarDrop> dropsToBeRemoved = new List<NectarDrop>();
        private float elapsedDropSpawnTime = 0;
        private float _dropSpawnTimeInterval = 1;

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
            float chance = rnd.Next(0, 100) / 100f;
            if (chance <= dropChance)
            {
                Vector2 spawnCoords = new Vector2(
                    rnd.Next((int)(HiveGame.screenSizeX * 0.1f), (int)(HiveGame.screenSizeX * 0.9f)),
                    rnd.Next((int)(HiveGame.screenSizeY * 0.1f), (int)(HiveGame.screenSizeY * 0.5f)));
                IDropBehaviour dropBehaviour;
                if (chance <= goldenDropChance)
                {
                    dropBehaviour = new GoldenNectarDropBehaviour();
                }
                else
                {
                    dropBehaviour = new RegularNectarDropBehaviour();
                }
                return new NectarDrop(nectarCounter, dropTexture, spawnCoords, this, dropBehaviour);
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
            if (elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                NectarDrop newDrop = SpawnRandomDrop();

                if (newDrop != null)
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

            foreach (NectarDrop drop in dropsToBeRemoved)
            {
                dropList.Remove(drop);
            }
            dropsToBeRemoved.Clear();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle? rectangle)
        {
            foreach (NectarDrop drop in dropList)
            {
                drop.Draw(gameTime, spriteBatch, rectangle);
            }
        }
    }
}
