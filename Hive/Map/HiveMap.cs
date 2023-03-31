using Hive.Common;
using Hive.Drops;
using Hive.Drops.DropBehaviour;
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
        private ConcurrentDictionary<Guid, AntObject> antBag = new ConcurrentDictionary<Guid, AntObject>();
        private ConcurrentDictionary<Guid, NectarObject> nectarBag = new ConcurrentDictionary<Guid, NectarObject>();
        private HiveGame game;
        private AntShop antShop;
        private ExpansionShop expansionShop;
        private SemaphoreSlim antNavigationSemaphore;
        private Counter nectarCounter;
        private Counter expansionCounter;

        private float elapsedDropSpawnTime = 0;
        private float _baseDropSpawnTimeInterval = 5f;

        private int width;
        private int height;
        private int antSpeed = 3;
        private float dropChance;

        private Color antColor = Color.Black;
        private const int MAX_ANT_NAV_TASKS = 100;

        private float DropSpawnTimeInterval
        {
            get
            {
                return _baseDropSpawnTimeInterval / (expansionCounter.GetCount() + 1);
            }
            set
            {
                _baseDropSpawnTimeInterval = value;
                elapsedDropSpawnTime = 0;
            }
        }

        public HiveMap(int width, int height, float dropChance, Texture2D texture, Vector2 position, ContentLoader content, HiveGame game, Counter nectarCounter, Counter expansionCounter) : base(texture, position, 1f)
        {
            this.content = content;
            this.width = width;
            this.height = height;
            this.dropChance = dropChance;
            this.game = game;
            this.antShop = game.AntShop;
            this.antShop.OnBuy += AntOnBuy;
            this.expansionShop = game.ExpansionShop;
            this.antNavigationSemaphore = new SemaphoreSlim(MAX_ANT_NAV_TASKS);
            this.nectarCounter = nectarCounter;
            this.expansionCounter = expansionCounter;
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
                Vector2 nectarCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X),
                                                rnd.Next((int)Position.Y, height + (int)Position.Y));
                NectarObject nectar = new NectarObject(this.content.nectarTexture, nectarCoordinates, nectarCounter, new NectarMapObjectDropBehaviour(expansionCounter));
                nectarBag.TryAdd(nectar.GetGuid(), nectar);
                nectar.onClaim += OnNectarClaim;
            }
        }

        public void SpawnAnt()
        {
            Random rnd = new Random();
            var antCoordinates = new Vector2(rnd.Next((int)Position.X, width + (int)Position.X),
                                             rnd.Next((int)Position.Y, height + (int)Position.Y));
            AntObject antObject = new AntObject(antSpeed, content.antTexture, antCoordinates, antColor);
            antObject.OnStateChanged += AntOnStateChanged;
            antBag.TryAdd(antObject.GetGuid(), antObject);
            antIdleBag.TryAdd(antObject.GetGuid(), antObject);
        }

        private void AntOnStateChanged(object sender, EventArgs e)
        {
            AntObject ant = (AntObject)sender;
            switch (ant.GetState())
            {
                case AntState.Idle:
                    antIdleBag.TryAdd(ant.GetGuid(), ant);
                    break;
                case AntState.Searching:
                    antIdleBag.TryRemove(ant.GetGuid(), out _);
                    break;
                case AntState.Moving:
                    break;
                default: break;
            }
        }

        private void OnNectarClaim(object sender, EventArgs e)
        {
            NectarObject nectar = (NectarObject)sender;
            nectarBag.TryRemove(nectar.GetGuid(), out _);
        }

        public void MoveAllAnts(ConcurrentDictionary<Guid, NectarObject> nectarList)
        {
            foreach (AntObject activeAnt in antBag.Values)
            {
                activeAnt.Move();
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
            SetDestinations();

            MoveAllAnts(nectarBag);

            foreach (var ant in antBag)
            {
                ant.Value.Update(gameTime);
            }
        }

        private void SetDestinations()
        {
            var rand = new Random();
            foreach (AntObject ant in antIdleBag.Values.OrderBy(x => rand.Next()))
            {
                if (nectarBag.IsEmpty) return;
                ant.SetState(AntState.Searching);

                //Thread thread = new Thread(() => SetDestinationQueue(ant));
                //thread.Start();
                Task.Factory.StartNew(() =>
                {
                    SetDestinationQueue(ant);
                });
            }

        }

        private void SetDestinationQueue(AntObject ant)
        {
            antNavigationSemaphore.Wait();
            ant.SetCurrentDestination(nectarBag);
            if (ant.GetState() == AntState.Searching)
            {
                ant.SetState(AntState.Idle);
            }
            antNavigationSemaphore.Release();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            foreach (KeyValuePair<Guid, AntObject> ant in antBag)
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