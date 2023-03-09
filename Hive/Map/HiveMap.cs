using Hive.Common;
using Hive.Drops;
using Hive.Shops;
using Hive.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hive.Map
{
    internal class HiveMap : DrawnEntity
    {
        private ContentLoader content;

        private ConcurrentDictionary<Guid, AntObject> antBag = new ConcurrentDictionary<Guid, AntObject>();
        private ConcurrentDictionary<Guid, NectarObject> nectarBag = new ConcurrentDictionary<Guid, NectarObject>();
        private HiveGame game;
        private AntShop antShop;
        private ExpansionShop expansionShop;
        private Semaphore antNavigationSemaphore;

        private float elapsedDropSpawnTime = 0;
        private float _dropSpawnTimeInterval = 5f;

        private int width;
        private int height;
        private int antSpeed = 1;
        private float dropChance;

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

        public HiveMap(int width, int height, float dropChance, Texture2D texture, Vector2 position, ContentLoader content, HiveGame game) : base(texture, position, 1f)
        {
            this.content = content;
            this.width = width;
            this.height = height;
            this.dropChance = dropChance;
            this.game = game;
            this.antShop = game.AntShop;
            this.antShop.OnBuy += AntOnBuy;
            this.expansionShop = game.ExpansionShop;
        }

        private void AntOnBuy(object sender, EventArgs e)
        {
            Task.Factory.StartNew(SpawnAnt);
        }

        public void SpawnNectar()
        {
            Random rnd = new Random();
            float chance = rnd.Next(0, 1);
            if (chance <= dropChance)
            {
                Vector2 nectarCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X), rnd.Next((int)Position.Y, height + (int)Position.Y));
                Guid guid = Guid.NewGuid();
                NectarObject nectar = new NectarObject(nectarCoordinates, this.content.nectarTexture, nectarCoordinates, guid);
                nectarBag.TryAdd(guid, nectar);
            }
        }

        public void SpawnAnt()
        {
            Random rnd = new Random();
            var antCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X), rnd.Next((int)Position.Y, height + (int)Position.Y));
            Guid guid = Guid.NewGuid();
            AntObject antObject = new AntObject(antSpeed, antCoordinates, content.antTexture, antCoordinates, guid);
            antObject.OnNectarPickUp += AntOnNectarPickUp;
            antBag.TryAdd(guid, antObject);
        }

        private void AntOnNectarPickUp(object sender, EventArgs e)
        {
            AntObject ant = (AntObject) sender;
            nectarBag.TryRemove(ant.CurrentTarget.GetGuid(), out _);
        }

        public void MoveAllAnts(ConcurrentDictionary<Guid, NectarObject> nectarList)
        {
            foreach (var ant in antBag)
            {
                ant.Value.Move(nectarList);
            }
    
        }


        public override void Update(GameTime gameTime)
        {
            if (elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                SpawnNectar();

                elapsedDropSpawnTime = 0;
            }

            elapsedDropSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            MoveAllAnts(nectarBag);
            foreach (var ant in antBag)
            {
                ant.Value.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(content.counterFont, nectarBag.Count().ToString(), new Vector2(500,500), Color.Black, 0f, Vector2.Zero, scale * 5, SpriteEffects.None, 1);

            foreach (KeyValuePair <Guid, AntObject> ant in antBag)
            {
                ant.Value.Draw(gameTime, spriteBatch);
            }

            foreach (KeyValuePair<Guid, NectarObject> nectar in nectarBag)
            {
                nectar.Value.Draw(gameTime, spriteBatch);
            }
        }
    }
}
