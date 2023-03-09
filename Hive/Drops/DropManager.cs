using Hive.Common;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Hive.Drops
{
    public class DropManager : IEntity
    {
        private float dropChance = 0.5f;
        private float goldenDropChance = 0.1f;
        private Counter nectarCounter;
        private ConcurrentDictionary<Guid, NectarDrop> dropList = new ConcurrentDictionary<Guid, NectarDrop>();
        private ConcurrentDictionary<Guid, EventText> eventTexts = new ConcurrentDictionary<Guid, EventText>();
        private float elapsedDropSpawnTime = 0;
        private float _dropSpawnTimeInterval = 0.1f;

        private Texture2D dropTexture;


        public DropManager(Counter nectarCounter, Texture2D dropTexture)
        {
            this.nectarCounter = nectarCounter;
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
                    rnd.Next((int)(HiveGame.screenSizeX * 0.5f), (int)(HiveGame.screenSizeX * 0.5f)),
                    rnd.Next((int)(HiveGame.screenSizeY * 0.5f), (int)(HiveGame.screenSizeY * 0.5f)));
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

        public void RemoveDrop(NectarDrop drop)
        {
            dropList.TryRemove(drop.GetGuid(), out _);
        }

        public void Update(GameTime gameTime)
        {
            if (elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                NectarDrop newDrop = SpawnRandomDrop();

                if (newDrop != null)
                {
                    dropList.TryAdd(newDrop.GetGuid(), newDrop);
                }
                elapsedDropSpawnTime = 0;
            }

            elapsedDropSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            List<NectarDrop> drops = dropList.Values.ToList();
            foreach (NectarDrop drop in drops)
            {
                drop.Update(gameTime);
            }

            List<EventText> eventTextList = eventTexts.Values.ToList();
            foreach (EventText eventText in eventTextList)
            {
                eventText.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            List<NectarDrop> drops = dropList.Values.ToList();
            foreach (NectarDrop drop in drops)
            {
                drop.Draw(gameTime, spriteBatch);
            }
            List<EventText> eventTextList = eventTexts.Values.ToList();
            foreach (EventText eventText in eventTextList)
            {
                eventText.Draw(gameTime, spriteBatch);
            }
        }

        public void AddEventText(Vector2 position, float scale, string text, Color color, float fadeOutSpeed)
        {
            EventText newText = new EventText(position, scale, text, color, fadeOutSpeed, this);
            eventTexts.TryAdd(newText.GetGuid(), newText);
        }

        internal void RemoveEventText(EventText eventText)
        {
            eventTexts.Remove(eventText.GetGuid(), out _);
        }
    }
}
