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

        private ConcurrentDictionary<Guid, AntObject> antIdleBag = new ConcurrentDictionary<Guid, AntObject>();
        private ConcurrentDictionary<Guid, AntObject> antActiveBag = new ConcurrentDictionary<Guid, AntObject>();
        private ConcurrentDictionary<Guid, NectarObject> nectarToPickUpBag = new ConcurrentDictionary<Guid, NectarObject>();
        private ConcurrentDictionary<Guid, NectarObject> nectarTargetedBag = new ConcurrentDictionary<Guid, NectarObject>();
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
                NectarObject nectar = new NectarObject(nectarCoordinates, this.content.nectarTexture, nectarCoordinates);
                nectarToPickUpBag.TryAdd(nectar.GetGuid(), nectar);
            }
        }

        public void SpawnAnt()
        {
            Random rnd = new Random();
            var antCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X), rnd.Next((int)Position.Y, height + (int)Position.Y));
            AntObject antObject = new AntObject(antSpeed, antCoordinates, content.antTexture, antCoordinates);
            antObject.OnNectarPickUp += AntOnNectarPickUp;
            antObject.OnNectarTargeted += AntOnNectarTargeted;
            antIdleBag.TryAdd(antObject.GetGuid(), antObject);
        }

        private void AntOnNectarTargeted(object sender, EventArgs e)
        {
            AntObject ant = (AntObject)sender;
            nectarToPickUpBag.TryRemove(ant.CurrentTarget.GetGuid(), out _);
            nectarTargetedBag.TryAdd(ant.CurrentTarget.GetGuid(), ant.CurrentTarget);

            antIdleBag.TryRemove(ant.GetGuid(), out _);
            antActiveBag.TryAdd(ant.GetGuid(), ant);
        }

        private void AntOnNectarPickUp(object sender, EventArgs e)
        {
            AntObject ant = (AntObject)sender;
            nectarTargetedBag.TryRemove(ant.CurrentTarget.GetGuid(), out _);
            ant.CurrentTarget = null;

            antActiveBag.TryRemove(ant.GetGuid(), out _);
            antIdleBag.TryAdd(ant.GetGuid(), ant);
        }

        public void MoveAllAnts(ConcurrentDictionary<Guid, NectarObject> nectarList)
        {
            
            GetClosestAntToNectar(out AntObject ant, out NectarObject nectar);

            if (nectar != null || ant != null)
            {
                ant.SetCurrentDestination(nectar);
            }


            foreach (AntObject activeAnt in antActiveBag.Values)
            {
                activeAnt.Move();
            }

        }

        internal void GetClosestAntToNectar(out AntObject ant, out NectarObject nectar)
        {
            float shortestDistance = float.MaxValue;
            AntObject nearestAnt = null;
            NectarObject nearestNectar = null;

            foreach (NectarObject nectarObject in nectarToPickUpBag.Values)
            {
                foreach (AntObject antObject in antIdleBag.Values)
                {
                    float distance = Vector2.Distance(nectarObject.GetMapCoordinates(), antObject.GetMapCoordinates());

                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestAnt = antObject;
                        nearestNectar = nectarObject;
                    }
                }
            }
            nectar = nearestNectar;
            ant = nearestAnt;
        }

        public override void Update(GameTime gameTime)
        {
            if (elapsedDropSpawnTime > DropSpawnTimeInterval)
            {
                SpawnNectar();

                elapsedDropSpawnTime = 0;
            }

            elapsedDropSpawnTime += (float)gameTime.ElapsedGameTime.TotalSeconds;


            MoveAllAnts(nectarToPickUpBag);

            foreach (var ant in antActiveBag)
            {
                ant.Value.Update(gameTime);
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (KeyValuePair<Guid, AntObject> ant in antIdleBag)
            {
                ant.Value.Draw(gameTime, spriteBatch);
            }

            foreach (KeyValuePair<Guid, AntObject> ant in antActiveBag)
            {
                ant.Value.Draw(gameTime, spriteBatch);
            }

            foreach (KeyValuePair<Guid, NectarObject> nectar in nectarToPickUpBag)
            {
                nectar.Value.Draw(gameTime, spriteBatch);
            }

            foreach (KeyValuePair<Guid, NectarObject> nectar in nectarTargetedBag)
            {
                nectar.Value.Draw(gameTime, spriteBatch);
            }
        }
    }
}
